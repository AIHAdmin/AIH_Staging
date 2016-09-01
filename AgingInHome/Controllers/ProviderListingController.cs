using AgingInHome.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Configuration;
using AgingInHome.Helpers;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using AgingInHome.BLL.Enums;
using AgingInHome.DAL;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Data;
using System.Xml;
using System.Security.Claims;
using System.Web.Security;
using AgingInHome.Helpers;

namespace AgingInHome.Controllers
{
    public class ProviderListingController : Controller
    {
        [Authorize(Roles = "Provider")]
        public ActionResult Dashboard()
        {
            var listing = new ProviderListingModel();
            var UserId = User.Identity.GetUserId();
            listing = listing.GetListingByUserId(UserId);
            ViewBag.alllisting = new ProviderListingModel().GetAllListing();
            listing.AvgRating = listing.AvgRatings(listing);
            ViewBag.allusers = new AgingInHomeContext().AspNetUsers.ToList();
            ViewBag.CustomeMessages = new InboxModel().GetAllCustomeMessagesByUserid(UserId).GroupBy(p => p.ConversationId)
                                      .Select(s => new CustomeMessagesModel { AllMessages = s.ToList() }
                                      ).ToList();
            return View(listing);
        }
        public ActionResult Index()
        {
            return View();
        }
        //[Authorize]
        public ActionResult SubmitListing(int ListingId = 0)
        {
            var providerListingModel = new ProviderListingModel();
            if (ListingId == 0)
            {
                ViewBag.Categorylist = new SelectList(providerListingModel.AllCategory(), "Id", "CategoryName", "Select the Category");
                //ViewBag.CategoryServicelist = new SelectList(providerListingModel.GetCategoryServices(1), "CategoryServiceId", "Name", "Select the Service");
                ViewBag.CategoryServicelist = providerListingModel.GetCategoryServices(1);

            }
            ViewBag.State = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", "Select the State");
            return View(providerListingModel);
        }

        public PartialViewResult ProviderListingPartial(SearchFilterModel searchFilterModel)
        {
            var providerListingModel = new ProviderListingModel();

            var AllListing = new ProviderListingModel().GetAllListing().Where(g => g.IsApproved == (int)ListingStatus.Accepted).ToList();
            ViewBag.AllListing = AllListing;

            if (searchFilterModel.selectedCategory != null)
            {
                AllListing = AllListing.Where(f => searchFilterModel.selectedCategory.Contains(f.ProviderCategory1.Id)).ToList();
                ViewBag.selectedCat = searchFilterModel.selectedCategory;
            }

            ViewBag.Categorylist = providerListingModel.AllCategory();
            return PartialView(providerListingModel);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult SubmitListing(ProviderListingModel providerListingModel)
        {
            providerListingModel.WhatWeDo = HttpUtility.UrlDecode(providerListingModel.WhatWeDo, System.Text.Encoding.Default);
            providerListingModel.WhyWeDo = HttpUtility.UrlDecode(providerListingModel.WhyWeDo, System.Text.Encoding.Default);
            providerListingModel.WhoWeAre = HttpUtility.UrlDecode(providerListingModel.WhoWeAre, System.Text.Encoding.Default);

            ModelState.Remove("LogoUrl"); ModelState.Remove("YearOfExperience");
            if (providerListingModel.ProviderListingId > 0)
            {
                ModelState.Remove("PrimaryEmail"); ModelState.Remove("Password"); ModelState.Remove("ConfirmPassword"); ModelState.Remove("imageUrl");
            }
            if (ModelState.IsValid)
            {
                if (providerListingModel.ServiceAreas.Count > 0)
                {
                    var cnt = providerListingModel.ServiceAreas.GroupBy(n => n).Any(c => c.Count() > 1);
                }

                if (providerListingModel.ProviderListingId == 0)
                {
                    //if (providerListingModel.Bitimagestring != null)
                    //{
                    //    providerListingModel.Logo = HelperClass.SaveImage64binarystring("~/Images/ProviderListingImages", providerListingModel.Bitimagestring);
                    //}
                    var _ProviderListingModel = new ProviderListingModel();
                    TempData["ListingId"] = _ProviderListingModel.AddProviderListing(providerListingModel);
                    EmailSender.SendEmail(providerListingModel.PrimaryEmail);
                    var ProviderRegisterUser = new RegisterViewModel();
                    ProviderRegisterUser.Email = providerListingModel.PrimaryEmail;
                    ProviderRegisterUser.Password = providerListingModel.Password;
                    ProviderRegisterUser.UserRole = "Provider";
                    TempData["ProviderRegister"] = ProviderRegisterUser;
                    return RedirectToAction("ProviderRegister", "Account");
                }
                else
                {
                    if (providerListingModel.Bitimagestring != null)
                    {
                        providerListingModel.Logo = HelperClass.SaveImage64binarystring("~/Images/ProviderListingImages", providerListingModel.Bitimagestring);
                    }
                    new ProviderListingModel().UpdateProviderListng(providerListingModel);
                    return RedirectToAction("MyListing");
                }

            }

            ViewBag.Categorylist = new SelectList(providerListingModel.AllCategory(), "Id", "CategoryName", "Select the Category");
            ViewBag.State = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", "Select the State");

            var _providerListingModel = new ProviderListingModel();
            return View(_providerListingModel);
        }
        //[Authorize]
        public ActionResult BecomeProvider()
        {
            ViewBag.CategoryList = new ProviderListingModel().AllCategory();
            return View();
        }
        //[Authorize]
        public ActionResult PromoteYourListing(int ProviderlistingId = 0)
        {
            if (ProviderlistingId == 0)
            {
                var listingId = (int)TempData["ListingId"];
                var userid = User.Identity.GetUserId();
                new ProviderListingModel().UpdateUserId(userid, listingId);
            }

            var servicesmodel = new ServicesModel();
            servicesmodel.servicelist = new ServicesModel().GetAllServices();
            return View(servicesmodel);
        }
        [HttpPost]
        public bool PromoteYourListing(OrderModel orderModel)
        {
            TempData["orderModel"] = new ServicesModel().AllSelectedServices(orderModel);

            return true;
        }
        public PartialViewResult ServiceAreaPartial(int count)
        {
            var providerListingModel = new ProviderListingModel();
            ViewBag.Count = count;
            ViewBag.State = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", "Select the State");
            return PartialView(providerListingModel);
        }
        public PartialViewResult LoadCategoryService(int categoryId)
        {
            // ViewBag.CategoryServicelist = new SelectList(new ProviderListingModel().GetCategoryServices(categoryId), "CategoryServiceId", "Name", "Select the Service");
            ViewBag.CategoryServicelist = new ProviderListingModel().GetCategoryServices(categoryId);
            return PartialView();
        }
        public JsonResult GetCategoryServicesByCatId(int categoryId)
        {
            var CategoryServicelist = new ProviderListingModel().GetCategoryServices(categoryId);
            return Json(new { result = CategoryServicelist }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult LoadCustomService(int count)
        {
            ViewBag.customId = count;
            return PartialView();
        }
        [Authorize]
        public ActionResult MyListing()
        {
            var listing = new ProviderListingModel();
            var UserId = User.Identity.GetUserId();
            listing = listing.GetListingByUserId(UserId);
            listing.ListingBlogs = listing.ListingBlogs.OrderByDescending(c => c.ListingBlogId).ToList();
            listing.AvgRating = listing.AvgRatings(listing);
            return View(listing);
        }
        public ActionResult ViewListing(string CompanyName)
        {
            CompanyName = CompanyName.Replace('-', ' ');
            var listing = new ProviderListingModel();
            listing = listing.GetListingByListingName(CompanyName);
            List<ServiceCategoryViewModel> _ServiceCategoryViewModelList = new List<ServiceCategoryViewModel>();
            if (listing.ServiceDetails.Count > 0)
            {
                foreach (var item in listing.ServiceDetails)
                {
                    ServiceCategoryViewModel ob = new ServiceCategoryViewModel();
                    ob.ServiceCategoryId = (long)item.Id;
                    ob.ServiceCategoryPrice = item.ServicePrice == null ? 0 : Convert.ToInt32(item.ServicePrice);
                    CategoryService _CategoryService = listing.GetCategoryServicesById((int)item.Id);
                    ob.ServiceCategoryName = _CategoryService == null ? "" : _CategoryService.Name;
                    _ServiceCategoryViewModelList.Add(ob);
                }
            }

            listing.ServiceCategoryViewModel = _ServiceCategoryViewModelList;
            var userid = User.Identity.GetUserId();
            //Get Logged In UserID
            listing.LoggedInUserId = userid;
            //var userIdentity = (ClaimsIdentity)User.Identity;
            //var claims = userIdentity.Claims;
            //var roleClaimType = userIdentity.RoleClaimType;
            //var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            //if (roles[0].Value == "Consumer")            
            if (User.Identity.GetUserId() != "")
            {
                if (User.IsInRole("Consumer"))
                {
                    ViewBag.ConsumerDetail = new AgingInHomeContext().AspNetUsers.Include("ConsumerProfiles").FirstOrDefault(s => s.Id == userid);
                }
                else
                {
                    ViewBag.ConsumerDetail = null;
                }
            }


            ViewBag.checkRatingShowOrNot = new AppointmentModel().CheckAppointmentStatus(userid, listing.UserId);
            GetProfileId(listing.ProviderListingId);
            ViewBag.SimilarProvider = listing.GetAllListing().Where(m => m.ProviderCategory1.Id == listing.ProviderCategory1.Id && m.IsApproved == (int)ListingStatus.Accepted && m.ProviderListingId != listing.ProviderListingId).ToList();
            return View(listing);
        }
        [Authorize]
        public ActionResult EditProviderListing()
        {
            var listing = new ProviderListingModel();
            var UserId = User.Identity.GetUserId();
            listing = listing.GetListingByUserId(UserId);
            ViewBag.Categorylist = new SelectList(listing.AllCategory(), "Id", "CategoryName", Convert.ToInt32(listing.CategoryId));
            ViewBag.State = new ServicesModel().GetUsStates();
            //ViewBag.State = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", listing.ServiceAreas[0].UsState.Id);
            //ViewBag.CategoryServicelist = new SelectList(listing.GetCategoryServices(Convert.ToInt32(listing.CategoryId)), "CategoryServiceId", "Name", listing.CategoryServiceId);
            ViewBag.CategoryServicelist = listing.GetCategoryServices(Convert.ToInt32(listing.CategoryId));
            return View(listing);
        }
        [HttpPost]
        public ActionResult PreviewProviderListing(ProviderListingModel providerListingModel)
        {
            var _providerListingModel = new ProviderListingModel();
            if (ModelState.IsValid)
            {

                if (providerListingModel.ServiceAreas.Count > 0)
                {
                    var ifDuplicateCities = providerListingModel.ServiceAreas.GroupBy(n => n.City).Any(c => c.Count() > 1);
                    if (ifDuplicateCities)
                    {
                        ViewBag.Categorylist = new SelectList(providerListingModel.AllCategory(), "Id", "CategoryName", "Select the Category");
                        ViewBag.State = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", "Select the State");
                        _providerListingModel.CityError = "Duplicate cities not allowed for providing service.";
                        return View("SubmitListing", _providerListingModel);
                    }
                }

                if (providerListingModel.Bitimagestring != null)
                {
                    providerListingModel.Logo = HelperClass.SaveImage64binarystring("~/Images/ProviderListingImages", providerListingModel.Bitimagestring);
                }
                ViewBag.Categorylist = new SelectList(providerListingModel.AllCategory(), "Id", "CategoryName", Convert.ToInt32(providerListingModel.CategoryId));
                ViewBag.State = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", providerListingModel.ServiceAreas[0].StateId);
                return View(providerListingModel);
            }
            ViewBag.Categorylist = new SelectList(providerListingModel.AllCategory(), "Id", "CategoryName", "Select the Category");
            ViewBag.State = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", "Select the State");

            //var _providerListingModel = new ProviderListingModel();
            return View("SubmitListing", _providerListingModel);

        }
        public ActionResult RatingDetail(int listingId)
        {
            return View();
        }
        public bool ListingRating(RatingModal _RatingModel)
        {
            return _RatingModel.AddRating(_RatingModel);
        }
        public bool EmailExits(RatingModal _RatingModel)
        {
            return _RatingModel.CheckEmailExits(_RatingModel);
        }
        public ActionResult SearchListing(SearchFilterModel searchFilterModel)
        {
            int pageSize = 5;
            int pageIndex = 1;
            pageIndex = searchFilterModel.page.HasValue ? Convert.ToInt32(searchFilterModel.page) : 1;

            ViewBag.CurrentSort = "desc";
            if (Session["selectedCategory"] != null && searchFilterModel.selectedCategory == null)
            {
                if (searchFilterModel.page.HasValue)
                    searchFilterModel.selectedCategory = (List<int>)Session["selectedCategory"];
                else
                    Session["selectedCategory"] = null;

            }
            else if (Session["selectedCategory"] == null && searchFilterModel.selectedCategory != null)
            {
                Session["selectedCategory"] = searchFilterModel.selectedCategory;
            }
            else if (Session["selectedCategory"] != null && searchFilterModel.selectedCategory != null)
            {
                var selectedCategory = (List<int>)Session["selectedCategory"];
                if (selectedCategory.Count() != searchFilterModel.selectedCategory.Count())
                {
                    Session["selectedCategory"] = searchFilterModel.selectedCategory;
                }
            }

            ViewBag.Location = searchFilterModel.Location;


            var AllListing = new ProviderListingModel().GetAllListing().Where(g => g.IsApproved == (int)ListingStatus.Accepted).ToList();
            ViewBag.AllListing = AllListing;
            ViewBag.CategoryList = new ProviderListingModel().AllCategory();
            ViewBag.Categorylists = new SelectList(new ProviderListingModel().AllCategory(), "Id", "CategoryName", "Select the Category");

            if (searchFilterModel.Location != null)
            {
                var newAllListing = new List<ProviderListingModel>();
                foreach (var list in AllListing)
                {
                    foreach (var Servicearea in list.ServiceAreas)
                    {

                        int num;
                        bool isNum = int.TryParse(searchFilterModel.Location, out num);
                        if (isNum)
                        {
                            int CheckZipCode = Convert.ToInt32(searchFilterModel.Location);
                            var distance = getDistance(CheckZipCode, (int)Servicearea.ZipCode);
                            if (distance <= Convert.ToDecimal(Servicearea.ServiceRadius))
                            {
                                newAllListing.Add(list);
                                break;
                            }

                        }
                        else
                        {
                            if (Servicearea.City.ToLower() == searchFilterModel.Location.ToLower())
                            {
                                newAllListing.Add(list);
                                break;
                            }
                        }

                    }
                }
                if (searchFilterModel.selectedCategory != null)
                {
                    newAllListing = newAllListing.Where(f => searchFilterModel.selectedCategory.Contains(f.ProviderCategory1.Id)).ToList();
                    ViewBag.selectedCat = searchFilterModel.selectedCategory;
                }
                if (searchFilterModel.SortBy != null)
                {
                    newAllListing = newAllListing.Where(f => f.ProviderCategory1.Id == searchFilterModel.SortBy).ToList();
                }

                return View(newAllListing.OrderByDescending(d => d.ProviderListingId).ToPagedList(searchFilterModel.page ?? 1, pageSize));
            }
            if (searchFilterModel.selectedCategory != null)
            {
                AllListing = AllListing.Where(f => searchFilterModel.selectedCategory.Contains(f.ProviderCategory1.Id)).ToList();
                ViewBag.selectedCat = searchFilterModel.selectedCategory;
            }
            if (searchFilterModel.SortBy != null)
            {
                AllListing = AllListing.Where(f => f.ProviderCategory1.Id == searchFilterModel.SortBy).ToList();
            }
            return View(AllListing.OrderByDescending(d => d.ProviderListingId).ToPagedList(searchFilterModel.page ?? 1, pageSize));
        }
        public JsonResult UserEmailExits(string PrimaryEmail)
        {
            return Json(new RatingModal().UserEmailExits(PrimaryEmail), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UserEmailExit(string Email)
        {
            return Json(new RatingModal().UserEmailExits(Email), JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddAboutUs(int listingId)
        {
            ViewBag.listingId = listingId;
            return View();
        }
        [HttpPost]
        public ActionResult AddAboutUs(ListingAboutusModel listingAboutusModel)
        {
            listingAboutusModel.CreatedBy = User.Identity.GetUserId();
            new ListingAboutusModel().AddAboutUs(listingAboutusModel);
            return RedirectToAction("MyListing");
        }
        public ActionResult UpdateAboutUs(int ListingAboutUsId)
        {
            var model = new ListingAboutusModel().EditDescription(ListingAboutUsId);
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateAboutUs(ListingAboutusModel listingAboutusModel)
        {
            new ListingAboutusModel().UpdateDescription(listingAboutusModel);
            return RedirectToAction("MyListing");
        }
        public bool ChangeStatusOfTabs(bool status, string feildName, int providerListingId)
        {
            return new ListingAboutusModel().ChangeStatusOfTabs(status, feildName, providerListingId);
        }
        public JsonResult AddImage(int ProviderListingId)
        {
            try
            {
                ProviderImageGalaryModel image = new ProviderImageGalaryModel();
                image.ProviderListingId = ProviderListingId;
                if (Request.Files.Count > 0)
                    image.ImageUrl = Request.Files[0];
                image.ImagePath = HelperClass.SaveImage("~/Images/ProviderGalaryImages", image.ImageUrl);
                image.CreatedBy = User.Identity.GetUserId();
                image.CreatedDate = DateTime.Now;
                image.Id = new ProviderListingModel().addProviderImage(image);
                return Json(new { ImagePath = image.ImagePath, Id = image.Id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult AddVideo(ProviderVideo video)
        {
            try
            {
                video.CreatedBy = User.Identity.GetUserId();
                video.CreatedDate = DateTime.Now;
                video.Id = new ProviderListingModel().addProvidervideo(video);
                return Json(new { VideoUrl = video.VideoUrl, Id = video.Id }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public bool DeleteImage(int imgid)
        {
            return new ProviderListingModel().DelProviderImage(imgid);
        }
        public bool DeleteVideo(int Videoid)
        {
            return new ProviderListingModel().DelProviderVideoLink(Videoid);
        }
        public bool DeleteTeam(Guid TeamId)
        {
            return new ProviderListingModel().DelProviderTeamLink(TeamId);
        }

        public decimal getDistance(int origin, int destination)
        {
            string url = @"http://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&sensor=false";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close();
            try
            {
                string times = "";
                DataSet ds = new DataSet();
                ds.ReadXml(new XmlTextReader(new StringReader(responsereader)));
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables["element"].Rows[0]["status"].ToString() == "OK")
                    {
                        // var dis = ds.Tables["duration"].Rows[0]["text"].ToString();
                        times = ds.Tables["distance"].Rows[0]["text"].ToString();
                        var distance = times.Split(' ');
                        decimal dd = Convert.ToDecimal(distance[0]) * Convert.ToDecimal(0.621371);

                        return dd;

                    }
                    return 100000;
                }

                return 100000;
            }
            catch (Exception)
            {
                return 100000;

            }



            //double distance = 0;
            ////string from = origin.Text;
            ////string to = destination.Text;
            //string url = "http://maps.googleapis.com/maps/api/directions/json?origin=" + origin + "&destination=" + destination + "&sensor=false";
            //string requesturl = url;
            ////string requesturl = @"http://maps.googleapis.com/maps/api/directions/json?origin=" + from + "&alternatives=false&units=imperial&destination=" + to + "&sensor=false";
            //string content = fileGetContents(requesturl);

            //try
            //{
            //    JObject o = JObject.Parse(content);
            //    var dd=(double) o.SelectToken("routes[0].legs[0].distance.value");
            //    distance = dd/ 1609;
            //    return distance;
            //}
            //catch
            //{
            //    return 10000;
            //}

        }
        protected string fileGetContents(string fileName)
        {
            string sContents = string.Empty;
            string me = string.Empty;
            try
            {
                if (fileName.ToLower().IndexOf("http:") > -1)
                {
                    System.Net.WebClient wc = new System.Net.WebClient();
                    byte[] response = wc.DownloadData(fileName);
                    sContents = System.Text.Encoding.ASCII.GetString(response);
                }
                else
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
                    sContents = sr.ReadToEnd();
                    sr.Close();
                }
            }
            catch { sContents = "unable to connect to server "; }
            return sContents;
        }
        public string GetZipCoordinates(string zip)
        {
            string address = "";
            address = "http://maps.googleapis.com/maps/api/geocode/xml?components=postal_code:" + zip.Trim() + "&sensor=false";

            var result = new System.Net.WebClient().DownloadString(address);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(result);
            XmlNodeList parentNode = doc.GetElementsByTagName("location");
            var lat = "";
            var lng = "";
            foreach (XmlNode childrenNode in parentNode)
            {
                lat = childrenNode.SelectSingleNode("lat").InnerText;
                lng = childrenNode.SelectSingleNode("lng").InnerText;
            }

            return lat + "," + lng;
        }
        public bool UpdateAchieveProviderStatus(int RequestId)
        {
            var model = new InboxModel().UpdateAchieveStatusByProvider(RequestId, User.Identity.GetUserId());
            return model;
        }
        public PartialViewResult GetinboxDetail()
        {
            var listing = new ProviderListingModel();
            var UserId = User.Identity.GetUserId();
            listing = listing.GetListingByUserId(UserId);
            listing.AvgRating = listing.AvgRatings(listing);
            ViewBag.allusers = new AgingInHomeContext().AspNetUsers.ToList();
            ViewBag.CustomeMessages = new InboxModel().GetAllCustomeMessagesByUserid(UserId).GroupBy(p => p.ConversationId)
                                     .Select(s => new CustomeMessagesModel { AllMessages = s.ToList() }
                                     ).ToList();
            return PartialView("_providerInbox", listing.ServiceRequestDetails.ToList());
        }
        public PartialViewResult ArchiMessageDetail(int RequestId)
        {
            var model = new InboxModel().GetMessageDetailByDetailId(RequestId, User.Identity.GetUserId());
            ViewBag.alllisting = new ProviderListingModel().GetAllListing();
            ViewBag.msgType = "ProviderArchive";
            return PartialView("_MessageDetailPartial", model.OrderByDescending(d => d.Id).ToList());
        }
        public PartialViewResult GetAllProviderAppointments(string bestTime, string status, int pagenumber = 1, int pageSize = 5)
        {
            int startRow = (pagenumber - 1) * pageSize;            
            var userid = User.Identity.GetUserId();
            var GetAllProviderAppointment = new AppointmentModel().GetAllProviderAppointment(userid).OrderByDescending(s => s.AppointmentId).ToList();
            ViewBag.TotalRecords = GetAllProviderAppointment.Count;
            if (!string.IsNullOrEmpty(bestTime) && string.IsNullOrEmpty(status))
            {
                ViewBag.TotalRecords = GetAllProviderAppointment.Where(x => x.BestTime == bestTime).ToList().Count;
                GetAllProviderAppointment = GetAllProviderAppointment.Where(x => x.BestTime == bestTime).Skip(startRow).Take(pageSize).ToList();
            }
            else
            {
                GetAllProviderAppointment = GetAllProviderAppointment.Skip(startRow).Take(pageSize).ToList();
            }
            return PartialView(GetAllProviderAppointment);
        }
        public bool MakeAppointment(AppointmentModel model)
        {
            model.SubmitAppointment(model, "Provider");
            var ConumerUser = new AgingInHomeContext().AspNetUsers.FirstOrDefault(s => s.Id == model.ConsumerId);
            EmailSender.SendEmailForNewMsg(ConumerUser.Email, User.Identity.Name);
            return true;
        }

        [HttpPost]
        public JsonResult SaveAboutUsDetails(string whatWeDo, string whyWeDo, string whoWeAre, int ProviderListingId)
        {
            whatWeDo = HttpUtility.UrlDecode(whatWeDo, System.Text.Encoding.Default);
            whyWeDo = HttpUtility.UrlDecode(whyWeDo, System.Text.Encoding.Default);
            whoWeAre = HttpUtility.UrlDecode(whoWeAre, System.Text.Encoding.Default);
            var _result = new ProviderListingModel().SaveAboutUsDetails(whatWeDo, whyWeDo, whoWeAre, ProviderListingId);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public void GetProfileId(int Id)
        {
            var listing = new ProviderListingModel();
            ViewBag.ProfileId = listing.GetProfileId(Id);
        }
        public JsonResult GetProvideTeamPageData(int pagenumber, int pageSize)
        {
            var UserId = User.Identity.GetUserId();
            List<ProviderTeamModel> result = new List<ProviderTeamModel>();
            result = new ProviderListingModel().GetProviderTeamData(pagenumber, pageSize, UserId).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProviderListing()
        {
            var listing = new ProviderListingModel();
            return Json(new { result = listing.GetProviderListing() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BlogNewsDetails(int BlogNewsId, bool IsBlog)
        {
            BlogNewsDetailViewModel _BlogNewsDetailViewModel = new ProviderListingModel().GetBlogNewsDetails(BlogNewsId, IsBlog);
            _BlogNewsDetailViewModel.BlogNewsId = BlogNewsId;
            _BlogNewsDetailViewModel.IsBlog = IsBlog;
            return View(_BlogNewsDetailViewModel);
        }

        public JsonResult SaveBlogNewComment(ListingBlogCommentsModel blogNewsDetailViewModel)
        {
            blogNewsDetailViewModel.CreatedBy = User.Identity.GetUserId();
            blogNewsDetailViewModel.CreatedDate = DateTime.UtcNow;
            blogNewsDetailViewModel.IsActive = true;
            ListingBlogCommentsModel _BlogNewsDetailViewModel = new ProviderListingModel().SaveBlogNewComment(blogNewsDetailViewModel);
            return Json(_BlogNewsDetailViewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBlogNewsComments(int BlogNewsId, bool IsBlog)
        {
            List<ListingBlogCommentsModel> _BlogNewsDetailViewModel = new ProviderListingModel().GetBlogNewsComments(BlogNewsId, IsBlog);
            return Json(_BlogNewsDetailViewModel, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult SearchInboxMailByText(string search)
        {
            string host = Request.Url.Host.ToLower();
            var userid = User.Identity.GetUserId();
            var listing = new ProviderListingModel().GetProviderInboxMailDetails(userid);
            ViewBag.search = search;
            ViewBag.alllisting = new ProviderListingModel().GetAllListing();
            ViewBag.allusers = new AgingInHomeContext().AspNetUsers.ToList();
            ViewBag.CustomeMessages = new InboxModel().GetAllCustomeMessagesByUserid(userid).GroupBy(p => p.ConversationId)
                                      .Select(s => new CustomeMessagesModel { AllMessages = s.ToList() }
                                      ).ToList();
            return PartialView(listing.ServiceRequestDetails.ToList());
        }
    }
}