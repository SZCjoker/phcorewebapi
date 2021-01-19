using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Auth
{
 public  interface IGetJwtTokenInfoService
    {
        /// <summary>
        /// 取得JWT內的使用者ID
        /// </summary>
        string UserId { get; set; }
        /// <summary>
        /// 取得JWT內的使用者登入名稱
        /// </summary>
        string FirstName { get; set; }
        /// <summary>
        ///token 過期時間 
        /// </summary>
        string ExpTime { get; set; }
        /// <summary>
        /// 取得JWT內的使用者SecertKey
        /// </summary>
        string SecertKey { get; set; }
     
        /// <summary>
        /// 取得部門
        /// </summary>
        string DeptID { get; set; }
        string Jobtitle { get; set; }
        string Isu { get; set; }
    }
}
