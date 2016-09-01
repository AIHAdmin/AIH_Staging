using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgingInHome.DAL;

namespace AgingInHome.BLL.Repositories
{
   public interface IRequiredListingRepository
    {
        int addRequiredListing(RequiedListing _RequiredListing);
    }
    public class RequiredListingClass : IRequiredListingRepository
    {
        public int addRequiredListing(RequiedListing _RequiredListing)
        {
            using (var db = new AgingInHomeContext())
            {
                
                db.RequiedListings.Add(_RequiredListing);
                db.SaveChanges();
                return db.RequiedListings.Max(k => k.Id);
            }
        }

      
    }
}
