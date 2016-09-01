using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class SearchFilterModel
    {
        public int? page { get; set; }
        public List<int> selectedCategory { get; set; }
        public string Location { get; set; }
        public int? SortBy { get; set; }
    }
}