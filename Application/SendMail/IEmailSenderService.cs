using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.SendMail
{
 public interface IEmailSenderService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
