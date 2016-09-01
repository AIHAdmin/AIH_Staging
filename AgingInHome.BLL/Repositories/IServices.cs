using AgingInHome.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgingInHome.BLL.Repositories
{
    public interface IServices
    {
        List<Service> GetAllServices();
        List<UsState> GetAllUsStates();
        List<Country> GetAllCountries();
    }
    public class Services : IServices
    {
        public List<Service> GetAllServices()
        {
            using (var db = new AgingInHomeContext())
            {
                return db.Services.ToList();
            }
        }

        public List<UsState> GetAllUsStates()
        {
            using (var db = new AgingInHomeContext())
            {
                return db.UsStates.ToList();
            }
        }
        public List<Country> GetAllCountries()
        {
            using (var db = new AgingInHomeContext())
            {
                return db.Countries.ToList();
            }
        }
    }
}
