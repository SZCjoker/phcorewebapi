using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.Repository.Record.Entity
{
    public class RecordEntity
    {
        public Int64 RecordNo { get; set; }

        public int DeptID { get; set; }

        public string Content { get; set; }

        public int Cdate { get; set; }

        public Int64 Ctime { get; set; }
        public int Udate { get; set; }

        public Int64 Utime { get; set; }


        public string ClientID { get; set; }

        public Int64 AttachmentNo { get; set; }

        public int State { get; set; }

    }
}
