using PHCoreWebAPI.Application;
using PHCoreWebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.Application.SendMail.Contract;

namespace PHCoreWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostingEnvironment;
        }
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostEnvironment { get; }
       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<AppSettings>(Configuration);
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddRazorPages();
            services.ConfigureDependencyInjections();
            services.AddHttpClient();
            services.AddCors(option =>
            {
                option.AddPolicy("CorsPolicy", builder =>
                //builder.WithOrigins("http://localhost:5500",
                //                    "http://127.0.0.1:5500",
                //                    "http://10.80.1.23:89")
                builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        //.AllowCredentials()


                );
            });
            services.AddControllers()   
                .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                
            })
                .AddNewtonsoftJson(
                config =>
                {
                    config.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    config.SerializerSettings.Converters.Add(new StringEnumConverter());
                    config.SerializerSettings.Converters.Add(new LongValueConverter());
                });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");

            //// Add OpenAPI/Swagger middlewares
            app.UseOpenApi();    // Serves the registered OpenAPI/Swagger documents by default on `/swagger/{documentName}/swagger.json`
            app.UseSwaggerUi3(); // Serves the Swagger UI 3 web ui to view the OpenAPI/Swagger documents by default on `/swagger`
            app.UseReDoc(config => { config.Path = "/redoc"; });

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.Use(next => context =>
            {
                context.Request.EnableBuffering();
                return next(context);
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
