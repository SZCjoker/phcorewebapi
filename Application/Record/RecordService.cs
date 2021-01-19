using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.Application.FileUpload;
using PHCoreWebAPI.Application.Record.Contract;
using PHCoreWebAPI.DAL.Repository.Attachment;
using PHCoreWebAPI.DAL.Repository.Record;
using PHCoreWebAPI.DAL.Repository.Record.Entity;
using PHCoreWebAPI.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Record
{
    public class RecordService : IRecordService
    {


        private readonly IRecordRepository _repository;
        private readonly IAttachmentRepository _attachment;
        
        private readonly IGenerateId _generate;

        public RecordService(IRecordRepository repository
                            , IAttachmentRepository attachment
                            , IGenerateId generate
                            , IFileUploadService uploadService)
        {
            _repository = repository;
            _attachment = attachment;
            _generate = generate;
        }





        public async ValueTask<BasicResponse> AddRecord(RecordRequest request)
        {
            Int64 uploadID = 0;
            var datetime = DateTimeOffset.UtcNow;
            var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
            var time = datetime.ToUnixTimeSeconds();
            
            var entity = new RecordEntity
            {
                RecordNo = _generate.GetId(),
                DeptID = request.DeptID,
                AttachmentNo = uploadID,
                ClientID = request.ClientID,
                Content = request.Content,
                Cdate = date,
                Ctime = time,
                State = 1
            };
            var data = await _repository.CreateRecord(entity);
            var result = StateCodeHandler.ForCount(data);
            return result;
        }

        public async ValueTask<BasicResponse<RecordResponse>> GetRecordByNo(long recordno)
        {
            var data = await _repository.GetRecordByNo(recordno);


            var result = new RecordResponse
            {
                RecordNo = data.RecordNo,
                Cdate = data.Cdate,
                Ctime = data.Ctime,
                ClientID = data.ClientID,
                AttachmentNo = data.AttachmentNo,
                DeptID = data.DeptID,
                State = data.State
            };
            return new BasicResponse<RecordResponse>() { code = (int)HttpStatusCode.OK, desc = "success", data = result };
        }

        public async ValueTask<BasicResponse<IEnumerable<RecordResponse>>> GetRecordList()
        {
            var data = await _repository.GetAllRecord();
            var result = data.Select(d => new RecordResponse
            {
                RecordNo = d.RecordNo,
                Cdate = d.Cdate,
                Ctime = d.Ctime,
                ClientID = d.ClientID,
                AttachmentNo = d.AttachmentNo,
                DeptID = d.DeptID,
                State = d.State
            }).AsQueryable();
            return new BasicResponse<IEnumerable<RecordResponse>>() { code = (int)HttpStatusCode.OK, desc = "success", data = result };
        }

        public async ValueTask<BasicResponse<IEnumerable<RecordResponse>>> GetRecordListByDept(int deptid)
        {
            var data = await _repository.GetAllRecordByDept(deptid);
            var result = data.Select(d => new RecordResponse
            {
                RecordNo = d.RecordNo,
                Cdate = d.Cdate,
                Ctime = d.Ctime,
                ClientID = d.ClientID,
                AttachmentNo = d.AttachmentNo,
                DeptID = d.DeptID,
                State = d.State
            }).AsQueryable();
            return new BasicResponse<IEnumerable<RecordResponse>>() { code = (int)HttpStatusCode.OK, desc = "success", data = result };
        }

        public async ValueTask<PagingResponse<IEnumerable<RecordResponse>>> QueryBycondition(QueryCondition condition)
        {
            string queryStr = string.Empty;

            var paging = new Paging(condition.pageoffset, condition.pagesize);
            condition.pageoffset = paging.Offset;
            
            if (!string.IsNullOrEmpty(condition.ClientID)) queryStr += $" AND ClientID like '%{condition.ClientID}%'";
            if (!string.IsNullOrEmpty(condition.BeginDate))
                queryStr += $" AND  Ctime > = {StringToUtcSeconds(condition.BeginDate)}";
            if (!string.IsNullOrEmpty(condition.EndDate))
                queryStr += $" AND  Ctime < = {StringToUtcSeconds(condition.EndDate)}";   

            var requestdata = new RecordEntity
            {
                RecordNo = condition.RecordNo
            };

            var data = await _repository.QueryByCondition(requestdata, queryStr, paging.Offset, paging.PageSize);
            var total = data.Count();
            paging.RowsTotal = total;
            var result = data.Select(d => new RecordResponse
            {
                RecordNo = d.RecordNo,
                DeptID = d.DeptID,
                ClientID = d.ClientID,
                AttachmentNo = d.AttachmentNo,
                Cdate = d.Cdate,
                Ctime = d.Ctime,
                Udate = d.Udate,
                Utime = d.Utime,
                State = d.State
            });

            var coUnt = data.Count();
            return StateCodeHandler.ForPagingCount(coUnt, result, paging);
        }
        internal Int64 StringToUtcSeconds(string dateStr)
        {   //if use html datepicker didn't need this . must be yyyy-mm-dd or yyyy/mm/dd 
            //var adjustStr = dateStr.Insert(4, "/").Insert(7, "/"); 
            var offsetDate = DateTimeOffset.Parse(dateStr).ToUnixTimeSeconds();
            //DateTime NewDate = DateTime.ParseExact(dateStr, "yyyyMMdd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces);
            return offsetDate;
        }
        public async ValueTask<BasicResponse> RecordStateChange(RecordRequest request)
        {
            var datetime = DateTimeOffset.UtcNow;
            var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
            var time = datetime.ToUnixTimeSeconds();
            //attachement 
            var entity = new RecordEntity
            {
                RecordNo = _generate.GetId(),
                Cdate = date,
                Ctime = time,
                State = 1
            };

            var data = await _repository.UpdateRecord(entity);
            var result = StateCodeHandler.ForCount(data);
            return result;
        }

        public async ValueTask<BasicResponse> UpdateRecord(RecordRequest request)
        {
            var datetime = DateTimeOffset.UtcNow;
            var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
            var time = datetime.ToUnixTimeSeconds();
            //attachement 
            var entity = new RecordEntity
            {
                RecordNo = request.RecordNo,
                DeptID = request.DeptID,
                ClientID = request.ClientID,
                Content = request.Content,
                Udate = date,
                Utime = time,
                State = request.State
            };
            var data = await _repository.UpdateRecord(entity);
            var result = StateCodeHandler.ForCount(data);
            return result;
        }
    }
}
