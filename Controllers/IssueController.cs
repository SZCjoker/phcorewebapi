using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.Application.DataExport;
using PHCoreWebAPI.Application.DataExport.Contract;
using PHCoreWebAPI.Application.Issue;
using PHCoreWebAPI.Application.Issue.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : Controller
    {

        private readonly IissueService _service;
        private readonly IExportService _exportService;

        public IssueController(IissueService service, 
                                IExportService exportService)
        {
            _service = service;
            _exportService = exportService;
        }

        /// <summary>
        /// CHECK
        /// </summary>
        /// <returns></returns>
        [HttpGet("check")]
        public async ValueTask<IActionResult> Check()
        {
            return Ok(DateTime.Now.ToString("D"));
        }

        /// <summary>
        /// 新增紀錄
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> AddIssue(IssueRequest request)
        {  
            return Ok(await _service.AddIssue(request));
        }


        /// <summary>
        ///取得所有紀錄 
        /// </summary>
        /// <param name="request">begingdate,enddate,pageoffset,pagesize</param>
        /// <remarks>預設區間為本日起往前三天的資料</remarks>
        /// <returns></returns>
        [HttpGet]
        public async ValueTask<IActionResult> GetIssueList([FromQuery] PagingRequest request)
        {
            return Ok(await _service.GetIssueList(request));
        }

        /// <summary>
        /// pm10 to am10
        /// </summary>
        /// <param name="request">begingdate,enddate,pageoffset,pagesize</param>
        /// <remarks>預設區間為本日起往前三天的資料</remarks>
        /// <returns></returns>
        [HttpGet("ten2ten")]
        public async ValueTask<IActionResult> Ten2Ten([FromQuery] PagingRequest request)
        {
            return Ok(await _service.GetIssuePm10toAm10(request));
        }
        /// <summary>
        /// 無法登入
        /// </summary>
        /// <param name="request">begingdate,enddate,pageoffset,pagesize</param>
        /// <remarks>預設區間為本日起往前三天的資料</remarks> 
        /// <returns></returns>
        [HttpGet("Loginfailed")]
        public async ValueTask<IActionResult> GetLoginFailed([FromQuery] PagingRequest request)
        {
            return Ok(await _service.GetLoginIssue(request));
        }

        /// <summary>
        /// 依部門取得紀錄
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        [HttpGet("{deptid}")]
        public async ValueTask<IActionResult> GetIssueListByDept(int deptid,[FromQuery]PagingRequest request)
        {
            return Ok(await _service.GetIssueListByDept(deptid,request));
        }
        /// <summary>
        /// 依單號取得問題單
        /// </summary>
        /// <param name="issueno"></param>
        /// <returns></returns>
        [HttpGet("issueno/{issueno}")]
        public async ValueTask<IActionResult> GetIssueByNo(Int64 issueno)
        {
            return Ok(await _service.GetIssueByNo(issueno));
        }

        /// <summary>
        /// 編輯紀錄
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async ValueTask<IActionResult> UpdateIssue(IssueRequest request)
        {
            return Ok(await _service.UpdateIssue(request));
        }
        /// <summary>
        /// 更改案件及附件狀態  處理中 送2  結案 9
        /// </summary>
        /// <param name="recordRequest"></param>
        /// <returns></returns>
        [HttpPut("State")]
        public async ValueTask<IActionResult> IssueStateChange(IssueRequest recordRequest)
        {
            return Ok(await _service.IssueStateChange(recordRequest));
        }
       
        /// <summary>
        /// 搜尋
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryByCondition([FromQuery] QueryCondition condition)
        {
            return Ok(await _service.QueryBycondition(condition));
        }

        /// <summary>
        /// 下載EXCEL
        /// </summary>
        /// <param name="condition"></param>
        /// <remarks>source=功能名稱字串{response,2210,loginfailed},target可為空,請將功能API相關日期參數
        /// 一併傳入begindate,enddate,預設區間為本日起往前三天的資料,分頁不用填預設是一千筆資料</remarks> 
        /// <returns></returns>
        [HttpGet("excel")]
        public async ValueTask<IActionResult> Print([FromQuery]DataCondition condition)
        {

            var data = await _exportService.Print(condition);
            var path = $"{data.data.Folder}{data.data.FileName}";
            var memoryStream = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memoryStream);
            }
            memoryStream.Seek(0, SeekOrigin.Begin);

            //return new FileStreamResult(memoryStream, "application/.xlsx");
          
            return File(memoryStream, "application/octet-stream", data.data.FileName);  
        }

    }
}
