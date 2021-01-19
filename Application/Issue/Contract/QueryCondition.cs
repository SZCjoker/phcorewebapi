using PHCoreWebAPI.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Issue.Contract
{
    public class QueryCondition:PagingRequest
    {
        public Int64 IssueNo { get; set; }
        public int DeptID { get; set; }
        public string ResponseTime { get; set; }
        public string ClientID { get; set; }
        public string Device { get; set; }
        public int EntranceType { get; set; }
        public int IssueCateID { get; set; }
        public int ProcessStatus { get; set; }
        public string EserviceID { get; set; }
        public string Recorder { get; set; }
    }
}
