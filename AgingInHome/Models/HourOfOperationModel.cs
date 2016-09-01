using System.ComponentModel.DataAnnotations;
namespace AgingInHome.Models
{
    public class HourOfOperationModel
    {
        public int HourOfOperationId { get; set; }
        [Required(ErrorMessage = "Monday time is required")]
        public string MonStart { get; set; }
        [Required(ErrorMessage = "Monday time is required")]

        public string MonEnd { get; set; }
        public string TueStart { get; set; }
        public string TueEnd { get; set; }
        public string WedStart { get; set; }
        public string WedEnd { get; set; }
        public string ThuStart { get; set; }
        public string ThuEnd { get; set; }
        public string FriStart { get; set; }
        public string FriEnd { get; set; }
        public string SatStart { get; set; }
        public string SatEnd { get; set; }
        public string SunStart { get; set; }
        public string SunEnd { get; set; }
    }
}