using PHCoreWebAPI.DAL.DbFactory;
using PHCoreWebAPI.Models.DAL.Repository.IssueCate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace PHCoreWebAPI.DAL.Repository.IssueCate
{
    public class IssueCateRepository : IIssueCateRepository
    {

        private readonly IDbFactory _dbfactory;

        public IssueCateRepository(IDbFactory dbfactory)
        {
            _dbfactory = dbfactory;
        }
        public async  ValueTask<int> AddIssueCate(IssueCateEntity entity)
        {
            using (var cn = await _dbfactory.OpenConnectionAsync())
            {  
                    string tsql = @"INSERT INTO DeptIssueCateMap(DpetID,IssueCateID) 
                                    VALUES(@DeptID,@IssueCateID)";
                    var result = await cn.ExecuteAsync(tsql, entity);
                    return result;
               
            };
        }

        public async ValueTask<int> StopIssueCate(int issueid, int state)
        {
            try
            {
                string tsql = $@"UPDATE  IssueCate
                                
                                SET     
                                        State = {state}
                                WHERE   IssueCateID = {issueid}";
                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.ExecuteAsync(tsql, new { issueid, state });
                    return data;
                };
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async ValueTask<IEnumerable<IssueCateEntity>> GetIssueCateByDeptID(int deptid)
        {
            try
            {

                string tSql = $@"SELECT *
                                 FROM IssueCate i 
                                 LEFT JOIN DeptIssueCateMap DIM 
                                 ON i.IssueCateID = DIM.issuecateID
                                 WHERE DIM.deptID={deptid}
                                 AND state = 1";



                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QueryAsync<IssueCateEntity>(tSql);
                    return data;
                }
            }
            catch(Exception ex)
            { 
                throw ex; 
            }
        }
        public async ValueTask<IEnumerable<IssueCateEntity>> GetIssueCate()
        {
            try
            {

                string tSql = $@"SELECT IssueCateID,IssueCateName,State
                                 FROM   IssueCate
                                 WHERE  state = 1";



                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QueryAsync<IssueCateEntity>(tSql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<int> UpdateIssueCate(IssueCateEntity entity)
        {
            try
            {
                string tsql = @"UPDATE  IssueCate I
                                
                                SET     I.IssueCateName = (@IssueCateName,IssueCateName),
                                        I.State = CASE WHEN @State= 0 THEN State ELSE @State END
                                WHERE   I.IssueCateID = @IssueCateID";
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
