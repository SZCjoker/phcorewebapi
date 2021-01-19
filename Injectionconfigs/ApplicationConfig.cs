using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PHCoreWebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSwag;
using OfficeOpenXml.ConditionalFormatting;
using NSwag.Generation.Processors.Security;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;
using PHCoreWebAPI.DAL.DbFactory;
using PHCoreWebAPI.Handler;
using PHCoreWebAPI.Application.Common;

namespace PHCoreWebAPI.Injectionconfigs
{
    [Injection]
    public class ApplicationConfig
    {

        public ApplicationConfig(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDbFactory, SqlDbFactory>()
                    .AddSingleton<IGenerateId>(s => new SnowflakeHandler(Environment.MachineName)); ;
            // add OpenAPI v3 document
            services.AddOpenApiDocument(config =>
            {
                // setup the document name (default: v1)
                config.DocumentName = "v1";

                // set up document version
                config.Version = "0.0.3";

                // title
                config.Title = "PH_83_Demo";

                // description
                config.Description = "平台介接資訊";
                config.GenerateEnumMappingDescription = true;

                var apischema = new OpenApiSecurityScheme()
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Copy this into the value field: Bearer {token}"

                };
                config.AddSecurity("JWT Token", Enumerable.Empty<string>(), apischema);

                config.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT Token"));
                config.ActualSerializerSettings.Converters.Add(new LongValueConverter());
                config.IgnoreObsoleteProperties=true;
                config.GenerateEnumMappingDescription = true;
               
            });
            services.AddResponseCompression(options =>
            {
                IEnumerable<string> MimeTypes = new[]
                {
                     "text/plain",
                     "application/json"
                 };
                options.EnableForHttps = true;
                options.MimeTypes = MimeTypes;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            

        }

    }
}
