using PHCoreWebAPI.DAL.Repository.Setting.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.Repository.Setting
{
 public  interface IManagementRepository
    {
        ValueTask<int> TieUpRecordCateDept(List<ManagementEntity> entity);

        ValueTask<int> TieUpIssueCateDept(List<ManagementEntity> entity);

        ValueTask<int> TieUpPermissionDept(List<ManagementEntity> entity);

        ValueTask<int> GetOrderby(int detpid);

    }
}
