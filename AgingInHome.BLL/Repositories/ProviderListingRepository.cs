using AgingInHome.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgingInHome.BLL.Repositories
{
    public interface IProviderListingRepository
    {
        int AddProviderListing(ProviderListing _ProviderListing);
        bool UpdateProviderListng(ProviderListing _ProviderListing);
        void updateUserid(string userid, int listingId);
        ProviderListing GetListingByUserId(string UserId);
        ProviderListing GetListingByListingId(int ListingId);
        List<ProviderListing> GetAllListing();
        bool UpdateListingStatus(int listingId, int listingStatus);
        int addProviderImage(ProviderImageGalary img);
        int addProvidervideo(ProviderVideo video);
        bool DelProviderimage(int imgid);
        bool DelProvidervideo(int videoid);
        bool DelProviderTeam(Guid teamid);

        List<ServiceRequest> GetServiceRequestByUserId(string UserId);
        List<ServiceRequest> GetAllServiceRequest();
        bool SaveAboutUsDetails(string whatWeDo, string whyWeDo, string whoWeAre, int ProviderListingId);
        List<ssp_GetProviderBlogData_Result> GetProviderBlogData(int PageNumber, int PageSize, string UserId);
        List<ssp_GetProviderNewsData_Result> GetProviderNewsData(int PageNumber, int PageSize, string UserId);
        List<ssp_GetProviderPhotosData_Result> GetProviderPhotosData(int PageNumber, int PageSize, string UserId);
        List<ssp_GetProviderVideosData_Result> GetProviderVideosData(int PageNumber, int PageSize, string UserId);
        List<ssp_GetProviderTeamData_Result> GetProviderTeamData(int PageNumber, int PageSize, string UserId);
        ssp_GetBlogNewsDetails_Result GetBlogNewsDetails(int BlogNewsId, bool IsBlog);
        ListingBlogComment SaveBlogNewComment(ListingBlogComment _model);
        List<ListingBlogComment> GetBlogNewsComments(int BlogNewsId, bool IsBlog);
        List<ServiceRequest> GetConsumerServiceRequest(string UserId);
        ProviderListing GetProviderInboxMailDetails(string UserId);
    }
    public class ProviderListingClass : IProviderListingRepository
    {
        public int addProviderImage(ProviderImageGalary img)
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {
                    db.ProviderImageGalaries.Add(img);
                    db.SaveChanges();
                    return img.Id;

                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public int AddProviderListing(ProviderListing _ProviderListing)
        {
            using (var db = new AgingInHomeContext())
            {
                var ListingId = 0;
                try
                {
                    _ProviderListing.CreatedBy = "New User";
                    _ProviderListing.CreatedDate = DateTime.Now;
                    _ProviderListing.ListingDate = DateTime.Now;
                    _ProviderListing.IsApproved = 0;
                    db.ProviderListings.Add(_ProviderListing);
                    db.SaveChanges();
                    ListingId = db.ProviderListings.Max(m => m.ProviderListingId);
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

                return ListingId;
            }
        }

        public int addProvidervideo(ProviderVideo video)
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {
                    db.ProviderVideos.Add(video);
                    db.SaveChanges();
                    return video.Id;

                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public bool DelProviderimage(int imgid)
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {
                    var dbImg = db.ProviderImageGalaries.Find(imgid);
                    db.ProviderImageGalaries.Remove(dbImg);
                    db.SaveChanges();
                    return true;

                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public bool DelProvidervideo(int videoid)
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {
                    var dbvideo = db.ProviderVideos.Find(videoid);
                    db.ProviderVideos.Remove(dbvideo);
                    db.SaveChanges();
                    return true;

                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public bool DelProviderTeam(Guid teamid)
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {
                    var dbvideo = db.ProviderTeams.Find(teamid);
                    db.ProviderTeams.Remove(dbvideo);
                    db.SaveChanges();
                    return true;

                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public List<ProviderListing> GetAllListing()
        {
            using (var db = new AgingInHomeContext())
            {
                var dbAllListing = db.ProviderListings.Include("HourOfOperation")
                    .Include("ProviderImageGalaries").Include("ProviderVideos")
                    .Include("ServiceAreas").Include("ProviderCategory1").Include("ServiceRequestDetails")
                    .Include("AspNetUser").Include("AspNetUser").Include("ServiceAreas.UsState")
                    .Include("RatingTables").Include("ListingAboutUs").Include("ListingArticles").Include("ProviderTeams")
                    .Include("ServiceDetails")
                    .Include("ListingBlogs").ToList();
                return dbAllListing;

            }
        }

        //Created By:Ishan
        public List<ProviderListing> GetAllListingNew(int pageNumber,int pageSize,out int TotalRecords)
        {
            List<ProviderListing> ListProviderListing = new List<ProviderListing>();
            using (var db = new AgingInHomeContext())
            {
                ProviderListing ProviderListing= null;
                ObjectParameter totalrecords = new ObjectParameter("TotalRecords",typeof(int));
                List<usp_GetProviderListingRequest_Result> _result = new List<usp_GetProviderListingRequest_Result>();
                _result = db.usp_GetProviderListingRequest(pageNumber, pageSize, totalrecords).ToList();

                foreach(usp_GetProviderListingRequest_Result provider in _result)
                {
                    ProviderListing = new DAL.ProviderListing();
                    ProviderListing.CompanyName = provider.CompanyName;
                    ProviderListing.ProviderListingId = provider.ProviderListingId;
                    ProviderListing.CreatedDate = provider.CreatedDate;
                    ProviderListing.IsApproved = provider.IsApproved;
                    ListProviderListing.Add(ProviderListing);
                }
                TotalRecords = (int)totalrecords.Value;
            }
            return ListProviderListing;
        }

        public ProviderListing GetListingByListingId(int ListingId)
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.ProviderListings.Include("ProviderImageGalaries").Include("ProviderVideos").Include("HourOfOperation").Include("ProviderTeams")
                    .Include("ServiceAreas").Include("ProviderCategory1").Include("AspNetUser").Include("ServiceAreas.UsState").Include("RatingTables")
                    .Include("ListingAboutUs").Include("ListingArticles").Include("ListingBlogs")
                    .Include("ServiceRequestDetails").Include("ServiceDetails")
                    .Where(h => h.ProviderListingId == ListingId).FirstOrDefault();
                if (dbListing != null)
                {
                    return dbListing;
                }
                return new ProviderListing();

            }
        }

        public ProviderListing GetListingByListingName(string ListingName)
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.ProviderListings.Include("HourOfOperation").Include("ProviderTeams").Include("ProviderVideos").Include("ProviderImageGalaries")
                    .Include("ListingArticles").Include("ListingBlogs")
                    .Include("ServiceAreas").Include("ProviderCategory1").Include("AspNetUser").Include("ServiceAreas.UsState").Include("RatingTables")
                    .Include("ListingAboutUs")
                    .Include("ServiceRequestDetails").Include("ServiceDetails")
                    .Where(h => h.CompanyName == ListingName).FirstOrDefault();
                if (dbListing != null)
                {
                    dbListing.ProviderTeams = dbListing.ProviderTeams.Where(c => c.IsActive == true && c.ListingId == dbListing.ProviderListingId).ToList();
                    dbListing.ProviderVideos = dbListing.ProviderVideos.Where(c => c.IsActive == true && c.ProviderListingId == dbListing.ProviderListingId).ToList();
                    dbListing.ProviderImageGalaries = dbListing.ProviderImageGalaries.Where(c => c.IsActive == true && c.ProviderListingId == dbListing.ProviderListingId).ToList();
                    dbListing.ListingArticles = dbListing.ListingArticles.Where(c => c.IsActive == true && c.ListingId == dbListing.ProviderListingId).ToList();
                    dbListing.ListingBlogs = dbListing.ListingBlogs.Where(c => c.IsActive == true && c.ListingId == dbListing.ProviderListingId).ToList();
                    return dbListing;
                }
                return new ProviderListing();

            }
        }

        public ProviderListing GetListingByUserId(string UserId)
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.ProviderListings.Include("HourOfOperation").Include("ProviderImageGalaries").Include("ProviderVideos")
                    .Include("ServiceAreas").Include("ProviderCategory1").Include("AspNetUser").Include("ServiceAreas.UsState")
                    .Include("RatingTables").Include("ListingAboutUs").Include("ProviderTeams").Include("ListingArticles").Include("ListingBlogs")
                    .Include("ServiceRequestDetails")
                    .Include("ServiceDetails")
                    .Include("ServiceRequestDetails.ProviderListing")
                   .Include("ServiceRequestDetails.ProviderListing.FavoriteDetails")
                   .Include("ServiceRequestDetails.ProviderListing.ListingAboutUs")
                   .Include("ServiceRequestDetails.ProviderListing.ListingArticles")
                   .Include("ServiceRequestDetails.ProviderListing.ListingBlogs")
                   .Include("ServiceRequestDetails.ProviderListing.ProviderVideos")
                   .Include("ServiceRequestDetails.ProviderListing.RatingTables")
                   .Include("ServiceRequestDetails.ProviderListing.ServiceAreas")
                   .Include("ServiceRequestDetails.ProviderListing.ServiceDetails")
                   .Include("ServiceRequestDetails.ProviderListing.ServiceAreas.UsState")
                   .Include("ServiceRequestDetails.ServiceRequestCategory")
                   .Include("ServiceRequestDetails.ProviderListing.AspNetUser")
                   .Include("ServiceRequestDetails.ServiceRequestCategory.ProviderCategory")
                   .Include("ServiceRequestDetails.ServiceRequestCategory.ServiceRequest")
                   .Include("ServiceRequestDetails.ServiceRequestCategory.ServiceRequest.ServiceRequestCategories")
                   .Include("ServiceRequestDetails.Inboxes")
                    //.Include("ServiceRequestDetails.Inboxes.ServiceRequestDetail")
                    .Where(h => h.UserId == UserId).FirstOrDefault();

                //dbListing.ServiceRequestDetails = (ICollection<ServiceRequestDetail>)new ProviderListingClass().GetServiceRequestByUserId(UserId).Select(s=>s.); ;
                return dbListing;

            }

        }

        public List<ServiceRequest> GetServiceRequestByUserId(string UserId)
        {
            List<ServiceRequest> Servicerequest = new List<ServiceRequest>();
            using (var db = new AgingInHomeContext())
            {
                Servicerequest = db.ServiceRequests.Include("ServiceRequestCategories")
                    .Include("ServiceRequestCategories.ProviderCategory").Include("ServiceRequestCategories.ServiceRequest")
                    .Include("ServiceRequestCategories.ServiceRequestDetails")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.HourOfOperation")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ProviderImageGalaries")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ProviderVideos")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ServiceAreas")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ProviderCategory1")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ServiceAreas.UsState")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.RatingTables")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.AspNetUser")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ListingAboutUs")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ListingArticles")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ServiceDetails")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ListingBlogs")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.Inboxes")
                    .Where(u => u.UserId == UserId).ToList();

            }
            return Servicerequest;
        }

        public List<ServiceRequest> GetAllServiceRequest()
        {
            using (var db = new AgingInHomeContext())
            {
                var Servicerequest = db.ServiceRequests.Include("ServiceRequestCategories")
                     .Include("ServiceRequestCategories.ProviderCategory").Include("ServiceRequestCategories.ServiceRequest")
                     .Include("ServiceRequestCategories.ServiceRequestDetails")
                     .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing")
                     .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.HourOfOperation")
                     .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ProviderImageGalaries")
                     .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ProviderVideos")
                     .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ServiceAreas")
                     .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ProviderCategory1")
                     .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ServiceAreas.UsState")
                     .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.RatingTables")
                     .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.AspNetUser")
                     .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ListingAboutUs")
                     .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ListingArticles")
                    .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ServiceDetails")
                     .Include("ServiceRequestCategories.ServiceRequestDetails.ProviderListing.ListingBlogs")
                     .Include("ServiceRequestCategories.ServiceRequestDetails.Inboxes").ToList();
                return Servicerequest;
            }
        }

        public bool UpdateListingStatus(int listingId, int listingStatus)
        {
            using (var db = new AgingInHomeContext())
            {
                var GetListing = db.ProviderListings.Find(listingId);
                GetListing.IsApproved = listingStatus;
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateProviderListng(ProviderListing _ProviderListing)
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.ProviderListings.Include("ServiceRequestDetails").Include("HourOfOperation").Include("ProviderImageGalaries")
                                .Include("ProviderVideos").Include("ServiceAreas").Include("ProviderCategory1").Include("AspNetUser").Include("AspNetUser")
                    .Include("ServiceDetails")
                                .Include("ServiceAreas.UsState").Include("ListingAboutUs").Include("ListingArticles").Include("ListingBlogs")
                                .Where(h => h.ProviderListingId == _ProviderListing.ProviderListingId).FirstOrDefault();

                dbListing.CommunicationEmail = _ProviderListing.CommunicationEmail;
                dbListing.Experience = _ProviderListing.Experience;
                dbListing.FacebookURL = _ProviderListing.FacebookURL;
                dbListing.GoogleURL = _ProviderListing.GoogleURL;
                dbListing.HourOfOperation.MonStart = _ProviderListing.HourOfOperation.MonStart;
                dbListing.HourOfOperation.MonEnd = _ProviderListing.HourOfOperation.MonEnd;
                dbListing.HourOfOperation.ThuStart = _ProviderListing.HourOfOperation.ThuStart;
                dbListing.HourOfOperation.ThuEnd = _ProviderListing.HourOfOperation.ThuEnd;
                dbListing.HourOfOperation.WedStart = _ProviderListing.HourOfOperation.WedStart;
                dbListing.HourOfOperation.WedEnd = _ProviderListing.HourOfOperation.WedEnd;
                dbListing.HourOfOperation.TueStart = _ProviderListing.HourOfOperation.TueStart;
                dbListing.HourOfOperation.TueEnd = _ProviderListing.HourOfOperation.TueEnd;
                dbListing.HourOfOperation.FriStart = _ProviderListing.HourOfOperation.FriStart;
                dbListing.HourOfOperation.FriEnd = _ProviderListing.HourOfOperation.FriEnd;
                dbListing.HourOfOperation.SatStart = _ProviderListing.HourOfOperation.SatStart;
                dbListing.HourOfOperation.SatEnd = _ProviderListing.HourOfOperation.SatEnd;
                dbListing.HourOfOperation.SunStart = _ProviderListing.HourOfOperation.SunStart;
                dbListing.HourOfOperation.SunEnd = _ProviderListing.HourOfOperation.SunEnd;
                dbListing.CategoryServiceId = _ProviderListing.CategoryServiceId;
                if (_ProviderListing.Logo != null)
                {
                    dbListing.Logo = _ProviderListing.Logo;
                }
                dbListing.ModifiedDate = DateTime.Now;
                dbListing.ProviderAddress = _ProviderListing.ProviderAddress;
                dbListing.ProviderContactNumber = _ProviderListing.ProviderContactNumber;
                dbListing.ServiceDescription = _ProviderListing.ServiceDescription;
                dbListing.ServicePrice = _ProviderListing.ServicePrice;
                dbListing.ServicePriceType = _ProviderListing.ServicePriceType;
                dbListing.TwitterURL = _ProviderListing.TwitterURL;
                dbListing.WebsiteURL = _ProviderListing.WebsiteURL;
                dbListing.WhatWeDo = _ProviderListing.WhatWeDo;
                dbListing.WhenWeDo = _ProviderListing.WhenWeDo;
                dbListing.WhoWeAre = _ProviderListing.WhoWeAre;
                dbListing.WhyWeDo = _ProviderListing.WhyWeDo;
                dbListing.CompanyName = _ProviderListing.CompanyName;
                dbListing.City = _ProviderListing.City;
                dbListing.Zipcode = _ProviderListing.Zipcode;
                dbListing.StateId = _ProviderListing.StateId;

                foreach (var service in dbListing.ServiceAreas.ToList())
                {
                    db.ServiceAreas.Remove(service);
                    dbListing.ServiceAreas.Remove(service);
                }

                foreach (var _services in _ProviderListing.ServiceAreas.ToList())
                {
                    ServiceArea service = new ServiceArea();
                    service.City = _services.City;
                    service.ServiceRadius = _services.ServiceRadius;
                    service.StateId = _services.StateId;
                    service.ZipCode = _services.ZipCode;
                    service.ProviderListingId = _services.ProviderListingId;
                    dbListing.ServiceAreas.Add(service);
                }


                //if (dbListing.CategoryId == _ProviderListing.CategoryId)
                //{
                //    var removelists = new List<ServiceDetail>();
                //    foreach (var eachservicedetail in dbListing.ServiceDetails.ToList())
                //    {
                //        var updatedservice = _ProviderListing.ServiceDetails.FirstOrDefault(s => s.Id == eachservicedetail.Id);
                //        if (updatedservice != null)
                //        {
                //            eachservicedetail.IsSelected = updatedservice.IsSelected;
                //            eachservicedetail.ServiceType = updatedservice.ServiceType;
                //            eachservicedetail.ServicePrice = updatedservice.ServicePrice;
                //            eachservicedetail.CustomeService = updatedservice.CustomeService;
                //        }
                //        else
                //        {
                //            var removeservice = db.ServiceDetails.Where(s => s.Id == eachservicedetail.Id).FirstOrDefault();
                //            removelists.Add(removeservice);
                //        }

                //    }
                //    foreach (var removeservices in removelists.ToList())
                //    {
                //        db.ServiceDetails.Remove(removeservices);
                //    }
                //    foreach (var addnew in _ProviderListing.ServiceDetails.Where(s => s.Id == 0).ToList())
                //    {
                //        db.ServiceDetails.Add(new ServiceDetail()
                //        {
                //            CategoryServiceId = addnew.CategoryServiceId,
                //            IsSelected = addnew.IsSelected,
                //            ProviderListingId = _ProviderListing.ProviderListingId,
                //            ServicePrice = addnew.ServicePrice,
                //            ServiceType = addnew.ServiceType,
                //            CustomeService = addnew.CustomeService
                //        });
                //    }
                //}
                //else
                //{
                foreach (var eachservicedetail in dbListing.ServiceDetails.ToList())
                {
                    db.ServiceDetails.Remove(eachservicedetail);
                }
                foreach (var addnewServiceDetail in _ProviderListing.ServiceDetails.ToList())
                {
                    db.ServiceDetails.Add(new ServiceDetail()
                    {
                        CategoryServiceId = addnewServiceDetail.CategoryServiceId,
                        IsSelected = addnewServiceDetail.IsSelected,
                        ProviderListingId = _ProviderListing.ProviderListingId,
                        ServicePrice = addnewServiceDetail.ServicePrice,
                        ServiceType = addnewServiceDetail.ServiceType,
                        CustomeService = addnewServiceDetail.CustomeService
                    });

                }

                // }


                dbListing.CategoryId = _ProviderListing.CategoryId;
                db.SaveChanges();
                return true;

            }
        }

        public void updateUserid(string userid, int listingId)
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.ProviderListings.Find(listingId);
                dbListing.UserId = userid;
                db.SaveChanges();

            }

        }

        public string getProfileId(int listingId)
        {
            string profileId = string.Empty;
            using (var db = new AgingInHomeContext())
            {
                var analytics = db.Analytics.Where(h => h.ProviderListingId == listingId).FirstOrDefault();
                profileId = analytics == null ? "" : analytics.ProfileID;
            }
            return profileId;
        }

        public string getViewId(int listingId)
        {
            string viewId = string.Empty;
            using (var db = new AgingInHomeContext())
            {
                var analytics = db.Analytics.Where(h => h.ProviderListingId == listingId).FirstOrDefault();
                viewId = analytics == null ? "" : analytics.ViewID;
            }
            return viewId;
        }

        public List<ssp_GetProviderBlogData_Result> GetProviderBlogData(int PageNumber, int PageSize, string UserId)
        {
            using (var db = new AgingInHomeContext())
            {
                List<ssp_GetProviderBlogData_Result> _result = new List<ssp_GetProviderBlogData_Result>();
                _result = db.ssp_GetProviderBlogData(PageNumber, PageSize, UserId).ToList();
                return _result;
            }
        }

        public List<ssp_GetProviderNewsData_Result> GetProviderNewsData(int PageNumber, int PageSize, string UserId)
        {
            using (var db = new AgingInHomeContext())
            {
                List<ssp_GetProviderNewsData_Result> _result = new List<ssp_GetProviderNewsData_Result>();
                _result = db.ssp_GetProviderNewsData(PageNumber, PageSize, UserId).ToList();
                return _result;
            }
        }

        public List<ssp_GetProviderPhotosData_Result> GetProviderPhotosData(int PageNumber, int PageSize, string UserId)
        {
            using (var db = new AgingInHomeContext())
            {
                List<ssp_GetProviderPhotosData_Result> _result = new List<ssp_GetProviderPhotosData_Result>();
                _result = db.ssp_GetProviderPhotosData(PageNumber, PageSize, UserId).ToList();
                return _result;
            }
        }

        public List<ssp_GetProviderVideosData_Result> GetProviderVideosData(int PageNumber, int PageSize, string UserId)
        {
            using (var db = new AgingInHomeContext())
            {
                List<ssp_GetProviderVideosData_Result> _result = new List<ssp_GetProviderVideosData_Result>();
                _result = db.ssp_GetProviderVideosData(PageNumber, PageSize, UserId).ToList();
                return _result;
            }
        }

        public List<ssp_GetProviderTeamData_Result> GetProviderTeamData(int PageNumber, int PageSize, string UserId)
        {
            using (var db = new AgingInHomeContext())
            {
                List<ssp_GetProviderTeamData_Result> _result = new List<ssp_GetProviderTeamData_Result>();
                _result = db.ssp_GetProviderTeamData(PageNumber, PageSize, UserId).ToList();
                return _result;
            }
        }

        public bool SaveAboutUsDetails(string whatWeDo, string whyWeDo, string whoWeAre, int ProviderListingId)
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.ProviderListings.Where(c => c.ProviderListingId == ProviderListingId).FirstOrDefault();
                if (dbListing != null)
                {
                    dbListing.WhatWeDo = whatWeDo;
                    dbListing.WhyWeDo = whyWeDo;
                    dbListing.WhoWeAre = whoWeAre;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public int GetProviderListingIdbyUserId(string UserId)
        {
            int providerListingId = 0;
            using (var db = new AgingInHomeContext())
            {
                var analytics = db.ProviderListings.Where(h => h.UserId == UserId).FirstOrDefault();
                providerListingId = analytics == null ? 0 : analytics.ProviderListingId;
            }
            return providerListingId;
        }

        public List<ssp_GetProviderListAnalytic_Result> GetProviderListing()
        {
            using (var db = new AgingInHomeContext())
            {
                List<ssp_GetProviderListAnalytic_Result> dbAllListing = db.ssp_GetProviderListAnalytic().ToList();
                return dbAllListing;
            }
        }
        public ssp_GetBlogNewsDetails_Result GetBlogNewsDetails(int BlogNewsId, bool IsBlog)
        {
            using (var db = new AgingInHomeContext())
            {
                ssp_GetBlogNewsDetails_Result dbAllListing = db.ssp_GetBlogNewsDetails(BlogNewsId, IsBlog).FirstOrDefault();
                return dbAllListing;
            }
        }

        public ListingBlogComment SaveBlogNewComment(ListingBlogComment _model)
        {
            using (var db = new AgingInHomeContext())
            {
                db.ListingBlogComments.Add(_model);
                db.SaveChanges();
            }
            return _model;
        }

        public List<ListingBlogComment> GetBlogNewsComments(int BlogNewsId, bool IsBlog)
        {
            using (var db = new AgingInHomeContext())
            {
                return db.ListingBlogComments.Where(c => c.ListingBlogId == BlogNewsId && c.IsBlog == IsBlog && c.IsActive).ToList();
            }
        }

        public List<ServiceRequest> GetConsumerServiceRequest(string UserId)
        {
            using (var db = new AgingInHomeContext())
            {
                var servicerquest = db.ServiceRequests.Include("ServiceRequestCategories")
                   .Include("ServiceRequestCategories.ProviderCategory").Include("ServiceRequestCategories.ServiceRequest")
                   .Include("ServiceRequestCategories.ServiceRequestDetails")
                   .Include("ServiceRequestCategories.ServiceRequestDetails.Inboxes")
                   .Where(c => c.UserId == UserId).ToList();

                return servicerquest;
            }
        }

        public ProviderListing GetProviderInboxMailDetails(string UserId)
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.ProviderListings.Include("HourOfOperation").Include("ProviderImageGalaries").Include("ProviderVideos")
                    .Include("ServiceAreas").Include("ProviderCategory1").Include("AspNetUser").Include("ServiceAreas.UsState")
                    .Include("RatingTables").Include("ListingAboutUs").Include("ProviderTeams").Include("ListingArticles").Include("ListingBlogs")
                    .Include("ServiceDetails")
                    .Include("ServiceRequestDetails").Include("ServiceRequestDetails.Inboxes").Where(h => h.UserId == UserId).FirstOrDefault();
                return dbListing;
            }
        }
    }
}
