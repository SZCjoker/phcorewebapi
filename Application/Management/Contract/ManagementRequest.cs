using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Setting.Contract
{
    public class ManagementRequest
    {

        public int DeptID { get; set; }
        public string DeptName { get; set; }
        public int IssueCateID { get; set; }
        public string IssueCateName { get; set; }
        public int RecordCateID { get; set; }
        public string RecordCateName { get; set; }
        public string PermissionName { get; set; }
        public int PermissionID { get; set; }
        public int Orderby { get; set; }
        public int State { get; set; }
    }
}
