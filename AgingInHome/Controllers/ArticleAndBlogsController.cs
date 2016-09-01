using AgingInHome.Helpers;
using AgingInHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet;
using Microsoft.AspNet.Identity;

namespace AgingInHome.Controllers
{
    public class ArticleAndBlogsController : Controller
    {
        // GET: ArticleAndBlogs
        public JsonResult AddArticle(int listingId = 0, int NewsId = 0)
        {
            ViewBag.listingId = listingId;
            ViewBag.NewsId = NewsId;
            if (NewsId > 0)
            {
                var GetNews = new ListingArticleModel().EditNews(NewsId);
                return Json(new { result = GetNews }, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddEditArticle(ListingArticleModel listingArticleModel)
        {
            if (Request.Files.Count > 0)
                listingArticleModel.imageUrl = Request.Files[0];
            if (listingArticleModel.imageUrl != null)
            {
                listingArticleModel.ArticleImage = HelperClass.SaveImage("~/Images/ArticleImages/", listingArticleModel.imageUrl);
            }
            if (listingArticleModel.ListingArticleId > 0)
            {
                listingArticleModel.ModifiedBy = User.Identity.GetUserId();
                listingArticleModel=new ListingArticleModel().UpdateNews(listingArticleModel);
                return Json(new
                {
                    Message = "Article updated successfully",
                    result = listingArticleModel
                }, JsonRequestBehavior.AllowGet);
            }
            listingArticleModel.CreatedBy = User.Identity.GetUserId();
            listingArticleModel=new ListingArticleModel().AddArticle(listingArticleModel);
            return Json(new
            {
                Message = "Article added successfully",
                result=listingArticleModel
            }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddBlog(int listingId = 0, int blogId = 0)
        {
            ViewBag.listingId = listingId;
            ViewBag.blogId = blogId;
            if (blogId > 0)
            {
                var GetBlog = new ListingBlogModel().EditBlog(blogId);
                return Json(new { result = GetBlog }, JsonRequestBehavior.AllowGet);
                //return View(GetBlog);
            }
            //return View(new ListingBlogModel());
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult AddEditBlog(ListingBlogModel listingBlogModel)
        {
            try
            {
                if (Request.Files.Count > 0)
                    listingBlogModel.imageUrl = Request.Files[0];
                if (listingBlogModel.imageUrl != null)
                {
                    listingBlogModel.BlogImage = HelperClass.SaveImage("~/Images/BlogImages/", listingBlogModel.imageUrl);
                }
                if (listingBlogModel.ListingBlogId > 0)
                {
                    listingBlogModel.ModifiedBy = User.Identity.GetUserId();
                    listingBlogModel = new ListingBlogModel().UpdateBlog(listingBlogModel);
                    //return RedirectToAction("MyListing", "ProviderListing");
                    return Json(new
                    {
                        Message = "Blog updated successfully",
                        ListingBlogId = listingBlogModel.ListingBlogId,
                        CreatedDate = listingBlogModel.ModifiedDate,
                        imageurl = listingBlogModel.BlogImage
                    }, JsonRequestBehavior.AllowGet);
                }
                listingBlogModel.CreatedBy = User.Identity.GetUserId();
                listingBlogModel = new ListingBlogModel().addBlog(listingBlogModel);
                return Json(new
                {
                    Message = "Blog added successfully",
                    ListingBlogId = listingBlogModel.ListingBlogId,
                    CreatedDate = listingBlogModel.CreatedDate,
                    imageurl = listingBlogModel.BlogImage
                }, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("MyListing", "ProviderListing");
            }
            catch (Exception ex)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteBlog(int blogId)
        {
            new ListingBlogModel().DeleteBlog(blogId);
            //return RedirectToAction("MyListing", "ProviderListing");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BlogIsPublic(int blogId,bool IsActive= false)
        {
            new ListingBlogModel().BlogIsPublic(blogId, IsActive);
            //return RedirectToAction("MyListing", "ProviderListing");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ArticleIsPublic(int ArticleId, bool IsActive = false)
        {
            new ListingBlogModel().ArticleIsPublic(ArticleId, IsActive);
            //return RedirectToAction("MyListing", "ProviderListing");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PhotoIsPublic(int PhotoId, bool IsActive = false)
        {
            new ListingBlogModel().PhotoIsPublic(PhotoId, IsActive);
            //return RedirectToAction("MyListing", "ProviderListing");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TeamIsPublic(Guid TeamId, bool IsActive = false)
        {
            new ListingBlogModel().TeamIsPublic(TeamId, IsActive);
            //return RedirectToAction("MyListing", "ProviderListing");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VideoIsPublic(int VideoId, bool IsActive = false)
        {
            new ListingBlogModel().VideoIsPublic(VideoId, IsActive);
            //return RedirectToAction("MyListing", "ProviderListing");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteNews(int NewsId)
        {
            new ListingArticleModel().DeleteNews(NewsId);
            //return RedirectToAction("MyListing", "ProviderListing");
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProvideBlogPageData(int pagenumber, int pageSize)
        {
            var UserId = User.Identity.GetUserId();
            List<ProviderBlogViewModel> result = new List<ProviderBlogViewModel>();
            result = new ProviderListingModel().GetProviderBlogData(pagenumber, pageSize, UserId).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProvideNewsPageData(int pagenumber, int pageSize)
        {
            var UserId = User.Identity.GetUserId();
            List<ProviderNewsViewModel> result = new List<ProviderNewsViewModel>();
            result = new ProviderListingModel().GetProviderNewsData(pagenumber, pageSize, UserId).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProvidePhotosPageData(int pagenumber, int pageSize)
        {
            var UserId = User.Identity.GetUserId();
            List<ProviderImageGalaryModel> result = new List<ProviderImageGalaryModel>();
            result = new ProviderListingModel().GetProviderPhotosData(pagenumber, pageSize, UserId).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProvideVideosPageData(int pagenumber, int pageSize)
        {
            var UserId = User.Identity.GetUserId();
            List<ProviderVideosViewModel> result = new List<ProviderVideosViewModel>();
            result = new ProviderListingModel().GetProviderVideosData(pagenumber, pageSize, UserId).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

      

    }
}