using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class PagerModel
    {
        public int TotalRecord { get; set; }
        public int PageNumber { get; set; }
        public int PerPageRecord { get; set; }
        public int TotalPage { get; set; }
        public Dictionary<int, string> PerPageRecordlist { get; set; }
    }
}