using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.Repository.Issue.Entity
{
 public class IssueEntity:PagingEntity
    {

        public Int64 IssueNo { get; set; }
        public int DeptID { get; set; }
        public Int64 CreateTime { get; set; }
        public Int64 CallTime { get; set; }
        public Int64 AskingTime { get; set; }
        public Int64 Utime { get; set; }
        /// <summary>
        /// 使用者或是客戶ID
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        /// 客戶使用的終端裝置
        /// </summary>
        public string Device { get; set; }
        /// <summary>
        /// 發生問題環境  1 PC 2全站 3體育 4H5
        /// </summary>
        public int EntranceType { get; set; }
        /// <summary>
        /// 問題種類ID  1 下載問題 2登入 3平台 4閃退 5劫持~~ connection with issueinfo
        /// </summary>
        public int IssueCateID { get; set; }
        /// <summary>
        /// 反饋問題
        /// </summary>
        public string Response { get; set; }
        /// <summary>
        /// 問題描述
        /// </summary>
        public string IssueDescription { get; set; }
        /// <summary>
        /// 附件ID
        /// </summary>
        public Int64 AttachmentNo { get; set; }
        public string TestResult { get; set; }
        /// <summary>
        /// 1新建 2處理中 9結案
        /// </summary>
        public int ProcessStatus { get; set; }
        public int AnswerStatus { get; set; }
        public int Solve { get; set; }
        public string Purpose { get; set; }
        /// <summary>
        /// 問題發生原因
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 溝通內容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 回復時間 UTC 秒數
        /// </summary>
        public Int64 ResponseTime { get; set; }
        public string Suggestion { get; set; }
        /// <summary>
        /// 處理結果
        /// </summary>
        public string ResponseResult { get; set; }
        /// <summary>
        /// 該次電話客服人員
        /// </summary>
        public string EserviceID { get; set; }        /// <summary>
        /// 紀錄者
        /// </summary>
        public string Recorder { get; set; }
        public string IssueRecord { get; set; }
    }
}
