using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Common
{
    public struct PagingResponse<T>
    {
        public int code { get; set; }
        public string desc { get; set; }
        public T data { get; set; }
        public Paging paging { get; set; }
    }
}
