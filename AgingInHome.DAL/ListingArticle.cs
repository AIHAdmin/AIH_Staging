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
    
    public partial class ListingArticle
    {
        public int ListingArticleId { get; set; }
        public int ListingId { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleDescription { get; set; }
        public string ArticleImage { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public bool IsCommentOn { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual ProviderListing ProviderListing { get; set; }
    }
}
