using PHCoreWebAPI.DAL.DbFactory;
using PHCoreWebAPI.DAL.Repository.Attachment.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace PHCoreWebAPI.DAL.Repository.Attachment
{
    public class AttachmentRepository : IAttachmentRepository
    {

        private readonly IDbFactory _dbfactory;

        public AttachmentRepository(IDbFactory dbfactory)
        {
            _dbfactory = dbfactory;
        }
        public async ValueTask<long> AddAttachment(AttachmentEntity entity)
        {

            try
            {
                string insetMainsql = $@"INSERT INTO dbo.Attachment(AttachmentNo
                                                                    ,AttachType
                                                                    ,State)
                                         OUTPUT INSERTED.AttachmentNo 
                                         VALUES(@AttachmentNo,
                                                @AttachType,
                                                @State)";

                string insertSubsql = $@"INSERT  INTO dbo.AttachmentDetail
                                                           (AttachmentNo
                                                           ,OriginalName
                                                           ,InternalName
                                                           ,Img
                                                           ,Url
                                                           ,Cdate
                                                           ,Ctime)
                                        VALUES
                                                            (@AttachmentNo, 
                                                             @OriginalName, 
                                                             @InternalName, 
                                                             @Img,
                                                             @Url,
                                                             @Cdate,
                                                             @Ctime
                                                             )";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.ExecuteScalarAsync<long>(insetMainsql+ insertSubsql,entity);
                    return data;
                }

            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async ValueTask<int> DeleteAttachment(long attachmentno, int state)
        {
            #region  TRUE DLETED SQL
            /*  
            $@"delete AttachmentDetail from AttachmentDetail as ad
            left join Attachment a on a.AttachmentNo = ad.AttachmentNo
            where a.AttachmentNo = {attachmentno}

            delete Attachment
            where Attachment.AttachmentNo = {attachmentno}";*/
            #endregion

            string tSql = $@"UPDATE Attachment
                             SET state = {state}
                             WHERE Record = {attachmentno} ";

            using (var cn = await _dbfactory.OpenConnectionAsync())
            {
                var data = await cn.ExecuteAsync(tSql, new { attachmentno, state });
                return data;
            }
        }

        public async ValueTask<AttachmentEntity> GetAttachFileByNo(long attachmentno)
        {
            try
            {
                string tsql = $@"SELECT *
                                 FROM AttachmentDetail 
                                 WHERE attachmentno = {attachmentno}";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QuerySingleOrDefaultAsync<AttachmentEntity>(tsql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public async ValueTask<IEnumerable<AttachmentEntity>> GetAttachmentByIssue(long issueno)
        {

            try
            {
                string tsql = $@"SELECT *
                                 FROM Record R LEFT JOIN AttachmentDetail  ad 
                                 ON  R.attachmentno = ad.attachmentno
                                 WHERE r.recordno ={issueno}";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QueryAsync<AttachmentEntity>(tsql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<IEnumerable<AttachmentEntity>> GetAttachmentList()
        {

            try
            {
                string tsql = $@"SELECT *
                                 FROM Attachment A LEFT JOIN AttachmentDetail  ad 
                                 ON  A.attachmentno = ad.attachmentno";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QueryAsync<AttachmentEntity>(tsql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            
        }

        public async ValueTask<string> GetUrlByNo(long attachmentno)
        {
            try
            {
                string tsql = $@"SELECT Url
                                 FROM AttachmentDetail   
                                 WHERE attachmentno = {attachmentno}";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QuerySingleOrDefaultAsync<string>(tsql,attachmentno);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ValueTask<int> UpdateAttachment(AttachmentEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
