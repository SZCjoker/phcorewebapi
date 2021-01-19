using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.Repository.Account.Entity
{
   public class UserInfoEntity
    {

        public string UserID { get; set; }
        public string PWD { get; set; }
        public int DeptID { get; set; }
        public string DeptName { get; set; }
        public int Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FirstCName { get; set; }
        public string LastCName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int LoginFailedCount { get; set; }
        public int Cdate { get; set; }
        public Int64 Ctime { get; set; }
        public int Udate { get; set; }
        public Int64 Utime { get; set; }
        public int State { get; set; }
    }
}
