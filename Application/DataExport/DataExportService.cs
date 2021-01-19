using PHCoreWebAPI.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PHCoreWebAPI.Application.Issue.Contract;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using PHCoreWebAPI.Application.DataExport.Contract;
using PHCoreWebAPI.DAL.Repository.Issue;
using PHCoreWebAPI.DAL.Repository.Attachment;
using OfficeOpenXml.Drawing;
using System.Drawing.Imaging;
using System.Net;
using PHCoreWebAPI.DAL.Repository.Issue.Entity;

namespace PHCoreWebAPI.Application.DataExport
{
    public class DataExportService : IExportService
    {


        private readonly IIssueRepository _issueRepository;
        private readonly IAttachmentRepository _attachmentRepository;

        public DataExportService(IIssueRepository issueRepository,
                                 IAttachmentRepository attachmentRepository)
        {
            _issueRepository = issueRepository;
            _attachmentRepository = attachmentRepository;
        }


        /// <summary>
        /// 拷貝EXCEL範本
        /// </summary>
        /// <param name="source">需要的範本名稱不含附檔名 ex . 2210;response;loginfailed </param>
        /// <param name="target">目標路徑 ex .目前先寫死 </param>
        /// <returns></returns>
        public DataInfo CopyTemplate(string source, string target)
        {
            var datetime = DateTimeOffset.UtcNow;
            var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
            var root = $@"D:\Files"; //target
            
            root = target == null ? root : target;
            var DataInfo = new DataInfo();
            
            //get data amount then decied how many row should be draw in empty .


            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            try
            {
                var currentFolder = Directory.GetCurrentDirectory();
                //source 
                //string sourceExeclFileName = $@"{currentFolder}\Excel\2210.xlsx"; // add condition
                string sourceExeclFileName = $@"{currentFolder}\Excel\{source}.xlsx";
                //target
                string targetExeclFileName = $@"{root}\{date}-{source}.xlsx";


                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                if (File.Exists(targetExeclFileName))
                {
                    File.Delete(targetExeclFileName);
                }

                using (ExcelPackage package = new ExcelPackage(new FileInfo(targetExeclFileName)))
                {
                    using (ExcelPackage sourcePackage = new ExcelPackage(new FileInfo(sourceExeclFileName)))
                    {
                        foreach (var sheet in sourcePackage.Workbook.Worksheets)
                        {
                            //new sheet form copy   SheetName
                            package.Workbook.Worksheets.Add(sheet.Name, sheet);
                            // can added more 
                        }
                    }
                    package.Save();

                    DataInfo.Folder = root;
                    DataInfo.FileName = $@"\{date}-{source}.xlsx";


                    return DataInfo;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }


        }

        public async ValueTask<IEnumerable<dynamic>> GetData(string Datasource,PagingRequest request)
        {

            var paging = new Paging(request.PageOffset, request.PageSize);
            request.PageOffset = paging.Offset;

            var datetime = DateTimeOffset.UtcNow;
            var Bdate = datetime.AddDays(-3).ToString("yyyy/MM/dd HH:mm:ss");
            var Edate = datetime.ToString("yyyy/MM/dd HH:mm:ss");
            if (string.IsNullOrEmpty(request.BeginDate)) request.BeginDate = Bdate;
            if (string.IsNullOrEmpty(request.EndDate)) request.EndDate = Edate;

            var entity = new IssueEntity
            {
                BeginDate = StringToUtcSeconds(request.BeginDate),
                EndDate = StringToUtcSeconds(request.EndDate),
                PageOffset = request.PageOffset,
                PageSize = request.PageSize
            };

            switch ( Datasource )
            {
                case "2210":

                   
                  var (Ten2Tendata,Ten2Tentotal) = await  _issueRepository.
                        GetIssuePm10toAm10(entity);
                  var Ten2Tenresult = Ten2Tendata.Select(d => new {
                          IssueNo = d.IssueNo,
                          CallTime = Date(d.CallTime)[10],//致電時間
                          EntranceType = EntranceMap(d.EntranceType),
                          IssueDescription = d.IssueDescription,
                          ClientID = d.ClientID,
                          CreateTime = Date(d?.CreateTime)[10], //提交時間
                          Purpose = d.Purpose,
                          AskingTime = Date(d.AskingTime)[10],//要求反饋時間
                          AnswerStatus = AnswerStatusMap(d.AnswerStatus),
                          Content = d.Content,
                          ResponseResult =d.ResponseResult,
                          Suggestion = d.Suggestion,
                          Solve = SolveMap(d.Solve),
                          EserviceID = d.EserviceID
                      });
                    return Ten2Tenresult;
                  
                  
                case "response":
                   var  (Responsedata,Responsetotal) = await _issueRepository
                        .GetAllIssue(entity);
                   var   AttachData = await _attachmentRepository.GetAttachmentList();
                   var   Responseresult = Responsedata.Select( d =>  new  {
                                   CreateDate = Date(d.CreateTime)[0],
                                   Createtime = Timeshift(d.CreateTime)[0],
                                   ClientID = d.ClientID ,
                                   EntranceType = EntranceMap(d.EntranceType),
                                   Device = d.Device,
                                   IssueCateID = d.IssueCateID,
                                   Response = d.Response,
                                   AttachmentUrl =  AttachData.Where(s=>s.AttachmentNo==d.AttachmentNo)?.FirstOrDefault()?.Url ?? string.Empty,
                                   TestResult = d.TestResult,
                                   ProcessStatus = ProcessMap(d.ProcessStatus),
                                   Reason = d.Reason,
                                   ResponseTime = Timeshift(d.ResponseTime)[0],
                                   ResponseResult = d.ResponseResult,
                                   EserviceID = d.EserviceID,
                                   DeptID = d.DeptID, 
                                   Recorder = d.Recorder
                      }).OrderBy(d=>d.IssueCateID);
                   

                   return Responseresult;
                   
                
                case "loginfailed":
                    var (LoginFaileddata,LoginFailedtotal)= await _issueRepository.
                        GetLoginIssue(entity);
                    var LoginFailedresult = LoginFaileddata.Select(d => new {
                        IssueNo = d.IssueNo,
                        CallDate = Date(d.CallTime)[0],
                        EntranceType = EntranceMap(d.EntranceType),
                        IssueDescription = d.IssueDescription,
                        ClientID = d.ClientID,
                        CreateDate = Date(d.CreateTime)[0],
                        CreateTime = Timeshift(d?.CreateTime)[0],
                        Purpose = d.Purpose,
                        AskingTime = Timeshift(d.AskingTime)[0],
                        AnswerStatus = AnswerStatusMap(d.AnswerStatus),
                        Content = d.Content,
                        ResponseResult = d.ResponseResult,
                        Suggestion = d.Suggestion,
                        Solve = SolveMap(d.Solve),
                        EserviceID = d.EserviceID
                    }).ToList();
                    return LoginFailedresult;

                default:
                    return null; 

            }
          
        }
               

        public async ValueTask<BasicResponse<DataInfo>> Print(DataCondition condition)
        {
            var datetime = DateTimeOffset.UtcNow;
            var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
            var time = datetime.ToUnixTimeSeconds();

            var dataInfo = new DataInfo();
            var copyResult = CopyTemplate(condition.Source, condition.Target);
            var request = new PagingRequest 
            {
                BeginDate = condition.BeginDate,
                EndDate = condition.EndDate,
                PageOffset = 1,
                PageSize = 1000
            };
            var datas = await GetData(condition.Source,request);
            //response

            try
            {
                switch (condition.Source )
                {
                    case  "response":
                      using (ExcelPackage package = new ExcelPackage(new FileInfo(copyResult.Folder + copyResult.FileName)))
                    {
                        //ExcelWorksheet ws = package.Workbook.Worksheets[0]; // binding with copied model.

                        ExcelWorkbook wb = package.Workbook;

                        int worksheet = 1;
                        foreach (var ws in wb.Worksheets)
                        {

                            int colIndex = 1;
                            int rowIndex = 1;

                            foreach (var data in datas) // Adding Data into rows
                            {
                                if ((int)data.IssueCateID == worksheet)
                                {
                                    Type t = data.GetType();
                                    PropertyInfo[] props = t.GetProperties();

                                    colIndex = 1;
                                    rowIndex++;
                                    foreach (var prop in props)
                                    {
                                        var cell = ws.Cells[rowIndex, colIndex];
                                        //Setting Value in cell
                                        cell.Value = prop.GetValue(data)?.ToString();
                                        if (cell.Value != null && cell.Value.ToString().Contains("\\") == true)
                                        {
                                            if (!cell.Value.ToString().Contains(".png"))
                                            {
                                                var pic = await ws.Drawings.AddPictureAsync(string.Empty, SetPic(cell));

                                                cell.Value = string.Empty;
                                                pic.SetPosition(rowIndex - 1, 0, colIndex - 1, 0);
                                                pic.SetSize(100, 90);
                                                pic.Dispose();
                                            }
                                            else
                                            {
                                                
                                                var file = SetPicWithStream(cell);
                                                var png = await ws.Drawings.AddPictureAsync(string.Empty, file);

                                                cell.Value = string.Empty;
                                                png.SetPosition(rowIndex - 1, 0, colIndex - 1, 0);
                                                png.SetSize(100, 90);
                                                png.Dispose();
                                            }
                                            

                                        }
                                        colIndex++;
                                    }
                                }
                            }
                            worksheet++;
                        }
                        package.Save();

                        //Generate A File with Random name
                        //Byte[] bin = package.GetAsByteArray();
                        //string path = @$"D:\Files";
                        //string fileName = @$"{date.ToString()}.xlsx";
                        //File.WriteAllBytes($@"{path}\{fileName}", bin);

                    }
                      dataInfo.Folder = copyResult.Folder;
                      dataInfo.FileName = copyResult.FileName;
                        break;

                    case "2210":
                      using (ExcelPackage package = new ExcelPackage(new FileInfo(copyResult.Folder + copyResult.FileName)))
                            {
                                //ExcelWorksheet ws = package.Workbook.Worksheets[0]; // binding with copied model.

                                ExcelWorkbook wb = package.Workbook;

                                int worksheet = 1;
                                foreach (var ws in wb.Worksheets)
                                {
                                    int colIndex = 1;
                                    int rowIndex = 1;
                                    foreach (var data in datas) // Adding Data into rows
                                    {
                                        Type t = data.GetType();
                                        PropertyInfo[] props = t.GetProperties();

                                        colIndex = 1;
                                        rowIndex++;
                                        foreach (var prop in props)
                                        {
                                            var cell = ws.Cells[rowIndex, colIndex];
                                            //Setting Value in cell
                                            cell.Value = prop.GetValue(data)?.ToString();
                                            colIndex++;
                                        }
                                    }
                                    worksheet++;
                                }
                                package.Save();
                            }
                      dataInfo.Folder = copyResult.Folder;
                      dataInfo.FileName = copyResult.FileName;
                        break;

                    case "loginfailed":
                      using (ExcelPackage package = new ExcelPackage(new FileInfo(copyResult.Folder + copyResult.FileName)))
                        {
                            ExcelWorkbook wb = package.Workbook;
                            int worksheet = 1;
                            foreach (var ws in wb.Worksheets)
                            {
                                int colIndex = 1;
                                int rowIndex = 1;
                                foreach (var data in datas) // Adding Data into rows
                                {
                                    Type t = data.GetType();
                                    PropertyInfo[] props = t.GetProperties();

                                    colIndex = 1;
                                    rowIndex++;
                                    foreach (var prop in props)
                                    {
                                        var cell = ws.Cells[rowIndex, colIndex];
                                        //Setting Value in cell
                                        cell.Value = prop.GetValue(data)?.ToString();
                                        colIndex++;
                                    }
                                }
                                worksheet++;
                            }
                            package.Save();
                        }
                      dataInfo.Folder = copyResult.Folder;
                      dataInfo.FileName = copyResult.FileName;
                        break;
                }

                return new BasicResponse<DataInfo>() { code = (int)HttpStatusCode.OK, desc = "success", data = dataInfo };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        
        internal string[] Timeshift(long? usecond)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0,DateTimeKind.Utc);

            var result = UnixEpoch.AddSeconds((double)usecond)
                .AddHours(8).GetDateTimeFormats('T');
            return result;
        }

        internal string[] Date(long? usecond)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            var result = UnixEpoch.AddSeconds((double)usecond)
                .AddHours(8).GetDateTimeFormats('g');
            return result;
        }

        internal Int64 StringToUtcSeconds(string dateStr)
        {  
         var offsetDate = DateTimeOffset.Parse(dateStr).ToUnixTimeSeconds();
            return offsetDate;
        }

        internal FileInfo SetPic(ExcelRangeBase cell)
        {
            var path = cell.Value.ToString().Contains("\\") ? cell.Value.ToString() : null;
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo;
        }

        internal FileInfo SetPicWithStream(ExcelRangeBase cell)
        {
            var path = cell.Value.ToString().Contains("\\") ? cell.Value.ToString() : null;
            FileInfo file = new FileInfo(path);
            
            //using (FileStream fs = file.OpenRead())
            //{
            //    int filelength = 0;
            //    filelength = (int)fs.Length; //獲得檔長度
            //    Byte[] image = new Byte[filelength]; //建立一個位元組陣列
            //    fs.Read(image, 0, filelength); //按位元組流讀取
            //    fs.Seek(0, SeekOrigin.Begin);

            //    Image img = Image.FromStream(fs);
            //    //Image img = Image.FromFile(file.FullName,false);
            //    img.Save(file.Name, ImageFormat.Jpeg);
            //    fs.Close();
            //}
            return file; 
        }

        internal string EntranceMap(int etype)
        { string entrance = string.Empty;
            //1 PC 2全站 3體育 4H5
            switch (etype)
            {
                case 1:
                    entrance = "PC";
                    break;
                case 2:
                    entrance = "全站";
                    break;
                case 3:
                    entrance = "體育";
                    break;
                case 4:
                    entrance = "H5";
                    break;
                default :
                    break;
            }
            return entrance;
        }
        internal string ProcessMap(int processtype)
        {
            //1新建 2處理中 9結案
            string process = string.Empty;
           
            switch (processtype)
            {
                case 1:
                    process = "新建";
                    break;
                case 2:
                    process = "處理中";
                    break;
                case 3:
                    process = "結案";
                    break;
                default:
                    break;
            }
            return process;

        }
        internal string SolveMap(int solvetype)
        {
            //1新建 2處理中 9結案
            string solve = string.Empty;

            switch (solvetype)
            {
                case 1:
                    solve = "新建";
                    break;
                case 2:
                    solve = "處理中";
                    break;
                case 3:
                    solve = "結案";
                    break;
                default:
                    break;
            }
            return solve;

        }        
        internal string AnswerStatusMap(int type)
        {
           
            string Answer = string.Empty;

            switch (type)
            {
                case 1:
                    Answer = "接通";
                    break;
                case 2:
                    Answer = "資料有誤";
                    break;
                case 3:
                    Answer = "無接通";
                    break;
                default:
                    break;
            }
            return Answer;

        }

    }
}
