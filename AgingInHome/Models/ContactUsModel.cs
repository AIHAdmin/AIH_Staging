using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using AgingInHome.DAL;
using AgingInHome.BLL.Repositories;
using System.ComponentModel.DataAnnotations;

namespace AgingInHome.Models
{
    public class ContactUsModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = " Email is required")]
        public string Email { get; set; }
        public Nullable<bool> IsCall { get; set; }
        public Nullable<bool> MbCall { get; set; }
        public Nullable<bool> IsEmail { get; set; }
        public Nullable<bool> AppointmentSchedule { get; set; }
        public Nullable<bool> MbAppointmentSchedule { get; set; }

        public Nullable<bool> CustomeMsg { get; set; }
        public string CustomeMessageBody { get; set; }
        public int ProviderListingId { get; set; }
        public string ProviderUserId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime mbAppointmentDate { get; set; }
        public string AppontDate { get; set; }
        public DateTime AppontTime { get; set; }
        public string AppointmentTime { get; set; }

        public bool IsMobileView { get;set; }

        public int addContactUsForm(ContactUsModel contactUsModel)
        {
            ContactU model = Mapper.Map<ContactU>(contactUsModel);
            return new ContactUs().AddContactUsForm(model);
        }
    }
}