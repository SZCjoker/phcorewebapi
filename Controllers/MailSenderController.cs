using Microsoft.AspNetCore.Mvc;
using PHCoreWebAPI.Application.SendMail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailSenderController : Controller
    {

        private readonly IEmailSenderService _emailSenderService;

        public MailSenderController(IEmailSenderService emailSenderService)
        {
            _emailSenderService = emailSenderService;
        }

        /// <summary>
        /// Check
        /// </summary>
        /// <returns></returns>
        public IActionResult Check()
        {
            return Ok(DateTime.Now.ToString("d"));
        }

        /// <summary>
        /// sendmail
        /// </summary>
        /// <returns></returns>
        [HttpPost("send")]
        public async ValueTask<IActionResult> SendMail()
        {
            await _emailSenderService.SendEmailAsync("", "", "");
           return Ok( );
        }
    }
}
