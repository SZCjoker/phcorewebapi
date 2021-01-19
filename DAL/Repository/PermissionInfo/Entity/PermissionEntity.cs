using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHCoreWebAPI.DAL.Repository.PermissionInfo.Entity
{
    public class PermissionEntity
    {
        public Int64 PermissionID { get; set; }
        public int DeptID { get; set; }
        public int Orderby { get; set; }
        public string Page { get; set; }
        public string PermissionName { get; set; }
        public string PermissionPath { get; set; }
        public string Code { get; set; }
        public int Cdate { get; set; }
        public Int64 Ctime { get; set; }
        public int Udate { get; set; }
        public Int64 Utime { get; set; }
        public bool State { get; set; }
    }
}