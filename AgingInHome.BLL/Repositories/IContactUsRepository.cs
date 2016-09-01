using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgingInHome.DAL;

namespace AgingInHome.BLL.Repositories
{
    public interface IContactUsRepository
    {
        int AddContactUsForm(ContactU contactUs);

    }
    public class ContactUs : IContactUsRepository
    {
        public int AddContactUsForm(ContactU contactUs)
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {
                    if (contactUs.AppointmentSchedule == false || contactUs.AppointmentSchedule == null)
                    {
                        contactUs.AppointmentDate = null;
                        contactUs.AppointmentTime = null;
                    }
                    var result= db.ContactUs.Add(contactUs);
                    db.SaveChanges();
                    return db.ContactUs.Max(s=>s.Id);
                }
                catch (Exception)
                {

                    throw;
                }
              
            }
        }
    }
}
