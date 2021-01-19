using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.DataExport.Contract
{
    public class DataCondition
    {
        public string Source { get; set; }

        public string Target { get; set; }

        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public int PageOffset { get; set; } = 1;
        public int PageSize { get; set; } = 1000;
    }
}
