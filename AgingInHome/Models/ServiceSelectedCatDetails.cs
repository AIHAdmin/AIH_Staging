using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class ServiceSelectedCatDetails
    {
        public int catId { get; set; }
        public string CatBestTime { get; set; }
        public string CatserviceDate { get; set; }
        public int ServiceRequestId { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}