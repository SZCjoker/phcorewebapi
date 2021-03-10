using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.DAL.Repository.PermissionInfo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dapper;
using PHCoreWebAPI.DAL.DbFactory;

namespace PHCoreWebAPI.DAL.Repository.PermissionInfo
{
    public class PermissionRepository : IPermissionRepository
    {


        private readonly IDbFactory _dbfactory;

        public PermissionRepository(IDbFactory dbfactory)
        {
            _dbfactory = dbfactory;
        }



        public async ValueTask<int> AddPermission(PermissionEntity entity)
        {
            try
            {
                string tsql = $@"INSERT INTO dbo.Permission(PermissionID,Page,PermissionName
                                                            ,PermissionPath,Code,OrderBy,Cdate
                                                            ,Ctime,State)
                                 VALUES(@PermissionID,@Page,@PermissionName,@PermissionPath,
                                        @Code,@OrderBy,@Cdate,@Ctime,State);";

                string mpSql = $@"INSERT NITO dbo.DeptPermissionMap(DeptID,PermissionID)
                                  VALUES(@DeptID,@PermissionID);";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.ExecuteAsync(tsql+mpSql,entity);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            throw new NotImplementedException();
        }

        public async ValueTask<int> DeletePermission(int permissionid)
        {
            try
            {
                string tsql = $@"UPDATE Permission 
                                 SET State = 0
                                 WHERE PermissionID ={permissionid}";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.ExecuteAsync(tsql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<IEnumerable<PermissionEntity>> GetPermissionList()
        {
            try
            {
                string Tsql = $@"SELECT PermissionID,Page,PermissionName,PermissionPath,
                                        code,State,cdate,udate
							     FROM   Permission
                                 ORDER BY Ctime";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QueryAsync<PermissionEntity>(Tsql);
                    return data;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async ValueTask<IEnumerable<PermissionEntity>> GetPermissionListByDept(int deptid)
        {
            try
            {
                string Tsql =$@"SELECT p.PermissionID,dp.DeptID,p.Page,p.PermissionName,p.PermissionPath,
                                       p.code,p.orderby,p.State,p.cdate,p.udate
							    FROM Permission p LEFT JOIN deptpermissionmap dp 
								ON dp.permissionid = p.permissionid  
                                WHERE dp.deptid = { deptid } AND State = 1
								ORDER BY Ctime";

                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.QueryAsync<PermissionEntity>(Tsql);
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async ValueTask<IEnumerable<PermissionEntity>> GetPermissionSquenceByDept(int deptid)
        //{

        //    string tSql = $@"SELECT dp.DeptID,p.permissionname,p.permissionid
        //                     FROM Permission p 
        //                     LEFT JOIN DeptPermissionMap dp
        //                     ON dp.permissionID = p.permissionID
        //                     WHERE dp.deptID ={deptid}
        //                     ORDER BY (SELECT Dateadd(s,p.Ctime,'1970-01-01 00:00'))";
        //    using (var cn = await _dbfactory.OpenConnectionAsync())
        //    {
        //        var data = await cn.QueryAsync<PermissionEntity>(tSql);
        //        return data;
        //    }
        //}

        public async ValueTask<int> UpdatePermission(PermissionEntity entity)
        {
            try
            {
                string tsql = @"UPDATE  Permission p
                                
                                SET     p.PermissionName = ISNULL(@PermissionName,p.PermissionName), 
                                        p.PermissionPath = ISNULL(@PermissionPath,p.PermissionPath), 
                                        p.page = ISNULL(@page,p.page), 
                                        p.Code =ISNULL(@Code,p.Code), 
                                        p.udate = CASE WHEN @udate = 0 THEN p.udate ELSE  @udate END , 
                                        p.utime = CASE WHEN @utime = 0 THEN p.utime ELSE  @utime END, 
                                        p.state = CASE WHEN @state = 0 THEN p.state ELSE  @state END
                                WHERE p.PermissionID = @PermissionID";
                using (var cn = await _dbfactory.OpenConnectionAsync())
                {
                    var data = await cn.ExecuteAsync(tsql, entity);
                    return data;
                };
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}