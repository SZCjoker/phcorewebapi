using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.Repository.Setting.Entity
{
    public class ManagementEntity
    {
        public int IssueCateID { get; set; }
        public int RecordCateID { get; set; }
        public int PermissionID { get; set; }
        public int DeptID { get; set; }
        public int Orderby { get; set; }
        public string IssueName { get; set; }
        public string PermissionName { get; set; }
        public string Code { get; set; }
        public int Cdate { get; set; }
        public Int64 Ctime { get; set; }
        public int Udate { get; set; }
        public Int64 Utime { get; set; }
        public bool State { get; set; }

    }
}
