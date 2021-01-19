using PHCoreWebAPI.Models.DAL.Repository.IssueCate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.Repository.IssueCate
{
public  interface IIssueCateRepository
    {
        /// <summary>
        /// 依組別取得問題單種類
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        ValueTask<IEnumerable<IssueCateEntity>> GetIssueCateByDeptID(int deptid);

        ValueTask<IEnumerable<IssueCateEntity>> GetIssueCate();

        /// <summary>
        /// 新增問題分類
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValueTask<int> AddIssueCate(IssueCateEntity entity);
        /// <summary>
        /// 編輯問題分類
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValueTask<int> UpdateIssueCate(IssueCateEntity entity);
        /// <summary>
        /// 停用分類
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValueTask<int> StopIssueCate(int issueid ,int state);
      
    }
}
