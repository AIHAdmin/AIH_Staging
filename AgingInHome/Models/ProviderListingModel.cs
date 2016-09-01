using AgingInHome.BLL.Repositories;
using AgingInHome.DAL;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AgingInHome.Controllers;
using System.Web.Mvc;


namespace AgingInHome.Models
{
    public class ProviderListingModel
    {
        public ProviderListingModel()
        {
            OperationHours = new Dictionary<string, string>();
            OperationHours.Add("12AM", "12AM");
            OperationHours.Add("1AM", "1AM");
            OperationHours.Add("2AM", "2AM");
            OperationHours.Add("3AM", "3AM");
            OperationHours.Add("4AM", "4AM");
            OperationHours.Add("5AM", "5AM");
            OperationHours.Add("6AM", "6AM");
            OperationHours.Add("7AM", "7AM");
            OperationHours.Add("8AM", "8AM");
            OperationHours.Add("9AM", "9AM");
            OperationHours.Add("10AM", "10AM");
            OperationHours.Add("11AM", "11AM");
            OperationHours.Add("12PM", "12PM");
            OperationHours.Add("1PM", "1PM");
            OperationHours.Add("2PM", "2PM");
            OperationHours.Add("3PM", "3PM");
            OperationHours.Add("4PM", "4PM");
            OperationHours.Add("5PM", "5PM");
            OperationHours.Add("6PM", "6PM");
            OperationHours.Add("7PM", "7PM");
            OperationHours.Add("8PM", "8AM");
            OperationHours.Add("9PM", "9AM");
            OperationHours.Add("10PM", "10PM");
            OperationHours.Add("11PM", "11PM");
            OperationHours.Add("Closed", "Closed");
            YearOfExperience = new Dictionary<string, string>();
            YearOfExperience.Add("1 year", "1 year");
            YearOfExperience.Add("2 year", "2 years");
            YearOfExperience.Add("3 year", "3 years");
            YearOfExperience.Add("4 year", "4 years");
            YearOfExperience.Add("5 year", "5 years");
            YearOfExperience.Add("6 year", "6 years");
            YearOfExperience.Add("7 year", "7 years");
            YearOfExperience.Add("8 year", "8 years");
            YearOfExperience.Add("9 year", "9 years");
            YearOfExperience.Add("10 year", "10 years");
            ServiceRadius = new Dictionary<string, string>();
            ServiceRadius.Add("1", "1 Miles");
            ServiceRadius.Add("5", "5 Miles");
            ServiceRadius.Add("10", "10 Miles");
            ServiceRadius.Add("15", "15 Miles");
            ServiceRadius.Add("25", "25 Miles");
            ServiceRadius.Add("50", "50 Miles");
            ServiceRadius.Add("100", "100 Miles");
            ServiceRadius.Add("150", "150 Miles");
            ServiceRadius.Add("250", "250 Miles");
            ServiceRadius.Add("500", "500 Miles");
            ServiceRadius.Add("1000", "1000 Miles");
            ServiceRadius.Add("1500", "1500 Miles");
            ServiceRadius.Add("2500", "2500 Miles");



        }
        //  public int ProviderListingId { get; set; }
        public int ProviderListingId { get; set; }
        public Dictionary<string, string> OperationHours { get; set; }
        public Dictionary<string, string> YearOfExperience { get; set; }
        public Dictionary<string, string> ServiceRadius { get; set; }

        public string CityError { get; set; }

        [Required(ErrorMessage = "Select Service")]
        public string CategoryId { get; set; }
        //  public string ProviderCategory { get; set; }
        public int ServicesAreaId { get; set; }
        public Nullable<int> CategoryServiceId { get; set; }
        [Required(ErrorMessage = "Experience is required")]
        public string Experience { get; set; }
        [Required(ErrorMessage = "Contact Number is required")]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[ .-. ]?([0-9]{4})$", ErrorMessage = "Entered phone format is not valid.")]
        public string ProviderContactNumber { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string ProviderAddress { get; set; }
        [Required(ErrorMessage = "Service Price is required")]
        public int ServicePrice { get; set; }
        public DateTime CreatedDate { get; set; }

        public int HourOfOperationId { get; set; }
        //[Required(ErrorMessage = "Service Description is required")]
        //[StringLength(150, ErrorMessage = "Must be between 20 and 150 characters", MinimumLength = 20)]
        public string ServiceDescription { get; set; }
        public string Logo { get; set; }
        public HttpPostedFileBase imageUrl { get; set; }
        [Required(ErrorMessage = "image is required")]
        public HttpPostedFileBase LogoUrl { get; set; }
        [Required(ErrorMessage = "Primary Email is required")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Please enter valid Email")]
        [Remote("UserEmailExits", "ProviderListing", ErrorMessage = "Email address already used")]
        public string PrimaryEmail { get; set; }
        public string Bitimagestring { get; set; }
        [Required(ErrorMessage = "Company Name is Required")]
        public string CompanyName { get; set; }
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Please enter valid Email")]
        public string CommunicationEmail { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*[A-Z])(?=.*[0-9])(?=.*[!@/#$%&/=?_.,:;\\-]).+$", ErrorMessage = "Your Password must have one capital letter one number and one special character")]
        public string Password { get; set; }
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Password did not match")]
        public string ConfirmPassword { get; set; }
        public int IsApproved { get; set; }
        public string GoogleURL { get; set; }
        [DataType(DataType.Url)]
        public string FacebookURL { get; set; }
        public string TwitterURL { get; set; }
        public string WebsiteURL { get; set; }
        public string PinterestURL { get; set; }
        public string UserId { get; set; }
        // public DateTime ListingDate { get; set; }
        [Required(ErrorMessage = "Please select atleast one ")]
        public string ServicePriceType { get; set; }
        [Required(ErrorMessage = "Who We Are is required")]
        public string WhoWeAre { get; set; }
        [Required(ErrorMessage = "Why Choose Us is required")]
        public string WhyWeDo { get; set; }
        [Required(ErrorMessage = "Our Mission is required")]

        public string WhatWeDo { get; set; }
        public string WhenWeDo { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "Zip code is required")]
        public int Zipcode { get; set; }
        public Decimal AvgRating { get; set; }
        //  public bool IsApproved { get; set; }
        //  public int CreatedBy { get; set; }
        public bool IsBlogShow { get; set; }
        public bool IsNewsShow { get; set; }
        public bool IsAboutUsShow { get; set; }
        public bool IsPhotosShow { get; set; }
        public bool IsVideoShow { get; set; }

        public bool IsTeamShow { get; set; }
        public bool IsMobileView { get; set; }
        public HourOfOperationModel hourOfOperation { get; set; }
        //public CategoryService CategoryService { get; set; }
        // public ProviderCategoryModel providerCategory { get; set; }
        public List<ServiceAreaModel> ServiceAreas { get; set; }
        public List<ListingArticleModel> ListingArticles { get; set; }
        public List<ListingBlogModel> ListingBlogs { get; set; }
        public List<ProviderTeamModel> ProviderTeams { get; set; }
        public List<ListingAboutusModel> ListingAboutUs { get; set; }
        public ProviderCategoryModel ProviderCategory1 { get; set; }
        public List<RatingModal> RatingTables { get; set; }
        public List<ProviderImageGalaryModel> ProviderImageGalaries { get; set; }
        public List<ProviderVideo> ProviderVideos { get; set; }
        public List<ServiceDetail> ServiceDetails { get; set; }
        public List<ServiceCategoryViewModel> ServiceCategoryViewModel { get; set; }
        public List<ServiceRequestDetail> ServiceRequestDetails { get; set; }
        public PagerModel PagerDetails { get; set; }

        public string LoggedInUserId { get; set; }
        public string LoggedInUserFirstName { get; set; }
        public string LoggedInUserLastName { get; set; }

        // public ServicePriceTypeModel servicePriceType { get; set; }
        public IEnumerable<ProviderCategoryModel> AllCategory()
        {
            var listCat = new ProviderCategoryClass().AllCategory();
            var resutl = Mapper.Map<IEnumerable<ProviderCategoryModel>>(listCat);
            return resutl;
        }
        public List<CategoryService> GetCategoryServices(int CategoryId)
        {
            var ServiceList = new ProviderCategoryClass().GetCategoryServices(CategoryId);
            return ServiceList;
        }
        public CategoryService GetCategoryServicesById(int CategoryId)
        {
            var ServiceList = new ProviderCategoryClass().GetCategoryServicesById(CategoryId);
            return ServiceList;
        }
        public int AddProviderListing(ProviderListingModel _ProviderListingModel)
        {
            var listingModel = Mapper.Map<ProviderListing>(_ProviderListingModel);
            return new ProviderListingClass().AddProviderListing(listingModel);
        }
        public bool UpdateProviderListng(ProviderListingModel _ProviderListingModel)
        {
            //_ProviderListingModel.ProviderCategory1 = _ProviderListingModel.AllCategory().FirstOrDefault(d => d.Id == Convert.ToInt32(_ProviderListingModel.CategoryId));
            var listingModel = Mapper.Map<ProviderListing>(_ProviderListingModel);

            return new ProviderListingClass().UpdateProviderListng(listingModel);
        }
        public void UpdateUserId(string Userid, int listingId)
        {
            new ProviderListingClass().updateUserid(Userid, listingId);
        }
        public ProviderListingModel GetListingByUserId(string UserId)
        {
            var _ProviderListingModel = new ProviderListingClass().GetListingByUserId(UserId);

            if (_ProviderListingModel != null)
            {
                ProviderListingModel providerListingModel = Mapper.Map<ProviderListingModel>(_ProviderListingModel);

                return providerListingModel;
            }
            return new ProviderListingModel();

        }

        public ProviderListingModel GetProviderInboxMailDetails(string UserId)
        {
            var _ProviderListingModel = new ProviderListingClass().GetProviderInboxMailDetails(UserId);

            if (_ProviderListingModel != null)
            {
                ProviderListingModel providerListingModel = Mapper.Map<ProviderListingModel>(_ProviderListingModel);

                return providerListingModel;
            }
            return new ProviderListingModel();
        }

        public ProviderListingModel GetListingByListingId(int ListingId)
        {
            var _ProviderListingModel = new ProviderListingClass().GetListingByListingId(ListingId);
            // _ProviderListingModel.HourOfOperation.ProviderListings = null;
            if (_ProviderListingModel != null)
            {
                ProviderListingModel providerListingModel = Mapper.Map<ProviderListingModel>(_ProviderListingModel);
                providerListingModel.AvgRating = providerListingModel.AvgRatings(providerListingModel);
                return providerListingModel;
            }
            return new ProviderListingModel();

        }
        public ProviderListingModel GetListingByListingName(string ListingName)
        {
            var _ProviderListingModel = new ProviderListingClass().GetListingByListingName(ListingName);
            // _ProviderListingModel.HourOfOperation.ProviderListings = null;
            if (_ProviderListingModel != null)
            {
                ProviderListingModel providerListingModel = Mapper.Map<ProviderListingModel>(_ProviderListingModel);
                providerListingModel.AvgRating = providerListingModel.AvgRatings(providerListingModel);
                return providerListingModel;
            }
            return new ProviderListingModel();

        }
        public List<ProviderListingModel> GetAllListing()
        {
            var _AllListing = new ProviderListingClass().GetAllListing();
            if (_AllListing.Count > 0)
            {
                List<ProviderListingModel> providerListingModel = Mapper.Map<List<ProviderListingModel>>(_AllListing);
                var finalListing = new List<ProviderListingModel>();
                foreach (var listing in providerListingModel)
                {
                    listing.AvgRating = listing.AvgRatings(listing);
                    finalListing.Add(listing);
                }
                return finalListing;
            }
            return new List<ProviderListingModel>();

        }

        public List<ProviderListingModel> GetAllListingNew(int pageNumber, int pageSize,out int totalRecords)
        {
            var _AllListing = new ProviderListingClass().GetAllListingNew(pageNumber,pageSize,out totalRecords);
            if (_AllListing.Count > 0)
            {
                List<ProviderListingModel> providerListingModel = Mapper.Map<List<ProviderListingModel>>(_AllListing);
                return providerListingModel;
            }
            return new List<ProviderListingModel>();
        }


        public bool SaveAboutUsDetails(string whatWeDo, string whyWeDo, string whoWeAre, int ProviderListingId)
        {
            var _result = new ProviderListingClass().SaveAboutUsDetails(whatWeDo, whyWeDo, whoWeAre, ProviderListingId);
            return _result;
        }

        public decimal AvgRatings(ProviderListingModel providerListingModel)
        {
            if (providerListingModel.RatingTables != null && providerListingModel.RatingTables.Count() > 0)
            {
                var speedTotal = providerListingModel.RatingTables.Sum(d => d.Speedpoint);
                var qualityTotal = providerListingModel.RatingTables.Sum(d => d.QualityPoint);
                var ReliablityTotal = providerListingModel.RatingTables.Sum(d => d.ReliabilityPoint);
                decimal avgratings = Convert.ToDecimal((speedTotal + qualityTotal + ReliablityTotal) / (3 * providerListingModel.RatingTables.Count()));
                if (avgratings > 0)
                {
                    return avgratings;
                }
                else
                {
                    return 0;
                }
            }
            return 0;

        }
        public bool UpdateListingStatus(int listingId, int listingStatus)
        {
            return new ProviderListingClass().UpdateListingStatus(listingId, listingStatus);
        }
        public int addProviderImage(ProviderImageGalaryModel img)
        {
            var model = Mapper.Map<ProviderImageGalary>(img);
            return new ProviderListingClass().addProviderImage(model);
        }
        public int addProvidervideo(ProviderVideo video)
        {
            return new ProviderListingClass().addProvidervideo(video);
        }
        public bool DelProviderImage(int imgid)
        {
            return new ProviderListingClass().DelProviderimage(imgid);
        }
        public bool DelProviderVideoLink(int videoid)
        {
            return new ProviderListingClass().DelProvidervideo(videoid);
        }
        public bool DelProviderTeamLink(Guid teamid)
        {
            return new ProviderListingClass().DelProviderTeam(teamid);
        }
        public List<ServiceRequestModel> GetServiceRequestByUserId(string UserId)
        {
            var result = new ProviderListingClass().GetServiceRequestByUserId(UserId);
            var model = Mapper.Map<List<ServiceRequestModel>>(result);
            foreach (var catnames in model.ToList())
            {
                var names = catnames.ServiceRequestCategories.Where(c => c.IsActive != false).Select(f => f.ProviderCategory.CategoryName).ToList();
                catnames.Categorynames = string.Join(", ", names);
            }
            return model;
        }

        public List<ServiceRequestModel> GetConsumerServiceRequest(string UserId, int pagenumber, int pageSize)
        {
            long totalRcords = 0;
            int startRow = (pagenumber - 1) * pageSize;
            var result = new ProviderListingClass().GetConsumerServiceRequest(UserId).ToList();
            totalRcords = result.Count();
            result = result.Skip(startRow).Take(pageSize).ToList();
            var model = Mapper.Map<List<ServiceRequestModel>>(result);
            foreach (var catnames in model.ToList())
            {
                var names = catnames.ServiceRequestCategories.Where(c => c.IsActive != false).Select(f => f.ProviderCategory.CategoryName).ToList();
                catnames.Categorynames = string.Join(", ", names);
                catnames.TotalRecords = totalRcords;
            }            
            return model;
        }
        public List<ServiceRequestModel> SearchInboxMailByText(string UserId)
        {         
            var result = new ProviderListingClass().GetConsumerServiceRequest(UserId).ToList();      
          
            var model = Mapper.Map<List<ServiceRequestModel>>(result);           
            return model;
        }

        public List<ServiceRequestModel> GetAllServiceRequest()
        {
            var result = new ProviderListingClass().GetAllServiceRequest();
            var model = Mapper.Map<List<ServiceRequestModel>>(result);
            foreach (var catnames in model)
            {
                var names = catnames.ServiceRequestCategories.Where(d => d.IsActive != false).Select(f => f.ProviderCategory.CategoryName).ToList();
                catnames.Categorynames = string.Join(", ", names);
            }
            return model;
        }

        public string GetProfileId(int listingId)
        {
            return new ProviderListingClass().getProfileId(listingId);
        }
        public string GetViewId(int listingId)
        {
            return new ProviderListingClass().getViewId(listingId);
        }
        public List<ProviderBlogViewModel> GetProviderBlogData(int PageNumber, int PageSize, string UserId)
        {
            var _result = new ProviderListingClass().GetProviderBlogData(PageNumber, PageSize, UserId).Select(c => new ProviderBlogViewModel()
            {
                BlogDescription = c.BlogDescription,
                BlogImage = c.BlogImage,
                BlogTitle = c.BlogTitle,
                IsActive = c.IsActive,
                ListingId = c.ListingId,
                IsPublished = c.IsPublished,
                CreatedBy = c.CreatedBy,
                CreatedDate = c.CreatedDate,
                IsCommentOn = c.IsCommentOn,
                ListingBlogId = c.ListingBlogId,
                ModifiedBy = c.ModifiedBy,
                ModifiedDate = c.ModifiedDate
            }).ToList();
            return _result;
        }

        public List<ProviderNewsViewModel> GetProviderNewsData(int PageNumber, int PageSize, string UserId)
        {
            var _result = new ProviderListingClass().GetProviderNewsData(PageNumber, PageSize, UserId).Select(c => new ProviderNewsViewModel()
            {
                ArticleDescription = c.ArticleDescription,
                ArticleImage = c.ArticleImage,
                ArticleTitle = c.ArticleTitle,
                IsActive = c.IsActive,
                ListingId = c.ListingId,
                IsPublished = c.IsPublished,
                CreatedBy = c.CreatedBy,
                CreatedDate = c.CreatedDate,
                IsCommentOn = c.IsCommentOn,
                ListingArticleId = c.ListingArticleId,
                ModifiedBy = c.ModifiedBy,
                ModifiedDate = c.ModifiedDate
            }).ToList();
            return _result;
        }
        public List<ProviderImageGalaryModel> GetProviderPhotosData(int PageNumber, int PageSize, string UserId)
        {
            var _result = new ProviderListingClass().GetProviderPhotosData(PageNumber, PageSize, UserId).Select(c => new ProviderImageGalaryModel()
            {
                Id = c.Id,
                ImagePath = c.ImagePath,
                ProviderListingId = c.ProviderListingId,
                CreatedBy = c.CreatedBy,
                CreatedDate = c.CreatedDate,
                TotalRecords = c.TotalRecords,
                IsActive = c.IsActive
            }).ToList();
            return _result;
        }

        public List<ProviderVideosViewModel> GetProviderVideosData(int PageNumber, int PageSize, string UserId)
        {
            var _result = new ProviderListingClass().GetProviderVideosData(PageNumber, PageSize, UserId).Select(c => new ProviderVideosViewModel()
            {
                Id = c.Id,
                VideoUrl = c.VideoUrl,
                ProviderListingId = c.ProviderListingId,
                CreatedBy = c.CreatedBy,
                CreatedDate = c.CreatedDate,
                TotalRecords = c.TotalRecords,
                IsActive = c.IsActive
            }).ToList();
            return _result;
        }

        public List<ProviderTeamModel> GetProviderTeamData(int PageNumber, int PageSize, string UserId)
        {
            var _result = new ProviderListingClass().GetProviderTeamData(PageNumber, PageSize, UserId).Select(c => new ProviderTeamModel()
            {
                Teamtitle = c.Teamtitle,
                TeamFirstName = c.TeamFirstName,
                TeamMiddleName = c.TeamMiddleName,
                IsActive = c.IsActive,
                ListingId = c.ListingId,
                TeamLastName = c.TeamLastName,
                CreatedBy = c.CreatedBy,
                CreatedDate = c.CreatedDate,
                TeamPosition = c.TeamPosition,
                TeamBiography = c.TeamBiography,
                ModifiedBy = c.ModifiedBy,
                ModifiedDate = c.ModifiedDate,
                TotalRecords = c.TotalRecords,
                ListingTeamId = c.ListingTeamId,
                imageUrl = c.imageUrl
            }).ToList();
            return _result;
        }

        public int GetProviderListingIdbyUserId(string UserId)
        {
            return new ProviderListingClass().GetProviderListingIdbyUserId(UserId);
        }
        public List<ProviderListingModel> GetProviderListing()
        {
            return new ProviderListingClass().GetProviderListing().Select(c => new ProviderListingModel()
            {
                ProviderListingId = c.ProviderListingId,
                CompanyName = c.CompanyName
            }).ToList();
        }

        public BlogNewsDetailViewModel GetBlogNewsDetails(int BlogNewsId, bool IsBlog)
        {
            BlogNewsDetailViewModel _BlogNewsDetailViewModel = new BlogNewsDetailViewModel();
            ssp_GetBlogNewsDetails_Result _ssp_GetBlogNewsDetails_Result = new ProviderListingClass().GetBlogNewsDetails(BlogNewsId, IsBlog);
            _BlogNewsDetailViewModel.ItemID = _ssp_GetBlogNewsDetails_Result.ItemID;
            _BlogNewsDetailViewModel.ItemDescription = _ssp_GetBlogNewsDetails_Result.ItemDescription;
            _BlogNewsDetailViewModel.ItemImage = _ssp_GetBlogNewsDetails_Result.ItemImage;
            _BlogNewsDetailViewModel.ItemTitle = _ssp_GetBlogNewsDetails_Result.ItemTitle;
            _BlogNewsDetailViewModel.ListingId = _ssp_GetBlogNewsDetails_Result.ListingId;
            _BlogNewsDetailViewModel.CreatedDate = _ssp_GetBlogNewsDetails_Result.CreatedDate;
            _BlogNewsDetailViewModel.CompanyName = _ssp_GetBlogNewsDetails_Result.CompanyName;
            return _BlogNewsDetailViewModel;

        }
        public ListingBlogCommentsModel SaveBlogNewComment(ListingBlogCommentsModel _model)
        {
            var model = Mapper.Map<ListingBlogComment>(_model);
            ListingBlogComment _result = new ProviderListingClass().SaveBlogNewComment(model);
            var result = Mapper.Map<ListingBlogCommentsModel>(_result);
            return result;
        }

        public List<ListingBlogCommentsModel> GetBlogNewsComments(int BlogNewsId, bool IsBlog)
        {          
            List<ListingBlogComment> _result = new ProviderListingClass().GetBlogNewsComments(BlogNewsId, IsBlog);
            var result = Mapper.Map<List<ListingBlogCommentsModel>>(_result);
            return result;
        }
    }
}