using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.Repository.Attachment.Contract
{
    public class AttachmentEntity
    {
        public Int64 AttachmentNo { get; set; }
        public int AttachType { get; set; }
        public string OriginalName { get; set; }
        public string InternalName { get; set;}
        public string Img { get; set; }
        public string Url { get; set; }
        public int Cdate { get; set; }
        public Int64 Ctime { get; set; }
        public int Udate { get; set; }
        public Int64 Utime { get; set; }
        public int State { get; set; }
    }

}
