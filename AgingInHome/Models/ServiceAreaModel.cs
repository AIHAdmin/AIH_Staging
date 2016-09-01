
using System.ComponentModel.DataAnnotations;

namespace AgingInHome.Models
{
    public class ServiceAreaModel
    {
        public int Id { get; set; }
        public int ProviderListingId { get; set; }
        [Required(ErrorMessage ="City is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string StateId { get; set; }
        [Required(ErrorMessage = "ZipCode is required")]
        public long ZipCode { get; set; }
        [Required(ErrorMessage = "Radius is required")]
        public string ServiceRadius { get; set; }
        public  UsStateModel UsState { get; set; }
    }
}