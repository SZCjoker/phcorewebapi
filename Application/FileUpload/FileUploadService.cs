using Microsoft.AspNetCore.Http;
using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.Application.FileUpload.Contract;
using PHCoreWebAPI.DAL.Repository.Attachment;
using PHCoreWebAPI.DAL.Repository.Attachment.Contract;
using PHCoreWebAPI.Handler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.FileUpload
{
    public class FileUploadService : IFileUploadService
    {

        private readonly IAttachmentRepository _attchmentRepository;
        private readonly IGenerateId _generate;
        public FileUploadService(IAttachmentRepository attachmentRepository
                                ,IGenerateId generate )
        {
            _attchmentRepository = attachmentRepository;
            _generate = generate;
        }


        public ValueTask<BasicResponse<AttachmentResponse>> GetAttachementByNo(int recordno)
        {
            throw new NotImplementedException();
        }

        public ValueTask<BasicResponse<AttachmentResponse>> UpdateAttachementState(int issueno)
        {
            throw new NotImplementedException();
        }

        public async  ValueTask<BasicResponse<AttachmentResponse>> UpLoadFile(IFormFileCollection files)
        {

            if (files == null || files.Count == 0)
                return new BasicResponse<AttachmentResponse>() { code = 9999, desc = "without file",data=null };

            var response = new AttachmentResponse();                
            var datetime = DateTimeOffset.UtcNow;
            var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
            var time = datetime.ToUnixTimeSeconds();
            var snowFlakeId = _generate.GetId();
            var UrlStr = new StringBuilder();
            var FileStr = new StringBuilder();

            if (files.Count > 0)
            {
                try
                {
                    #region   TODO  refector  a method
                    foreach (var file in files)
                    {
                        string fileExtension = Path.GetExtension(file.FileName);

                        if (new[] { ".gif", ".jpg", ".jpeg", ".png", ".doc", ".docx" }.Contains(fileExtension))
                        {
                            var rootFolder = @"D:\Files";
                            var fileName = file.FileName;
                            var filePath = Path.Combine(rootFolder, fileName);

                            var path = filePath;
                            if (!Directory.Exists(rootFolder))
                            {
                                Directory.CreateDirectory(rootFolder);

                                using (var stream = new FileStream(path, FileMode.Create))
                                {

                                    await file.CopyToAsync(stream);
                                }
                                FileStr.Append($"{fileName},");
                                UrlStr.Append($"{filePath},");
                            }


                            using (var stream = new FileStream(path, FileMode.Create))
                            {

                                await file.CopyToAsync(stream);
                            }
                            FileStr.Append($"{fileName},");
                            UrlStr.Append($"{filePath},");
                        }
                        else
                        {
                            return new BasicResponse<AttachmentResponse>() { code = 9999, desc = "please check file formate", data = null };
                        }

                    }

                    var entity = new AttachmentEntity
                    {
                        AttachmentNo = snowFlakeId,
                        OriginalName = FileStr.ToString(),
                        InternalName = $"{snowFlakeId}-file",
                        AttachType = ExtensionNameFilter(string.Empty),
                        Url = UrlStr.ToString(),
                        Cdate = date,
                        Ctime = time,
                        State = 1
                    };


                    var data = await _attchmentRepository.AddAttachment(entity);


                    var attachment = await _attchmentRepository.GetAttachFileByNo(data);
                    response.AttachmentNo = attachment.AttachmentNo;
                    response.Url = attachment.Url;


                    return new BasicResponse<AttachmentResponse> { code = 1111, desc = "isnsert success", data = response };



                }
                catch (Exception ex)
                {

                    throw ex;
                }
                #endregion
            }

            try
            {
                string fileExtension = Path.GetExtension(files[0].FileName);

                if (new[] { ".gif", ".jpg", ".jpeg", ".png",".doc",".docx" }.Contains(fileExtension))
                {
                    var rootFolder = @"D:\Files";
                    var fileName = files[0].FileName;
                    var filePath = Path.Combine(rootFolder, fileName);
                    

                    if (files[0].Length > 0)
                    {
                        var path = filePath;
                        if (!Directory.Exists(rootFolder))
                        {
                            Directory.CreateDirectory(rootFolder);

                            using (var stream = new FileStream(path, FileMode.Create))
                            {

                                await files[0].CopyToAsync(stream);
                            }
                        }

                        using (var stream = new FileStream(path, FileMode.Create))
                        {

                            await files[0].CopyToAsync(stream);
                        }

                    }


                    var entity = new AttachmentEntity 
                    {  
                        AttachmentNo = snowFlakeId,
                        OriginalName = fileName,
                        InternalName = $"{snowFlakeId}-file",
                        AttachType = ExtensionNameFilter(fileExtension),
                        Url   = filePath,
                        Cdate = date,
                        Ctime = time,
                        State =1
                    };


                var data  = await _attchmentRepository.AddAttachment(entity);


                var attachment = await _attchmentRepository.GetAttachFileByNo(data);
                    response.AttachmentNo = attachment.AttachmentNo;
                    response.Url = attachment.Url;
                }
                else
                {
                    return new BasicResponse<AttachmentResponse>() { code = 9999, desc = "please check file formate",data=null };
                }
                

                return new BasicResponse<AttachmentResponse> { code = 1111, desc = "isnsert success", data = response };



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async  ValueTask<BasicResponse<AttachmentResponse>> UpLoadFiles(IFormFileCollection files)
        {
            if (files.Count == 0||files==null) 
                return new BasicResponse<AttachmentResponse>() { code = 9999, desc = "with out any file", data = null };            

            var response = new AttachmentResponse();
            var datetime = DateTimeOffset.UtcNow;
            var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
            var time = datetime.ToUnixTimeSeconds();
            var snowFlakeId = _generate.GetId();
            var UrlStr = new StringBuilder();
           
            try
            {
                foreach (var file in files)
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    
                    if (new[] { "gif", "jpg", "jpeg", "png", "doc", "docx" }.Contains(fileExtension))
                    {
                        var rootFolder = @"D:\Files";
                        var fileName = file.FileName;
                        var filePath = Path.Combine(rootFolder, fileName);
                        
                        var path = $@"{rootFolder}\{file.FileName}";
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        UrlStr.Append($",{filePath}");
                    }
                    else
                    {
                        return new BasicResponse<AttachmentResponse>() { code = 9999, desc = "please check file formate", data = null };
                    }

                }

                var entity = new AttachmentEntity
                {
                    AttachmentNo = snowFlakeId,
                    OriginalName = "mixedList",
                    AttachType = ExtensionNameFilter(string.Empty),
                    Url = UrlStr.ToString(),
                    Cdate = date,
                    Ctime = time,
                    State = 1
                };


                var data = await _attchmentRepository.AddAttachment(entity);


                var attachment = await _attchmentRepository.GetAttachFileByNo(data);
                response.AttachmentNo = attachment.AttachmentNo;
                response.Url = attachment.Url;


                return new BasicResponse<AttachmentResponse> { code = 1111, desc = "isnsert success", data = response };



            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        internal int ExtensionNameFilter(string extensionname)
        {
            
            switch(extensionname)
            {
                case "gif":
                    return Convert.ToInt32(AttachType.pic);

                case "jpg":
                    return Convert.ToInt32(AttachType.pic);

                case "jpeg":
                    return Convert.ToInt32(AttachType.pic);

                case "png":
                    return Convert.ToInt32(AttachType.pic);

                case "doc":
                    return Convert.ToInt32(AttachType.doc);
               case "docx":
                    return Convert.ToInt32(AttachType.doc);
               default:
                    return 2;
            }
        }

        public enum AttachType 
        { pic,
          doc,
          mixed
        }
    }
}
