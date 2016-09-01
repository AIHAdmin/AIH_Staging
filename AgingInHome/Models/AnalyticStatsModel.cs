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
    public class AnalyticStatsModel
    {
        ////Adword Buget
        public int Id { get; set; }

        public int ProviderListingId { get; set; }

        public int Users { get; set; }

        public int Sessions { get; set; }

        public decimal CTR { get; set; }

        public decimal CostPerTransaction { get; set; }

        public decimal CPC { get; set; }

        public int AdClicks { get; set; }

        public int Impressions { get; set; }

        public int Pageviews { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int AddAnalyticStats(AnalyticStatsModel _analyticStatsModel)
        {
            var analyticStatsModel = Mapper.Map<AnalyticsStat>(_analyticStatsModel);
            return new AnalyticStatsClass().AddAnalyticStats(analyticStatsModel);
        }
        public AnalyticStatsModel GetAnalyticStatsByProviderId(int ProviderId)
        {
            var _analyticStatsModel = new AnalyticStatsClass().GetAnalyticsStatByProviderId(ProviderId);

            if (_analyticStatsModel != null)
            {
                AnalyticStatsModel analyticStatsModel = Mapper.Map<AnalyticStatsModel>(_analyticStatsModel);
                return analyticStatsModel;
            }
            return new AnalyticStatsModel();
        }
        public bool UpdateAdwordBudget(AnalyticStatsModel _analyticStatsModel)
        {
            var analyticStatsModel = Mapper.Map<AnalyticsStat>(_analyticStatsModel);
            return new AnalyticStatsClass().UpdateAnalyticsStat(analyticStatsModel);
        }
    }

    public class MarkupModel
    {
        public int Id { get; set; }

        public decimal Markup1 { get; set; }

        public bool IsActive { get; set; }

        public bool IsDefault { get; set; }

        public int AddMarkup(MarkupModel _markupModel)
        {
            var markupModel = Mapper.Map<Markup>(_markupModel);
            return new MarkupClass().AddMarkup(markupModel);
        }
        public IEnumerable<Markup> GetMarkup()
        {
            MarkupClass objmarkup = new MarkupClass();
            List<Markup> _markup = objmarkup.GetMarkup();
            if (_markup != null)
            {
                return _markup;
            }
            return _markup;
        }
        public bool UpdateMarkup(MarkupModel _markupModel)
        {
            var markupModel = Mapper.Map<Markup>(_markupModel);
            return new MarkupClass().UpdateMarkup(markupModel);
        }
        public bool DeleteMarkup(int Id)
        {
            return new MarkupClass().DeleteMarkup(Id);
        }
        public Markup GetDefaultMarkup()
        {
            MarkupClass objmarkup = new MarkupClass();
            Markup _markup = objmarkup.GetDefaultMarkup();
            if (_markup != null)
            {
                return _markup;
            }
            return _markup;
        }
        public List<MarkupModel> GetAllMarkups(int PageNumber,int PageSize,out int TotalRecords)
        {
            List<MarkupModel> ListMarkUp = new List<MarkupModel>();
            MarkupModel MarkupModel = null;

            MarkupClass MarkupClass = new MarkupClass();
            List<usp_GetMarkups_Result> ListMarkUpResult = MarkupClass.GetMarkUps(PageNumber, PageSize, out TotalRecords);
            if (ListMarkUpResult != null)
            {
                foreach (usp_GetMarkups_Result Result in ListMarkUpResult)
                {
                    MarkupModel = new MarkupModel();
                    MarkupModel.Id = Result.Id;
                    MarkupModel.IsActive =(bool) Result.IsActive;
                    MarkupModel.IsDefault =(bool) Result.IsDefault;
                    MarkupModel.Markup1 = (decimal)Result.Markup;
                    ListMarkUp.Add(MarkupModel);
                }
                return ListMarkUp;
            }
            else
            {
                return new List<MarkupModel>();
            }
        }

        public bool ResetDefaultMarkup()
         {
             MarkupClass objmarkup = new MarkupClass();
             bool _markup = objmarkup.ResetDefaultMarkup();
             return _markup;
         }
    }
}