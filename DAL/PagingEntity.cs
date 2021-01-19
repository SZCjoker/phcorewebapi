using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL
{
    public class PagingEntity
    {
        public long BeginDate { get; set; }
        public long EndDate { get; set; }
        public int PageOffset { get; set; }
        public int PageSize { get; set; }
    }
}
