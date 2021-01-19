using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Auth.Contract
{
    public class JwtUserInfo
    {
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string secret_key { get; set; }
        public string DeptID { get; set; }
        public string job_title { get; set; }
       
    }
}
