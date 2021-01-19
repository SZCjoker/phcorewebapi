using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Auth
{
 public   interface IGenerateJwtTokenService
    {
     string GenerateJwtToken(string userid, string firstname ,string jobtitle ,string deptid, string secretKey);

    }
}
