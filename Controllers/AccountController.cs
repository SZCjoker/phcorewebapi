using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PHCoreWebAPI.Application.Account;
using PHCoreWebAPI.Application.Account.Contract;
using PHCoreWebAPI.Application.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {


        private readonly IAccountService _service;
        private readonly IGetJwtTokenInfoService _token;
        private readonly ILogger<AccountController> _logger;


        public AccountController(IAccountService service,
                                 IGetJwtTokenInfoService token,
                                 ILogger<AccountController> logger
                                    )
        {
            _token = token;
            _service = service;
            _logger = logger;

        }


        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login"), AllowAnonymous]
        public async ValueTask<IActionResult> Login([FromBody] LoginRequest request)
        {
            return Ok(await _service.Login(request));
        }

        /// <summary>
        /// 新增用戶-含各部門人員
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> AddUserInfo(CreateUpdateUserRequest request)
        {
            return Ok(await _service.CreateUser(request));
        }
        /// <summary>
        /// 取得所有使用者
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async ValueTask<IActionResult> GetUserInfo()
        {
            return Ok(await _service.GetUserInfo());
        }
        /// <summary>
        /// 依ID取得使用者
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpGet("{userid}")]
        public async ValueTask<IActionResult> GetUserInfoByID(string userid)
        {
            return Ok(await _service.GetUserInfoById(userid));
        }
        /// <summary>
        /// 編輯使用者資料
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        public async ValueTask<IActionResult> UpdaateUserInfo(CreateUpdateUserRequest request)
        {
            return Ok(await _service.UpdateUserInfo(request));
        }
        /// <summary>
        /// 使用者停權
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        [HttpDelete("{userid}")]
        public async ValueTask<IActionResult> DeleteUserInfo(string userid)
        {
            return Ok(await _service.DeleteUser(userid));
        }


    }
}
