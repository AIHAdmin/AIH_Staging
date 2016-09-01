using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class ServiceRequestDetailModel
    {
        public int Id { get; set; }
        public int ProviderListingId { get; set; }
        public bool IsProviderInvite { get; set; }
        public bool IsConsumerAcceptInvite { get; set; }
        public DateTime EmailDateTime { get; set; }
        public bool Mailrespose { get; set; }
        public bool IsWaitMore7Days { get; set; }
        public bool IsActive { get; set; }
        public int ServiceRequestCatId { get; set; }

        public ProviderListingModel ProviderListing { get; set; }
        public ServiceRequestCategoryModel ServiceRequestCategory { get; set; }
        public  List<InboxModel> Inboxes { get; set; }
    }
}