using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgingInHome.DAL;

namespace AgingInHome.BLL.Repositories
{
    public interface IListingblogRepository
    {
        ListingBlog AddBlog(ListingBlog listingBlog);
        ListingBlog EditBlog(int listingBlogId);
        ListingBlog UpdateBlog(ListingBlog _ListingBlog);
        ListingBlog GetBlogById(int blogId);
        bool DelBlogById(int BlogId);
        bool BlogIsPublic(int BlogId, bool IsActive);
        bool ArticleIsPublic(int ArticleId, bool IsActive);
        bool VideoIsPublic(int VideoId, bool IsActive);
        bool TeamIsPublic(Guid TeamId, bool IsActive);
        ProviderTeam GetTeamById(Guid teamid);
    }
    public class ListingBlogClass : IListingblogRepository
    {
        public ListingBlog AddBlog(ListingBlog listingBlog)
        {
            using (var db = new AgingInHomeContext())
            {
                db.ListingBlogs.Add(listingBlog);
                db.SaveChanges();
                return listingBlog;
            }
        }

        public bool DelBlogById(int BlogId)
        {
            using (var db = new AgingInHomeContext())
            {
                var _Blog = db.ListingBlogs.Find(BlogId);
                db.ListingBlogs.Remove(_Blog);
                db.SaveChanges();
                return true;

            }
        }
        public bool BlogIsPublic(int BlogId, bool IsActive)
        {
            using (var db = new AgingInHomeContext())
            {
                var _Blog = db.ListingBlogs.Find(BlogId);
                _Blog.IsActive = IsActive;
                db.SaveChanges();
                return true;

            }
        }

        public bool ArticleIsPublic(int ArticleId, bool IsActive)
        {
            using (var db = new AgingInHomeContext())
            {
                var Article = db.ListingArticles.Find(ArticleId);
                Article.IsActive = IsActive;
                db.SaveChanges();
                return true;
            }
        }

        public bool PhotoIsPublic(int PhotoId, bool IsActive)
        {
            using (var db = new AgingInHomeContext())
            {
                var Photos = db.ProviderImageGalaries.Find(PhotoId);
                Photos.IsActive = IsActive;
                db.SaveChanges();
                return true;
            }
        }
        public bool TeamIsPublic(Guid TeamId, bool IsActive)
        {
            using (var db = new AgingInHomeContext())
            {
                var Photos = db.ProviderTeams.Find(TeamId);
                Photos.IsActive = IsActive;
                db.SaveChanges();
                return true;
            }
        }
        public bool VideoIsPublic(int VideoId, bool IsActive)
        {
            using (var db = new AgingInHomeContext())
            {
                var Photos = db.ProviderVideos.Find(VideoId);
                Photos.IsActive = IsActive;
                db.SaveChanges();
                return true;
            }
        }

        public ListingBlog EditBlog(int listingBlogId)
        {
            using (var db = new AgingInHomeContext())
            {
                var _Blog = db.ListingBlogs.Find(listingBlogId);

                return _Blog;

            }
        }

        public ListingBlog GetBlogById(int blogId)
        {
            using (var db = new AgingInHomeContext())
            {
                return db.ListingBlogs.Find(blogId);
            }
        }

        public ProviderTeam GetTeamById(Guid teamid)
        {
            using (var db = new AgingInHomeContext())
            {
                return db.ProviderTeams.Find(teamid);
            }
        }

        public ListingBlog UpdateBlog(ListingBlog _ListingBlog)
        {
            using (var db = new AgingInHomeContext())
            {
                var _Blog = db.ListingBlogs.Find(_ListingBlog.ListingBlogId);
                _Blog.BlogDescription = _ListingBlog.BlogDescription;
                if (_ListingBlog.BlogImage != null)
                {
                    _Blog.BlogImage = _ListingBlog.BlogImage;
                }
                _Blog.BlogTitle = _ListingBlog.BlogTitle;
                _Blog.IsActive = _ListingBlog.IsActive;
                _Blog.IsCommentOn = _ListingBlog.IsCommentOn;
                _Blog.IsPublished = _ListingBlog.IsPublished;
                _Blog.ModifiedBy = _ListingBlog.ModifiedBy;
                _Blog.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return _Blog;
            }
        }
    }
}
