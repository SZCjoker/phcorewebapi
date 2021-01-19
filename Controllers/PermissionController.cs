using Microsoft.AspNetCore.Mvc;
using PHCoreWebAPI.Application.Permission;
using PHCoreWebAPI.Application.Permission.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : Controller
    {

        private readonly IPermissionService _service;

        public PermissionController(IPermissionService service)
        {
            _service = service;
        }



        [HttpGet("check")]
        public async ValueTask<IActionResult> Cehck()
        {
            return Ok(DateTime.Now.ToString("D"));
        }

        /// <summary>
        /// 新增功能權限
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> AddPermission(int deptid)
        {
            return Ok(await _service.GetPermissionListByDept(deptid));
        }
        /// <summary>
        /// 依ID取得功能權限清單
        /// </summary>
        /// <param name="deptid"></param>
        /// <returns></returns>
        [HttpGet("{deptid}")]
        public async ValueTask<IActionResult> GetPermissionByID(int deptid)
        {
            return Ok(await _service.GetPermissionListByDept(deptid));
        }
        /// <summary>
        /// 取得所有功能權限清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async ValueTask<IActionResult> GetPermissionList()
        {
            return Ok(await _service.GetPermissionList());
        }
        /// <summary>
        /// 修改功能權限
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async ValueTask<IActionResult> UpdatePermission(PermissionRequest request)
        {
            return Ok(await _service.UpdatePermission(request));
        }

        /// <summary>
        /// 停用功能權限
        /// </summary>
        /// <param name="permissionid"></param>
        /// <returns></returns>
        [HttpDelete("{permissionid}")]
        public async ValueTask<IActionResult> StopPermission(int permissionid)
        {
            return Ok(await _service.DeletePermission(permissionid));
        }

    }
}
