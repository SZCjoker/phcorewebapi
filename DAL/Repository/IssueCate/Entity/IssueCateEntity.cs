using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PHCoreWebAPI.Models.DAL.Repository.IssueCate.Entity
{
    public class IssueCateEntity
    {
        public int IssueCateID { get; set; }
        public int DeptID { get; set; }
        public string IssueCateName { get; set; }
        public int State { get; set; }
    }
}