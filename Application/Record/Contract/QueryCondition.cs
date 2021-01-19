using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Record.Contract
{
    public class QueryCondition
    {
        public Int64 RecordNo { get; set; }

        public int DeptID { get; set; }

        public string Content { get; set; }

        public int Cdate { get; set; }

        public Int64 Ctime { get; set; }

        public string ClientID { get; set; }

        public string BeginDate { get; set; }
        public string EndDate { get; set; }

        public int pageoffset { get; set; }
        public int pagesize { get; set; }
        public int State { get; set; }
    }
}
