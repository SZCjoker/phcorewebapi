using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.FileUpload.Contract
{
    public class AttachmentRequest
    {
       
            public Int64 AttachmentNo { get; set; }
            public int Type { get; set; }
            public int State { get; set; }
            public string OriginalName { get; set; }
            public string InternalName { get; set; }
            public string Img { get; set; }
            public string Url { get; set; }
            public int Cdate { get; set; }
            public Int64 Ctime { get; set; }
            public int Udate { get; set; }
            public Int64 Utime { get; set; }        
            public List<IFormFile> Files { get; set; }
    }
   
}
