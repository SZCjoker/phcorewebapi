using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PHCoreWebAPI.Application.Account.Contract
{
 public class LoginRequest
    {   
       
        public string UserID { get; set; }
        
        public string PWD { get; set; }
    }
}
