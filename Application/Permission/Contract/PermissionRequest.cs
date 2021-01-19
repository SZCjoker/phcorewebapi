using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHCoreWebAPI.Application.Permission.Contract
{
    public class PermissionRequest
    {
        public Int64 PermissionID { get; set; }
        public int DeptID { get; set; }
        public string PermissionName { get; set; }
        public string PermissionPath { get; set; }
        public string Code { get; set; }
        public string Page { get; set; }
        
    }
}