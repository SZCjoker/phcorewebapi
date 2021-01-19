using PHCoreWebAPI.Application.Common;
using PHCoreWebAPI.Application.DataExport.Contract;
using PHCoreWebAPI.Application.Issue.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.DataExport
{
 public interface IExportService 
    {
             

        DataInfo CopyTemplate(string source, string target);

       ValueTask<IEnumerable<dynamic>> GetData(string Datasource, PagingRequest request);

       ValueTask<BasicResponse<DataInfo>> Print(DataCondition condition);


    }
}
