using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class ListingBlogCommentsModel
    {
        public int ListingBlogCommentId { get; set; }
        public int ListingBlogId { get; set; }
        public string UserId { get; set; }
        public string BlogCommentDescription { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public bool IsActive { get; set; }
        public bool IsBlog { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

    }
}