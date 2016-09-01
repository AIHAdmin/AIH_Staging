using AgingInHome.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgingInHome.BLL.Repositories
{
    public interface IAboutUsRepository
    {
        bool AddAboutUs(ListingAboutU listingAboutU);
        ListingAboutU EditAboutUs(int aboutusId);
        bool UpdateAboutUs(ListingAboutU _listingAboutU);
        bool ChangeStatusOfTabs(bool status, string feildName, int providerListingId);
    }
    public class AboutUsClass : IAboutUsRepository
    {
        public bool AddAboutUs(ListingAboutU listingAboutU)
        {
            using (var db = new AgingInHomeContext())
            {
                listingAboutU.CreatedDate = DateTime.Now;
                db.ListingAboutUs.Add(listingAboutU);
                db.SaveChanges();
                return true;
            }
        }

        public bool ChangeStatusOfTabs(bool status, string feildName, int providerListingId)
        {
            using (var db = new AgingInHomeContext())
            {
                var model = db.ProviderListings.Find(providerListingId);
                switch (feildName)
                {
                    case "IsBlogShow":
                        model.IsBlogShow = status;
                        break;
                    case "IsNewsShow":
                        model.IsNewsShow = status;
                        break;
                    case "IsPhotosShow":
                        model.IsPhotosShow = status;
                        break;
                    case "IsVideoShow":
                        model.IsVideoShow = status;
                        break;
                    case "IsAboutUsShow":
                        model.IsAboutUsShow = status;
                        break;
                    case "IsTeamShow":
                        model.IsTeamShow = status;
                        break;
                    default:
                        break;
                }
                db.SaveChanges();
                return true;
            }
        }

        public ListingAboutU EditAboutUs(int aboutusId)
        {
            using (var db = new AgingInHomeContext())
            {
                var _aboutus = db.ListingAboutUs.Find(aboutusId);

                return _aboutus;

            }
        }

        public bool UpdateAboutUs(ListingAboutU _listingAboutU)
        {
            using (var db = new AgingInHomeContext())
            {
                var _aboutus = db.ListingAboutUs.Find(_listingAboutU.ListingAboutUsId);
                _aboutus.AboutUsDescription = _listingAboutU.AboutUsDescription;
                _aboutus.IsActive = _listingAboutU.IsActive;
                _aboutus.ModifiedBy = _listingAboutU.ModifiedBy;
                _aboutus.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;

            }
        }
    }
}
