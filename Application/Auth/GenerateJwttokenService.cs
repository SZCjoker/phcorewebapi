using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Auth
{
    public class GenerateJwttokenService : IGenerateJwtTokenService
    {
        private readonly AppSettings _config;
        private readonly ILogger<GenerateJwttokenService> _logger;


        public GenerateJwttokenService(IOptions<AppSettings> config
                                      , ILogger<GenerateJwttokenService> logger)
        {
            this._config = config.Value;
            _logger = logger;
        }


        public string GenerateJwtToken(string userid,string firstname, string jobtitle, string deptid, string secretKey)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,userid),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,DateTimeOffset.UtcNow.AddMinutes(_config.jwt.JwtExpireMinutes).ToString())
            };
                claims.Add(new Claim("firstname", firstname.ToString()));
                claims.Add(new Claim("jobtitle", jobtitle.ToString()));
                claims.Add(new Claim("deptid", deptid.ToString()));
                claims.Add(new Claim("secretKey", secretKey));

            var userClaimsIdentity = new ClaimsIdentity(claims);
            return this.BuildToken(userClaimsIdentity, _config.jwt.JwtIssuer.ToString() ,null);
        }

        private string BuildToken(ClaimsIdentity claims, string isu, int? timeWindow)
        {
            try
            {

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config.jwt.JwtKey)) ?? null;
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature) ?? null;
                var expiresTime = DateTimeOffset.Now.AddMinutes(timeWindow ?? this._config.jwt.JwtExpireMinutes);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = isu,
                    Subject = claims,
                    // Expires = expiresTime.DateTime,
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var serializeToken = tokenHandler.WriteToken(securityToken);
                return serializeToken;
            }

            catch (Exception ex)
            {
                _logger.LogInformation($"Exception on generate token");
                throw ex;
            }
        }
    }
}
