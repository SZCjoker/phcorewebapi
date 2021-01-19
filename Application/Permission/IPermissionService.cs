using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.Application.Permission.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Permission
{
 public interface IPermissionService
    {
        ValueTask<BasicResponse<IEnumerable<PermissionResponse>>> GetPermissionList();
        ValueTask<BasicResponse<IEnumerable<PermissionResponse>>> GetPermissionListByDept(int deptid);
        ValueTask<BasicResponse<int>> AddPermission(PermissionRequest request);
        ValueTask<BasicResponse<int>> UpdatePermission(PermissionRequest request);
        ValueTask<BasicResponse<int>> DeletePermission(int permissionid);

    }
}
