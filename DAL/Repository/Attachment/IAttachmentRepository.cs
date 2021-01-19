using PHCoreWebAPI.DAL.Repository.Attachment.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.Repository.Attachment
{
 public interface IAttachmentRepository
    {
        ValueTask<IEnumerable<AttachmentEntity>> GetAttachmentList();
        ValueTask<IEnumerable<AttachmentEntity>> GetAttachmentByIssue(long issueno);
        ValueTask<AttachmentEntity> GetAttachFileByNo(long attachmentno);
        ValueTask<long> AddAttachment(AttachmentEntity entity);
        ValueTask<int> UpdateAttachment(AttachmentEntity entity);
        ValueTask<int> DeleteAttachment(long attachmentno,int state);
        ValueTask<string> GetUrlByNo(long attachmentno);
    }
}
