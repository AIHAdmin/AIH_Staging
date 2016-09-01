using AgingInHome.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgingInHome.BLL.Repositories
{
    public interface IProviderCategoryRepository
    {
        IEnumerable<ProviderCategory> AllCategory();
        List<CategoryService> GetCategoryServices(int CategoryId);
        CategoryService GetCategoryServicesById(int CategoryId);
    }
    public class ProviderCategoryClass : IProviderCategoryRepository
    {
        public IEnumerable<ProviderCategory> AllCategory()
        {
            using (var db = new AgingInHomeContext())
            {
                return db.ProviderCategories.ToList();
            }
        }
        public List<CategoryService> GetCategoryServices(int CategoryId)
        {
            using (var db = new AgingInHomeContext())
            {
                return db.CategoryServices.Where(s => s.ProviderCategoryId == CategoryId).ToList();
            }
        }

        public CategoryService GetCategoryServicesById(int CategoryId)
        {
            using (var db = new AgingInHomeContext())
            {
                return db.CategoryServices.Where(s => s.CategoryServiceId == CategoryId).FirstOrDefault();
            }
        }


    }
}

