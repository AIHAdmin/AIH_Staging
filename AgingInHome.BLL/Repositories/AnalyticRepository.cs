using AgingInHome.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgingInHome.BLL.Repositories
{
    public interface IAnalyticRepository
    {
        int AddProviderAnalytic(Analytic _analytic);
        bool UpdateProviderAnalytics(Analytic _analytic);
    }
    public class AnalyticClass : IAnalyticRepository
    {
        public int AddProviderAnalytic(Analytic analytic)
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {
                    db.Analytics.Add(analytic);
                    db.SaveChanges();
                    return analytic.Id;
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
        public List<Analytic> GetProviderAnalytic()
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.Analytics.ToList();
                return dbListing;
            }
        }

        public List<usp_GetAnalyticsListing_Result> GetProviderAnalyticNew(int? pagenumber, int? pagesize, out int TotalRecords)
        {
            using (var db = new AgingInHomeContext())
            {
                

                ObjectParameter totalrecords = new ObjectParameter("TotalRecords", typeof(int));
                List<usp_GetAnalyticsListing_Result> dbListing = db.usp_GetAnalyticsListing(pagenumber, pagesize, totalrecords).ToList();
              
                TotalRecords = (int)totalrecords.Value;
                return dbListing;
            }
        }


        public bool UpdateProviderAnalytics(Analytic _analytic)
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.Analytics.Where(h => h.Id == _analytic.Id).FirstOrDefault();
                dbListing.ProviderListingId = _analytic.ProviderListingId;
                dbListing.ProfileID = _analytic.ProfileID;
                dbListing.ViewID = _analytic.ViewID;
                dbListing.IsActive = _analytic.IsActive;
                db.SaveChanges();
                return true;
            }
        }
        public bool DeleteProviderAnalytic(int Id)
        {
            bool retval = false;
            try
            {
                using (var db = new AgingInHomeContext())
                {
                    var dbListing = db.Analytics.Find(Id);
                    db.Analytics.Remove(dbListing);
                    db.SaveChanges();
                    retval = true;
                }
            }
            catch (Exception ex)
            {
                retval = false;
            }
            return retval;
        }
    }
}
