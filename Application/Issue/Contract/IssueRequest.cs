using Microsoft.AspNetCore.Http;
using PHCoreWebAPI.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PHCoreWebAPI.Application.Issue.Contract
{
 public  class IssueRequest:PagingRequest
    {
        
        public Int64 IssueNo { get; set; }
        public int DeptID { get; set; }  
        public string Content { get; set; }
        public string CreateTime { get; set; }
        public string AskingTime { get; set; }
        public string CallTime { get; set; }
        public string Utime { get; set; }
        public string ClientID { get; set; }
        public string TestResult { get; set; }
        public string Device { get; set; }
        public int EntranceType { get; set; }
        public int IssueCateID { get; set; }
        public string IssueDescription { get; set; }
        /// <summary>
        /// 問題紀錄
        /// </summary>
        public string IssueRecord { get; set; }
        public string Response { get; set; }
        /// <summary>
        /// 1新建 2處理中 9結案
        /// </summary>
        public int ProcessStatus { get; set; }
        public int AnswerStatus { get; set; }
        public int Solve { get; set; }
        public string Suggestion { get; set; }
        public string Purpose { get; set; }
        public string Reason { get; set; }
        /// <summary>
        /// 處理結果
        /// </summary>
        public string ResponseResult { get; set; }
        public string ResponseTime { get; set; }
        public string EserviceID { get; set; }
        public string Recorder { get; set; }
        public Int64 AttachmentNo { get; set; }        
    }
}
