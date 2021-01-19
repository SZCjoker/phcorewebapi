using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PHCoreWebAPI.Application.Account;
using PHCoreWebAPI.Application.Permission;
using PHCoreWebAPI.DAL.DbFactory;
using PHCoreWebAPI.DAL.Repository.PermissionInfo;
using PHCoreWebAPI.DAL.Repository.Account;
using PHCoreWebAPI.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PHCoreWebAPI.Application.Issue;
using PHCoreWebAPI.DAL.Repository.Issue;
using PHCoreWebAPI.Application.FileUpload;
using PHCoreWebAPI.DAL.Repository.Attachment;
using PHCoreWebAPI.Application.Setting;
using PHCoreWebAPI.DAL.Repository.Setting;
using PHCoreWebAPI.DAL.Repository.Department;
using PHCoreWebAPI.DAL.Repository.IssueCate;
using PHCoreWebAPI.Application.DataExport;
using PHCoreWebAPI.DAL.Repository.RecordCate;
using PHCoreWebAPI.DAL.Repository.Record;
using PHCoreWebAPI.Application.Record;
using PHCoreWebAPI.Application.SendMail;

namespace PHCoreWebAPI.Injectionconfigs
{
    [Injection]
    public class ServiceConfig
    {

        public ServiceConfig(IServiceCollection services, IConfiguration configuration)
        {



            services.AddScoped<IEmailSenderService, MailSender>();
            services.AddScoped<IManagementService, ManagementService>()
                    .AddScoped<IManagementRepository, ManagementRepository>();
            services.AddScoped<IExportService, DataExportService>();
            services.AddScoped<IDeptInfoRepository, DeptInfoRepository>();
            services.AddScoped<IIssueCateRepository,IssueCateRepository>();
            services.AddScoped<IRecordCateRepository, RecordCateRepository>();

            services.AddScoped<IFileUploadService, FileUploadService>()
                    .AddScoped<IAttachmentRepository, AttachmentRepository>();
            services.AddScoped<IissueService, IssueService>()
                    .AddScoped<IIssueRepository, IssueRepository>();
                
            services.AddScoped<IRecordService,RecordService>()
                    .AddScoped<IRecordRepository,RecordRepository>();
            
            services.AddScoped<IAccountService, AccountService>()
                   .AddScoped<IAccountRepository,AccountRepository>();
            services.AddScoped<IPermissionService, PermissionService>()
                  .AddScoped<IPermissionRepository, PermissionRepository>();
            
        }
    }
}
