using Microsoft.AspNetCore.Mvc;
using PHCoreWebAPI.Application.Setting;
using PHCoreWebAPI.Application.Setting.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : Controller
    {


        private readonly IManagementService _service;


        public ManagementController(IManagementService service)
        {
            _service = service;
        }


        public IActionResult Check()
        {
            return View(DateTime.Now.ToString("D"));
        }

        /// <summary>
        /// 系統管理 - 綁定問題種類與部門
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("tieupIssueDept")]
        public async ValueTask<IActionResult> TieUpIssueDept(List<ManagementRequest> request)
        {
            return Ok(await _service.TieUpIssueCateDept(request));
        }
        /// <summary>
        /// 系統管理 -綁定或更新權限(頁面)與部門
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("tieupPermissionDept")]
        public async ValueTask<IActionResult> TieUpPermissionDept(List<ManagementRequest> request)
        {
            return Ok(await _service.TieUpPermissionDept(request));
        }

        [HttpPost("tieupRecordCate")]
        public async ValueTask<IActionResult> TieUpRecordCateDept(List<ManagementRequest> request)
        {
            return Ok(await _service.TieUpRecordCateDept(request));
        }


        /// <summary>
        /// 新增部門
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("addDept")]
        public async ValueTask<IActionResult> AddDept(ManagementRequest request)
        {
            return Ok(await _service.AddDept(request));
        }
        /// <summary>
        /// 新增問題種類
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("addIssueCate")]
        public async ValueTask<IActionResult> AddIssueCate(ManagementRequest request)
        {
            return Ok(await _service.AddIssueCate(request));
        }
        /// <summary>
        /// 新增 紀錄分類
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("addRecordCate")]
        public async ValueTask<IActionResult> AddRecordCate(ManagementRequest request)
        {
            return Ok(await _service.AddRecordCate(request));
        }



        /// <summary>
        /// 取得所有部門清單
        /// </summary>
        /// <returns></returns>
        [HttpGet("deptList")]
        public async ValueTask<IActionResult> GetDeptList()
        {
            return Ok(await _service.GetDeptList());
        }
        /// <summary>
        /// 依登入者cookie部門ID取得對應部門
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        [HttpGet("dept/{deptid}")]
        public async ValueTask<IActionResult> GetDeptListByID(int deptid)
        {
            return Ok(await _service.GetDeptsByDeptID(deptid));
        }

        /// <summary>
        /// 取得所有問題種類清單
        /// </summary>
        /// <returns></returns>
        [HttpGet("IssueCateList")]
        public async ValueTask<IActionResult> GetIssueList()
        {
            return Ok(await _service.GetIssueCateList());

        }
        /// <summary>
        /// 依登入者部門取得對應問題種類
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        [HttpGet("issueCate/{deptid}")]
        public async ValueTask<IActionResult> GetIssueListByID(int deptid)
        {
            return Ok(await _service.GetIssueCateByDeptID(deptid));
        }

        /// <summary>
        /// 取得所有紀錄種類清單
        /// </summary>
        /// <returns></returns>
        [HttpGet("RecordCateList")]
        public async ValueTask<IActionResult> GetRecordCateList()
        {
            return Ok(await _service.GetRecordCateList());

        }
        /// <summary>
        /// 依登入者部門取得對應紀錄種類
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        [HttpGet("RecordCate/{deptid}")]
        public async ValueTask<IActionResult> GetRecordCateByDeptID(int deptid)
        {
            return Ok(await _service.GetRecordCateByDeptID(deptid));
        }


        /// <summary>
        /// 編輯部門資料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("dept")]
        public async ValueTask<IActionResult> UpdateDept(ManagementRequest request)
        {
            return Ok(await _service.UpdateDept(request));
        }
        /// <summary>
        /// 編輯問題種類
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("issueCate")]
        public async ValueTask<IActionResult> UpdateIssueCate(ManagementRequest request)
        {
            return Ok(await _service.UpdateIssueCate(request));
        }

        /// <summary>
        /// 編輯紀錄種類
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("RecordCate")]
        public async ValueTask<IActionResult> UpdateRecordCate(ManagementRequest request)
        {
            return Ok(await _service.UpdateRecordCate(request));
        }


        /// <summary>
        /// 刪除部門
        /// </summary>
        /// <param name="deptid"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpDelete("dept")]
        public async ValueTask<IActionResult> StopDept(int deptid, int state)
        {
            return Ok(await _service.StopDept(deptid, state));
        }
        /// <summary>
        /// 刪除問題種類
        /// </summary>
        /// <param name="issuetypeid"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpDelete("IssueCate")]
        public async ValueTask<IActionResult> StopIssueCate(int issuecateid, int state)
        {
            return Ok(await _service.StopIssueCate(issuecateid, state));
        }

        /// <summary>
        /// 刪除紀錄種類
        /// </summary>
        /// <param name="recordcateid"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        [HttpDelete("RecordCate")]
        public async ValueTask<IActionResult> StopRecordCate(int recordcateid, int state)
        {
            return Ok(await _service.StopRecordCate(recordcateid,state));
        }
        /// <summary>
        /// 取得目前版本號
        /// </summary>
        /// <returns></returns>
        [HttpGet("version")]
        public async ValueTask<IActionResult> GetVersion()
        {
            return Ok(await _service.GetVersion());
        }

        [HttpGet("environment")]
        public  async ValueTask<IActionResult> GetEnvironment()
        {

            return Ok(await _service.GetEnvironment());
        }
    }
}
