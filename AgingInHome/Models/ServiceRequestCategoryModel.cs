using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class ServiceRequestCategoryModel
    {
        public int Id { get; set; }
        public int ProviderCategoryId { get; set; }
        public int ServiceRequestId { get; set; }
        public Nullable<int> ProviderListingId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public  List<ServiceRequestDetailModel> ServiceRequestDetails { get; set; }
        public  ProviderCategoryModel ProviderCategory { get; set; }
       // public  ServiceRequestModel ServiceRequest { get; set; }
    }
}