using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgingInHome.DAL;

namespace AgingInHome.BLL.Repositories
{
    public interface IQueueListingRepsitory
    {
        List<RequiedListing> GetAllQueueListing();
        int AddManuallyAssignProvider(int ProviderListingId, int ServiceRequestCatId,int QueueListingId);
    }
    public class QueueListingClass : IQueueListingRepsitory
    {
        public int AddManuallyAssignProvider(int ProviderListingId, int ServiceRequestCatId, int QueueListingId)
        {
            using (var db = new AgingInHomeContext())
            {
                var NewDetail = new ServiceRequestDetail();
                NewDetail.EmailDateTime = DateTime.Now;
                NewDetail.IsActive = true;
                NewDetail.IsConsumerAcceptInvite = false;
                NewDetail.IsProviderInvite = false;
                NewDetail.LastUpdation = DateTime.Now;
                NewDetail.Mailrespose = false;
                NewDetail.ProviderListingId = ProviderListingId;
                NewDetail.ServiceRequestCatId = ServiceRequestCatId;
                db.ServiceRequestDetails.Add(NewDetail);
                //change status isqueue =false
                var queuelisting=db.RequiedListings.Find(QueueListingId);
                queuelisting.IsQueue = false;
                queuelisting.AssignProviderName = db.ProviderListings.Find(ProviderListingId).CompanyName;
                db.SaveChanges();
                var GetServiceRequestId = db.ServiceRequestCategories.Find(ServiceRequestCatId);
                return GetServiceRequestId.ServiceRequestId;
            }
        }

        public List<RequiedListing> GetAllQueueListing()
        {
            using (var db=new AgingInHomeContext())
            {
                return db.RequiedListings
                    .Include("AspNetUser").Include("AspNetUser.ConsumerProfiles")
                    .Include("ProviderCategory").ToList();
            }
        }
    }
}
