using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Record.Contract
{
    public class RecordRequest
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
