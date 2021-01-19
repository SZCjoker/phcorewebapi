using Microsoft.AspNetCore.Http;
using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.Application.FileUpload.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.FileUpload
{
 public  interface IFileUploadService
    {
        ValueTask<BasicResponse<AttachmentResponse>> UpLoadFile(IFormFileCollection file);
        ValueTask<BasicResponse<AttachmentResponse>> UpLoadFiles(IFormFileCollection files);
        ValueTask<BasicResponse<AttachmentResponse>> GetAttachementByNo(int issueno); 
        ValueTask<BasicResponse<AttachmentResponse>> UpdateAttachementState(int issueno); 

    }
}
