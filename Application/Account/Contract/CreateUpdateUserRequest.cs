using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PHCoreWebAPI.Application.Account.Contract
{
    public class CreateUpdateUserRequest
    {   [Required]
        public string UserID { get; set; }
        [Required]
        public string PWD { get; set; }
        public string FirstName { get; set; }
        public string FirstCName { get; set; }
        public int Gender { get; set; }
        public string LastName { get; set; }
        public string LastCName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        [Required]
        public int DeptID { get; set; }
        public int LoginFailedCount { get; set; }
        public int State { get; set; }
    }
}