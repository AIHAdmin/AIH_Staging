using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;
using AgingInHome.DAL;
using AgingInHome.BLL.Repositories;
using AgingInHome.Helpers;
using AgingInHome.BLL.Enums;
using System.Web.Mvc;
using AgingInHome.DAL.Helper;

namespace AgingInHome.Models
{
    public class ServiceRequestModel
    {
        public ServiceRequestModel()
        {
            GenderList = new Dictionary<string, string>();
            GenderList.Add("Male", "Male");
            GenderList.Add("Female", "Female");
        }
        public Dictionary<string, string> GenderList { get; set; }
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Address is Required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@/#$%&/=?_.,:;\\-]).+$", ErrorMessage = "Your Password must have one capital letter one number and one special character")]
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password did not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "City is Required")]

        public string City { get; set; }
        [Required(ErrorMessage = "State is Required")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "ZipCode is Required")]
        //[MinLength(5,ErrorMessage = "Zipcode must be 5 digit")]
        [Range(10000, 99999, ErrorMessage = "Zipcode must be 5 digit")]

        public int ZipCode { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Please enter valid Email")]
        [Remote("UserEmailExits", "ProviderListing", ErrorMessage = "Email address already used")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Primary Phone is Required")]
        public string PrimaryPhone { get; set; }
        public string AlternatePhone { get; set; }
        [Required(ErrorMessage = "ServiceDate is Required")]
        public DateTime ServiceDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public List<string> SelectedCategory { get; set; }
        public string Categorynames { get; set; }
        public bool IsDirectContact { get; set; }
        public string UserId { get; set; }
        public string ResultUrl { get; set; }
        public string Relation { get; set; }
        [Required(ErrorMessage = "Age is Required")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Gender is Required")]
        public string Gender { get; set; }
        public string TypeOfMedicalInsurence { get; set; }
        public string BestTime { get; set; }

        public long TotalRecords { get; set; }
        public Nullable<int> RequestStatus { get; set; }

        public Nullable<System.DateTime> LastUpdation { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public List<ServiceRequestCategory> ServiceRequestCategories { get; set; }

        public string AddServiceRequest(ServiceRequestModel _ServiceRequestModel)
        {
            _ServiceRequestModel.ServiceDate = Convert.ToDateTime(_ServiceRequestModel.ServiceDate);
            _ServiceRequestModel.ResultUrl = "/ProviderListing/SearchListing?Location=" + _ServiceRequestModel.ZipCode.ToString();
            var ListingAll = new ProviderListingModel().GetAllListing().Where(j => j.IsApproved == (int)ListingStatus.Accepted).ToList();
            var allCategories = new ProviderListingModel().AllCategory();
            var model = Mapper.Map<ServiceRequest>(_ServiceRequestModel);
            model.CreatedDate = DateTime.Now;
            if (_ServiceRequestModel.ServiceDate == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
            {
                //set service date when request comes from home page
                _ServiceRequestModel.ServiceDate = DateTime.Now;
                model.ServiceDate = DateTime.Now;
                model.RequestDate = DateTime.Now;
            }


            foreach (var cat in _ServiceRequestModel.SelectedCategory)
            {
                model.ServiceRequestCategories.Add(new ServiceRequestCategory() { ProviderCategoryId = Convert.ToInt32(cat) });
                model.ResultUrl = model.ResultUrl + "&selectedCategory=" + cat;
                var CurrentListingWithinCat = ListingAll.Where(j => j.ProviderCategory1.Id == Convert.ToInt32(cat) && j.IsApproved == (int)ListingStatus.Accepted).ToList();

                if (CurrentListingWithinCat == null || CurrentListingWithinCat.Count == 0)
                {
                    var categoryName = allCategories.FirstOrDefault(f => f.Id == Convert.ToInt32(cat)).CategoryName;
                    var newRequiredListing = new RequiedListing() { CategoryId = Convert.ToInt32(cat), City = _ServiceRequestModel.City, Is7daysWait = null, RequestDate = DateTime.Now, ServiceDate = _ServiceRequestModel.ServiceDate, Zipcode = _ServiceRequestModel.ZipCode, UserId = _ServiceRequestModel.UserId };
                    var RequiredListingId = new RequiredListingClass().addRequiredListing(newRequiredListing);
                    EmailSender.SendEmailFor7daysWait(_ServiceRequestModel.Email, categoryName, RequiredListingId);
                }
                foreach (var currentListingCat in CurrentListingWithinCat)
                {
                    var _currentListingCat = new ServiceRequestDetail();
                    _currentListingCat.IsActive = true;
                    _currentListingCat.IsConsumerAcceptInvite = false;
                    _currentListingCat.IsProviderInvite = false;
                    _currentListingCat.IsWaitMore7Days = false;
                    _currentListingCat.Mailrespose = false;
                    _currentListingCat.ProviderListingId = currentListingCat.ProviderListingId;
                    model.ServiceRequestCategories.Last().ServiceRequestDetails.Add(_currentListingCat);


                }
            }


            return new ServiceRequestRepository().AddServiceRequest(model,new List<int>());
        }
        public string SubmitServiceRequest(List<ServiceSelectedCatDetails> Requests, ConsumerProfile userdetail)
        {
            // we need to update RequestserviceCatid in required listing table after save service request and get serviceRequestCat id
            var requiedListingIds =new List<int>();
            //end

            ServiceRequestModel _ServiceRequestModel = Mapper.Map<ServiceRequestModel>(userdetail);
            _ServiceRequestModel.ServiceDate = Convert.ToDateTime(_ServiceRequestModel.ServiceDate);
            _ServiceRequestModel.ResultUrl = "/ProviderListing/SearchListing?Location=" + _ServiceRequestModel.ZipCode.ToString();
            var ListingAll = new ProviderListingModel().GetAllListing().Where(j => j.IsApproved == (int)ListingStatus.Accepted).ToList();
            var allCategories = new ProviderListingModel().AllCategory();
            var model = Mapper.Map<ServiceRequest>(_ServiceRequestModel);
            model.CreatedDate = DateTime.Now;
            if (_ServiceRequestModel.ServiceDate == Convert.ToDateTime("1/1/0001 12:00:00 AM"))
            {
                //set service date when request comes from home page
                _ServiceRequestModel.ServiceDate = DateTime.Now;
                model.ServiceDate = DateTime.Now;
                model.RequestDate = DateTime.Now;
            }


            foreach (var cat in Requests)
            {
                model.ServiceRequestCategories.Add(new ServiceRequestCategory()
                {
                    ProviderCategoryId = Convert.ToInt32(cat.catId)
                    ,
                    ServiceDate = cat.CatserviceDate == "" ? DateTime.Now : Convert.ToDateTime(cat.CatserviceDate)
                    ,
                    BestTime = cat.CatBestTime
                });
                model.ResultUrl = model.ResultUrl + "&selectedCategory=" + cat.catId;
                var CurrentListingWithinCat = ListingAll.Where(j => j.ProviderCategory1.Id == Convert.ToInt32(cat.catId) && j.IsApproved == (int)ListingStatus.Accepted).ToList();

                if (CurrentListingWithinCat == null || CurrentListingWithinCat.Count == 0)
                {
                    var categoryName = allCategories.FirstOrDefault(f => f.Id == Convert.ToInt32(cat.catId)).CategoryName;
                    var newRequiredListing = new RequiedListing() { CategoryId = Convert.ToInt32(cat.catId), Is7daysWait = null, RequestDate = DateTime.Now, ServiceDate = _ServiceRequestModel.ServiceDate, Zipcode = userdetail.Zipcode, UserId = userdetail.UserId };
                    var RequiredListingId = new RequiredListingClass().addRequiredListing(newRequiredListing);
                    requiedListingIds.Add(RequiredListingId);
                    EmailSender.SendEmailFor7daysWait(userdetail.AspNetUser.Email, categoryName, RequiredListingId);
                }
                foreach (var currentListingCat in CurrentListingWithinCat.OrderByDescending(s=>s.ProviderListingId).ToList())
                {
                    var _currentListingCat = new ServiceRequestDetail();
                    _currentListingCat.IsActive = true;
                    _currentListingCat.IsConsumerAcceptInvite = false;
                    _currentListingCat.IsProviderInvite = false;
                    _currentListingCat.IsWaitMore7Days = false;
                    _currentListingCat.Mailrespose = false;
                    _currentListingCat.ProviderListingId = currentListingCat.ProviderListingId;
                    model.ServiceRequestCategories.Last().ServiceRequestDetails.Add(_currentListingCat);
                    break;

                }
            }
            return new ServiceRequestRepository().AddServiceRequest(model,requiedListingIds);

        }
        public bool DeleteServiceRequest(int serviceId)
        {
            return new ServiceRequestRepository().DeleteServiceRequest(serviceId);
        }
        public bool CancelServiceRequest(int serviceId)
        {
            return new ServiceRequestRepository().CancelServiceRequest(serviceId);
        }
        public bool UpdateServiceRequest(ServiceRequestModel _ServiceRequestModel)
        {
            _ServiceRequestModel.ServiceDate = Convert.ToDateTime(_ServiceRequestModel.ServiceDate);
            _ServiceRequestModel.ResultUrl = "/ProviderListing/SearchListing?Location=" + _ServiceRequestModel.ZipCode.ToString();
            var ListingAll = new ProviderListingModel().GetAllListing().Where(j => j.IsApproved == (int)ListingStatus.Accepted).ToList();
            var allCategories = new ProviderListingModel().AllCategory();
            var model = Mapper.Map<ServiceRequest>(_ServiceRequestModel);
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = _ServiceRequestModel.UserId;


            foreach (var cat in _ServiceRequestModel.SelectedCategory)
            {
                model.ServiceRequestCategories.Add(new ServiceRequestCategory() { ProviderCategoryId = Convert.ToInt32(cat) });
                model.ResultUrl = model.ResultUrl + "&selectedCategory=" + cat;
                var CurrentListingWithinCat = ListingAll.Where(j => j.ProviderCategory1.Id == Convert.ToInt32(cat) && j.IsApproved == (int)ListingStatus.Accepted).ToList();

                if (CurrentListingWithinCat == null || CurrentListingWithinCat.Count == 0)
                {
                    var categoryName = allCategories.FirstOrDefault(f => f.Id == Convert.ToInt32(cat)).CategoryName;
                    var newRequiredListing = new RequiedListing() { CategoryId = Convert.ToInt32(cat), City = _ServiceRequestModel.City, Is7daysWait = null, RequestDate = DateTime.Now, ServiceDate = _ServiceRequestModel.ServiceDate, Zipcode = _ServiceRequestModel.ZipCode, UserId = _ServiceRequestModel.UserId };
                    var RequiredListingId = new RequiredListingClass().addRequiredListing(newRequiredListing);
                    EmailSender.SendEmailFor7daysWait(_ServiceRequestModel.Email, categoryName, RequiredListingId);
                }
                foreach (var currentListingCat in CurrentListingWithinCat)
                {
                    var _currentListingCat = new ServiceRequestDetail();
                    _currentListingCat.IsActive = true;
                    _currentListingCat.IsConsumerAcceptInvite = false;
                    _currentListingCat.IsProviderInvite = false;
                    _currentListingCat.IsWaitMore7Days = false;
                    _currentListingCat.Mailrespose = false;
                    _currentListingCat.ProviderListingId = currentListingCat.ProviderListingId;
                    model.ServiceRequestCategories.Last().ServiceRequestDetails.Add(_currentListingCat);


                }
            }


            return new ServiceRequestRepository().UpdateServiceRequest(model);
        }

        public bool UpdateInvitationStatus(int RequestId, int ProviderListingId, bool Status,int alter)
        {
            new ServiceRequestRepository().UpdateInvitationStatus(RequestId, ProviderListingId, Status, alter);
            return true;
        }
        public bool CancelCategoryRequest(int CatId)
        {
            return new ServiceRequestRepository().CancelCategoryRequest(CatId);
        }
        public string UpdateServiceRequest(List<ServiceSelectedCatDetails> Requests, ConsumerProfile userdetail)
        {
            var providerlistingids = "";
            ServiceRequestModel _ServiceRequestModel = Mapper.Map<ServiceRequestModel>(userdetail);
            _ServiceRequestModel.ServiceDate = Convert.ToDateTime(_ServiceRequestModel.ServiceDate);
            _ServiceRequestModel.ResultUrl = "/ProviderListing/SearchListing?Location=" + _ServiceRequestModel.ZipCode.ToString();
            var ListingAll = new ProviderListingModel().GetAllListing().Where(j => j.IsApproved == (int)ListingStatus.Accepted).ToList();
            var allCategories = new ProviderListingModel().AllCategory();
            var model = Mapper.Map<ServiceRequest>(_ServiceRequestModel);
            model.ModifiedDate = DateTime.Now;
            model.ModifiedBy = _ServiceRequestModel.UserId;
            model.Id = Requests.First().ServiceRequestId;
            //Get Existing Data Against ServiceRequest Which I update
            var existingServiceRequestData = new ServiceRequestRepository().GetServiceRequestById(Requests.First().ServiceRequestId);
            //End

            foreach (var cat in Requests)
            {
                //mail will not be send and not add in Required listing table if user select same category again
                if (!existingServiceRequestData.ServiceRequestCategories.Any(d => d.ProviderCategoryId == cat.catId &&d.IsActive!=false))
                {
                    model.ServiceRequestCategories.Add(new ServiceRequestCategory() {
                        ProviderCategoryId = Convert.ToInt32(cat.catId)
                    ,
                        ServiceDate = cat.CatserviceDate == "" ? DateTime.Now : Convert.ToDateTime(cat.CatserviceDate)
                    ,
                        BestTime = cat.CatBestTime
                    });
                    model.ResultUrl = model.ResultUrl + "&selectedCategory=" + cat;
                    var CurrentListingWithinCat = ListingAll.Where(j => j.ProviderCategory1.Id == Convert.ToInt32(cat.catId) && j.IsApproved == (int)ListingStatus.Accepted).ToList();

                    if (CurrentListingWithinCat == null || CurrentListingWithinCat.Count == 0)
                    {

                        var categoryName = allCategories.FirstOrDefault(f => f.Id == Convert.ToInt32(cat.catId)).CategoryName;
                        var newRequiredListing = new RequiedListing() { CategoryId = Convert.ToInt32(cat.catId), City = _ServiceRequestModel.City, Is7daysWait = null, RequestDate = DateTime.Now,
                            ServiceDate =Convert.ToDateTime(cat.CatserviceDate), Zipcode = _ServiceRequestModel.ZipCode, UserId = _ServiceRequestModel.UserId };
                        var RequiredListingId = new RequiredListingClass().addRequiredListing(newRequiredListing);
                        EmailSender.SendEmailFor7daysWait(userdetail.AspNetUser.Email, categoryName, RequiredListingId);


                    }
                    foreach (var currentListingCat in CurrentListingWithinCat)
                    {
                        if (!existingServiceRequestData.ServiceRequestCategories.Any(d => d.ProviderCategoryId == cat.catId))
                        {
                            var _currentListingCat = new ServiceRequestDetail();
                            _currentListingCat.IsActive = true;
                            _currentListingCat.IsConsumerAcceptInvite = false;
                            _currentListingCat.IsProviderInvite = false;
                            _currentListingCat.IsWaitMore7Days = false;
                            _currentListingCat.Mailrespose = false;
                           
                            _currentListingCat.ProviderListingId = currentListingCat.ProviderListingId;
                            model.ServiceRequestCategories.Last().ServiceRequestDetails.Add(_currentListingCat);
                            providerlistingids = providerlistingids + "," + currentListingCat.ProviderListingId;


                        }

                    }
                }
            }
           
            //pass selected category details
            var obj = Mapper.Map<List<SelectedCategoryDetails>>(Requests);
            new ServiceRequestRepository().UpdateServiceRequestById(model, obj);
            return providerlistingids;
        }
    }
}