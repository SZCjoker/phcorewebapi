using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PHCoreWebAPI.Application.Common
{
    public class Paging
    {
        public const int DefaultPageSize = 30;

        private int _pageIndex = 1;

        public Paging(int pageIndex, long recordCount) : this(pageIndex, 30, recordCount)
        { }

        public Paging(int pageIndex, int pageSize) : this(pageIndex, pageSize, 0)
        { }

        public Paging(int pageIndex, int pageSize, long recordCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            RowsTotal = recordCount;
        }

        /// <summary>
        /// 目前頁碼
        /// </summary>
        [JsonProperty("index")]
        public int PageIndex
        {
            get => _pageIndex;
            set => _pageIndex = value <= 0 ? 1 : value;
        }

        private int _pageSize = 30;

        /// <summary>
        /// 單頁資料最大筆數
        /// </summary>
        [JsonProperty("size")]
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value <= 0 ? 30 : value;
        }

        /// <summary>
        /// 總頁數
        /// </summary>
        [JsonProperty("page")]
        public int PageCount
        {
            get
            {
                if (RowsTotal == 0 || PageSize == 0)
                {
                    return 0;
                }

                return (int)Math.Ceiling((double)RowsTotal / PageSize);
            }
        }

        /// <summary>
        /// 位移
        /// </summary>
        [JsonIgnore]
        public int Offset => PageIndex <= 1 ? 0 : (PageIndex - 1) * PageSize;

        /// <summary>
        /// 資料總筆數
        /// </summary>
        [JsonProperty("total")]
        public long RowsTotal { get; set; }


    }
}
