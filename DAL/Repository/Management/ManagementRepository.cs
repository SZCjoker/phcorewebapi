using PHCoreWebAPI.DAL.DbFactory;
using PHCoreWebAPI.DAL.Repository.Setting.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace PHCoreWebAPI.DAL.Repository.Setting
{
    public class ManagementRepository : IManagementRepository
    {

        private readonly IDbFactory _dbFactory;

        public ManagementRepository(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async ValueTask<int> GetOrderby(int detpid)
        {

            try
            {
                string tsql = $@"SELECT COUNT(Orderby)
                                 FROM   DeptPermissionMap
                                 WHERE  DeptID= {detpid}";

                using (var cn = await _dbFactory.OpenConnectionAsync())
                {
                    var data = await cn.QuerySingleAsync<int>(tsql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        //need get list first then  front end click get item's id and value 
        public  async ValueTask<int> TieUpIssueCateDept(List<ManagementEntity> entity)
        {
            try
                {
                    using (var cn = await _dbFactory.OpenConnectionAsync())
                    {
                        using (var trans = cn.BeginTransaction())
                        {
                            string tsql = @"If EXISTS (SELECT *  
                                                       FROM DeptIssueCateMap 
                                                       WHERE IssueID = @issuecateid AND DeptID = @deptid)
  
                                             BEGIN
                                             UPDATE DeptIssueCateMap
                                             SET   IssueID = @issuecateid
                                             WHERE DeptID = @deptid
                                             END  
 
                                           ELSE
                                            
                                             BEGIN
                                             INSERT INTO DeptIssueCateMap(DeptID,IssueCateID)
                                             VALUES(@deptid,@issuecateid)
                                             END;";
                            var result = await cn.ExecuteAsync(tsql, entity, trans);
                            trans.Commit();
                            return result;
                        }
                    };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }

        public  async ValueTask<int> TieUpPermissionDept(List<ManagementEntity> entity)
        {

            try
            {
                using (var cn = await _dbFactory.OpenConnectionAsync())
                {
                    using (var trans = cn.BeginTransaction())
                    {
                        string tsql = @"If EXISTS (SELECT *  
                                                       FROM DeptPermissionMap 
                                                       WHERE DeptID = @DeptID AND PermissionID = @permissionID)
  
                                            BEGIN
                                            UPDATE DeptPermissionMap
                                            SET   PermissionID = @permissionID
                                            WHERE DeptID = @DeptID
                                            END  
 
                                        ELSE
                                            
                                            BEGIN
                                            INSERT INTO DeptPermissionMap(DeptID,PermissionID)
                                            VALUES(@DeptID,@permissionID)
                                            END;";
                        var result = await cn.ExecuteAsync(tsql, entity, trans);
                        trans.Commit();
                        return result;
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            throw new NotImplementedException();
        }

        public async ValueTask<int> TieUpRecordCateDept(List<ManagementEntity> entity)
        {
            try
            {
                using (var cn = await _dbFactory.OpenConnectionAsync())
                {
                    using (var trans = cn.BeginTransaction())
                    {
                        string tsql = @"If EXISTS (SELECT *  
                                                       FROM DeptRecordCateMap 
                                                       WHERE IssueID = @recordcateid AND DeptID = @deptid)
  
                                             BEGIN
                                             UPDATE DeptRecordCateMap
                                             SET   IssueID = @recordcateid
                                             WHERE DeptID = @deptid
                                             END  
 
                                           ELSE
                                            
                                             BEGIN
                                             INSERT INTO DeptRecordCateMap(DeptID,RecordCateID)
                                             VALUES(@deptid,@recordcateid)
                                             END;";
                        var result = await cn.ExecuteAsync(tsql, entity, trans);
                        trans.Commit();
                        return result;
                    }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
