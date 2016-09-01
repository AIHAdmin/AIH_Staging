using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgingInHome.DAL;
using System.Web;
using AgingInHome.DAL.Helper;

namespace AgingInHome.BLL.Repositories
{
    public interface IServiceRequestRepository
    {
        string AddServiceRequest(ServiceRequest serviceRequest,List<int> RequiredListingIds);
        bool UpdateServiceRequest(ServiceRequest serviceRequest);
        //ServiceRequest GetServiceRequest(int serviceId);
        bool DeleteServiceRequest(int serviceId);
        bool UpdateInvitationStatus(int RequestId, int ProviderListingId, bool Status,int alter);
        bool CancelCategoryRequest(int catId);
        ServiceRequest GetServiceRequestById(int serviceId);

    }
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        public string AddServiceRequest(ServiceRequest serviceRequest,List<int> RequiredListingIds)
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {
                    var _ServiceRequest = new ServiceRequest();
                    _ServiceRequest.Address = serviceRequest.Address;
                    _ServiceRequest.City = serviceRequest.City;
                    _ServiceRequest.CreatedBy = serviceRequest.CreatedBy;
                    _ServiceRequest.CreatedDate = serviceRequest.CreatedDate;
                    _ServiceRequest.Email = serviceRequest.Email;
                    _ServiceRequest.FirstName = serviceRequest.FirstName;
                    _ServiceRequest.IsDirectContact = serviceRequest.IsDirectContact;
                    _ServiceRequest.LastName = serviceRequest.LastName;
                    _ServiceRequest.PrimaryPhone = serviceRequest.PrimaryPhone;
                    _ServiceRequest.AlternatePhone = serviceRequest.AlternatePhone;
                    _ServiceRequest.ServiceDate = serviceRequest.ServiceDate;
                    _ServiceRequest.StateId = serviceRequest.StateId;
                    _ServiceRequest.ZipCode = serviceRequest.ZipCode;
                    _ServiceRequest.UserId = serviceRequest.UserId;
                    _ServiceRequest.Age = serviceRequest.Age;
                    _ServiceRequest.Gender = serviceRequest.Gender;
                    _ServiceRequest.TypeOfMedicalInsurence = serviceRequest.TypeOfMedicalInsurence;
                    _ServiceRequest.LastUpdation = DateTime.Now;
                    _ServiceRequest.Relation = serviceRequest.Relation;
                    _ServiceRequest.BestTime = serviceRequest.BestTime;
                    
                    if (serviceRequest.IsDirectContact==true)
                    {
                        _ServiceRequest.ResultUrl = serviceRequest.ResultUrl;
                    }
                    else
                    {
                        _ServiceRequest.ResultUrl = "";
                    }


                    db.ServiceRequests.Add(_ServiceRequest);
                    db.SaveChanges();
                    var id = db.ServiceRequests.Max(m => m.Id);
                    var getListing = db.RequiedListings.Where(d => RequiredListingIds.Contains(d.Id)).ToList();
                    foreach (var ServiceRequestCat in serviceRequest.ServiceRequestCategories)
                    {
                        var SRC = new ServiceRequestCategory();

                        SRC.ProviderCategoryId = ServiceRequestCat.ProviderCategoryId;
                        SRC.ServiceRequestId = id;
                        SRC.LastUpdation = DateTime.Now;
                        SRC.BestTime = ServiceRequestCat.BestTime;
                        SRC.ServiceDate = ServiceRequestCat.ServiceDate;
                        db.ServiceRequestCategories.Add(SRC);
                        db.SaveChanges();
                        var SRC_Id = db.ServiceRequestCategories.Max(m => m.Id);
                        if (ServiceRequestCat.ServiceRequestDetails.Count()<1)
                        {
                            var updatedListing = getListing.FirstOrDefault(d => d.CategoryId == ServiceRequestCat.ProviderCategoryId);
                            if (updatedListing!=null)
                            {
                                updatedListing.ServiceRequestCatId = SRC_Id;
                                updatedListing.IsQueue = true;
                                db.SaveChanges();
                            }
                            
                        }
                        foreach (var IndividualListing in ServiceRequestCat.ServiceRequestDetails)
                        {
                            IndividualListing.ServiceRequestCatId = SRC_Id;
                            IndividualListing.LastUpdation = DateTime.Now;
                            db.ServiceRequestDetails.Add(IndividualListing);
                            db.SaveChanges();

                        }
                    }

                    return serviceRequest.ResultUrl + "," + id;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    System.IO.File.AppendAllLines(@"f:\agging.txt", outputLines);
                    throw;
                }
            }
        }
        public  bool UpdateServiceRequestById(ServiceRequest serviceRequest,List<SelectedCategoryDetails> SelectedCat)
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {

                    var _ServiceRequest = db.ServiceRequests.Find(serviceRequest.Id);
                    foreach (var serviceCat in _ServiceRequest.ServiceRequestCategories.ToList())
                    {
                        //remove only those category which user unchecked
                        if (!SelectedCat.Any(d=>d.catId==serviceCat.ProviderCategoryId))
                        {
                            foreach (var listingremove in serviceCat.ServiceRequestDetails.ToList())
                            {
                                var updateCancelRequest = new Inbox();
                                foreach (var inbox in listingremove.Inboxes.ToList())
                                {
                                    db.Inboxes.Remove(inbox);
                                }
                                if (listingremove.Inboxes.Count()>0)
                                {
                                  updateCancelRequest = listingremove.Inboxes.First();
                                }
                                                              
                                updateCancelRequest.IsRead = false;
                                updateCancelRequest.IsProviderArchive = false;
                                updateCancelRequest.IsArchive = false;
                                updateCancelRequest.MessageBody = "Consumer Remove Service Request";
                                updateCancelRequest.Subject = "Service Request Cancelled";
                                //db.ServiceRequestDetails.Remove(listingremove);
                                listingremove.IsActive = false;
                            }
                            //db.ServiceRequestCategories.Remove(serviceCat);
                            serviceCat.IsActive = false;
                            db.SaveChanges();
                        }
                        else
                        {
                            //if user update servicedate and best time
                            var GetCategotory = SelectedCat.FirstOrDefault(s=>s.catId== serviceCat.ProviderCategoryId);
                            serviceCat.ServiceDate =Convert.ToDateTime(GetCategotory.CatserviceDate);
                            serviceCat.BestTime = GetCategotory.CatBestTime;
                            db.SaveChanges();
                        }
                        
                        
                    }

                    foreach (var ServiceRequestCat in serviceRequest.ServiceRequestCategories)
                    {
                        var SRC = new ServiceRequestCategory();

                        SRC.ProviderCategoryId = ServiceRequestCat.ProviderCategoryId;
                        SRC.ServiceRequestId = _ServiceRequest.Id;
                        SRC.LastUpdation = DateTime.Now;
                        SRC.BestTime = ServiceRequestCat.BestTime;
                        SRC.ServiceDate = ServiceRequestCat.ServiceDate;
                        db.ServiceRequestCategories.Add(SRC);
                        db.SaveChanges();
                        var SRC_Id = db.ServiceRequestCategories.Max(m => m.Id);
                        foreach (var IndividualListing in ServiceRequestCat.ServiceRequestDetails)
                        {
                            IndividualListing.ServiceRequestCatId = SRC_Id;
                            IndividualListing.LastUpdation = DateTime.Now;
                            db.ServiceRequestDetails.Add(IndividualListing);
                            db.SaveChanges();

                        }
                    }

                    return true;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    System.IO.File.AppendAllLines(@"f:\agging.txt", outputLines);
                    throw;
                }
            }
        }
        public bool UpdateServiceRequest(ServiceRequest serviceRequest)
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {

                    var _ServiceRequest = db.ServiceRequests.Find(serviceRequest.Id);
                    _ServiceRequest.Address = serviceRequest.Address;
                    _ServiceRequest.City = serviceRequest.City;
                    //_ServiceRequest.ModifiedBy = serviceRequest.ModifiedBy;
                    //_ServiceRequest.ModifiedDate = serviceRequest.ModifiedDate;
                    _ServiceRequest.Email = serviceRequest.Email;
                    _ServiceRequest.FirstName = serviceRequest.FirstName;
                    _ServiceRequest.LastName = serviceRequest.LastName;
                    _ServiceRequest.PrimaryPhone = serviceRequest.PrimaryPhone;
                    _ServiceRequest.AlternatePhone = serviceRequest.AlternatePhone;
                    _ServiceRequest.ServiceDate = serviceRequest.ServiceDate;
                    _ServiceRequest.StateId = serviceRequest.StateId;
                    _ServiceRequest.ZipCode =Convert.ToInt32(serviceRequest.StateId);
                    _ServiceRequest.UserId = serviceRequest.UserId;
                    _ServiceRequest.Age = serviceRequest.Age;
                    _ServiceRequest.Gender = serviceRequest.Gender;
                    _ServiceRequest.TypeOfMedicalInsurence = serviceRequest.TypeOfMedicalInsurence;
                    _ServiceRequest.LastUpdation = DateTime.Now;
                    //if (serviceRequest.IsDirectContact)
                    //{
                    //    _ServiceRequest.ResultUrl = serviceRequest.ResultUrl;
                    //}
                    //else
                    //{
                    //    _ServiceRequest.ResultUrl = "";
                    //}


                    db.SaveChanges();
                    foreach (var serviceCat in _ServiceRequest.ServiceRequestCategories.ToList())
                    {

                        foreach (var listingremove in serviceCat.ServiceRequestDetails.ToList())
                        {
                            db.ServiceRequestDetails.Remove(listingremove);
                        }
                        db.ServiceRequestCategories.Remove(serviceCat);
                        db.SaveChanges();
                    }

                    foreach (var ServiceRequestCat in serviceRequest.ServiceRequestCategories)
                    {
                        var SRC = new ServiceRequestCategory();

                        SRC.ProviderCategoryId = ServiceRequestCat.ProviderCategoryId;
                        SRC.ServiceRequestId = _ServiceRequest.Id;
                        SRC.LastUpdation = DateTime.Now;
                        db.ServiceRequestCategories.Add(SRC);
                        db.SaveChanges();
                        var SRC_Id = db.ServiceRequestCategories.Max(m => m.Id);
                        foreach (var IndividualListing in ServiceRequestCat.ServiceRequestDetails)
                        {
                            IndividualListing.ServiceRequestCatId = SRC_Id;
                            IndividualListing.LastUpdation = DateTime.Now;
                            db.ServiceRequestDetails.Add(IndividualListing);
                            db.SaveChanges();

                        }
                    }

                    return true;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    System.IO.File.AppendAllLines(@"f:\agging.txt", outputLines);
                    throw;
                }
            }
        }

        public bool DeleteServiceRequest(int serviceId)
        {
            using (var db = new AgingInHomeContext())
            {
                var result = db.ServiceRequests.Find(serviceId);
                foreach (var serCat in result.ServiceRequestCategories.ToList())
                {
                    //foreach (var Detail in serCat.ServiceRequestDetails.ToList())
                   // {
                   //     var deleteRecord = db.ServiceRequestDetails.FirstOrDefault(d => d.ServiceRequestCatId == serCat.Id);
                   //     db.ServiceRequestDetails.Remove(deleteRecord);
                   // }
                    serCat.IsActive = false;
                    //db.ServiceRequestCategories.Remove(serCat);
                }
                result.IsActive = false;
                //db.ServiceRequests.Remove(result);
                db.SaveChanges();
                return true;

            }

        }
        public bool CancelServiceRequest(int serviceId)
        {
            using (var db = new AgingInHomeContext())
            {
                var result = db.ServiceRequests.Find(serviceId);
                foreach (var serCat in result.ServiceRequestCategories.ToList())
                {
                    //foreach (var Detail in serCat.ServiceRequestDetails.ToList())
                    // {
                    //     var deleteRecord = db.ServiceRequestDetails.FirstOrDefault(d => d.ServiceRequestCatId == serCat.Id);
                    //     db.ServiceRequestDetails.Remove(deleteRecord);
                    // }
                    serCat.IsActive = false;
                    
                    //db.ServiceRequestCategories.Remove(serCat);
                }
                result.IsActive = false;
                //db.ServiceRequests.Remove(result);
                db.SaveChanges();
                return true;

            }

        }

        public bool UpdateInvitationStatus(int RequestId, int ProviderListingId, bool Status,int alter)
        {
            using (var db = new AgingInHomeContext())
            {
                var dbServiceRequest = db.ServiceRequests.Find(RequestId);
                if (dbServiceRequest != null)
                {
                    var ProviderListing = db.ProviderListings.Find(ProviderListingId);
                    if (ProviderListing != null)
                    {
                        //var ServiceDetailRecord = dbServiceRequest.ServiceRequestCategories.Where(g => g.ServiceRequestDetails.FirstOrDefault(h => h.ProviderListingId == ProviderListingId)).FirstOrDefault();
                        var requestCat = dbServiceRequest.ServiceRequestCategories.FirstOrDefault(r => r.ProviderCategoryId == ProviderListing.ProviderCategory1.Id);
                        if (requestCat != null)
                        {
                            var serviceDetailRecord = requestCat.ServiceRequestDetails.FirstOrDefault(d => d.ProviderListingId == ProviderListingId);


                            if (serviceDetailRecord != null)
                            {
                                serviceDetailRecord.IsProviderInvite = Status;
                                serviceDetailRecord.Mailrespose = true;
                                var msg = new Inbox();
                                msg.IsRead = false;
                                msg.IsArchive = false;
                                msg.IsProviderArchive = false;
                                if (alter==1)
                                {
                                    msg.MessageBody = "I am not available In this date i offering you in this date 23/12/2026.If you wants to takes My services Thanks";
                                    msg.Subject = "Provider Send You offer";
                                }
                                else if (Status)
                                {
                                    msg.MessageBody = "Your invitation successfully Accepted";
                                    msg.Subject = "invitation successfully Accepted";
                                }
                                else if(!Status)
                                {
                                    msg.MessageBody = "Your invitation has been Rejected";
                                    msg.Subject = "invitation Rejected";
                                }
                                
                                msg.SenderId = ProviderListing.UserId;
                                msg.RecipientId = dbServiceRequest.UserId;
                                msg.SentDate = DateTime.Now;
                                msg.ServiceRequestDetailsId = serviceDetailRecord.Id;
                                db.Inboxes.Add(msg);
                                db.SaveChanges();
                                return true;
                            }
                        }

                    }
                }
                return false;
            }
        }

        public bool CancelCategoryRequest(int catId)
        {
            using (var db=new AgingInHomeContext())
            {
                var result=db.ServiceRequestCategories.Find(catId);
                foreach (var Catdetails in result.ServiceRequestDetails.ToList())
                {
                    foreach (var item in Catdetails.Inboxes)
                    {
                        //db.Inboxes.Remove(item);
                    }
                    //db.ServiceRequestDetails.Remove(Catdetails);
                    Catdetails.IsActive = false;
                }
                //db.ServiceRequestCategories.Remove(result);
                result.IsActive = false;
                var count = db.ServiceRequestCategories.Where(d=>d.ServiceRequestId==result.ServiceRequestId).ToList().Count();
                if (count<2)
                {
                    var Dbdata=db.ServiceRequests.Find(result.ServiceRequestId);
                    //db.ServiceRequests.Remove(Dbdata);
                    Dbdata.IsActive = false;
                }
                db.SaveChanges();
                return true;
            }
        }

        public ServiceRequest GetServiceRequestById(int serviceId)
        {
            using (var db=new AgingInHomeContext())
            {
                var _ServiceRequest = db.ServiceRequests.Include("ServiceRequestCategories").Include("ServiceRequestCategories.ServiceRequestDetails")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.Inboxes").FirstOrDefault(d=>d.Id==serviceId);
                return _ServiceRequest;
            }
        }

        //public ServiceRequest GetServiceRequest(int serviceId)
        //{
        //    using (var db=new AgingInHomeContext())
        //    {
        //        //var result=db.ser
        //    }
        //}
    }
}
