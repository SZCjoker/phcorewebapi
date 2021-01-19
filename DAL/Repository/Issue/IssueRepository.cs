using PHCoreWebAPI.DAL.Repository.Issue;
using PHCoreWebAPI.DAL.Repository.Issue.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PHCoreWebAPI.DAL.DbFactory;
using Dapper;

namespace PHCoreWebAPI.DAL.Repository.Issue
{
    public class IssueRepository : IIssueRepository
    {

        private readonly IDbFactory _dbfactory;

        public IssueRepository(IDbFactory dbfactory)
        {
            _dbfactory = dbfactory;
        }
        

        public async  ValueTask<(IEnumerable<IssueEntity> rows, long total)> GetAllIssue(IssueEntity entity)
        {
            try
            {
                string totalSql = $@"SELECT  COUNT(IssueNo) AS 'TOTAL'
                                     FROM Issue 
                                     WHERE Createtime  
                                     BETWEEN {entity.BeginDate}
                                     AND     {entity.EndDate}";


                string tsql = $@"SELECT Createtime,clientid,EntranceType,Device,
                                        IssueCateID,AttachmentNo,TestResult,ProcessStatus,
                                        Reason,ResponseTime,ResponseResult,EserviceID,DeptID,Recorder
                                 FROM Issue
                                 WHERE Createtime  
                                 BETWEEN {entity.BeginDate}
                                 AND     {entity.EndDate}
                                 ORDER BY Createtime DESC
                                 OFFSET {entity.PageOffset} ROWS
                                 FETCH NEXT {entity.PageSize} ROWS ONLY";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var multiple = await cn.QueryMultipleAsync(totalSql + tsql, entity);
                    var total = await multiple.ReadFirstOrDefaultAsync<int>();
                    var rows = await multiple.ReadAsync<IssueEntity>();
                    return (rows, total);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        
        public async  ValueTask<(IEnumerable<IssueEntity> rows, long total)> GetAllIssueByDept(int deptid)
        {
            try
            {
                string totalSql = $@"SELECT  COUNT(*) AS 'TOTAL'
                                     FROM Issue 
                                     WHERE deptid = {deptid}";

                string tsql = $@"SELECT Createtime,clientid,EntranceType,Device,
                                        IssueCateID,AttachmentNo,TestResult,ProcessStatus,
                                        Reason,ResponseTime,ResponseResult,EserviceID,DeptID,Recorder
                                 FROM Issue 
                                 WHERE deptid = {deptid}
                                 ORDER BY Createtime DESC";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var multiple = await cn.QueryMultipleAsync(totalSql + tsql, deptid);
                    var total = await multiple.ReadFirstOrDefaultAsync<int>();
                    var rows = await multiple.ReadAsync<IssueEntity>();
                    return (rows, total);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async ValueTask<(IEnumerable<IssueEntity> rows, long total)> GetIssueByclientid(string clientid)
        {
            try
            {
                string totalSql = $@"SELECT  COUNT(*) AS 'TOTAL'
                                     FROM Issue 
                                     WHERE clientid = {clientid}";

                string tsql = $@"SELECT *
                                 FROM Issue 
                                 WHERE clientid = {clientid}
                                 ORDER BY Createtime DESC";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var multiple = await cn.QueryMultipleAsync(totalSql + tsql, clientid);
                    var total = await multiple.ReadFirstOrDefaultAsync<int>();
                    var rows = await multiple.ReadAsync<IssueEntity>();
                    return (rows, total);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<(IEnumerable<IssueEntity> rows, long total)> GetIssuePm10toAm10(IssueEntity entity)
        {
            try
            {
                string totalSql = $@"SELECT  COUNT(IssueNo) AS 'TOTAL'
                                     FROM Issue 
                                     WHERE DeptID = {entity.DeptID} AND Createtime  
                                     BETWEEN {entity.BeginDate}
                                     AND     {entity.EndDate}";

                string tsql = $@"SELECT  IssueNo,EntranceType,IssueDescription,
                                         clientid,Createtime,Purpose,AskingTime,AnswerStatus,
                                         Content,IssueRecord,Suggestion,Solve,EserviceID
                                 FROM Issue 
                                 WHERE  DeptID = {entity.DeptID} AND Createtime  
                                 BETWEEN {entity.BeginDate}
                                 AND     {entity.EndDate}
                                 ORDER BY Createtime DESC
                                 OFFSET {entity.PageOffset} ROWS
                                 FETCH NEXT {entity.PageSize} ROWS ONLY";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var multiple = await cn.QueryMultipleAsync(totalSql + tsql, entity);
                    var total = await multiple.ReadFirstOrDefaultAsync<int>();
                    var rows = await multiple.ReadAsync<IssueEntity>();
                    return (rows, total);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async ValueTask<(IEnumerable<IssueEntity> rows, long total)> QueryByCondition(IssueEntity entity, string querySql)
        {
            try
            {
                string totalSql = $@"SELECT  COUNT(*) AS 'TOTAL'
                                     FROM Issue 
                                     WHERE 1=1 {querySql}";

                string tSql = $@"SELECT * 
                                 FROM Issue 
                                 WHERE 1=1 {querySql}
                                 ORDER BY Createtime  DESC
                                 OFFSET {entity.PageOffset} ROWS
                                 FETCH NEXT {entity.PageSize} ROWS ONLY";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var multiple = await cn.QueryMultipleAsync(totalSql+ tSql, entity);
                    var total = await multiple.ReadFirstOrDefaultAsync<int>();
                    var rows = await multiple.ReadAsync<IssueEntity>();
                    return (rows, total);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<(IEnumerable<IssueEntity> rows, long total)> GetLoginIssue(IssueEntity entity)
        {
            try
            {
                string totalSql = $@"SELECT  COUNT(*) AS 'TOTAL'
                                 FROM Issue 
                                 WHERE Createtime  
                                 BETWEEN {entity.BeginDate}
                                 AND     {entity.EndDate}";


                string tsql = $@"SELECT  IssueNo,EntranceType,IssueDescription,
                                         clientid,Createtime,Purpose,AskingTime,AnswerStatus,
                                         Content,ResponseResult,Suggestion,Solve,EserviceID
                                 FROM Issue 
                                 WHERE Createtime  
                                 BETWEEN {entity.BeginDate}
                                 AND     {entity.EndDate}
                                 ORDER BY Createtime DESC
                                 OFFSET {entity.PageOffset} ROWS
                                 FETCH NEXT {entity.PageSize} ROWS ONLY";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var multiple = await cn.QueryMultipleAsync(totalSql+ tsql, entity);
                    var total = await multiple.ReadFirstOrDefaultAsync<int>();
                    var rows = await multiple.ReadAsync<IssueEntity>();
                    return (rows,total);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async  ValueTask<int> CreateIssue(IssueEntity entity)
        {

            try
            {
                string tsql = $@"INSERT INTO Issue(IssueNo,Createtime,askingtime,clientid
                                               ,Device,EntranceType,IssueCateID,IssueDescription,AttachmentNo
                                               ,TestResult,ProcessStatus,AnswerStatus,purpose,solve
                                               ,Reason,Content,ResponseTime,Suggestion
                                               ,ResponseResult,EserviceID
                                               ,Recorder) 
                                         VALUES(@IssueNo,@Createtime,@askingtime,@clientid
                                               ,@Device,@EntranceType,@IssueCateID,@IssueDescription,@AttachmentNo
                                               ,@TestResult,@ProcessStatus,@AnswerStatus,@purpose,@solve
                                               ,@Reason,@Content,@ResponseTime,@Suggestion
                                               ,@ResponseResult,@EserviceID
                                               ,@Recorder)";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.ExecuteAsync(tsql, entity);
                    return data;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async ValueTask<IssueEntity> GetIssueByNo(Int64 Issueno) 
        {
            try
            {
                string tsql = $@"SELECT *
                                 FROM Issue 
                                 WHERE IssueNo = {Issueno}";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QuerySingleOrDefaultAsync<IssueEntity>(tsql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async ValueTask<int> UpdateIssue(IssueEntity entity)
        {
            try
            {
                string tsql = $@"UPDATE  Issue 
                                
                                SET     clientid = ISNULL( @clientid,clientid ),
                                        device = ISNULL( @device,device ),
                                        utime = CASE WHEN @utime = 0 THEN utime ELSE @utime END,
                                        calltime = CASE WHEN @calltime = 0 THEN calltime ELSE @calltime END,
                                        askingtime = CASE WHEN @askingtime = 0 THEN askingtime ELSE @askingtime END,
                                        entrancetype =  CASE WHEN @entrancetype = 0 THEN entrancetype ELSE @entrancetype END,
                                        issuecateid = CASE WHEN @issuecateid = 0 THEN issuecateid ELSE @issuecateid END,
                                        ProcessStatus = CASE WHEN @ProcessStatus = 0 THEN ProcessStatus ELSE @ProcessStatus END, 
                                        AnswerStatus =  CASE WHEN @AnswerStatus = 0 THEN AnswerStatus ELSE @AnswerStatus END, 
                                        Solve =        CASE WHEN @Solve = 0 THEN Solve ELSE @Solve END,
                                        AttachmentNo = CASE WHEN @AttachmentNo = 0 THEN AttachmentNo ELSE @AttachmentNo END,
                                        ResponseTime = CASE WHEN @ResponseTime = 0 THEN ResponseTime ELSE @ResponseTime END,
                                        IssueRecord = ISNULL(@IssueRecord,IssueRecord),
                                        IssueDescription = ISNULL( @IssueDescription,IssueDescription ),
                                        TestResult = ISNULL(@TestResult,TestResult ),
                                        Purpose = ISNULL( @Purpose,Purpose ),
                                        Reason = ISNULL( @Reason,Reason ),
                                        Content = ISNULL( @Content,Content ),
                                        Suggestion = ISNULL( @Suggestion,Suggestion ),
                                        ResponseResult = ISNULL( @ResponseResult,ResponseResult ),
                                        EserviceID = ISNULL(@EserviceID,EserviceID ),
                                        recorder = ISNULL( @recorder,recorder),
                                        DeptId = CASE WHEN @DeptId = 0 THEN DeptId ELSE @DeptId END

                                WHERE IssueNo = @IssueNo";
                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.ExecuteAsync(tsql, entity);
                    return data;
                };
            }

            catch (Exception ex)
            {
                
                throw ex;
            }
        }
        
       
       
        
    }
}
