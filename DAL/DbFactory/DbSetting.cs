using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.DAL.DbFactory
{
    public class DbSetting
    {
        public string Connection { get; set; }
        public string DbProvider { get; set; }

        public int Timeout { get; set; } = 30;
    }
}
