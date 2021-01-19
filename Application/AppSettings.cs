using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application
{
    
        public class AppSettings
        {
            public Storage storage { get; set; }
            public Application application { get; set; }
            public Logging Logging { get; set; }
            public Jwt jwt { get; set; }
            public WhiteList WhiteList { get; set; }
            public Api api { get; set; }
        }

        public class Jwt
        {
            public string JwtKey { get; set; }
            public string JwtIssuer { get; set; }
            public int JwtExpireMinutes { get; set; }
        }

        public class Storage
        {
            public Mssql mssql { get; set; }
            public Redis Redis { get; set; }
            public Elasticsearch elasticsearch { get; set; }
        }

        public class Mssql
        {
            public Readonly readOnly { get; set; }
            public Readwrite readWrite { get; set; }
        }

        public class Readonly
        {
            public string connection { get; set; }
            public string provider { get; set; }
        }

        public class Readwrite
        {
            public string connection { get; set; }
            public string provider { get; set; }
        }

        public class Redis
        {
            public string master { get; set; }
            public string slave { get; set; }
        }

        public class Elasticsearch
        {
            public string host { get; set; }
            public string index { get; set; }
        }

        public class Application
        {
            public bool hashCode { get; set; }
            public bool swagger { get; set; }
            public Slack slack { get; set; }
            public Api api { get; set; }
        }

        public class Slack
        {
            public string channel { get; set; }
            public string url { get; set; }
        }

        public class Api
        {
            public string ODM { get; set; }
            public string IGSS { get; set; }
            public string ISES { get; set; }
            public string BAF { get; set; }
            public string PMIS { get; set; }
            //test 
            public string AESKEY { get; set; }
            public string TEST { get; set; }
        }

        public class ConnectionString
        {
            public string PH83 { get; set; }

        }

        public class Logging
        {
            public Loglevel LogLevel { get; set; }
        }

        public class Loglevel
        {
            public string Default { get; set; }
            public string System { get; set; }
            public string Microsoft { get; set; }
        }

        public class WhiteList
        {
            public string EPA { get; set; }
            public string AnyWhere { get; set; }

        }
    
}
