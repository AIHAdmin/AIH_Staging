using AgingInHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgingInHome.Helpers;
using AgingInHome.DAL;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace AgingInHome.Controllers

{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {

        public ActionResult Dashboard()        
        {
            int totalCount = 0;
            int totalMarkUpCount = 0;
            int totalAnalyticCount= 0;
            var model = new AdminDashboardModel();
            var userid = User.Identity.GetUserId();
            var AllListing = new ProviderListingModel().GetAllListing().OrderByDescending(f => f.ProviderListingId).ToList();
            List<string> UserIds = AllListing.Select(m => m.UserId).ToList();
            
            var AllServiceRequests = new ProviderListingModel().GetAllServiceRequest().OrderByDescending(s => s.Id).ToList();
            var AllAppointments = new AppointmentModel().GetAllAppointments().Where(m=>UserIds.Contains(m.ProviderId));
            var AllQueuelisting = new QueueListingModel().GetAllQueueListing();
            var UnassignedProviders = AllQueuelisting.Where(m => m.IsQueue == true);
            var AssignedProviders = AllQueuelisting.Where(m => m.IsQueue == false);
            ViewBag.allusers = new AgingInHomeContext().AspNetUsers.ToList();
            model.AllListingNew = new ProviderListingModel().GetAllListingNew(1, 5, out totalCount);
            model.AllListing = AllListing;
            model.AllQueuelisting = AllQueuelisting;
            model.UnassignProvider = UnassignedProviders.Skip(0).Take(5).ToList();
            model.AssignedProvider = AssignedProviders.Skip(0).Take(5).ToList();
            model.AllAppointment = AllAppointments.Skip(0).Take(5).ToList() ;
            model.AllServiceRequests = AllServiceRequests.Skip(0).Take(5).ToList();
            model.AnalyticsModelList = new AnalyticsModel().GetProviderAnalyticNew(1, 5, out totalAnalyticCount);
            model.MarkUpList = new MarkupModel().GetAllMarkups(1, 5, out totalMarkUpCount);
            ViewBag.CustomeMessages = new InboxModel().GetAllCustomeMessagesByUserid(userid).OrderByDescending(e => e.SentDate).GroupBy(p => p.ConversationId)
                                      .Select(s => new CustomeMessagesModel { AllMessages = s.ToList() }
                                      ).ToList();
            ViewBag.UnAssignedProvidersCount = UnassignedProviders.ToList().Count;
            ViewBag.AssignedProvidersCount = AssignedProviders.ToList().Count;
            ViewBag.AlllistingCount = totalCount;
            ViewBag.AllServiceRequests = AllServiceRequests.Count;
            ViewBag.TotalMarkUpCount = totalMarkUpCount;
            ViewBag.TotalAnalyticCount = totalAnalyticCount;
            ViewBag.AppointmentsCount = AllAppointments.ToList().Count;
            return View("AdminDashboard",model);
        }

        public PartialViewResult GetProviderListingRequest(int pagenumber, int pageSize)
        {
            int totalRcords = 0;
            int startRow = (pagenumber - 1) * pageSize;
            var result = new ProviderListingModel().GetAllListingNew(pagenumber,pageSize,out totalRcords).OrderByDescending(f => f.ProviderListingId).ToList();
           // totalRcords = result.Count();
           // result = result.Skip(startRow).Take(pageSize).ToList();
            return PartialView(result);
        }

        public PartialViewResult GetServiceRequest(int pagenumber, int pageSize)
        {
            long totalRcords = 0;
            int startRow = (pagenumber - 1) * pageSize;
            var result = new ProviderListingModel().GetAllServiceRequest().OrderByDescending(s => s.Id).ToList();
            totalRcords = result.Count();
            result = result.Skip(startRow).Take(pageSize).ToList();
            return PartialView(result);
        }

        public PartialViewResult GetAppointmentList(int pagenumber, int pageSize)
        {
            var model = new AdminDashboardModel();
            var AllListing = new ProviderListingModel().GetAllListing().OrderByDescending(f => f.ProviderListingId).ToList();
            List<string> UserIds = AllListing.Select(m => m.UserId).ToList();
            model.AllListing = AllListing;
            long totalRcords = 0;
            int startRow = (pagenumber - 1) * pageSize;
            var result = new AppointmentModel().GetAllAppointments().Where(m=>UserIds.Contains(m.ProviderId));
            totalRcords = result.Count();
            result = result.Skip(startRow).Take(pageSize).ToList();
            model.AllAppointment = result.ToList();
            return PartialView(model);
        }

        public PartialViewResult GetUnassignProviderList(int pagenumber, int pageSize)
        {
            long totalRcords = 0;
            int startRow = (pagenumber - 1) * pageSize;
            var result = new QueueListingModel().GetAllQueueListing().Where(m=>m.IsQueue == true).ToList();
            totalRcords = result.Count();
            result = result.Skip(startRow).Take(pageSize).ToList();
            return PartialView(result);
        }

        public PartialViewResult GetAssignProviderList(int pagenumber, int pageSize)
        {
            long totalRcords = 0;
            int startRow = (pagenumber - 1) * pageSize;
            var result = new QueueListingModel().GetAllQueueListing().Where(m => m.IsQueue == false).ToList();
            totalRcords = result.Count();
            result = result.Skip(startRow).Take(pageSize).ToList();
            return PartialView(result);
        }

        public PartialViewResult GetMarkUpsList(int pagenumber,int pageSize)
        {
            int totalRcords = 0;
            int startRow = (pagenumber - 1) * pageSize;
            var result = new MarkupModel().GetAllMarkups(pagenumber,pageSize,out totalRcords );
            //totalRcords = result.Count();
            return PartialView(result);
        }

        public PartialViewResult GetAnalyticsList(int pageNumber, int pageSize)
        {
            int totalRcords = 0;
            var result = new AnalyticsModel().GetProviderAnalyticNew(pageNumber, pageSize, out totalRcords);
            return PartialView(result);
        }

        public PartialViewResult LoadInboxLeftContent()
        {
            var userid = User.Identity.GetUserId();
            ViewBag.CustomeMessages = new InboxModel().GetAllCustomeMessagesByUserid(userid)
                                      .Where(send => send.isAdminArchive != true)
                                      .OrderByDescending(e => e.SentDate).GroupBy(p => p.ConversationId)
                                      .Select(s => new CustomeMessagesModel { AllMessages = s.ToList() }
                                      ).ToList();
            ViewBag.allusers = new AgingInHomeContext().AspNetUsers.ToList();
            return PartialView();
        }

        public PartialViewResult SearchAdminMails(string keyword)
        {
            var userid = User.Identity.GetUserId();
            ViewBag.CustomeMessages = new InboxModel().GetAllCustomeMessagesByUserid(userid)
                                      .Where(send => send.isAdminArchive != true)
                                      .OrderByDescending(e => e.SentDate).GroupBy(p => p.ConversationId)
                                      .Select(s => new CustomeMessagesModel { AllMessages = s.ToList() }
                                      ).ToList();
            ViewBag.allusers = new AgingInHomeContext().AspNetUsers.ToList();
            ViewBag.Keyword = keyword;
            if (string.IsNullOrEmpty(keyword))
                return PartialView("LoadInboxLeftContent1");
            else
                return PartialView();
        }
        


        public PartialViewResult LoadInboxLeftContent1()
        {
            var userid = User.Identity.GetUserId();
            ViewBag.CustomeMessages = new InboxModel().GetAllCustomeMessagesByUserid(userid)
                                      .Where(send => send.isAdminArchive != true)
                                      .OrderByDescending(e => e.SentDate).GroupBy(p => p.ConversationId)
                                      .Select(s => new CustomeMessagesModel { AllMessages = s.ToList() }
                                      ).ToList();
            ViewBag.allusers = new AgingInHomeContext().AspNetUsers.ToList();
            return PartialView();
        }

        public PartialViewResult GetAdminMessageBody()
        {
            var userid = User.Identity.GetUserId();
            ViewBag.CustomeMessages = new InboxModel().GetAllCustomeMessagesByUserid(userid)
                                      .Where(send => send.isAdminArchive != true)
                                      .OrderByDescending(e => e.SentDate).GroupBy(p => p.ConversationId)
                                      .Select(s => new CustomeMessagesModel { AllMessages = s.ToList() }
                                      ).ToList();
            ViewBag.allusers = new AgingInHomeContext().AspNetUsers.ToList();
            return PartialView();
        }


        public bool UpdateListingStatus(int listingId, int listingStatus)
        {
            return new ProviderListingModel().UpdateListingStatus(listingId, listingStatus);
        }
        public PartialViewResult AvailableProviderList(int catId)
        {
            var model = new ProviderListingModel().GetAllListing().Where(s => s.ProviderCategory1.Id == catId).ToList();
            if (model.Count() < 1)
            {
                ViewBag.Count = 0;
            }
            ViewBag.AvailableProviderList = new SelectList(model, "ProviderListingId", "CompanyName", "Select Provider");
            return PartialView();
        }
        public bool AssignProvider(QueueListingModel obj)
        {
            var getUser = new QueueListingModel().GetAllQueueListing().Where(d => d.Id == obj.Id).FirstOrDefault().AspNetUser;
            var provider = new ProviderListingModel().GetListingByListingId(obj.ProviderListingId);
            var serviceequestId = new QueueListingModel().ManuallyAssignProvider(obj.ProviderListingId, Convert.ToInt32(obj.ServiceRequestCatId), obj.Id);
            var servicerequestmodel = new ServiceRequestModel();
            servicerequestmodel.FirstName = getUser.ConsumerProfiles.FirstOrDefault().FirstName;
            servicerequestmodel.LastName = getUser.ConsumerProfiles.FirstOrDefault().LastName;
            servicerequestmodel.Address = "";
            servicerequestmodel.PrimaryPhone = getUser.ConsumerProfiles.FirstOrDefault().PrimaryPhone;
            servicerequestmodel.Email = getUser.Email;
            servicerequestmodel.ServiceDate = Convert.ToDateTime(obj.ServiceDate);
            servicerequestmodel.BestTime = "Afternoon";
            servicerequestmodel.Id = serviceequestId;
            EmailSender.SendEmailToServiceProvider(provider, servicerequestmodel);
            return true;
        }
        public JsonResult GetAllUsers(String term)
        {
            var db = new AgingInHomeContext();

            var result = db.AspNetUsers.Where(d => d.Email.StartsWith(term))
                .Select(g => g.Email).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public bool ComposeMessage(InboxModel model)
        {
            var message = new InboxModel()
            {
                IsActive = true,
                IsArchive = false,
                IsProviderArchive = false,
                IsRead = false,
                IsStarred = false,
                MessageBody = model.MessageBody,
                RecipientId = model.RecipientId,
                SenderId = User.Identity.GetUserId(),
                SentDate = DateTime.Now,
                Subject = model.Subject
            };
            return true;
        }
        public bool SendCustomerMessage(InboxModel message)
        {
            var userid = User.Identity.GetUserId();
            message.RecipientId = new AgingInHomeContext().AspNetUsers.FirstOrDefault(s => s.Email == message.ReciptEmail).Id;
            message.IsActive = true;
            message.IsArchive = false;
            message.IsProviderArchive = false;
            message.IsRead = false;
            message.IsStarred = false;
            message.SenderId = userid;
            message.SentDate = DateTime.Now;
            new InboxModel().SaveCustomeMessage(message);
            return true;
        }
        public PartialViewResult LoadAdminInbox()
        {
            var userid = User.Identity.GetUserId();
            ViewBag.CustomeMessages = new InboxModel().GetAllCustomeMessagesByUserid(userid)
                                      .Where(send => send.isAdminArchive != true)
                                      .OrderByDescending(e => e.SentDate).GroupBy(p => p.ConversationId)
                                      .Select(s => new CustomeMessagesModel { AllMessages = s.ToList() }
                                      ).ToList();
            ViewBag.allusers = new AgingInHomeContext().AspNetUsers.ToList();
            ViewBag.InboxType = "Inbox";
            return PartialView("_AdminInbox");
        }
        public PartialViewResult LoadsendBox()
        {
            var userid = User.Identity.GetUserId();
            ViewBag.CustomeMessages = new InboxModel().GetAllCustomeMessagesByUserid(userid)
                                      .Where(send => send.SenderId == userid)
                                      .OrderByDescending(e => e.SentDate).GroupBy(p => p.ConversationId)
                                      .Select(s => new CustomeMessagesModel { AllMessages = s.ToList() }
                                      ).ToList();
            ViewBag.allusers = new AgingInHomeContext().AspNetUsers.ToList();
            ViewBag.InboxType = "sendBox";
            return PartialView("_AdminInbox");
        }
        public PartialViewResult LoadArchiveBox()
        {
            var userid = User.Identity.GetUserId();
            ViewBag.CustomeMessages = new InboxModel().GetAllCustomeMessagesByUserid(userid)
                                      .Where(send => send.isAdminArchive == true)
                                      .OrderByDescending(e => e.SentDate).GroupBy(p => p.ConversationId)
                                      .Select(s => new CustomeMessagesModel { AllMessages = s.ToList() }
                                      ).ToList();
            ViewBag.allusers = new AgingInHomeContext().AspNetUsers.ToList();
            ViewBag.InboxType = "Archive";
            return PartialView("_AdminInbox");
        }
        //public PartialViewResult SentMessageDetail(int ConversationId)
        //{
        //    //var userid = User.Identity.GetUserId();
        //    //var model = new InboxModel().GetSendMessageDetailByDetailId(ConversationId, userid).ToList();
        //    //ViewBag.alllisting = new ProviderListingModel().GetAllListing();
        //    var model = new InboxModel().GetMessageDetailByConversationId(ConversationId, User.Identity.GetUserId(),"");
        //    ViewBag.alllisting = new ProviderListingModel().GetAllListing();
        //    ViewBag.msgType = "Sent";
        //    return PartialView("_CustomeMessageDetail", model.OrderByDescending(d => d.Id).ToList());
        //}
        //public PartialViewResult ArchiMessageDetail(int ConversationId)
        //{
        //    //var model = new InboxModel().GetMessageDetailByDetailId(ConversationId, User.Identity.GetUserId()).ToList();
        //    //ViewBag.alllisting = new ProviderListingModel().GetAllListing();
        //    var model = new InboxModel().GetMessageDetailByConversationId(ConversationId, User.Identity.GetUserId(),"");
        //    ViewBag.alllisting = new ProviderListingModel().GetAllListing();
        //    ViewBag.msgType = "AdminArchive";
        //    return PartialView("_CustomeMessageDetail", model.OrderByDescending(d => d.Id).ToList());
        //}

       

        public string UpdateAchieveStatus(int RequestId)
        {
            //var userIdentity = (ClaimsIdentity)User.Identity;
            //var claims = userIdentity.Claims;
            //var roleClaimType = userIdentity.RoleClaimType;
            //var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            var model = "";
            //if (roles[0].Value == "Consumer")
            if (User.IsInRole("Consumer"))
            {
                new InboxModel().UpdateAchieveStatusByAdmin(RequestId,"Consumer");
                model = "true";
            }
            //if (roles[0].Value == "Provider")
            if (User.IsInRole("Provider"))
            {
                new InboxModel().UpdateAchieveStatusByAdmin(RequestId,"Provider");
                model = "false";
            }
            else
            {
                new InboxModel().UpdateAchieveStatusByAdmin(RequestId,"Admin");
                model = "Admin";
            }

            return model;
        }
    }
}