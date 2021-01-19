using PHCoreWebAPI.DAL.Repository.RecordCate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PHCoreWebAPI.DAL.DbFactory;

namespace PHCoreWebAPI.DAL.Repository.RecordCate
{
    public class RecordCateRepository : IRecordCateRepository
    {
        private readonly IDbFactory _dbfactory;

        public RecordCateRepository(IDbFactory dbfactory)
        {
            _dbfactory = dbfactory;
        }


        public async ValueTask<int> AddRecordCate(RecordCateEntity entity)
        {
            using (var cn = await _dbfactory.OpenConnectionAsync())
            {
                string tsql = @"INSERT INTO DeptrecordcateMap(DpetID,recordcateID) 
                                    VALUES(@DeptID,@recordcateID)";
                var result = await cn.ExecuteAsync(tsql, entity);
                return result;

            };
        }

        public async ValueTask<IEnumerable<RecordCateEntity>> GetRecordCate()
        {
            try
            {

                string tSql = $@"SELECT recordcateID,IssueName,State
                                 FROM   recordcate
                                 WHERE  state = 1";



                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QueryAsync<RecordCateEntity>(tSql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<IEnumerable<RecordCateEntity>> GetRecordCateByDeptID(int deptid)
        {
            try
            {

                string tSql = $@"SELECT *
                                 FROM recordcate i 
                                 LEFT JOIN DeptrecordcateMap DIM 
                                 ON i.recordcateID = DIM.recordcateID
                                 WHERE DIM.deptID={deptid}
                                 AND state = 1";



                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QueryAsync<RecordCateEntity>(tSql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<int> StopRecordCate(int RecordCateid, int state)
        {
            try
            {
                string tsql = $@"UPDATE  recordcate
                                
                                SET     
                                        State = {state}
                                WHERE   recordcateID = {RecordCateid}";
                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.ExecuteAsync(tsql, new { RecordCateid, state });
                    return data;
                };
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async ValueTask<int> UpdateRecordCate(RecordCateEntity entity)
        {
            try
            {
                string tsql = @"UPDATE  recordcate I
                                
                                SET     I.recordcateName = (@recordcateName,recordcateName),
                                        I.State = CASE WHEN @State= 0 THEN State ELSE @State END
                                WHERE   I.recordcateID = @recordcateID";
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
