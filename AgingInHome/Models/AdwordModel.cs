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
    public class AdwordModel
    {
        ////Adword Buget
        public int Id { get; set; }

        public int ProviderListingId { get; set; }

        [Required(ErrorMessage = "Monthly promotional budget is Required")]
        public decimal MonthlyPromotionalBudget { get; set; }

        [Required(ErrorMessage = "Remaining promotional budget is Required")]
        public decimal RemainingPromotionalBudget { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool IsActive { get; set; }

        public int AddAdwordBudget(AdwordModel _adwordModel)
        {
            var adwordModel = Mapper.Map<AdwordBuget>(_adwordModel);
            return new AdwordClass().insertAdWordBudget(adwordModel);
        }
        public AdwordModel GetAdwordByProviderId(int ProviderId)
        {
            var _adwordModel = new AdwordClass().GetAdwordBudgetByProviderId(ProviderId);

            if (_adwordModel != null)
            {
                AdwordModel adwordModel = Mapper.Map<AdwordModel>(_adwordModel);
                return adwordModel;
            }
            return new AdwordModel();
        }
        public bool UpdateAdwordBudget(AdwordModel _adwordModel)
        {
            var adwordModel = Mapper.Map<AdwordBuget>(_adwordModel);
            return new AdwordClass().UpdateAdWordBudget(adwordModel);
        }
    }
}