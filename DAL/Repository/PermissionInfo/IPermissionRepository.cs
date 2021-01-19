using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.DAL.Repository.PermissionInfo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.Repository.PermissionInfo
{
public  interface IPermissionRepository
    {
        ValueTask<IEnumerable<PermissionEntity>> GetPermissionList();
        ValueTask<IEnumerable<PermissionEntity>> GetPermissionListByDept(int deptid );
        ValueTask<int> AddPermission(PermissionEntity entity);
        ValueTask<int> UpdatePermission(PermissionEntity entity);
        ValueTask<int> DeletePermission(int permissionid);
      //  ValueTask<IEnumerable<PermissionEntity>> GetPermissionSquenceByDept(int deptid);
    }
}
