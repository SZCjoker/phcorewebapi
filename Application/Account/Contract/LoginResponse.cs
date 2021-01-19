using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Account.Contract
{
 public  class LoginResponse
    {
        public string FirstName { get; set; }
        public string FirstCName { get; set; }
        public string DeptName { get; set; }        
        public int DeptID { get; set; }

        public List<Permission> PermissionList { get; set; }

    }

    public class Permission 
    { 
        public int DeptID { get; set; }
        public string Page { get; set; }
        public string PermissionName { get; set; }
        public string PermissionPath { get; set; }
        public string Code { get; set; }
        public int OrderBy { get; set; }
        public bool State { get; set; }
    }

}
