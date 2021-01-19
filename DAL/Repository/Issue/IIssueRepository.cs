using PHCoreWebAPI.DAL.Repository.Issue.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.Repository.Issue
{
 public interface IIssueRepository
    {

        /// <summary>
        /// 夜間十點到早上十點 優先處理單
        /// </summary>
        /// <returns></returns>
        ValueTask<(IEnumerable<IssueEntity> rows, long total)> GetIssuePm10toAm10(IssueEntity entity);
        ValueTask<(IEnumerable<IssueEntity>rows,long total)> GetLoginIssue(IssueEntity entity);
        /// <summary>
        /// 取回全部問題單
        /// </summary>
        /// <returns></returns>
        ValueTask<(IEnumerable<IssueEntity>rows,long total)> GetAllIssue(IssueEntity entity);
        /// <summary>
        /// 依照部門取得問題清單
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        ValueTask<(IEnumerable<IssueEntity> rows, long total)> GetAllIssueByDept(int deptid);
        /// <summary>
        /// 取得單一用戶問題單
        /// </summary>
        /// <param name="clientid"></param>
        /// <returns></returns>
        ValueTask<(IEnumerable<IssueEntity> rows, long total)> GetIssueByclientid(string clientid);

        ValueTask<(IEnumerable<IssueEntity> rows, long total)> QueryByCondition(IssueEntity entity, string querySql);

        /// <summary>
        /// 透過單號取得問題單
        /// </summary>
        /// <param name="issueno"></param>
        /// <returns></returns>
        ValueTask<IssueEntity> GetIssueByNo(Int64 recorno);
        /// <summary>
        /// 新增問題單
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ValueTask<int> CreateIssue(IssueEntity entity);
        /// <summary>
        /// 編輯問題單
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ValueTask<int> UpdateIssue(IssueEntity entity);
        /// <summary>
        /// 結清問題單
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
       

    }
}
