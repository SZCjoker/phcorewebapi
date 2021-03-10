using PHCoreWebAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PHCoreWebAPI.DAL.DbFactory;
using PHCoreWebAPI.DAL.Repository.Account;
using PHCoreWebAPI.DAL.Repository.Account.Entity;
using Microsoft.Extensions.Logging;

namespace PHCoreWebAPI.DAL.Repository.Account
{
    public class AccountRepository : IAccountRepository
    {

        private readonly IDbFactory _dbfactory;
        private readonly ILogger<AccountRepository> _logger;


        public AccountRepository(IDbFactory dbfactory,
                                 ILogger<AccountRepository> logger)
        {
            _dbfactory = dbfactory;
            _logger = logger;
        }

        public async ValueTask<int> Authenticate(string userid, string pwd)
        {
            try
            {
                string Tsql = $@"SELECT COUNT(userid) FROM userinfo
                                 WHERE UserID = '{userid}'
                                 AND PWD = '{pwd}'";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QuerySingleOrDefaultAsync<int>(Tsql, new { userid, pwd });
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<int> CreateUser(UserInfoEntity entity)
        {
            try
            {
                string tSql = $@"INSERT INTO UserInfo(UserID,PWD,DeptID,Gender,FirstName,LastName,FirstCName
                                                  ,LastCName,Email,Mobile,LoginFailedCount,Cdate,Ctime,State)
                                 VALUES
                                                 (@UserID, 
                                                  @PWD,
                                                  @DeptID,
                                                  @Gender,
                                                  @FirstName,
                                                  @LastName, 
                                                  @FirstCName, 
                                                  @LastCName, 
                                                  @Email, 
                                                  @Mobile, 
                                                  @LoginFailedCount,
                                                  @Cdate,
                                                  @Ctime,
                                                  @State)";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.ExecuteAsync(tSql, entity);
                    return data;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }     

        public async ValueTask<int> DeleteUserInfo(string userid)
        {
            try
            {
                string tSql = $@"UPDATE UserInfo
                                 SET state = 9
                                 WHERE userid ='{userid}'";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.ExecuteAsync(tSql, userid);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public async ValueTask<int> Exists(string userid)
        {
            try
            {  
                string Tsql = $@"SELECT Count(userid) 
                                 FROM userinfo
                                 WHERE UserID = '{userid}'
                                 AND state= 1";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QuerySingleAsync<int>(Tsql);
                    return data;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<IEnumerable<UserInfoEntity>> GetUserInfo()
        {
            try
            {
                string Tsql = @"SELECT u.UserID,u.FirstName,u.FirstCName,u.LastCName,
                                       u.DeptID,d.DeptName as DeptName,u.Gender,u.Email,
                                       u.Mobile,u.LoginFailedCount,u.state
							    FROM UserInfo u LEFT JOIN DeptInfo d ON u.deptid = d.deptid
                                WHERE u.state = 1";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QueryAsync<UserInfoEntity>(Tsql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<UserInfoEntity> GetUserInfoById(string userid)
        {
            try
            {
                string Tsql = $@"SELECT u.UserID,u.FirstName,u.FirstCName,u.LastCName,
                                       u.DeptID,d.DeptName as DeptName,u.Gender,u.Email,
                                       u.Mobile,u.LoginFailedCount,u.state
							    FROM UserInfo u LEFT JOIN DeptInfo d ON u.deptid = d.deptid 
                                WHERE userid ='{userid}'
                                AND u.state = 1";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QuerySingleOrDefaultAsync<UserInfoEntity>(Tsql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
              

        public async  ValueTask<int> ResetFailedCount(string userid)
        {
            string tSql = $@"UPDATE UserInfo
                             SET LoginFailedCount = 0
                             WHERE userid like '{userid}'";
            using (var cn = await _dbfactory.OpenConnectionAsync())
            {
                var data = await cn.ExecuteAsync(tSql,new { userid});
                return data;
            }
        }

        public async ValueTask<int> UpdateUserInfo(UserInfoEntity entity)
        {
            try
            {
                string tSql =  $@"UPDATE UserInfo 
                                  SET    DeptID = CASE WHEN @DeptID = 0 THEN DeptID ELSE @DeptID END,
                                         Gender = CASE WHEN @Gender = 0 THEN Gender ELSE @Gender END,
                                         udate =  CASE WHEN @udate = 0  THEN udate  ELSE @udate END,
                                         utime =  CASE WHEN @utime = 0  THEN utime  ELSE @utime END,
                                         LoginFailedCount = CASE WHEN @LoginFailedCount = 0 THEN LoginFailedCount ELSE @LoginFailedCount  END,
                                         State = CASE WHEN @State = 0 THEN State ELSE @State END,
                                         PWD =  ISNULL(@PWD,PWD),
                                         FirstName = ISNULL(@FirstName,FirstName),
                                         LastName =  ISNULL(@LastName,LastName),
                                         FirstCName = ISNULL(@FirstCName,FirstCName),
                                         LastCName =  ISNULL(@LastCName,LastCName),
                                         Email =  ISNULL(@Email,Email),
                                         Mobile = ISNULL(@Mobile,Mobile)
                                 WHERE   userid = @UserID";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.ExecuteAsync(tSql, entity);
                    return data;
                };
            }

            catch (Exception ex)
            {
                _logger.LogInformation($"Account Update error {entity.UserID},{ex.Message}");
                throw ex;
            }
            
        }

        public async ValueTask<int> Zeroing(string userid)
        {
            try
            {
                string tSql = $@"UPDATE UserInfo
                                 SET state = 1 , LoginFailedCount = 0
                                 WHERE userid ='{userid}'";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.ExecuteAsync(tSql, userid);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
