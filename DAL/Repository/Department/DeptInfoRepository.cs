using PHCoreWebAPI.DAL.DbFactory;
using PHCoreWebAPI.Models.DAL.Department.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace PHCoreWebAPI.DAL.Repository.Department
{
    public class DeptInfoRepository : IDeptInfoRepository
    {
        private readonly IDbFactory _dbFactory;


        public DeptInfoRepository(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }


        public async  ValueTask<int> AddDept(DeptInfoEntity entity)
        {
            try
            {
                string tSql = $@"INSERT INTO IssueInfo(DeptName,State)
                                 VALUES(@DeptName,@State)";

                using (var cn = await _dbFactory.OpenConnectionAsync())
                {
                    var data = await cn.ExecuteAsync(tSql, entity);
                    return data;
                }
            }

            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<int> DimissDept(int deptid, int state)
        {
            try
            {
                string tsql = $@"UPDATE  DeptInfo
                                
                                SET     
                                        State = {state}
                                WHERE DeptID = {deptid}";
                using (var cn = await _dbFactory.OpenConnectionAsync())
                {
                    var data = await cn.ExecuteAsync(tsql,new { deptid,state});
                    return data;
                };
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async ValueTask<IEnumerable<DeptInfoEntity>> GetAllDepts()
        {
            try
            {
                string Tsql = $@"SELECT *
							     FROM DeptInfo
                                 WHERE state =1";

                using (var cn = await _dbFactory.OpenConnectionAsync())
                {
                    var data = await cn.QueryAsync<DeptInfoEntity>(Tsql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<IEnumerable<DeptInfoEntity>> GetDeptsByDeptID(int deptid)
        {
            try
            {
                string Tsql = $@"SELECT *
							     FROM DeptInfo 
                                 WHERE Deptid = { deptid }
                                 AND state =1";

                using (var cn = await _dbFactory.OpenConnectionAsync())
                {
                    var data = await cn.QueryAsync<DeptInfoEntity>(Tsql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public async ValueTask<int> UpdateDept(DeptInfoEntity entity)
        {
            try
            {
                string tsql = @"UPDATE  DeptInfo
                                
                                SET     
                                        DeptName =ISNULL(@DeptName,DeptName),
                                        State = CASE WHEN @State= 0 THEN State ELSE @State END
                                WHERE DeptID = @DeptID";
                using (var cn = await _dbFactory.OpenConnectionAsync())
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
