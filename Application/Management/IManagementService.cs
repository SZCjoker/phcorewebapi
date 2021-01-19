using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.Application.Setting.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Setting
{
 public interface IManagementService
    {  
        ValueTask<BasicResponse> TieUpIssueCateDept(List<ManagementRequest> request);
        ValueTask<BasicResponse> TieUpRecordCateDept(List<ManagementRequest> request);
        ValueTask<BasicResponse> TieUpPermissionDept(List<ManagementRequest> request);
        ValueTask<BasicResponse> AddDept(ManagementRequest request);
        ValueTask<BasicResponse> AddIssueCate(ManagementRequest request);
        ValueTask<BasicResponse> AddRecordCate(ManagementRequest request);


        ValueTask<BasicResponse<IEnumerable<ManagementResponse>>> GetDeptList();
        ValueTask<BasicResponse<IEnumerable<ManagementResponse>>> GetDeptsByDeptID(int deptid);
        ValueTask<BasicResponse<IEnumerable<ManagementResponse>>> GetIssueCateList();
        ValueTask<BasicResponse<IEnumerable<ManagementResponse>>> GetIssueCateByDeptID(int deptid);
        ValueTask<BasicResponse<IEnumerable<ManagementResponse>>> GetRecordCateList();
        ValueTask<BasicResponse<IEnumerable<ManagementResponse>>> GetRecordCateByDeptID(int deptid);


        ValueTask<BasicResponse> UpdateDept(ManagementRequest request);
        ValueTask<BasicResponse> UpdateIssueCate(ManagementRequest request);
        ValueTask<BasicResponse> StopDept(int deptid,int state);
        ValueTask<BasicResponse> StopIssueCate(int issuecateid, int state);
        ValueTask<BasicResponse> StopRecordCate(int recordcateid, int state);
        ValueTask<BasicResponse> UpdateRecordCate(ManagementRequest request);
        ValueTask<BasicResponse<string>> GetVersion();


        ValueTask<BasicResponse<string>> GetEnvironment();

    }
}
