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
    public class TeamController : Controller
    {
        // GET: ArticleAndBlogs
        public ActionResult AddArticle(int listingId = 0, int NewsId = 0)
        {
            ViewBag.listingId = listingId;
            ViewBag.NewsId = NewsId;
            if (NewsId > 0)
            {
                var GetNews = new ListingArticleModel().EditNews(NewsId);
                return View(GetNews);
            }
            return View();
        }
        [HttpPost]
        public ActionResult AddArticle(ListingArticleModel listingArticleModel)
        {
            if (listingArticleModel.imageUrl != null)
            {
                listingArticleModel.ArticleImage = HelperClass.SaveImage("~/Images/ArticleImages/", listingArticleModel.imageUrl);
            }
            if (listingArticleModel.ListingArticleId > 0)
            {
                listingArticleModel.ModifiedBy = User.Identity.GetUserId();
                new ListingArticleModel().UpdateNews(listingArticleModel);
                return RedirectToAction("MyListing", "ProviderListing");
            }
            listingArticleModel.CreatedBy = User.Identity.GetUserId();
            new ListingArticleModel().AddArticle(listingArticleModel);
            return RedirectToAction("MyListing", "ProviderListing");
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
        public JsonResult AddEditTeam(ProviderTeamModel ProviderTeamModel)
        {
            try
            {
                if (Request.Files.Count > 0)
                    ProviderTeamModel.imageUrlhttp = Request.Files[0];

                if (ProviderTeamModel.Imagebytes != "" && ProviderTeamModel.Imagebytes != null)
                {
                    //ProviderTeamModel.imageUrl = HelperClass.SaveImage("~/Images/TeamImages/", ProviderTeamModel.imageUrlhttp);
                    ProviderTeamModel.imageUrl = HelperClass.SaveImage64binarystring("~/Images/TeamImages/", ProviderTeamModel.Imagebytes);
                }

                if (ProviderTeamModel.ListingTeamId != Guid.Empty)
                {

                    ProviderTeamModel.ModifiedBy = User.Identity.GetUserId();
                    ProviderTeamModel = new ProviderTeamModel().UpdateTeam(ProviderTeamModel);


                    return Json(new
                    {
                        Message = "Team updated successfully",
                        result = new
                        {
                            imageUrl = ProviderTeamModel.imageUrl,
                            IsActive = ProviderTeamModel.IsActive,
                            ListingId = ProviderTeamModel.ListingId,
                            ListingTeamId = ProviderTeamModel.ListingTeamId,
                            TeamBiography = ProviderTeamModel.TeamBiography,
                            TeamFirstName = ProviderTeamModel.TeamFirstName,
                            TeamLastName = ProviderTeamModel.TeamLastName,
                            TeamMiddleName = ProviderTeamModel.TeamMiddleName,
                            TeamPosition = ProviderTeamModel.TeamPosition,
                            Teamtitle = ProviderTeamModel.Teamtitle
                        }
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    ProviderTeamModel.ListingTeamId = Guid.NewGuid();
                    ProviderTeamModel.CreatedBy = User.Identity.GetUserId();
                    ProviderTeamModel.IsDeleted = false;
                    ProviderTeamModel.CreatedDate = DateTime.Now;
                    ProviderTeamModel.IsActive = true;
                    ProviderTeamModel.ListingId = ProviderTeamModel.ListingId;
                    ProviderTeamModel = new ProviderTeamModel().addTeam(ProviderTeamModel);
                    return Json(new
                    {
                        Message = "Team member added successfully",
                        result = new
                        {
                            imageUrl = ProviderTeamModel.imageUrl,
                            IsActive = ProviderTeamModel.IsActive,
                            ListingId = ProviderTeamModel.ListingId,
                            ListingTeamId = ProviderTeamModel.ListingTeamId,
                            TeamBiography = ProviderTeamModel.TeamBiography,
                            TeamFirstName = ProviderTeamModel.TeamFirstName,
                            TeamLastName = ProviderTeamModel.TeamLastName,
                            TeamMiddleName = ProviderTeamModel.TeamMiddleName,
                            TeamPosition = ProviderTeamModel.TeamPosition,
                            Teamtitle = ProviderTeamModel.Teamtitle
                        }
                    }, JsonRequestBehavior.AllowGet);
                    //return RedirectToAction("MyListing", "ProviderListing");

                }

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

        public JsonResult BlogIsPublic(int blogId, bool IsActive = false)
        {
            new ListingBlogModel().BlogIsPublic(blogId, IsActive);
            //return RedirectToAction("MyListing", "ProviderListing");
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteNews(int NewsId)
        {
            new ListingArticleModel().DeleteNews(NewsId);
            return RedirectToAction("MyListing", "ProviderListing");
        }

        public JsonResult GetProvideTeamPageData(int pagenumber, int pageSize)
        {
            var UserId = User.Identity.GetUserId();
            List<ProviderTeamModel> result = new List<ProviderTeamModel>();
            result = new ProviderListingModel().GetProviderTeamData(pagenumber, pageSize, UserId).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddTeam(Guid? teamId, int listingId = 0)
        {
            if (teamId != Guid.Empty && teamId != null)
            {
                var GetTeam = new ListingBlogModel().EditTeam((Guid)teamId);
                return Json(new { result = GetTeam }, JsonRequestBehavior.AllowGet);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}