using AgingInHome.DAL;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgingInHome.BLL.Repositories;

namespace AgingInHome.Models
{
    public class AppointmentModel
    {
        public int AppointmentId { get; set; }
        public string ProviderId { get; set; }
        public string ConsumerId { get; set; }
        public System.DateTime ServiceDate { get; set; }
        public string BestTime { get; set; }
        public Nullable<bool> IsCancel { get; set; }
        public int ContactUsId { get; set; }
        public Nullable<System.DateTime> IsAlterTime { get; set; }
        public Nullable<bool> IsAlterOffer { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<int> ServiceRequestDetailsId { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual AspNetUser AspNetUser1 { get; set; }
        public Nullable<bool> IsProviderAccept { get; set; }
        public Nullable<bool> IsConsumerAccept { get; set; }
        public Nullable<int> AppointmentSendBy { get; set; }
        public long totalRecords { get; set; }
        public virtual ServiceRequestDetail ServiceRequestDetail { get; set; }
        public bool SubmitAppointment(AppointmentModel model,string UserType)
        {
            var _appointment = Mapper.Map<Appointment>(model);
            return new AppointmentsClass().SubmitAppointment(_appointment,UserType);
        }
        public bool SubmitCustomeAppointment(AppointmentModel model, string UserType)
        {
            var _appointment = Mapper.Map<Appointment>(model);
            return new AppointmentsClass().SubmitCustomeAppointment(_appointment, UserType);
        }
        
        public List<AppointmentModel> GetAllConsumerAppointment(string UserId)
        {
            var _appointment = new AppointmentsClass().GetappointmentByConsumerId(UserId);
            var model = Mapper.Map<List<AppointmentModel>>(_appointment);
            return model;
        }
        public List<AppointmentModel> GetAllProviderAppointment(string UserId)
        {
            var _appointment = new AppointmentsClass().GetappointmentByProviderId(UserId);
            var model = Mapper.Map<List<AppointmentModel>>(_appointment);
            return model;
        }
        public bool CancelAppointment(int appointId)
        {
            return new AppointmentsClass().CancelAppointment(appointId);
        }
        public bool AcceptAppointment(int appointId,string UserType)
        {
            return new AppointmentsClass().AcceptAppointment(appointId, UserType);
        }
        public bool RejectAppointment(int appointId,string UserType)
        {
            return new AppointmentsClass().RejectAppointment(appointId, UserType);
        }
        public bool CheckAppointmentStatus(string UserId,string ProviderId)
        {
            var getappointment=new AgingInHomeContext().Appointments.Where(s => s.ConsumerId == UserId && s.ProviderId == ProviderId).ToList();
            if (getappointment.Count>0)
            {
                if (getappointment.Last().IsRatingDisable!=true && getappointment.Last().ServiceDate.AddHours(24)<=DateTime.Now)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
        public bool ChangeAppointmentStatus(string Userid,string ProviderId)
        {
            using (var db=new AgingInHomeContext())
            {

           
            var getappointment = db.Appointments.Where(s => s.ConsumerId == Userid && s.ProviderId == ProviderId).ToList();
            if (getappointment.Count > 0)
            {
                    getappointment.Last().IsRatingDisable = true;
                    db.SaveChanges();
                    return true;
            }
            return false;
            }
        }
        public List<AppointmentModel> GetAllAppointments()
        {
            var _appointment = new AppointmentsClass().GetAllAppointments();
            var model = Mapper.Map<List<AppointmentModel>>(_appointment);
            return model;
        }
        public AppointmentModel GetAppointmentByID(string appointmentID)
        {
            var _appointment = new AppointmentsClass().GetAppointmentByID(Convert.ToInt32(appointmentID));
            var model = Mapper.Map<AppointmentModel>(_appointment);
            return model;
        }
    }
}