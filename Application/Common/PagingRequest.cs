using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Common
{
    public class PagingRequest
    {
        public string BeginDate { get; set; }
        public string EndDate { get; set; }
        public int PageOffset { get; set; }
        public int PageSize { get; set; }
    }
}
