using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgingInHome.DAL;

namespace AgingInHome.BLL.Repositories
{
    public interface IappointmentRepository
    {
        bool SubmitAppointment(Appointment appointment, string UserType);
        bool SubmitCustomeAppointment(Appointment appointment, string UserType);
        bool CancelAppointment(int appointmentId);
        bool AcceptAppointment(int appointmentId, string UserType);
        bool RejectAppointment(int appointmentId, string UserType);
        List<Appointment> GetappointmentByConsumerId(string UserId);
        List<Appointment> GetappointmentByProviderId(string UserId);
        List<Appointment> GetAllAppointments();

    }
    public class AppointmentsClass : IappointmentRepository
    {
        public List<Appointment> GetappointmentByProviderId(string UserId)
        {
            using (var db = new AgingInHomeContext())
            {
                var Getdata = db.Appointments.Include("AspNetUser").Include("AspNetUser1")
                    .Include("ServiceRequestDetail").Include("ServiceRequestDetail.ProviderListing")
                    .Include("AspNetUser.ConsumerProfiles").Include("AspNetUser1.ConsumerProfiles")
                    .Where(s => s.ProviderId == UserId).ToList();
                return Getdata;
            }
        }

        public List<Appointment> GetappointmentByConsumerId(string UserId)
        {
            using (var db = new AgingInHomeContext())
            {
                var Getdata = db.Appointments.Include("AspNetUser").Include("AspNetUser1")
                    .Include("ServiceRequestDetail").Include("ServiceRequestDetail.ProviderListing")
                    .Include("AspNetUser.ConsumerProfiles")
                    .Where(s => s.ConsumerId == UserId).ToList();
                return Getdata;
            }
        }


        public bool SubmitAppointment(Appointment appointment, string UserType)
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {
                    if (appointment.AppointmentId > 0)
                    {
                        var GetAppoint = db.Appointments.Find(appointment.AppointmentId);
                        GetAppoint.IsCancel = true;
                        db.SaveChanges();

                    }
                    appointment.CreatedBy = appointment.ConsumerId;
                    appointment.CreatedDate = DateTime.Now;
                    appointment.IsAlterOffer = false;
                    appointment.IsCancel = false;
                    appointment.IsProviderCancel = false;
                    var message = new Inbox();
                    if (UserType == "Provider")
                    {
                        appointment.AppointmentSendBy = 2;
                        message.SenderId = appointment.ProviderId;
                        message.RecipientId = appointment.ConsumerId;
                      

                    }
                    else
                    {
                        appointment.AppointmentSendBy = 1;
                        message.SenderId = appointment.ConsumerId;
                        message.RecipientId = appointment.ProviderId;
                    }
                    db.Appointments.Add(appointment);
                   
                    var conversation = db.Inboxes.FirstOrDefault(s => s.ContactUsId == appointment.ContactUsid);
                    if (conversation!= null)
                    {
                        message.ConversationId = conversation.ConversationId;
                        message.Subject = "Consumer wants your services";
                    }
                    if (appointment.ServiceRequestDetailsId != null && appointment.ServiceRequestDetailsId != 0)
                    {
                        message.ServiceRequestDetailsId = appointment.ServiceRequestDetailsId;
                        message.Subject = "Invitation Accept Successfully";
                    }
                    message.IsActive = true;
                    message.isAdminArchive = false;
                    message.IsArchive = false;
                    message.IsProviderArchive = false;
                    message.IsRead = false;
                    message.IsStarred = false;
                    message.MessageBody = " Send You Updated Appointment Please  <a class='OpenAppointments'>Click Here</a>";
                    message.SentDate = DateTime.Now;
                    if (appointment.AppointmentId > 0)
                    {
                        message.Subject = "Update Appointment";
                    }
                    
                        
                    db.Inboxes.Add(message);
                    db.SaveChanges();
                    return true;



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
                    System.IO.File.AppendAllLines(@"f:\submitAppointment.txt", outputLines);
                    throw;
                }


            }
        }
        public bool SubmitCustomeAppointment(Appointment appointment, string UserType)
        {
            using (var db = new AgingInHomeContext())
            {
                try
                {
                    //if (appointment.AppointmentId > 0)
                    //{
                    //    var GetAppoint = db.Appointments.Find(appointment.AppointmentId);
                    //    GetAppoint.IsCancel = true;
                    //    db.SaveChanges();

                    //}
                    appointment.CreatedBy = appointment.ConsumerId;
                    appointment.CreatedDate = DateTime.Now;
                    appointment.IsAlterOffer = false;
                    appointment.IsCancel = false;
                    appointment.IsProviderCancel = false;
                    appointment.AppointmentSendBy = 1;
                    db.Appointments.Add(appointment);
                    var messege = new Inbox();
                    var conversation = db.Inboxes.FirstOrDefault(s => s.ContactUsId == appointment.ContactUsid);
                    if (conversation != null)
                    {
                        messege.ConversationId = conversation.ConversationId== null ?0 : conversation.ConversationId;
                    }                    
                    messege.IsActive = true;
                    messege.isAdminArchive = false;
                    messege.IsArchive = false;
                    messege.IsProviderArchive = false;
                    messege.IsRead = false;
                    messege.IsStarred = false;
                    messege.MessageBody = "Consumer Send You Appointment Please  <a class='OpenAppointments'>Click Here</a>";
                    messege.RecipientId = appointment.ConsumerId;
                    messege.SenderId = appointment.ProviderId;
                    messege.SentDate = DateTime.Now;
                    messege.Subject = "Send Appointment";
                    db.Inboxes.Add(messege);
                    db.SaveChanges();
                    return true;



                }
                catch (Exception)
                {

                    throw;
                }


            }
        }

        public bool CancelAppointment(int appointmentId)
        {
            using (var db = new AgingInHomeContext())
            {
                var GetAppoint = db.Appointments.Find(appointmentId);
                GetAppoint.IsCancel = true;
                db.SaveChanges();
                return true;
            }
        }
        public bool AcceptAppointment(int appointmentId, string UserType)
        {
            using (var db = new AgingInHomeContext())
            {
                var GetAppoint = db.Appointments.Find(appointmentId);
                if (UserType == "Provider")
                {
                    GetAppoint.IsProviderAccept = true;
                }
                else
                {
                    GetAppoint.IsConsumerAccept = true;
                }

                db.SaveChanges();
                return true;
            }
        }
        public bool RejectAppointment(int appointmentId, string UserType)
        {
            using (var db = new AgingInHomeContext())
            {
                var GetAppoint = db.Appointments.Find(appointmentId);
                if (UserType == "Provider")
                {
                    GetAppoint.IsProviderAccept = false;
                }
                else
                {
                    GetAppoint.IsConsumerAccept = false;
                }
                db.SaveChanges();
                return true;
            }
        }

        public List<Appointment> GetAllAppointments()
        {
            using (var db = new AgingInHomeContext())
            {
                return db.Appointments.Include("AspNetUser").Include("AspNetUser1")
                    .Include("ServiceRequestDetail").Include("ServiceRequestDetail.ProviderListing")
                    .Include("AspNetUser1.ConsumerProfiles").ToList();
            }
        }

        ////createdby: Ishan Sharma
        //public List<Appointment> GetAllAppointmentNew()
        //{
        //    using (var db = new AgingInHomeContext())
        //    {

        //    }
        //}


        public Appointment GetAppointmentByID(int appointmentID)
        {
            using (var db = new AgingInHomeContext())
            {
                return db.Appointments.Where(x => x.AppointmentId == appointmentID).FirstOrDefault();
            }
        }
    }
}
