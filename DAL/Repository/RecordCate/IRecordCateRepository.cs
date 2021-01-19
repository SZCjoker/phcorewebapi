using PHCoreWebAPI.DAL.Repository.RecordCate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.Repository.RecordCate
{
 public interface IRecordCateRepository
    {
        /// <summary>
        /// 依組別取得紀錄單種類
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        ValueTask<IEnumerable<RecordCateEntity>> GetRecordCateByDeptID(int deptid);

        ValueTask<IEnumerable<RecordCateEntity>> GetRecordCate();

        /// <summary>
        /// 新增紀錄分類
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValueTask<int> AddRecordCate(RecordCateEntity entity);
        /// <summary>
        /// 編輯紀錄分類
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValueTask<int> UpdateRecordCate(RecordCateEntity entity);
        /// <summary>
        /// 停用分類
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValueTask<int> StopRecordCate(int issueid, int state);
    }
}
