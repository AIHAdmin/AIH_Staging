using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class AdminDashboardModel
    {
        public List<ProviderListingModel> AllListing { get; set; }
        public List<ProviderListingModel> AllListingNew { get; set; }
        public List<AppointmentModel> AllAppointment{ get; set; }
        public List<QueueListingModel> AllQueuelisting{ get; set; }
        public List<QueueListingModel> UnassignProvider { get; set; }
        public List<QueueListingModel> AssignedProvider { get; set; }
        public List<ServiceRequestModel> AllServiceRequests { get; set; }
        public List<MarkupModel> MarkUpList{get;set;}
        public List<AnalyticsModel> AnalyticsModelList { get; set; }
    }
}