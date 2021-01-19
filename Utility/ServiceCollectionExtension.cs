using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Utility
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection ConfigureDependencyInjections(this IServiceCollection services)
        {
            return ConfigureDependencyInjections(services, GetInjectionTypes());
        }

        public static IServiceCollection ConfigureDependencyInjections<TConfig>(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var assembly = Assembly.GetEntryAssembly();

            if (assembly == null) throw new NullReferenceException("EntryAssembly not found.");

            var types = assembly.GetTypes()
                                .Where(t => t.GetConstructors(BindingFlags.Instance | BindingFlags.Public).Length == 1
                                         && typeof(TConfig).IsAssignableFrom(t))
                                .ToArray();

            return ConfigureDependencyInjections(services, types);
        }


        public static IServiceCollection ConfigureDependencyInjections(this IServiceCollection services, params Type[] configTypes)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            if (configTypes == null) throw new ArgumentNullException(nameof(configTypes));

            if (!configTypes.Any()) return services;

            var sortTypes = configTypes.Where(t => t.GetConstructors().Length == 1)
                                       .OrderByDescending(t => t.GetCustomAttribute<InjectionAttribute>()?.Index ?? 0)
                                       .ThenBy(t => t.Name)
                                       .ToArray();

            foreach (var t in sortTypes)
            {
                var args = new List<object>();
                var ctor = t.GetConstructors();
                var ctorParams = ctor[0].GetParameters();

                foreach (var p in ctorParams)
                {
                    if (p.ParameterType == typeof(IServiceCollection))
                    {
                        args.Add(services);

                        continue;
                    }

                    var svr = services.GetRequiredService(p.ParameterType);
                    if (svr != null) args.Add(svr);
                }

                if (args.Count == 0) continue;

                Activator.CreateInstance(t, args.ToArray());
            }

            return services;
        }

        public static T GetRequiredService<T>(this IServiceCollection services) where T : class
        {
            return services.BuildServiceProvider().GetRequiredService<T>();
        }

        public static object GetRequiredService(this IServiceCollection services, Type serviceType)
        {
            return services.BuildServiceProvider().GetRequiredService(serviceType);
        }

        public static T GetService<T>(this IServiceCollection services) where T : class
        {
            return services.BuildServiceProvider().GetService<T>();
        }

        public static object GetService(this IServiceCollection services, Type serviceType)
        {
            return services.BuildServiceProvider().GetService(serviceType);
        }

        private static Type[] GetInjectionTypes()
        {
            var assembly = Assembly.GetEntryAssembly();

            if (assembly == null) throw new NullReferenceException("EntryAssembly not found.");

            var types = assembly.GetTypes()
                                .Where(t => t.GetConstructors(BindingFlags.Instance | BindingFlags.Public).Length == 1
                                         && t.CustomAttributes.Any(a => a.AttributeType == typeof(InjectionAttribute)))
                                .ToArray();

            return types;
        }
    }
}
