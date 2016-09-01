using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class ProviderImageGalaryModel
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public int ProviderListingId { get; set; }
        public HttpPostedFileBase ImageUrl { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<long> TotalRecords { get; set; }
        public Nullable<bool> IsActive { get; set; }

    }
}