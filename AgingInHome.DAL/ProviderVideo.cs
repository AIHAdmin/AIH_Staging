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
    
    public partial class ProviderVideo
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
        public int ProviderListingId { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifyBy { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
    
        public virtual ProviderListing ProviderListing { get; set; }
    }
}
