using AgingInHome.DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgingInHome.BLL.Repositories
{
    public interface IAnalyticStatsRepository
    {
        int AddAnalyticStats(AnalyticsStat _AnalyticsStat);
        bool UpdateAnalyticsStat(AnalyticsStat _AnalyticsStat);
    }
    public class AnalyticStatsClass: IAnalyticStatsRepository
    {
        public int AddAnalyticStats(AnalyticsStat analyticsStat)
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {
                    db.AnalyticsStats.Add(analyticsStat);
                    db.SaveChanges();
                    return analyticsStat.Id;

                }
                catch (Exception)
                {

                    throw;
                }

            }
        }

        public AnalyticsStat GetAnalyticsStatByProviderId(int ProviderId)
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.AnalyticsStats.Where(h => h.ProviderListingId == ProviderId).FirstOrDefault();
                return dbListing;
            }

        }

        public bool UpdateAnalyticsStat(AnalyticsStat _analyticsStat)
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.AnalyticsStats.Where(h => h.Id == _analyticsStat.Id).FirstOrDefault();
                dbListing.ProviderListingId = _analyticsStat.ProviderListingId;
                dbListing.Users = _analyticsStat.Users;
                dbListing.Sessions = _analyticsStat.Sessions;
                dbListing.CTR = _analyticsStat.CTR;
                dbListing.CostPerTransaction = _analyticsStat.CostPerTransaction;
                dbListing.CPC = _analyticsStat.CPC;
                dbListing.AdClicks = _analyticsStat.AdClicks;
                dbListing.Impressions = _analyticsStat.Impressions;
                dbListing.Pageviews = _analyticsStat.Pageviews;
                dbListing.MarkupId = _analyticsStat.MarkupId;
                dbListing.StartDate = _analyticsStat.StartDate;
                dbListing.EndDate = _analyticsStat.EndDate;
                db.SaveChanges();
                return true;
            }
        }
    }
    public interface IMarkupRepository
    {
        int AddMarkup(Markup _Markup);
        bool UpdateMarkup(Markup _Markup);
        bool DeleteMarkup(int Id);
    }
    public class MarkupClass : IMarkupRepository
    {
        public int AddMarkup(Markup markUp)
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {
                    db.Markups.Add(markUp);
                    db.SaveChanges();
                    return markUp.Id;

                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
        public List<Markup> GetMarkup()
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.Markups.ToList();
                return dbListing;
            }
        }
        public bool UpdateMarkup(Markup _markUp)
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.Markups.Where(h => h.Id == _markUp.Id).FirstOrDefault();
                dbListing.Markup1 = _markUp.Markup1;
                dbListing.IsActive = _markUp.IsActive;
                dbListing.IsDefault = _markUp.IsDefault;
                db.SaveChanges();
                return true;
            }
        }
        public bool DeleteMarkup(int Id)
        {
            bool retval = false;
            try
            {
                using (var db = new AgingInHomeContext())
                {
                    var dbListing = db.Markups.Find(Id);
                    db.Markups.Remove(dbListing);
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

        public Markup GetDefaultMarkup()
        {
            using (var db = new AgingInHomeContext())
            {
                var dbListing = db.Markups.Where(h => h.IsDefault == true).SingleOrDefault();
                return dbListing;
            }
        }

        public List<usp_GetMarkups_Result> GetMarkUps(int pagenumber, int pageSize, out int TotalRecords)
        {
            using (var db = new AgingInHomeContext())
            {
                ObjectParameter totalrecords = new ObjectParameter("TotalRecords", typeof(int));
                List<usp_GetMarkups_Result> _result = db.usp_GetMarkups(pagenumber, pageSize, totalrecords).ToList();
                TotalRecords = (int) totalrecords.Value;
                return _result;
            }
        }

        public bool ResetDefaultMarkup()
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {
                    var dbListing = db.Markups.Where(h => h.IsDefault == true).FirstOrDefault();
                    if (dbListing!=null)
                    {
                        dbListing.Markup1 = dbListing.Markup1;
                        dbListing.IsActive = dbListing.IsActive;
                        dbListing.IsDefault = false;
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
