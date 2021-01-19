using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.Application.FileUpload;
using PHCoreWebAPI.Application.Issue.Contract;
using PHCoreWebAPI.DAL.Repository.Attachment;
using PHCoreWebAPI.DAL.Repository.Issue;
using PHCoreWebAPI.DAL.Repository.Issue.Entity;
using PHCoreWebAPI.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Issue
{
    public class IssueService : IissueService
    {
        private readonly IIssueRepository _repository;
        private readonly IAttachmentRepository _attachment;
        private readonly IFileUploadService _uploadService;
        private readonly IGenerateId _generate;

        public IssueService(IIssueRepository repository
                            ,IAttachmentRepository attachment
                            ,IGenerateId generate
                            ,IFileUploadService uploadService)
        {
            _repository = repository;
            _attachment = attachment;
            _generate = generate;
            _uploadService = uploadService;
        }

        public async ValueTask<BasicResponse> AddIssue(IssueRequest request)
        {
            
            var datetime = DateTimeOffset.UtcNow;
            var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
            var time = datetime.ToUnixTimeSeconds();
            
            var entity = new IssueEntity
            {
                IssueNo = _generate.GetId(),
                DeptID = request.DeptID,
                AttachmentNo = request.AttachmentNo,
                IssueCateID = request.IssueCateID,
                IssueDescription = request.IssueDescription,
                EntranceType = request.EntranceType,                
                ClientID = request.ClientID,
                Device = request.Device,
                EserviceID = request.EserviceID,
                Recorder = request.Recorder,
                Content = request.Content,
                Reason = request.Reason,
                ResponseResult = request.ResponseResult,
                TestResult = request.TestResult,
                CreateTime = StringToUtcSeconds(request.CreateTime),                
                ProcessStatus = 1
            };
            var data = await _repository.CreateIssue(entity);
            var result = StateCodeHandler.ForCount(data);
            return result;
        }
        public async ValueTask<PagingResponse<IEnumerable<IssueResponse>>> QueryBycondition(QueryCondition condition)
        {
            string queryStr = string.Empty;
            var datetime = DateTimeOffset.UtcNow;
            var Bdate = datetime.AddDays(-3).ToString("yyyy/MM/dd HH:mm:ss");
            var Edate = datetime.ToString("yyyy/MM/dd HH:mm:ss");

            var paging = new Paging(condition.PageOffset, condition.PageSize);
            condition.PageOffset = paging.Offset;
            if (condition.DeptID != 0) queryStr += $" AND deptid = {condition.DeptID}";
            if (condition.ProcessStatus != 0) queryStr += $" AND ProcessStatus = {condition.ProcessStatus}";
            if (condition.EntranceType != 0) queryStr += $" AND EntranceType = {condition.EntranceType}";
            if (condition.IssueCateID != 0) queryStr += $" AND IssueCateID = {condition.IssueCateID}";
            if (!string.IsNullOrEmpty(condition.ClientID)) queryStr += $" AND ClientID like '%{condition.ClientID}%'";
            if (!string.IsNullOrEmpty(condition.Device)) queryStr += $" AND Device like '%{condition.Device}%'";
            if (!string.IsNullOrEmpty(condition.EserviceID)) queryStr += $" AND EserviceID like '%{condition.EserviceID}%'";
            if (!string.IsNullOrEmpty(condition.Recorder)) queryStr += $" AND Recorder like '%{condition.Recorder}%'";

            if (!string.IsNullOrEmpty(condition.BeginDate))
                queryStr += $" AND  Createtime > = {StringToUtcSeconds(condition.BeginDate)}";
            if (!string.IsNullOrEmpty(condition.EndDate))
                queryStr += $" AND  Createtime < = {StringToUtcSeconds(condition.EndDate)}";
            if (!string.IsNullOrEmpty(condition.ResponseTime))
                queryStr += $" AND  ResponseTime <= {StringToUtcSeconds(condition.ResponseTime)}";



            var requestdata = new IssueEntity
            {
                IssueNo = condition.IssueNo,
                 //BeginDate = StringToUtcSeconds(condition.BeginDate??Bdate),
                //EndDate = StringToUtcSeconds(condition.EndDate??Edate),
                PageOffset = paging.Offset,
                PageSize = paging.PageSize
            };

            var (rows,total) = await _repository.QueryByCondition(requestdata, queryStr);
            paging.RowsTotal = total;
            paging.RowsTotal = total;
            var result = rows.Select(d => new IssueResponse
            {
                IssueNo = d.IssueNo,
                DeptID = d.DeptID,
                AttachmentNo = d.AttachmentNo,
                IssueCateID = d.IssueCateID,
                IssueDescription = d.IssueDescription,
                ClientID = d.ClientID,
                Device = d.Device,
                EserviceID = d.EserviceID,
                Recorder = d.Recorder,
                Content = d.Content,
                CreateTime = d.CreateTime,
                ProcessStatus = d.ProcessStatus,
                AnswerStatus = d.AnswerStatus,
                AskingTime = d.AskingTime,
                CallTime = d.CallTime,
                EntranceType = d.EntranceType,
                Purpose = d.Purpose,
                Reason = d.Reason,
                ResponseResult = d.ResponseResult,
                ResponseTime = d.ResponseTime,
                Solve = d.Solve,
                TestResult = d.TestResult,
                Utime = d.Utime
            }) ?? null;

            var coUnt = result.Count();
            return new PagingResponse<IEnumerable<IssueResponse>>() { code = (int)HttpStatusCode.OK, desc = "success", data = result, paging = paging };
        }

        public async ValueTask<PagingResponse<IEnumerable<IssueResponse>>> GetIssuePm10toAm10(PagingRequest request)
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
            var (rows,total) = await _repository
                .GetIssuePm10toAm10(entity);
            paging.RowsTotal = total;
            var result = rows.Select(d => new IssueResponse
            {
                IssueNo = d.IssueNo,
                ProcessStatus = d.ProcessStatus,
                IssueDescription = d.IssueDescription,
                Purpose = d.Purpose,
                Solve = d.Solve,
                EntranceType = d.EntranceType,
                IssueRecord = d.IssueRecord,
                CreateTime = d?.CreateTime,
                ClientID = d.ClientID,
                Suggestion = d.Suggestion,
                EserviceID = d.EserviceID
            }).AsQueryable();

            return new PagingResponse<IEnumerable<IssueResponse>>() { code = (int)HttpStatusCode.OK, desc = "success", data = result,paging=paging };

        }

        public async ValueTask<PagingResponse<IEnumerable<IssueResponse>>> GetLoginIssue(PagingRequest request)
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

            var (rows,total) = await _repository
                .GetLoginIssue(entity);
            paging.RowsTotal = total;
            var result = rows.Select(d => new IssueResponse
            {
                IssueNo = d.IssueNo,
                ProcessStatus = d.ProcessStatus,
                IssueDescription = d.IssueDescription,
                EntranceType = d.EntranceType,
                CreateTime = d?.CreateTime,
                ClientID = d.ClientID,
                Suggestion = d.Suggestion
            }).AsQueryable();

            return new PagingResponse<IEnumerable<IssueResponse>>() { code = (int)HttpStatusCode.OK, desc = "success", data = result,paging=paging };

        }
        public async ValueTask<PagingResponse<IEnumerable<IssueResponse>>> GetIssueList(PagingRequest request)
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
            var AttachData = await _attachment.GetAttachmentList();
            var (rows,total) = await _repository
                .GetAllIssue(entity);
            paging.RowsTotal = total;
            var result = rows.Select(d => new IssueResponse
            {   IssueNo = d.IssueNo,
                CreateTime = d.CreateTime,
                ClientID = d.ClientID,
                EntranceType = d.EntranceType,
                IssueCateID = d.IssueCateID,
                IssueDescription = d.IssueDescription,
                AttachmentUrl = AttachData.Where(s => s.AttachmentNo == d.AttachmentNo)?.FirstOrDefault()?.Url ?? string.Empty,
                ResponseResult = d.ResponseResult,
                ProcessStatus = d.ProcessStatus,
                Reason = d.Reason,
                ResponseTime = d.ResponseTime,
                EserviceID = d.EserviceID,
                DeptID = d.DeptID,
                Suggestion = d.Suggestion,
                Recorder = d.Recorder
            }).AsQueryable();

            return new PagingResponse<IEnumerable<IssueResponse>>() {code= (int)HttpStatusCode.OK,desc="success",data=result,paging=paging };
        }

        public async ValueTask<PagingResponse<IEnumerable<IssueResponse>>> GetIssueListByDept(int deptid, PagingRequest request)
        {
            var paging = new Paging(request.PageOffset, request.PageSize);
            request.PageOffset = paging.Offset;

            var (rows,total) = await _repository.GetAllIssueByDept(deptid);
            var result = rows.Select(d => new IssueResponse
            {   IssueNo = d.IssueNo,
                DeptID = d.DeptID,
                AttachmentNo = d.AttachmentNo,
                IssueCateID = d.IssueCateID,
                IssueDescription = d.IssueDescription,
                ClientID = d.ClientID,
                Device = d.Device,
                EserviceID = d.EserviceID,
                Recorder = d.Recorder,
                Content = d.Content,
                CreateTime = d?.CreateTime,
                ProcessStatus = d.ProcessStatus
            }).AsQueryable();
            paging.RowsTotal = total;
            return new PagingResponse<IEnumerable<IssueResponse>>() { code = (int)HttpStatusCode.OK, desc = "success", data = result,paging=paging };
        }       

        public async ValueTask<BasicResponse> IssueStateChange(IssueRequest request)
        {

            var datetime = DateTimeOffset.UtcNow;
            var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
            var time = datetime.ToUnixTimeSeconds();
            //attachement 
            var entity = new IssueEntity
            {   
                IssueNo = request.IssueNo,
                Utime = time,
                ProcessStatus = request.ProcessStatus
            };

            var attResult = await _attachment.DeleteAttachment(request.IssueNo,request.ProcessStatus);

            if (attResult == 0) return new BasicResponse() { code = 9999, desc = "attchment delete failed" };

            var data = await _repository.UpdateIssue(entity);
            var result = StateCodeHandler.ForCount(data);
            return result;
        }

        public async ValueTask<BasicResponse> UpdateIssue(IssueRequest request)
        {
            if (request.IssueNo == 0) return new BasicResponse() { code = 9999, desc = "without parameter"};
            var datetime = DateTimeOffset.UtcNow;
            var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
            var time = datetime.ToUnixTimeSeconds();
            //attachement 
            var entity = new IssueEntity
            { IssueNo = request.IssueNo,
                DeptID = request.DeptID,
                IssueCateID = request.IssueCateID,
                IssueDescription = request.IssueDescription??null,
                IssueRecord = request.IssueRecord??null,
                ClientID = request.ClientID??null,
                Device = request.Device??null,
                EserviceID = request.EserviceID??null,
                Recorder = request.Recorder??null,
                Content = request.Content??null,
                TestResult = request.TestResult??null,
                Utime = time,
                CallTime = StringToUtcSeconds(request.CallTime),
                AskingTime = StringToUtcSeconds(request.AskingTime),
                EntranceType = request.EntranceType,
                ProcessStatus = request.ProcessStatus,
                AnswerStatus = request.AnswerStatus,
                Solve = request.Solve,
                Suggestion =request.Suggestion??null,
                Purpose = request.Purpose??null,
                Reason = request.Reason??null,
                ResponseResult = request.ResponseResult??null,
                ResponseTime = StringToUtcSeconds(request.ResponseTime)
            };
            var data = await _repository.UpdateIssue(entity);
            var result = StateCodeHandler.ForCount(data);
            return result;
        }

        public async ValueTask<BasicResponse<IssueResponse>> GetIssueByNo(long recordno)
        {
            var data = await _repository.GetIssueByNo(recordno);


            var result = new IssueResponse 
            {
                IssueNo = data.IssueNo,
                DeptID = data.DeptID,
                AttachmentNo = data.AttachmentNo,
                IssueCateID = data.IssueCateID,
                IssueDescription = data.IssueDescription,
                Reason = data.Reason,
                ResponseResult = data.ResponseResult,
                ResponseTime = data.ResponseTime,
                ClientID = data.ClientID,
                Device = data.Device,
                EserviceID = data.EserviceID,
                Recorder = data.Recorder,
                Content = data.Content,
                EntranceType = data.EntranceType,
                CreateTime = data?.CreateTime,
                CallTime = data.CallTime,
                AskingTime = data .AskingTime,
                ProcessStatus = data.ProcessStatus,
                TestResult = data.TestResult,
                Purpose = data.Purpose,
                Suggestion = data.Suggestion
            };

            return new BasicResponse<IssueResponse>() { code = (int)HttpStatusCode.OK, desc = "success", data = result };

        }
        internal Int64 StringToUtcSeconds(string dateStr)
        { 
            Int64  offsetDate = 0;
           if(!string.IsNullOrEmpty(dateStr))
            {
                offsetDate = DateTimeOffset.Parse(dateStr)
                               .ToUnixTimeSeconds();
                return offsetDate;

            }
            return offsetDate;
        }        

    }

}
