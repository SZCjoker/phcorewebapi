using PHCoreWebAPI.Application.Account.Contract;
using PHCoreWebAPI.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Account
{
public  interface IAccountService
    {

        ValueTask<BasicResponse> Zeroing(string userid);
        ValueTask<BasicResponse<bool>> Exists(string userid);
        ValueTask<BasicResponse<bool>> Authenticate(LoginRequest request);
        ValueTask<BasicResponse<bool>> UpdateUserInfo(CreateUpdateUserRequest request);
        ValueTask<BasicResponse<bool>> DeleteUser(string userid);
        ValueTask<BasicResponse<GetUserInfoByIDResponse>> GetUserInfoById(string userid);
        ValueTask<BasicResponse<List<GetUserInfoByIDResponse>>> GetUserInfo();
        ValueTask<BasicResponse<bool>> CreateUser(CreateUpdateUserRequest request);
        ValueTask<BasicResponse<LoginResponse>> Login(LoginRequest request);

    }
}
