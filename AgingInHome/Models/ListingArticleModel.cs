using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgingInHome.BLL.Repositories;
using AgingInHome.DAL;

namespace AgingInHome.Models
{
    public class ListingArticleModel
    {
        public int ListingArticleId { get; set; }
        public int ListingId { get; set; }
        public string ArticleTitle { get; set; }
        public HttpPostedFileBase imageUrl { get; set; }
        public string ArticleDescription { get; set; }
        public string ArticleImage { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublished { get; set; }
        public bool IsCommentOn { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public ListingArticleModel AddArticle(ListingArticleModel listingArticleModel)
        {
            listingArticleModel.CreatedDate = DateTime.Now;
            var model = Mapper.Map<ListingArticle>(listingArticleModel);
            var News = new ListingArticleClass().AddArticle(model);
            return Mapper.Map<ListingArticleModel>(News);
        }
        public ListingArticleModel EditNews(int NewsId)
        {
            var News=new ListingArticleClass().GetNewsById(NewsId);
            var model = Mapper.Map<ListingArticleModel>(News);
            return model;
        }
        public ListingArticleModel UpdateNews(ListingArticleModel listingArticleModel)
        {
            var model = Mapper.Map<ListingArticle>(listingArticleModel);
            var News = new ListingArticleClass().UpdateArticle(model);           
            return Mapper.Map<ListingArticleModel>(News); 
        }
        public bool DeleteNews(int NewsId)
        {
            var News = new ListingArticleClass().DelNewsById(NewsId);
            return News;
        }
        
    }
}