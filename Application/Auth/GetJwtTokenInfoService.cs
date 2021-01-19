using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Auth
{
    public class GetJwtTokenInfoService : IGetJwtTokenInfoService
    {
        private readonly IHttpContextAccessor _ctx;

        public GetJwtTokenInfoService(IHttpContextAccessor ctx)
        {
            _ctx = ctx;
        }


        public string UserId
        {
            get => _ctx.HttpContext.User.Claims.FirstOrDefault(o => o.Type.Equals(JwtRegisteredClaimNames.Sub))?.Value;
            set { }
        }

        public string UserName
        {
            get => _ctx.HttpContext.User.Identity.Name;
            set { }
        }
        public string SecertKey
        {
            get => _ctx.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "secertKey")?.Value.ToString();
            set { }
        }

        public string DeptID
        {
            get => _ctx.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "deptid")?.Value.ToString();
            set { }
        }

        public string ExpTime
        {
            get => _ctx.HttpContext.User.Claims.FirstOrDefault(o => o.Type.Equals(JwtRegisteredClaimNames.Exp))?.Value;
            set { }
        }

        public string FirstName {
            get => _ctx.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "firstname")?.Value.ToString();
            set { }
        }
        public string Jobtitle {
            get => _ctx.HttpContext.User.Claims.FirstOrDefault(p => p.Type == "jobtitle")?.Value.ToString();
            set { }
        }
        public string Isu {
            get => _ctx.HttpContext.User.Claims.FirstOrDefault(o => o.Type.Equals(JwtRegisteredClaimNames.Iss))?.Value.ToString();
            set { }
        }
    }
}

