using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgingInHome.BLL.Repositories;
using AutoMapper;
using AgingInHome.DAL;
using Microsoft.AspNet;
using Microsoft.AspNet.Identity;

namespace AgingInHome.Models
{
    public class ListingAboutusModel
    {
        public int ListingAboutUsId { get; set; }
        public int ListingId { get; set; }
        public string AboutUsDescription { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool AddAboutUs(ListingAboutusModel listingAboutusModel)
        {
            var result= Mapper.Map<ListingAboutU>(listingAboutusModel);
            return new AboutUsClass().AddAboutUs(result);
        }
        public bool UpdateDescription(ListingAboutusModel listingAboutusModel)
        {
            var result = Mapper.Map<ListingAboutU>(listingAboutusModel);
            return new AboutUsClass().UpdateAboutUs(result);
        }
        public ListingAboutusModel EditDescription(int ListingAboutUsId)
        {
            var model= new AboutUsClass().EditAboutUs(ListingAboutUsId);
            var result = Mapper.Map<ListingAboutusModel>(model);
            return result;
        }
        public bool ChangeStatusOfTabs(bool status, string feildName,int providerListingId)
        {
            return new AboutUsClass().ChangeStatusOfTabs(status, feildName, providerListingId);
        }


    }
}