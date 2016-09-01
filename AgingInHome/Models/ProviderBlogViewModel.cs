using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class ProviderBlogViewModel
    {
        public Nullable<long> RowNum { get; set; }
        public int ListingBlogId { get; set; }
        public int ListingId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogDescription { get; set; }
        public string BlogImage { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public bool IsCommentOn { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}