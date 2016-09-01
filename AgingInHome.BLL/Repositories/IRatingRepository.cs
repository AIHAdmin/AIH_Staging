using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgingInHome.DAL;

namespace AgingInHome.BLL.Repositories
{
    public interface IRatingRepository
    {
        bool AddRating(RatingTable _Rating);
        bool UserEmailExits(string Email);
       
        bool CheckEmailExist(RatingTable _Rating); 
    }
    public class Rating : IRatingRepository
    {
        public bool AddRating(RatingTable _Rating)
        {
            using (var db = new AgingInHomeContext())
            {
                db.RatingTables.Add(_Rating);
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException e)
                {
                    var outputLines = new List<string>();
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        outputLines.Add(string.Format(
                            "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:",
                            DateTime.Now, eve.Entry.Entity.GetType().Name, eve.Entry.State));
                        foreach (var ve in eve.ValidationErrors)
                        {
                            outputLines.Add(string.Format(
                                "- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage));
                        }
                    }
                    System.IO.File.AppendAllLines(@"f:\DBerrors.txt", outputLines);
                    throw;
                }
              
                return true;
            }
          
        }

        public bool CheckEmailExist(RatingTable _Rating)
        {
            using (var db = new AgingInHomeContext())
            {
               var result= db.RatingTables.Where(g => g.UserId == _Rating.UserId && g.ProviderListingId == _Rating.ProviderListingId).ToList();
                if (result.Count>0)
                {
                    return true;
                }
                return false;

               
            }
        }

        public bool UserEmailExits(string Email)
        {
            using (var db = new AgingInHomeContext())
            {
                var result = db.AspNetUsers.Where(j => j.Email == Email).FirstOrDefault();
                if (result!=null)
                {
                    return false;
                }
                return true;


            }
        }
    }
}
