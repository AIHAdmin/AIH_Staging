using AgingInHome.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AgingInHome.BLL.Repositories
{
    public interface IAdwordRepository
    {
        int insertAdWordBudget(AdwordBuget adwordBudget);
    }
    public class AdwordClass : IAdwordRepository
    {
        public int insertAdWordBudget(AdwordBuget adwordBudget)
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {
                    db.AdwordBugets.Add(adwordBudget);
                    db.SaveChanges();
                    return adwordBudget.Id;

                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public AdwordBuget GetAdwordBudgetByProviderId(int ProviderId)
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.AdwordBugets.Where(h => h.ProviderListingId == ProviderId).FirstOrDefault();
                return dbListing;
            }

        }

        public bool UpdateAdWordBudget(AdwordBuget _adwordBudget)
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.AdwordBugets.Where(h => h.Id == _adwordBudget.Id).FirstOrDefault();
                dbListing.MonthlyPromotionalBudget = _adwordBudget.MonthlyPromotionalBudget;
                dbListing.RemainingPromotionalBudget = _adwordBudget.RemainingPromotionalBudget;
                dbListing.StartDate = _adwordBudget.StartDate;
                dbListing.EndDate = _adwordBudget.EndDate;
                dbListing.ProviderListingId = _adwordBudget.ProviderListingId;
                db.SaveChanges();
                return true;
            }
        }
    }
}
