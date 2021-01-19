using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.Application.Record.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Record
{
 public  interface IRecordService
    {
        ValueTask<PagingResponse<IEnumerable<RecordResponse>>> QueryBycondition(QueryCondition condition);
        ValueTask<BasicResponse<IEnumerable<RecordResponse>>> GetRecordList();
        ValueTask<BasicResponse<IEnumerable<RecordResponse>>> GetRecordListByDept(int deptid);
        
        ValueTask<BasicResponse> AddRecord(RecordRequest request);
        ValueTask<BasicResponse<RecordResponse>> GetRecordByNo(Int64 recordno);
        ValueTask<BasicResponse> UpdateRecord(RecordRequest request);
        ValueTask<BasicResponse> RecordStateChange(RecordRequest request);
    }
}
