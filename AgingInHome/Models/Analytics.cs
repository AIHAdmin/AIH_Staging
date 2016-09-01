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
    public class AnalyticsModel
    {
        /// <summary>
        /// Provider Listing Id
        /// </summary>
        /// 
        public int Id { get; set; }

        public int ProviderListingId { get; set; }

        public string profileId { get; set; }

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public string viewId { get; set; }
        public string Provider { get; set; }
        public bool isActive { get; set; }

        public int AddProviderAnalytic(AnalyticsModel _analyticsModel)
        {
            var analyticsModel = Mapper.Map<Analytic>(_analyticsModel);
            return new AnalyticClass().AddProviderAnalytic(analyticsModel);
        }
        public IEnumerable<Analytic> GetProviderAnalytic()
        {
            AnalyticClass objAnalytic = new AnalyticClass();
            List<Analytic> _analytic = objAnalytic.GetProviderAnalytic();
            if (_analytic != null)
            {
                return _analytic;
            }
            return _analytic;
        }

        public List<AnalyticsModel> GetProviderAnalyticNew(int pageNumber, int pageSize, out int TotalRecords)
        {
            List<AnalyticsModel> ListAnalyticsModel = new List<AnalyticsModel>();
            AnalyticsModel AnalyticsModel = null;
            AnalyticClass objAnalytic = new AnalyticClass();
            List<usp_GetAnalyticsListing_Result> _analytic = objAnalytic.GetProviderAnalyticNew(pageNumber, pageSize, out TotalRecords);
            if (_analytic != null)
            {
                foreach (usp_GetAnalyticsListing_Result Result in _analytic)
                {
                    AnalyticsModel = new Models.AnalyticsModel();
                    AnalyticsModel.profileId = Result.ProfileId;
                    AnalyticsModel.isActive = (bool)Result.IsActive;
                    AnalyticsModel.viewId = Result.ViewId;
                    AnalyticsModel.Provider = Result.CompanyName;
                    ListAnalyticsModel.Add(AnalyticsModel);
                }
            }
            return ListAnalyticsModel;
        }
       


        public bool UpdateProviderAnalytic(AnalyticsModel _analyticsModel)
        {
            var analyticsModel = Mapper.Map<Analytic>(_analyticsModel);
            return new AnalyticClass().UpdateProviderAnalytics(analyticsModel);
        }
        public bool DeleteProviderAnalytic(int Id)
        {
            return new AnalyticClass().DeleteProviderAnalytic(Id);
        }

    }
}