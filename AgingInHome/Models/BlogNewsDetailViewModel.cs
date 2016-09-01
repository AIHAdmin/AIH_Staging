using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class BlogNewsDetailViewModel
    {
        public int ItemID { get; set; }
        public int ListingId { get; set; }
        public string ItemTitle { get; set; }
        public string ItemDescription { get; set; }
        public string ItemImage { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string CompanyName { get; set; }
        public int BlogNewsId { get; set;}
        public bool IsBlog { get; set; }

    }
}