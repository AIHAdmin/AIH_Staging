using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class CheckoutDetailModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is requied")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is requied")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Company Name is requied")]
        public string CompanyName { get; set; }
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Please enter valid Email")]
        [Required(ErrorMessage = "Email Name is requied")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone is requied")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Phone Name is requied")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Country is requied")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is requied")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is requied")]
        public string State { get; set; }
        public int ZipCode { get; set; }
        public List<ServicesModel> SelectedServices { get; set; } 
    }
}
