using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgingInHome.DAL;

namespace AgingInHome.BLL.Repositories
{
    public interface IListingArticleRepository
    {
        ListingArticle AddArticle(ListingArticle listingArticle);
        ListingArticle EditArticle(int listingArticleId);
        ListingArticle UpdateArticle(ListingArticle _listingArticle);
        ListingArticle GetNewsById(int NewsId);
        bool DelNewsById(int NewsId);
    }
    public class ListingArticleClass : IListingArticleRepository
    {
        public ListingArticle AddArticle(ListingArticle listingArticle)
        {
            using (var db = new AgingInHomeContext())
            {
                db.ListingArticles.Add(listingArticle);
                db.SaveChanges();
                return listingArticle;
            }
        }

        public bool DelNewsById(int NewsId)
        {
            using (var db = new AgingInHomeContext())
            {
                var _article = db.ListingArticles.Find(NewsId);
                db.ListingArticles.Remove(_article);
                db.SaveChanges();
                return true;

            }
        }

        public ListingArticle EditArticle(int listingArticleId)
        {
            using (var db = new AgingInHomeContext())
            {
                var _article = db.ListingArticles.Find(listingArticleId);

                return _article;

            }
        }

        public ListingArticle GetNewsById(int NewsId)
        {
            using (var db = new AgingInHomeContext())
            {
                var _article = db.ListingArticles.Find(NewsId);

                return _article;

            }
        }

        public ListingArticle UpdateArticle(ListingArticle _listingArticle)
        {
            using (var db = new AgingInHomeContext())
            {
                var _article = db.ListingArticles.Find(_listingArticle.ListingArticleId);
                _article.ArticleDescription = _listingArticle.ArticleDescription;
                if (_listingArticle.ArticleImage!=null)
                {
                    _article.ArticleImage = _listingArticle.ArticleImage;
                }
                 _article.ArticleTitle= _listingArticle.ArticleTitle;
                _article.IsActive= _listingArticle.IsActive;
                _article.IsCommentOn= _listingArticle.IsCommentOn;
                _article.IsPublished = _listingArticle.IsPublished;
                _article.ModifiedBy = _listingArticle.ModifiedBy;
                _article.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return _article;
            }
        }
    }

}
