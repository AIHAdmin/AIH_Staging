//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgingInHome.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProviderTeam
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
        public bool IsDeleted { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual ProviderListing ProviderListing { get; set; }
    }
}
