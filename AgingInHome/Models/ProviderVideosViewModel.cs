using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class ProviderVideosViewModel
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
        public int ProviderListingId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<bool> IsActive { get; set; }

        public Nullable<long> TotalRecords { get; set; }
    }
}