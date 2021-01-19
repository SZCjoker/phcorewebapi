using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.Repository.RecordCate.Entity
{
    public class RecordCateEntity
    {
        public int RecordCateID { get; set; }
        public int DeptID { get; set; }
        public string RecordCateName { get; set; }
        public int State { get; set; }
    }
}
