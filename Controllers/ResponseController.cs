using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : Controller
    {
        /// <summary>
        /// 測試
        /// </summary>
        /// <returns></returns>
        [HttpGet("check")]
        public async ValueTask<IActionResult> Cehck()
        {
            return Ok(DateTime.Now.ToString("D"));
        }
    }
}
