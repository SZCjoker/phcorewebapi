using PHCoreWebAPI.DAL.Repository.Account.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.Repository.Account
{
public  interface IAccountRepository
    {
        ValueTask<int> Zeroing(string userid);
        ValueTask<IEnumerable<UserInfoEntity>> GetUserInfo();
        ValueTask<UserInfoEntity> GetUserInfoById(string userid);
        ValueTask<int> UpdateUserInfo(UserInfoEntity entity);
        ValueTask<int> DeleteUserInfo(string userid);
        ValueTask<int> Authenticate(string userid, string pwd);
        ValueTask<int> Exists(string userid);
        ValueTask<int> CreateUser(UserInfoEntity entity);
        
        ValueTask<int> ResetFailedCount(string userid);
    }
}
