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
    
    public partial class CategoryService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CategoryService()
        {
            this.ServiceDetails = new HashSet<ServiceDetail>();
            this.ProviderListings = new HashSet<ProviderListing>();
        }
    
        public int CategoryServiceId { get; set; }
        public string Name { get; set; }
        public Nullable<int> ProviderCategoryId { get; set; }
    
        public virtual ProviderCategory ProviderCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServiceDetail> ServiceDetails { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProviderListing> ProviderListings { get; set; }
    }
}