using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PHCoreWebAPI.Application;
using PHCoreWebAPI.Application.Auth;
using PHCoreWebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Injectionconfigs
{
    [Injection]
    public class AuthConfig
    {
       
        
            public AuthConfig(IServiceCollection services, IConfiguration configuration)
            {
                var token = configuration.GetSection("Jwt").Get<Jwt>();

                services.AddScoped<IGenerateJwtTokenService, GenerateJwttokenService>();
                services.AddScoped<IGetJwtTokenInfoService, GetJwtTokenInfoService>();
                
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(opt =>
               {
                   opt.IncludeErrorDetails = true;
               //opt.RequireHttpsMetadata = false;
               // opt.SaveToken = true;
               opt.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
                       RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
                       ValidateIssuer = true,
                       ValidIssuer = configuration.GetValue<string>("Jwt:JwtIssuer"),
                       ValidateAudience = false,
                       ValidateLifetime = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:JwtKey")))
                   };
               });

                //services.AddAuthorization(Opt =>
                //{
                //    Opt.AddPolicy("ip", policy =>
                //    {
                //        policy.Requirements.Add(new IPRequirement("0.0.0.1"));

                //    });
                //});
                //services.AddSingleton<IAuthorizationHandler, IpHandler>();
            }
        }
    }

