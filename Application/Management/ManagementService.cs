using Microsoft.Extensions.Configuration;
using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.Application.Setting.Contract;
using PHCoreWebAPI.DAL.Repository.Department;
using PHCoreWebAPI.DAL.Repository.IssueCate;
using PHCoreWebAPI.DAL.Repository.RecordCate;
using PHCoreWebAPI.DAL.Repository.RecordCate.Entity;
using PHCoreWebAPI.DAL.Repository.Setting;
using PHCoreWebAPI.DAL.Repository.Setting.Entity;
using PHCoreWebAPI.Models.DAL.Department.Entity;
using PHCoreWebAPI.Models.DAL.Repository.IssueCate.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Setting
{
    public class ManagementService : IManagementService
    {

        private readonly IManagementRepository _repository;
        private readonly IDeptInfoRepository _deptRepository;
        private readonly IIssueCateRepository _issueCateRepository;
        private readonly IRecordCateRepository _recordCateRepository;
        private readonly IConfiguration _configuration;



        public ManagementService(IManagementRepository repository
                                 ,IDeptInfoRepository deptRepository
                                 , IIssueCateRepository issueCateRepository
                                 , IRecordCateRepository recordCateRepository
                                 , IConfiguration configuration )
        {
            _repository = repository;
            _deptRepository = deptRepository;
            _issueCateRepository = issueCateRepository;
            _recordCateRepository = recordCateRepository;
            _configuration = configuration;
        }

        public async ValueTask<BasicResponse> AddDept(ManagementRequest request)
        {
            if(string.IsNullOrEmpty(request.DeptName))
                return   new BasicResponse() { code = 0000, desc = "failed without data"};
            var entity = new DeptInfoEntity 
            { 
             DeptName = request.DeptName,
             State =request.State
            };
            var data = await _deptRepository.AddDept(entity);
            return StateCodeHandler.ForCount(data);
        }

        public async ValueTask<BasicResponse> AddIssueCate(ManagementRequest request)
        {
            if (string.IsNullOrEmpty(request.IssueCateName))
                return new BasicResponse() { code = 0000, desc = "failed without data" };
            var entity = new IssueCateEntity 
            {  
                 IssueCateName = request.IssueCateName,
                 State = 1
            };
            var data = await _issueCateRepository.AddIssueCate(entity);
            return StateCodeHandler.ForCount(data);
        }


        public async ValueTask<BasicResponse> AddRecordCate(ManagementRequest request)
        {
            if (string.IsNullOrEmpty(request.RecordCateName))
                return new BasicResponse() { code = 0000, desc = "failed without data" };
            var entity = new RecordCateEntity
            {
                RecordCateName = request.RecordCateName,
                State = request.State
            };
            var data = await _recordCateRepository.AddRecordCate(entity);
            return StateCodeHandler.ForCount(data);
        }

        public async ValueTask<BasicResponse<IEnumerable<ManagementResponse>>> GetDeptList()
        {
            var data = await _deptRepository.GetAllDepts();

            var result = data.Select(d=>new ManagementResponse 
            {  
                DeptID = d.DeptID, 
                DeptName = d.DeptName,
                State = d.State
            });
            return new BasicResponse<IEnumerable<ManagementResponse>>() { code = 0000, desc = "success", data = result };
            
        }

        public async ValueTask<BasicResponse<IEnumerable<ManagementResponse>>> GetDeptsByDeptID(int deptid)
        {

            var data = await _deptRepository.GetDeptsByDeptID(deptid);

            var result = data.Select(d => new ManagementResponse 
            {  
                DeptID = d.DeptID,
                DeptName = d.DeptName,
                State =d.State
            });
            return new BasicResponse<IEnumerable<ManagementResponse>>() { code = 0000, desc = "success", data = result };
        }

        public async  ValueTask<BasicResponse<IEnumerable<ManagementResponse>>> GetIssueCateList()
        {
            var data = await _issueCateRepository.GetIssueCate();

            var result = data.Select(d=>new ManagementResponse 
            {  
                IssueCateID = d.IssueCateID,
                IssueCateName = d.IssueCateName,
                State =d.State
            });
            return new BasicResponse<IEnumerable<ManagementResponse>>() { code = 0000, desc = "success", data = result };
            
        }

        public async ValueTask<BasicResponse<IEnumerable<ManagementResponse>>> GetIssueCateByDeptID(int deptid)
        {

            var data = await _issueCateRepository.GetIssueCateByDeptID(deptid);

            var result = data.Select(d => new ManagementResponse
            {
                IssueCateID = d.IssueCateID,
                IssueCateName = d.IssueCateName,
                State = d.State
            });
            return new BasicResponse<IEnumerable<ManagementResponse>>() { code = 0000, desc = "success", data = result };
            
        }

        public async ValueTask<BasicResponse<IEnumerable<ManagementResponse>>> GetRecordCateList()
        {
            var data = await _recordCateRepository.GetRecordCate();

            var result = data.Select(d => new ManagementResponse
            {
                RecordCateID = d.RecordCateID,
                IssueCateName = d.RecordCateName,
                State = d.State
            });
            return new BasicResponse<IEnumerable<ManagementResponse>>() { code = 0000, desc = "success", data = result };
        }

        public async ValueTask<BasicResponse<IEnumerable<ManagementResponse>>> GetRecordCateByDeptID(int deptid)
        {
            var data = await _recordCateRepository.GetRecordCateByDeptID(deptid);

            var result = data.Select(d => new ManagementResponse
            {
                RecordCateID = d.RecordCateID,
                RecordCateName = d.RecordCateName,
                State = d.State
            });
            return new BasicResponse<IEnumerable<ManagementResponse>>() { code = 0000, desc = "success", data = result };
        }


        public async ValueTask<BasicResponse> TieUpIssueCateDept(List<ManagementRequest> request)
        {
            var ListEntity = new List<ManagementEntity>();
            try
            {

                foreach (var issue in request)
                {
                    var data = new ManagementEntity
                    {
                        DeptID = issue.DeptID,
                        IssueCateID = issue.IssueCateID
                    };
                    ListEntity.Add(data);
                }


                var result = await _repository.TieUpIssueCateDept(ListEntity);

                return StateCodeHandler.ForCount(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<BasicResponse> TieUpPermissionDept(List<ManagementRequest> request)
        {
            var ListEntity = new List<ManagementEntity>();

            try
            {
                foreach (var issue in request)
                {
                    var data = new ManagementEntity
                    {
                        DeptID = issue.DeptID,
                        PermissionID = issue.PermissionID,
                        Orderby = await _repository.GetOrderby(issue.DeptID) + 1
                    };
                    ListEntity.Add(data);
                }

                var result = await _repository.TieUpPermissionDept(ListEntity);
                return StateCodeHandler.ForCount(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<BasicResponse> TieUpRecordCateDept(List<ManagementRequest> request)
        {
            var ListEntity = new List<ManagementEntity>();
            try
            {

                foreach (var issue in request)
                {
                    var data = new ManagementEntity
                    {
                        DeptID = issue.DeptID,
                        RecordCateID = issue.RecordCateID
                    };
                    ListEntity.Add(data);
                }


                var result = await _repository.TieUpRecordCateDept(ListEntity);

                return StateCodeHandler.ForCount(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async ValueTask<BasicResponse> StopDept(int deptid, int state)
        {
            var data = await _deptRepository.DimissDept(deptid, state);
            return StateCodeHandler.ForCount(data);

        }

        public async ValueTask<BasicResponse> StopIssueCate(int IssueCateid, int state)
        {
            var data = await _issueCateRepository.StopIssueCate(IssueCateid, state);
            return StateCodeHandler.ForCount(data);
        }

        public async ValueTask<BasicResponse> StopRecordCate(int recordcateid, int state)
        {
            var data = await _recordCateRepository.StopRecordCate(recordcateid, state);
            return StateCodeHandler.ForCount(data);
        }

        public async ValueTask<BasicResponse> UpdateDept(ManagementRequest request)
        {

            var entity = new DeptInfoEntity 
            {
             DeptID= request.DeptID,
             DeptName = request.DeptName,
             State =request.State
            };
            var data = await _deptRepository.UpdateDept(entity);
            return StateCodeHandler.ForCount(data);
        }

        public async ValueTask<BasicResponse> UpdateIssueCate(ManagementRequest request)
        {
            var entity = new IssueCateEntity 
            { 
              IssueCateID = request.IssueCateID,
              IssueCateName = request.IssueCateName,
              State = request.State
            };
            var data = await _issueCateRepository.UpdateIssueCate(entity);
            return StateCodeHandler.ForCount(data);
        }

        public async ValueTask<BasicResponse> UpdateRecordCate(ManagementRequest request)
        {
            
                 var entity = new RecordCateEntity
                 {
                     RecordCateID = request.RecordCateID,
                     RecordCateName = request.RecordCateName,
                     State = request.State
                 };
            var data = await _recordCateRepository.UpdateRecordCate(entity);
            return StateCodeHandler.ForCount(data);
        }

        public async ValueTask<BasicResponse<string>> GetVersion()
        {
            var data = _configuration.GetValue<string>("Version");

            return new BasicResponse<string>() { code = 0000, desc = "success", data = data };
        }

        public async ValueTask<BasicResponse<string>> GetEnvironment()
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return new BasicResponse<string>() { code = 0000, desc = "Environment is :", data = env };
        }
    }
}
