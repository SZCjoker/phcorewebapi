using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.Application.Permission.Contract;
using PHCoreWebAPI.DAL.Repository.PermissionInfo;
using PHCoreWebAPI.DAL.Repository.PermissionInfo.Entity;
using PHCoreWebAPI.Utility.Handler;
using PHCoreWebAPI.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace PHCoreWebAPI.Application.Permission
{
    public class PermissionService : IPermissionService
    {

        private readonly IPermissionRepository _repository;
        private readonly IGenerateId _generator;

        public PermissionService(IPermissionRepository repository
                                 , IGenerateId generator)
        {
            _repository = repository;
            _generator = generator;
        }
        
        public async ValueTask<BasicResponse<int>> AddPermission(PermissionRequest request)
        {
            var datetime = DateTimeOffset.UtcNow;
            var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
            var time = datetime.ToUnixTimeSeconds();

            if (
                string.IsNullOrEmpty(request.PermissionName)||
                string.IsNullOrEmpty(request.PermissionPath)||
                string.IsNullOrEmpty(request.Page)||
                string.IsNullOrEmpty(request.Code)) 
                return new BasicResponse<int>() { code = 9999, desc = "please check the column again", data = 0 };

           

            var entity = new PermissionEntity
            {   
                PermissionID = _generator.GetId(),
                Page = $"{request.Page}.html",
                PermissionName = request.PermissionName,
                Code = request.Code,
                Cdate = date,
                Ctime = time, 
                State = true
            };

            var data = await _repository.AddPermission(entity);
            return new BasicResponse<int>() { code = 1111, desc = "success", data = data };
        }

       

        public async ValueTask<BasicResponse<int>> DeletePermission(int permissionid)
        {
            var datetime = DateTimeOffset.UtcNow;
            var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
            var time = datetime.ToUnixTimeSeconds();


            if (permissionid ==0) 
                return new BasicResponse<int>() { code = 9999, desc = "please make sure the checkbox being checked", data = 0 };

            var data = await _repository.DeletePermission(permissionid);
            return new BasicResponse<int>() { code = 1111, desc = "success", data = data };
        }

        public async ValueTask<BasicResponse<IEnumerable<PermissionResponse>>> GetPermissionList()
        {
            var data = await _repository.GetPermissionList();
            var result = data.Select(d => new PermissionResponse
            {
                PermissionID = d.PermissionID,                
                Page = d.Page,
                PermissionName = d.PermissionName,
                PermissionPath = d.PermissionPath,
                Code = d.Code,
                Cdate = d.Cdate,
                Ctime = d.Ctime,
                Udate = d.Udate,
                Utime = d.Utime,
                State = d.State
            }).AsQueryable();

            return new BasicResponse<IEnumerable<PermissionResponse>>()
            { code = (int)HttpStatusCode.OK, desc = "success", data = result };
        }

        public  async ValueTask<BasicResponse<IEnumerable<PermissionResponse>>> GetPermissionListByDept(int deptid)
        {
            if (deptid == 0) return  new BasicResponse<IEnumerable<PermissionResponse>>() { code = (int)HttpStatusCode.BadRequest,desc="without parameter" ,data = Enumerable.Empty<PermissionResponse>() };

            var data = await _repository.GetPermissionListByDept(deptid);

            var result = data.Select(d => new PermissionResponse 
            { 
                PermissionID = d.PermissionID,                
                Page =  d.Page,
                OrderBy = d.Orderby,
                PermissionName = d.PermissionName,
                PermissionPath = d.PermissionPath,
                Code = d.Code,
                Cdate = d.Cdate,
                Ctime = d.Ctime,
                Udate = d.Udate,
                Utime = d.Utime,
                State = d.State
            }).AsQueryable();

            return new BasicResponse<IEnumerable<PermissionResponse>>() 
            { code = (int)HttpStatusCode.OK, desc = "success", data = result };
        }

        public async ValueTask<BasicResponse<int>> UpdatePermission(PermissionRequest request)
        {
            var datetime = DateTimeOffset.UtcNow;
            var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
            var time = datetime.ToUnixTimeSeconds();

            if(request.PermissionID==0)
                return new BasicResponse<int>() { code = 9999, desc = "please check the column again", data = 0 };

            var entity = new PermissionEntity
            {
                PermissionID = request.PermissionID,
                Page = $"{request.Page}.html",
                PermissionName = request.PermissionName,
                Code = request.Code,
                Udate = date,
                Utime = time,
                State = true
            };

            var data = await _repository.UpdatePermission(entity);
            return new BasicResponse<int>() { code = 1111, desc = "success", data = data };
           
        }
    }
}