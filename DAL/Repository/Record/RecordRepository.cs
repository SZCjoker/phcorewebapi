using PHCoreWebAPI.DAL.Repository.Record.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PHCoreWebAPI.DAL.DbFactory;

namespace PHCoreWebAPI.DAL.Repository.Record
{
    public class RecordRepository : IRecordRepository
    {



        private readonly IDbFactory _dbfactory;

        public RecordRepository(IDbFactory dbfactory)
        {
            _dbfactory = dbfactory;
        }





        public async ValueTask<int> CreateRecord(RecordEntity entity)
        {
            try
            {
                string tsql = $@"INSERT INTO Record(RecordNo,DeptID,RecordCateID,Content
                                                    ,Cdate,Ctime,clientid,AttachmentNo,State) 
                                 VALUES(@RecordNo,@DeptID,@RecordCateID,@Content
                                        ,@Cdate,@Ctime,@clientid,@AttachmentNo,@State)";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.ExecuteAsync(tsql, entity);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<IEnumerable<RecordEntity>> GetAllRecord()
        {
            try
            {
                string tsql = $@"SELECT *
                                 FROM Record 
                                 ORDER BY ctime DESC";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QueryAsync<RecordEntity>(tsql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<IEnumerable<RecordEntity>> GetAllRecordByDept(int deptid)
        {
            try
            {
                string tsql = $@"SELECT *
                                 FROM Record 
                                 WHERE deptid = {deptid}
                                 ORDER BY ctime DESC";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QueryAsync<RecordEntity>(tsql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<RecordEntity> GetRecordByNo(long recorno)
        {
            try
            {
                string tsql = $@"SELECT *
                                 FROM Record 
                                 WHERE RecordNo = {recorno}
                                 ORDER BY ctime DESC";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QuerySingleOrDefaultAsync<RecordEntity>(tsql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<IEnumerable<RecordEntity>> GetRecordByclientid(string clientid)
        {
            try
            {
                string tsql = $@"SELECT *
                                 FROM Record 
                                 WHERE clientid = {clientid}
                                 ORDER BY ctime DESC";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QueryAsync<RecordEntity>(tsql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<IEnumerable<RecordEntity>> QueryByCondition(RecordEntity entity, string querySql, int pageoffset, int pagesize)
        {
            try
            {
                string tSql = $@"SELECT * 
                                 FROM Record 
                                 WHERE 1=1 {querySql}
                                 ORDER BY ctime  DESC";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QueryAsync<RecordEntity>(tSql, new { pageoffset, pagesize });
                    return data;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<int> UpdateRecord(RecordEntity entity)
        {
            try
            {
                string tsql = @"UPDATE  Record 
                                SET     
                                        DeptId = CASE WHEN @DeptId = 0 THEN DeptId ELSE @DeptId END,
                                        Recordcateid = CASE WHEN @Recordcateid = 0 THEN Recordcateid ELSE @Recordcateid END,
                                        Content = ISNULL( @Content,Content ),
                                        Cdate = CASE WHEN @Cdate = 0 THEN Cdate ELSE @Cdate END,
                                        Ctime = CASE WHEN @Ctime = 0 THEN Ctime ELSE @Ctime END,
                                        clientid = ISNULL( @clientid,clientid ), 
                                        AttachmentNo = CASE WHEN @AttachmentNo = 0 THEN AttachmentNo ELSE @AttachmentNo END,
                                        State = CASE WHEN @State = 0 THEN State ELSE @State END
                                WHERE RecordNo = @RecordNo";
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
