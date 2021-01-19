using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PHCoreWebAPI.Application.FileUpload;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : Controller
    {

        private readonly IFileUploadService _service;

        public FileController(IFileUploadService service)
        {
            _service = service;
        }



        /// <summary>
        /// 測試
        /// </summary>
        /// <returns>回傳今日日期</returns>
        [HttpGet("check")]
        public IActionResult Index()
        {
            return Ok(DateTime.Now.ToString("D"));
        }
        /// <summary>
        /// 單筆上傳
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]//2000M 2147483648  2G
        [RequestSizeLimit(2147483648)]
        public async ValueTask<IActionResult> UploadFile()
        {
            IFormFileCollection file  = Request.Form.Files ?? null;
            return Ok(await _service.UpLoadFile(file));
        }
        /// <summary>
        /// 多筆上傳
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        //[HttpPost("multiple")]
        //[RequestFormLimits(MultipartBodyLengthLimit = 2147483648)]//2000M 2147483648  2G
        //[RequestSizeLimit(2147483648)]
        //public async ValueTask<IActionResult> UploadFiles()
        //{
        //    IFormFileCollection file = Request.Form.Files ?? null;
        //    return Ok(await _service.UpLoadFiles(file));
        //}

    }
}
