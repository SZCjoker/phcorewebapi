using PHCoreWebAPI.DAL.Repository.Record.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.Repository.Record
{
public interface IRecordRepository
    {
        /// <summary>
        /// 取回全部紀錄單
        /// </summary>
        /// <returns></returns>
        ValueTask<IEnumerable<RecordEntity>> GetAllRecord();
        /// <summary>
        /// 依照部門取得紀錄清單
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        ValueTask<IEnumerable<RecordEntity>> GetAllRecordByDept(int deptid);
        /// <summary>
        /// 取得單一用戶紀錄單
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        ValueTask<IEnumerable<RecordEntity>> GetRecordByclientid(string clientid);

        ValueTask<IEnumerable<RecordEntity>> QueryByCondition(RecordEntity entity, string querySql, int pageoffset, int pagesize);

        /// <summary>
        /// 透過單號取得紀錄單
        /// </summary>
        /// <param name="Recordno"></param>
        /// <returns></returns>
        ValueTask<RecordEntity> GetRecordByNo(Int64 recorno);
        /// <summary>
        /// 新增紀錄單
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ValueTask<int> CreateRecord(RecordEntity entity);
        /// <summary>
        /// 編輯紀錄單
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ValueTask<int> UpdateRecord(RecordEntity entity);
        /// <summary>
        /// 結清紀錄單
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

    }
}
