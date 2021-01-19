using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Common
{
 public struct BasicResponse<T>
    {
        public int code { get; set; }
        public string desc { get; set; }
        public T data { get; set; }
    }

    public struct BasicResponse
    {
        public int code { get; set; }
        public string desc { get; set; }
    }
}
