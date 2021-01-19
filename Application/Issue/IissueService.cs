using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.Application.Issue.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Issue
{
public  interface IissueService
    {
        ValueTask<PagingResponse<IEnumerable<IssueResponse>>> QueryBycondition(QueryCondition condition);
        ValueTask<PagingResponse<IEnumerable<IssueResponse>>> GetIssueList(PagingRequest request);
        ValueTask<PagingResponse<IEnumerable<IssueResponse>>> GetIssueListByDept(int deptid,PagingRequest request);
        ValueTask<PagingResponse<IEnumerable<IssueResponse>>> GetIssuePm10toAm10(PagingRequest request);
        ValueTask<PagingResponse<IEnumerable<IssueResponse>>> GetLoginIssue(PagingRequest request);
        ValueTask<BasicResponse> AddIssue(IssueRequest request);
        ValueTask<BasicResponse<IssueResponse>> GetIssueByNo(Int64 recordno);
        ValueTask<BasicResponse> UpdateIssue(IssueRequest request);
        ValueTask<BasicResponse> IssueStateChange(IssueRequest request);
    }
}
