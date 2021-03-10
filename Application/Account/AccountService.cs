using PHCoreWebAPI.Application.Account.Contract;
using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.Application.Permission;
using PHCoreWebAPI.DAL.Repository.PermissionInfo;
using PHCoreWebAPI.DAL.Repository.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using PHCoreWebAPI.DAL.Repository.Account.Entity;
using Microsoft.Extensions.Logging;

namespace PHCoreWebAPI.Application.Account
{
    public class AccountService : IAccountService
    {

        private readonly IAccountRepository _repository;
        private readonly IPermissionRepository _PermissionRepository;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IAccountRepository repository,
                              IPermissionRepository  PermissionRepository,
                              ILogger<AccountService> logger)
        {
            _repository = repository;
            _PermissionRepository = PermissionRepository;
            _logger = logger;
        }



        public async ValueTask<BasicResponse<bool>> Authenticate(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.UserID) || string.IsNullOrEmpty(request.PWD))
                return new BasicResponse<bool>() { code =(int)HttpStatusCode.BadRequest,desc="without parameter",data=false};            

            var existsOrnot = await _repository.Exists(request.UserID);
            if (existsOrnot==0)
                return new BasicResponse<bool>() { code = (int)HttpStatusCode.BadRequest, desc = "unauthenticate user", data = false };
            
            var userInfo = await _repository.GetUserInfoById(request.UserID);

            if (userInfo.LoginFailedCount >= 3)
            {
                _logger.LogInformation($"Login password error over three times{ request.UserID }");
                await _repository.UpdateUserInfo(new UserInfoEntity { UserID = request.UserID,
                    State = 9 });
                return new BasicResponse<bool>() { code = (int)HttpStatusCode.BadRequest, desc = "pwd error over three times", data = false };
            }
            var data = await _repository.Authenticate(request.UserID, request.PWD);

            if (data == 0)
            {
                var entity = new UserInfoEntity
                {
                    UserID = userInfo.UserID,
                    LoginFailedCount = userInfo.LoginFailedCount + 1
                };
                var LogingfaileData = await _repository.UpdateUserInfo(entity);
                return new BasicResponse<bool>() { code = (int)HttpStatusCode.BadRequest, desc = "error password", data = false };
            }

             
           return new BasicResponse<bool>() { code = (int)HttpStatusCode.OK, desc = "success", data = true };
        }

        public async ValueTask<BasicResponse<bool>> CreateUser(CreateUpdateUserRequest request)
        {
            var datetime = DateTimeOffset.UtcNow;
            var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
            var time = datetime.ToUnixTimeSeconds();
            if (string.IsNullOrEmpty(request.UserID) || string.IsNullOrEmpty(request.PWD) ||
                request.DeptID == 0)
                return new BasicResponse<bool>() { code = 9999, desc = "please check data column" };

            var exists = await _repository.Exists(request.UserID);
            if(exists!=0)
                return new BasicResponse<bool>() { code = 9999, desc = "userid being choose please regenerated one" };
            var entity = new UserInfoEntity
            {
                UserID = request.UserID,
                DeptID = request.DeptID,
                PWD = request.PWD,
                FirstName = request.FirstName,
                LastName = request.LastName,
                FirstCName = request.FirstCName,
                LastCName = request.LastCName,
                Email = request.Email,
                Gender = request.Gender,
                Mobile = request.Mobile,
                Cdate = date,
                Ctime = time,
                State = 1
            };
            var data = await _repository.CreateUser(entity);
            if(data==0)
                return new BasicResponse<bool>() { code = 9999, desc = "create user failed, please check data column" };
            return new BasicResponse<bool>() { code = 0000, desc = "create user success" ,data = true };
        }

        public async ValueTask<BasicResponse<bool>> DeleteUser(string userid)
        {
            var data = await _repository.DeleteUserInfo(userid);
            if(data==0)
                return new BasicResponse<bool>() { code = 9999, desc = "delete user failed" };
            return new BasicResponse<bool>() { code = 0000, desc = "delete user success" };

        }

        public async  ValueTask<BasicResponse<bool>> Exists(string userid)
        {
            if (string.IsNullOrEmpty(userid))
                return new BasicResponse<bool>() { code = (int)HttpStatusCode.BadRequest, desc = "without parameter", data = false };
            var data = await _repository.Exists(userid);
            if(data==0)
                return new BasicResponse<bool>() { code = (int)HttpStatusCode.BadRequest, desc = "unauthentiacate user", data = false };
            return new BasicResponse<bool>() { code = (int)HttpStatusCode.OK, desc = "success", data = true };    
        }

        public async ValueTask<BasicResponse<List<GetUserInfoByIDResponse>>> GetUserInfo()
        {
            var data = await _repository.GetUserInfo();

            var result = data.Select(d => new GetUserInfoByIDResponse
            {
                UserID = d.UserID,
                DeptID = d.DeptID,
                DeptName = d.DeptName,
                FirstName = d.FirstName,
                FirstCName = d.FirstCName,
                LastCName = d.LastCName,
                Gender = d.Gender,
                Email = d?.Email,
                Mobile = d?.Mobile,
                LoginFailedCount = d?.LoginFailedCount,
                Cdate = d?.Cdate,
                Ctime = d?.Ctime,
                State = d.State
            }).ToList();

            return new BasicResponse<List<GetUserInfoByIDResponse>>() 
                        { code = (int)HttpStatusCode.OK, desc = "success", data = result };

        }

        public async ValueTask<BasicResponse<GetUserInfoByIDResponse>> GetUserInfoById(string userid)
        {
            if (string.IsNullOrEmpty(userid))
                return new BasicResponse<GetUserInfoByIDResponse>() 
                { code = (int)HttpStatusCode.BadRequest, desc = "without parameter", data = null };

            var exists = await _repository.Exists(userid);
            if (exists==0)
                return new BasicResponse<GetUserInfoByIDResponse>() 
                  { code = (int)HttpStatusCode.BadRequest, desc = "unauthentiacate user", data =null };

            var data = await _repository.GetUserInfoById(userid);
                var result = new GetUserInfoByIDResponse 
                { 
                    UserID = data.UserID,
                    DeptID = data.DeptID,
                    DeptName = data.DeptName,
                    FirstName = data.FirstName,
                    FirstCName = data.FirstCName,
                    LastCName = data.LastCName,
                    Gender = data.Gender,
                    Email = data?.Email,
                    Mobile = data?.Mobile,
                    LoginFailedCount = data.LoginFailedCount,
                    State = data.State
                };
            return new BasicResponse<GetUserInfoByIDResponse>() { code = (int)HttpStatusCode.OK, desc = "success", data = result };

        }

        /// <summary>
        /// 登入  -- 之後改SSO回傳 TOKEN
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async  ValueTask<BasicResponse<LoginResponse>> Login(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.UserID)) new BasicResponse<LoginResponse>() { code = (int)HttpStatusCode.BadRequest, desc = "without parameter", data = null };

            var auth = await Authenticate(request);
            if(auth.data)
            {
                var userinfo = await GetUserInfoById(request.UserID);

                var PermissionList = await _PermissionRepository.GetPermissionListByDept(userinfo.data.DeptID);
                var result = new LoginResponse 
                { 
                    DeptName = userinfo.data.DeptName,
                    FirstName = userinfo.data.FirstName,
                    FirstCName = userinfo.data.FirstCName,
                    DeptID = userinfo.data.DeptID,
                    PermissionList = PermissionList.Select(d => new Contract.Permission
                    {   Page = d.Page,
                        DeptID = d.DeptID,
                        PermissionName = d.PermissionName,
                        PermissionPath = d.PermissionPath,
                        Code = d.Code,
                        State = d.State,
                        OrderBy = d.Orderby
                    }).OrderBy(o => o.OrderBy).ToList()
                };

                var adjsutZero = Zeroing(request.UserID);

                return new BasicResponse<LoginResponse>() { code = (int)HttpStatusCode.OK, desc = "success", data = result };
            }

            return new BasicResponse<LoginResponse>() { code = (int)HttpStatusCode.BadRequest, desc = $"{auth.desc}", data = null };

            //loginfailed plus one 
        }

        public  async ValueTask<BasicResponse<bool>> UpdateUserInfo(CreateUpdateUserRequest request)
        {
            var datetime = DateTimeOffset.UtcNow;
            var date = Convert.ToInt32(datetime.ToString("yyyyMMdd"));
            var time = datetime.ToUnixTimeSeconds();

            var entity = new UserInfoEntity 
            {

                UserID = request.UserID ?? null,
                DeptID = request.DeptID,
                PWD = request.PWD ?? null,
                Gender = request.Gender,
                FirstName = request.FirstName ?? null,
                LastName = request.LastName ?? null,
                FirstCName = request.FirstCName ?? null,
                LastCName = request.LastCName ?? null,
                Email = request.Email ?? null,
                Mobile = request.Mobile ?? null,
                Udate = date,
                Utime = time,
                LoginFailedCount = request.LoginFailedCount,
                State = request.State
            };

            var data = await _repository.UpdateUserInfo(entity);
            if (data == 0)
                return new BasicResponse<bool>() { code = 9999, desc = "update failed", data = false };
            return new BasicResponse<bool>() { code = 0000, desc = $"update success total updaate {data}", data = true };
        }

        public async ValueTask<BasicResponse> Zeroing(string userid)
        {
            var result = await _repository.Zeroing(userid);

            return StateCodeHandler.ForCount(result);
        }
    }
}