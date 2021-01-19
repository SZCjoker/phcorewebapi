using Microsoft.AspNetCore.Mvc;
using PHCoreWebAPI.Application.Record;
using PHCoreWebAPI.Application.Record.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : Controller
    {
        private readonly IRecordService _service;


        public RecordController(IRecordService service
                                )
        {
            _service = service;
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
        public async ValueTask<IActionResult> AddRecord(RecordRequest request)
        {
            var data = this.Request.Form.Files;
            return Ok(await _service.AddRecord(request));
        }

        /// <summary>
        /// 取得所有紀錄
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async ValueTask<IActionResult> GetRecordList()
        {
            return Ok(await _service.GetRecordList());
        }
       

        /// <summary>
        /// 依部門取得紀錄
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        [HttpGet("{deptid}")]
        public async ValueTask<IActionResult> GetRecordListByDept(int deptid)
        {
            return Ok(await _service.GetRecordListByDept(deptid));
        }
        /// <summary>
        /// 依單號取得紀錄單
        /// </summary>
        /// <param name="Recordno"></param>
        /// <returns></returns>
        [HttpGet("Recordno/{Recordno}")]
        public async ValueTask<IActionResult> GetRecordByNo(Int64 recordno)
        {
            return Ok(await _service.GetRecordByNo(recordno));
        }

        /// <summary>
        /// 編輯紀錄
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async ValueTask<IActionResult> UpdateRecord(RecordRequest request)
        {
            return Ok(await _service.UpdateRecord(request));
        }
        /// <summary>
        /// 更改案件及附件狀態  處理中 送2  結案 9
        /// </summary>
        /// <param name="recordRequest"></param>
        /// <returns></returns>
        [HttpPut("State")]
        public async ValueTask<IActionResult> RecordStateChange(RecordRequest recordRequest)
        {
            return Ok(await _service.RecordStateChange(recordRequest));
        }
        /// <summary>
        /// 搜尋紀錄
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpGet("query")]
        public async ValueTask<IActionResult> QueryByCondition([FromQuery] QueryCondition condition)
        {
            return Ok(await _service.QueryBycondition(condition));
        }





    }
}
