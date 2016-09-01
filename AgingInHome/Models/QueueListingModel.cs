using AgingInHome.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AgingInHome.BLL.Repositories;

namespace AgingInHome.Models
{
    public class QueueListingModel
    {
        public int Id { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string UserId { get; set; }
        public string City { get; set; }
        public Nullable<int> Zipcode { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        public Nullable<System.DateTime> ServiceDate { get; set; }
        public Nullable<bool> Is7daysWait { get; set; }
        public Nullable<int> ServiceRequestCatId { get; set; }
        public Nullable<bool> IsQueue { get; set; }
        public int ProviderListingId { get; set; }
        public string AssignProviderName { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ProviderCategory ProviderCategory { get; set; }
        public List<QueueListingModel> GetAllQueueListing()
        {
            var getQueueListing = new QueueListingClass().GetAllQueueListing();
            var model = Mapper.Map<List<QueueListingModel>>(getQueueListing);
            return model;
        }
        //public List<ProviderListingModel> ProviderByCategoryId(int id)
        //{
        //    var getQueueListing = new QueueListingClass().GetAllQueueListing();
        //    var model = Mapper.Map<List<QueueListingModel>>(getQueueListing);
        //    return model;
        //}
        public int ManuallyAssignProvider(int ProviderListingId,int ServiceRequestCatId,int queueId)
        {
            return new QueueListingClass().AddManuallyAssignProvider(ProviderListingId, ServiceRequestCatId,queueId);
        }
        
    }
}