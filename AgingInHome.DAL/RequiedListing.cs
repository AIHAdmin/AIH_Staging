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
    
    public partial class RequiedListing
    {
        public int Id { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string UserId { get; set; }
        public string City { get; set; }
        public Nullable<int> Zipcode { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        public Nullable<System.DateTime> ServiceDate { get; set; }
        public Nullable<bool> Is7daysWait { get; set; }
        public Nullable<int> ServiceRequestCatId { get; set; }
        public Nullable<bool> IsQueue { get; set; }
        public string AssignProviderName { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual ProviderCategory ProviderCategory { get; set; }
    }
}
