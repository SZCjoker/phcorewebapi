using PHCoreWebAPI.Models.DAL.Department.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.Repository.Department
{
 public interface IDeptInfoRepository
    {

        /// <summary>
        /// 依ID取得組別
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        ValueTask<IEnumerable<DeptInfoEntity>> GetDeptsByDeptID(int deptid);
        /// <summary>
        /// 新增組別分類
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValueTask<int> AddDept(DeptInfoEntity entity);
        /// <summary>
        /// 編輯組別分類
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValueTask<int> UpdateDept(DeptInfoEntity entity);
        /// <summary>
        /// 停用組別
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValueTask<int> DimissDept(int deptid, int state);
        ValueTask<IEnumerable<DeptInfoEntity>> GetAllDepts();


    }
}
