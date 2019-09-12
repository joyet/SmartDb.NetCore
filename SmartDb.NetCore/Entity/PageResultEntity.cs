using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartDb.NetCore
{
    public class PageResultEntity
    {
        public long PageSize { get; set; }

        public long CurrentPageIndex { get; set; }

        public long TotalCount { get; set; }

        public long TotalPageIndex { get; set; }

        public object Data{ get; set; }

        public PageResultEntity()
        {
            PageSize = 0;
            CurrentPageIndex = 1;
            TotalCount = 0;
            TotalPageIndex = 0;
            Data = null;
        }

    }
}
