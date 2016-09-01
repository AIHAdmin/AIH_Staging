using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;
using AgingInHome.Models;
using AgingInHome.DAL;
using AgingInHome.DAL.Helper;

namespace AgingInHome
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void Mapper()
        {
            AutoMapper.Mapper.Initialize(J =>
            {
                J.CreateMap<ProviderListing, ProviderListingModel> ();
                J.CreateMap<ProviderCategory, ProviderCategoryModel>();
                
                J.CreateMap<ProviderListingModel, ProviderListing>();

                J.CreateMap<HourOfOperationModel, HourOfOperation>();
                J.CreateMap<ServiceAreaModel, ServiceArea>();
                J.CreateMap<ServicePriceTypeModel, ServicePriceType>();
                J.CreateMap<ServicesModel, Service>();
                J.CreateMap<CountryModel, Country>();
                J.CreateMap<UsStateModel, UsState>();
                J.CreateMap<RatingModal, RatingTable>();
                J.CreateMap<ContactUsModel, ContactU>();
                J.CreateMap<ListingAboutU, ListingAboutusModel>();
                J.CreateMap<ListingBlog, ListingBlogModel>();
                J.CreateMap<ListingArticle, ListingArticleModel>();
                J.CreateMap<ProviderTeam, ProviderTeamModel>();
                J.CreateMap<ListingBlogComment, ListingBlogCommentsModel>();
                J.CreateMap<ServiceRequest, ServiceRequestModel>();
                J.CreateMap<ServiceRequestCategory, ServiceRequestCategoryModel>();
                J.CreateMap<ProviderImageGalary, ProviderImageGalaryModel>();
                J.CreateMap<ServiceRequestDetail, ServiceRequestDetailModel>();
                J.CreateMap<InboxModel,Inbox>();
                J.CreateMap<ConsumerProfile, ServiceRequestModel>();
                J.CreateMap<ServiceSelectedCatDetails,SelectedCategoryDetails>();
                J.CreateMap<Appointment,AppointmentModel>();
                J.CreateMap<QueueListingModel,RequiedListing>();
                J.CreateMap<ListingBlogCommentsModel, ListingBlogComment>();



                J.CreateMap<RequiedListing, QueueListingModel>();
                J.CreateMap<AppointmentModel, Appointment>();
                J.CreateMap<SelectedCategoryDetails, ServiceSelectedCatDetails>();
                J.CreateMap<ServiceRequestModel, ConsumerProfile>();
                J.CreateMap<Inbox, InboxModel>();
                J.CreateMap<ServiceRequestDetailModel, ServiceRequestDetail>();
                J.CreateMap<ProviderImageGalaryModel, ProviderImageGalary>();
                J.CreateMap<ServiceRequestCategoryModel, ServiceRequestCategory>();
                J.CreateMap<ServiceRequestModel, ServiceRequest>();
                J.CreateMap<HourOfOperation, HourOfOperationModel>();
                J.CreateMap<ServiceArea, ServiceAreaModel>();
                J.CreateMap<ServicePriceType, ServicePriceTypeModel>();
                J.CreateMap<Service, ServicesModel>();
                J.CreateMap<Country, CountryModel>();
                J.CreateMap<UsState, UsStateModel>();
                J.CreateMap<RatingTable, RatingModal>();
                J.CreateMap<ProviderCategoryModel, ProviderCategory>();
                J.CreateMap<ContactU, ContactUsModel>();
                J.CreateMap<ListingAboutusModel, ListingAboutU>();
                J.CreateMap<ListingBlogModel, ListingBlog>();
                J.CreateMap<ListingArticleModel, ListingArticle>();
                J.CreateMap<ListingBlogCommentsModel, ListingBlogComment>();
              
                J.CreateMap<ProviderTeamModel, ProviderTeam>();


                J.CreateMap<ListingBlogCommentsModel, ListingBlogComment>();
                J.CreateMap<AdwordModel, AdwordBuget>();
                J.CreateMap<AdwordBuget, AdwordModel>();
                J.CreateMap<AnalyticStatsModel, AnalyticsStat>();
                J.CreateMap<AnalyticsStat, AnalyticStatsModel>();
                J.CreateMap<MarkupModel, Markup>();
                J.CreateMap<Markup, MarkupModel>();
                J.CreateMap<AnalyticsModel, Analytic>();
                J.CreateMap<Analytic, AnalyticsModel>();

                J.CreateMap<ListingBlogComment, ListingBlogCommentsModel>();
            }
            );
        }
    }
}
