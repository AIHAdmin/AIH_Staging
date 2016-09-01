using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class ProviderTeamViewModel
    {
        public System.Guid ListingTeamId { get; set; }
        public int ListingId { get; set; }
        public string Teamtitle { get; set; }
        public string TeamFirstName { get; set; }
        public string TeamMiddleName { get; set; }
        public string TeamLastName { get; set; }
        public string TeamPosition { get; set; }
        public string TeamBiography { get; set; }
        public string imageUrl { get; set; }
        public bool IsActive { get; set; }
    }
}