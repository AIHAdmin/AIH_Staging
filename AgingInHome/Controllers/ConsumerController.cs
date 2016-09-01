using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgingInHome.Models;
using Microsoft.AspNet.Identity;
using AgingInHome.Helpers;
using AgingInHome.BLL.Enums;
using System.Security.Claims;
using AgingInHome.DAL;

namespace AgingInHome.Controllers
{
    public class ConsumerController : BaseController
    {
        [Authorize(Roles = "Consumer")]
        public ActionResult Dashboard()
        {
            string host = Request.Url.Host.ToLower();
            var userid = User.Identity.GetUserId();
            var Servicerequest = new ProviderListingModel().GetServiceRequestByUserId(User.Identity.GetUserId()).ToList();
            ViewBag.CustomeMessages = new InboxModel().GetAllCustomeMessagesByUserid(userid).GroupBy(p => p.ConversationId)
                                   .Select(s => new CustomeMessagesModel { AllMessages = s.ToList() }
                                   ).ToList();
            ViewBag.alllisting = new ProviderListingModel().GetAllListing();

            return View(Servicerequest);
        }
        public ActionResult ServiceRequest(int zipcode = 0)
        {
            ViewBag.StateList = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", "Select the State");
            ViewBag.categoryList = new ProviderListingModel().AllCategory();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Select relation", Value = "", Selected = true });
            items.Add(new SelectListItem { Text = "Spouse/Partner", Value = "Spouse/Partner" });
            items.Add(new SelectListItem { Text = "Other Relative", Value = "Other Relative" });
            items.Add(new SelectListItem { Text = "Friend", Value = "Friend" });
            items.Add(new SelectListItem { Text = "Client", Value = "Client" });
            items.Add(new SelectListItem { Text = "Myself", Value = "Myself" });
            ViewBag.RelationList = items;
            ViewBag.zipcode = zipcode;
            var model = new ServiceRequestModel();
            return View();
        }
        [HttpPost]
        public ActionResult ServiceRequest(ServiceRequestModel serviceRequestModel)
        {
            if (ModelState.IsValid)
            {

            }
            ViewBag.StateList = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", "Select the State");
            ViewBag.categoryList = new ProviderListingModel().AllCategory();
            var model = new ServiceRequestModel();
            return View();
        }
        [HttpPost]
        public string AddServiceRequest(ServiceRequestModel serviceRequestModel)
        {
            if (serviceRequestModel.IsDirectContact)
            {

                var url = new ServiceRequestModel().AddServiceRequest(serviceRequestModel);
                return url.Split(',')[0];

            }
            else
            {
                var url = new ServiceRequestModel().AddServiceRequest(serviceRequestModel);
                var listinglist = new ProviderListingModel().GetAllListing()
                    .Where(o => serviceRequestModel.SelectedCategory.Contains(o.ProviderCategory1.Id.ToString()) && o.IsApproved == (int)ListingStatus.Accepted).ToList();
                //Get service RequestId
                var RequestId = url.Split(',')[1];
                serviceRequestModel.Id = Convert.ToInt32(RequestId);
                foreach (var providerlisting in listinglist)
                {
                    EmailSender.SendEmailToServiceProvider(providerlisting, serviceRequestModel);
                }

                return "Dashboard";
            }
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
            var ReceiptUser = new AgingInHomeContext().AspNetUsers.FirstOrDefault(s => s.Email == message.ReciptEmail);
            if (ReceiptUser != null)
            {
                message.RecipientId = new AgingInHomeContext().AspNetUsers.FirstOrDefault(s => s.Email == message.ReciptEmail).Id;
                message.IsActive = true;
                message.IsArchive = false;
                message.IsProviderArchive = false;
                message.IsRead = false;
                message.IsStarred = false;
                message.SenderId = userid;
                message.SentDate = DateTime.Now;
                new InboxModel().SaveCustomeMessage(message);
                EmailSender.SendEmailForNewMsg(ReceiptUser.Email, User.Identity.Name);
                return true;
            }
            else
                return false;
        }
        public ActionResult SuccessServiceRequest()
        {
            return View();
        }
        public ActionResult IsWait(int RequiredListingId, bool Iswait)
        {
            return View();
        }
        public PartialViewResult ServiceRequestDetail(int RequestId)
        {
            var Servicerequest = new ProviderListingModel().GetServiceRequestByUserId(User.Identity.GetUserId()).FirstOrDefault(d => d.Id == RequestId);

            return PartialView("ConsumerServiceRequestDetail", Servicerequest);
        }
        public ActionResult ServiceRequestDelete(int RequestId)
        {
            new ServiceRequestModel().DeleteServiceRequest(RequestId);

            return RedirectToAction("Dashboard");
        }
        public ActionResult UpdateServiceRequest(int RequestId)
        {
            ViewBag.StateList = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", "Select the State");
            ViewBag.categoryList = new ProviderListingModel().AllCategory();

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Select relation", Value = "", Selected = true });
            items.Add(new SelectListItem { Text = "Spouse/Partner", Value = "Spouse/Partner" });
            items.Add(new SelectListItem { Text = "Other Relative", Value = "Other Relative" });
            items.Add(new SelectListItem { Text = "Friend", Value = "Friend" });
            items.Add(new SelectListItem { Text = "Client", Value = "Client" });
            items.Add(new SelectListItem { Text = "Myself", Value = "Myself" });
            ViewBag.RelationList = items;
            var model = new ServiceRequestModel();
            var Servicerequest = new ProviderListingModel().GetServiceRequestByUserId(User.Identity.GetUserId()).FirstOrDefault(d => d.Id == RequestId);
            return View(Servicerequest);
        }
        public JsonResult CancelConsumerAppointment(int appontmentID)
        {
            AppointmentModel appointmentModel = new AppointmentModel();
            var appointmentDetail = appointmentModel.GetAllAppointments().Where(x => x.AppointmentId == appontmentID).FirstOrDefault();//.GetAppointmentByID(appontmentID.ToString());
            var result = new AppointmentModel().CancelAppointment(appontmentID);
            var userid = User.Identity.GetUserId();
            var provider = new AgingInHomeContext().ProviderListings.ToList().FirstOrDefault(s => s.UserId == appointmentDetail.ProviderId);
            var ProviderName = provider.CompanyName;


            if (result)
            {

                //Message to provider
                InboxModel message = new InboxModel();
                message.Subject = "Appointment Cancelled";
                message.MessageBody = "Appointment for " + Convert.ToString(appointmentDetail.ServiceDate) + " with provider:" + ProviderName + " has been cancelled by consumer";
                var providerUser = new AgingInHomeContext().AspNetUsers.FirstOrDefault(s => s.Email == provider.PrimaryEmail);
                message.RecipientId = Convert.ToString(appointmentDetail.ProviderId);//new AgingInHomeContext().AspNetUsers.FirstOrDefault(s => s.Email == message.ReciptEmail).Id;
                message.IsActive = true;
                message.IsArchive = false;
                message.IsProviderArchive = false;
                message.IsRead = false;
                message.IsStarred = false;
                message.SenderId = userid;
                message.SentDate = DateTime.Now;
                new InboxModel().SaveCustomeMessage(message);
                EmailSender.SendEmailForNewMsg(provider.PrimaryEmail, User.Identity.Name);

                //Message to admin
                InboxModel messageAdmin = new InboxModel();
                messageAdmin.Subject = "Appointment Cancelled";
                messageAdmin.MessageBody = "Appointment for " + Convert.ToString(appointmentDetail.ServiceDate) + " with provider:" + ProviderName + " has been cancelled by consumer";

                //var adminUser = new AgingInHomeContext().AspNetUsers.FirstOrDefault(s => s.Email == message.ReciptEmail);
                messageAdmin.RecipientId = new AgingInHomeContext().AspNetUsers.FirstOrDefault(s => s.Email == "testadmin@gmail.com").Id;
                messageAdmin.IsActive = true;
                messageAdmin.IsArchive = false;
                messageAdmin.IsProviderArchive = false;
                messageAdmin.IsRead = false;
                messageAdmin.IsStarred = false;
                messageAdmin.SenderId = userid;
                messageAdmin.SentDate = DateTime.Now;
                new InboxModel().SaveCustomeMessage(messageAdmin);
                EmailSender.SendEmailForNewMsg("testadmin@gmail.com", User.Identity.Name);
            }
            return Json("true", JsonRequestBehavior.AllowGet);
            //var GetAllConsumerAppointment = new AppointmentModel().GetAllConsumerAppointment(userid).OrderByDescending(d => d.AppointmentId).ToList();           
            //return PartialView(GetAllConsumerAppointment);
        }

        [HttpPost]
        public string UpdateServiceRequest(ServiceRequestModel serviceRequestModel)
        {

            var url = new ServiceRequestModel().UpdateServiceRequest(serviceRequestModel);
            //var listinglist = new ProviderListingModel().GetAllListing()
            //    .Where(o => serviceRequestModel.SelectedCategory.Contains(o.ProviderCategory1.Id.ToString()) && o.IsApproved == (int)ListingStatus.Accepted).ToList();
            //foreach (var providerlisting in listinglist)
            //{
            //    EmailSender.SendEmailToServiceProvider(providerlisting.PrimaryEmail, serviceRequestModel);
            //}

            return "Dashboard";

        }
        public ActionResult ProviderInvatationStatus(int RequestId, int ProviderListingId, bool Status, int alter = 0)
        {
            new ServiceRequestModel().UpdateInvitationStatus(RequestId, ProviderListingId, Status, alter);
            return RedirectToAction("Dashboard", "ProviderListing");
        }
        public PartialViewResult GetMessageDetail(int RequestId)
        {
            var model = new InboxModel().GetMessageDetailByDetailId(RequestId, User.Identity.GetUserId());

            ViewBag.alllisting = new ProviderListingModel().GetAllListing();
            ViewBag.msgType = "all";
            return PartialView("_MessageDetailPartial", model.OrderByDescending(d => d.Id).ToList());
        }
        public PartialViewResult GetCustomMessageDetail(int ConversationId, string MsgSenderId, bool refresh = false)
        {
            List<InboxModel> model = new List<InboxModel>();
            if (!refresh)
            {
                model = new InboxModel().GetMessageDetailByConversationId(ConversationId, User.Identity.GetUserId(), MsgSenderId);
            }
            else
            {
                model = new InboxModel().GetMessageDetailByConversationId(ConversationId, User.Identity.GetUserId(), MsgSenderId);
                model = model.Where(c => c.IsRead == false && c.SenderId != User.Identity.GetUserId()).ToList();
            }
            var offset = new InboxModel().GetCurrentUserOffSet(User.Identity.GetUserId());
            ViewBag.offset = offset;
            if (User.IsInRole("Provider"))
            {
                model = model.Where(s => s.IsProviderArchive != true).ToList();
            }
            if (User.IsInRole("Admin"))
            {
                model = model.Where(s => s.isAdminArchive != true).ToList();
            }
            if (User.IsInRole("Consumer"))
            {
                model = model.Where(s => s.IsArchive != true).ToList();
            }
            ViewBag.alllisting = new ProviderListingModel().GetAllListing();
            ViewBag.msgType = "all";
            if (!refresh)
            {
                return PartialView("_CustomeMessageDetail", model.OrderByDescending(d => d.Id).ToList());
            }
            else
            {
                return PartialView("_CustomRefreshMessages", model.OrderByDescending(d => d.Id).ToList());
            }
        }

        public PartialViewResult GetinboxDetail()
        {
            var userid = User.Identity.GetUserId();
            var Servicerequest = new ProviderListingModel().GetServiceRequestByUserId(userid).ToList();
            ViewBag.CustomeMessages = new InboxModel().GetAllCustomeMessagesByUserid(userid).GroupBy(p => p.ConversationId)
                                    .Select(s => new CustomeMessagesModel { AllMessages = s.ToList() }
                                    ).ToList();
            ViewBag.alllisting = new ProviderListingModel().GetAllListing();
            return PartialView("_inboxMessageDetail", Servicerequest);
        }

        public PartialViewResult SentMessageDetail(int RequestId, string type = "")
        {
            var model = new InboxModel().GetSendMessageDetailByDetailId(RequestId, User.Identity.GetUserId(), type);
            ViewBag.alllisting = new ProviderListingModel().GetAllListing();
            ViewBag.msgType = "Sent";
            return PartialView("_MessageDetailPartial", model.OrderByDescending(d => d.Id).ToList());
        }
        public PartialViewResult RecMessageDetail(int RequestId)
        {
            var model = new InboxModel().GetMessageDetailByDetailId(RequestId, User.Identity.GetUserId());
            ViewBag.alllisting = new ProviderListingModel().GetAllListing();
            ViewBag.msgType = "Recieved";
            return PartialView("_MessageDetailPartial", model.OrderByDescending(d => d.Id).ToList());
        }
        public PartialViewResult ArchiMessageDetail(int RequestId)
        {
            var model = new InboxModel().GetMessageDetailByDetailId(RequestId, User.Identity.GetUserId());
            ViewBag.alllisting = new ProviderListingModel().GetAllListing();
            ViewBag.msgType = "Archive";
            return PartialView("_MessageDetailPartial", model.OrderByDescending(d => d.Id).ToList());
        }
        public bool UpdateAchieveStatus(int RequestId)
        {
            var userIdentity = (ClaimsIdentity)User.Identity;
            string UserId = User.Identity.GetUserId();
            var claims = userIdentity.Claims;
            var roleClaimType = userIdentity.RoleClaimType;
            var roles = claims.Where(c => c.Type == ClaimTypes.Role).ToList();
            var model = false;
            if (roles[0].Value == "Consumer")
            {
                model = new InboxModel().UpdateAchieveStatus(RequestId, UserId);
                model = true;
            }
            if (roles[0].Value == "Provider")
            {
                model = new InboxModel().UpdateAchieveStatusByProvider(RequestId, UserId);
                model = false;
            }

            return model;
        }

        public bool SaveMessage(InboxModel Message)
        {
            Message.SaveMessage(Message);
            try
            {
                var providerUser = new AgingInHomeContext().AspNetUsers.FirstOrDefault(s => s.Id == Message.RecipientId);
                EmailSender.SendEmailForNewMsg(providerUser.Email, User.Identity.Name);
            }
            catch (Exception ex)
            { }
            return true;
        }
        public PartialViewResult LoadServiceDateAndBestTime()
        {
            return PartialView("_ServiceDateandBestitme");
        }
        [HttpPost]
        public bool ServiceRequestDetails(List<ServiceSelectedCatDetails> ServiceRequest)
        {
            var userid = User.Identity.GetUserId();
            var UserDetails = new AgingInHomeContext().ConsumerProfiles.FirstOrDefault(d => d.UserId == userid);
            var url = new ServiceRequestModel().SubmitServiceRequest(ServiceRequest, UserDetails);
            var providercatlistig = ServiceRequest.Select(d => d.catId.ToString());
            var listinglist = new ProviderListingModel().GetAllListing()
                    .Where(o => providercatlistig.Contains(o.ProviderCategory1.Id.ToString()) && o.IsApproved == (int)ListingStatus.Accepted).ToList();
            //Get service RequestId
            var RequestId = url.Split(',')[1];
            ServiceRequestModel serviceRequestModel = AutoMapper.Mapper.Map<ServiceRequestModel>(UserDetails);
            serviceRequestModel.Id = Convert.ToInt32(RequestId);
            serviceRequestModel.Email = UserDetails.AspNetUser.Email;
            foreach (var providerlisting in listinglist.OrderByDescending(s => s.ProviderListingId).ToList())
            {
                //Get selected date time and best time
                var getselectedInfo = ServiceRequest.FirstOrDefault(s => s.catId == providerlisting.ProviderCategory1.Id);
                serviceRequestModel.ServiceDate = Convert.ToDateTime(getselectedInfo.CatserviceDate);
                serviceRequestModel.BestTime = getselectedInfo.CatBestTime;
                EmailSender.SendEmailToServiceProvider(providerlisting, serviceRequestModel);
                break;
            }
            return true;
        }
        [HttpPost]
        public bool UpdateServiceRequestDetails(List<ServiceSelectedCatDetails> ServiceRequest)
        {
            var userid = User.Identity.GetUserId();
            var UserDetails = new AgingInHomeContext().ConsumerProfiles.FirstOrDefault(d => d.UserId == userid);
            var returnProviderListingIds = new ServiceRequestModel().UpdateServiceRequest(ServiceRequest, UserDetails);
            if (returnProviderListingIds != "")
            {
                var providercatlistig = returnProviderListingIds.Substring(1).Split(',');
                var listinglist = new ProviderListingModel().GetAllListing();
                var listingForMail = new List<ProviderListingModel>();
                foreach (var listing in listinglist)
                {
                    if (providercatlistig.Contains(listing.CategoryId.ToString()))
                    {
                        listingForMail.Add(listing);
                    }

                }
                //.Where(o => providercatlistig.Contains(o.ProviderCategory1.Id.ToString()) && o.IsApproved == (int)ListingStatus.Accepted).ToList();
                //Get service RequestId
                var RequestId = ServiceRequest.First().ServiceRequestId;
                ServiceRequestModel serviceRequestModel = AutoMapper.Mapper.Map<ServiceRequestModel>(UserDetails);
                serviceRequestModel.Id = Convert.ToInt32(RequestId);
                serviceRequestModel.Email = UserDetails.AspNetUser.Email;
                foreach (var providerlisting in listingForMail)
                {
                    if (providerlisting.PrimaryEmail.Contains("@"))
                    {
                        //Get selected date time and best time
                        var getselectedInfo = ServiceRequest.FirstOrDefault(s => s.catId == providerlisting.ProviderCategory1.Id);
                        if (getselectedInfo != null)
                        {
                            serviceRequestModel.ServiceDate = Convert.ToDateTime(getselectedInfo.CatserviceDate);
                            serviceRequestModel.BestTime = getselectedInfo.CatBestTime;
                            EmailSender.SendEmailToServiceProvider(providerlisting, serviceRequestModel);
                        }


                    }
                }
            }
            return true;
        }
        public ActionResult CancelCategoryRequest(int CatId)
        {
            new ServiceRequestModel().CancelCategoryRequest(CatId);
            return RedirectToAction("Dashboard");
        }
        public bool MakeAppointment(AppointmentModel model)
        {
            model.SubmitAppointment(model, "Consumer");
            var providerUser = new AgingInHomeContext().AspNetUsers.FirstOrDefault(s => s.Id == model.ProviderId);
            EmailSender.SendEmailForNewMsg(providerUser.Email, User.Identity.Name);
            return true;
        }
        public bool MakeCustomeAppointment(AppointmentModel model)
        {
            model.SubmitCustomeAppointment(model, "Consumer");
            var providerUser = new AgingInHomeContext().AspNetUsers.FirstOrDefault(s => s.Id == model.ProviderId);
            EmailSender.SendEmailForNewMsg(providerUser.Email, User.Identity.Name);
            return true;
        }

        public PartialViewResult GetAllConsumerAppointments(string bestTime, string status, int pagenumber = 1, int pageSize = 5)
        {
            int startRow = (pagenumber - 1) * pageSize;
            var userid = User.Identity.GetUserId();
            var GetAllConsumerAppointment = new AppointmentModel().GetAllConsumerAppointment(userid).OrderByDescending(d => d.AppointmentId).ToList();
            ViewBag.TotalRecords = GetAllConsumerAppointment.Count;
            if (!string.IsNullOrEmpty(bestTime) && string.IsNullOrEmpty(status))
            {
                ViewBag.TotalRecords = GetAllConsumerAppointment.Where(x => x.BestTime == bestTime).ToList().Count;
                GetAllConsumerAppointment = GetAllConsumerAppointment.Where(x => x.BestTime == bestTime).Skip(startRow).Take(pageSize).ToList();
            }
            else
            {
                GetAllConsumerAppointment = GetAllConsumerAppointment.Skip(startRow).Take(pageSize).ToList();
            }
            return PartialView(GetAllConsumerAppointment);
        }
        public bool CancelAppointment(int AppointId)
        {
            return new AppointmentModel().CancelAppointment(AppointId);
        }
        public bool AcceptAppointment(int AppointId, string UserType)
        {
            return new AppointmentModel().AcceptAppointment(AppointId, UserType);
        }
        public bool RejectAppointment(int AppointId, string UserType)
        {
            return new AppointmentModel().RejectAppointment(AppointId, UserType);
        }

        public PartialViewResult GetConsumerServiceRequest(int pagenumber = 1, int pageSize = 3)
        {
            string host = Request.Url.Host.ToLower();
            var userid = User.Identity.GetUserId();
            var Servicerequest = new ProviderListingModel().GetConsumerServiceRequest(userid, pagenumber, pageSize).ToList();
            return PartialView(Servicerequest);
        }

        public PartialViewResult SearchInboxMailByText(string search)
        {
            string host = Request.Url.Host.ToLower();
            var userid = User.Identity.GetUserId();
            var Servicerequest = new ProviderListingModel().SearchInboxMailByText(userid).ToList();
            ViewBag.search = search;
            ViewBag.CustomeMessages = new InboxModel().GetAllCustomeMessagesByUserid(userid).GroupBy(p => p.ConversationId)
                                 .Select(s => new CustomeMessagesModel { AllMessages = s.ToList() }
                                 ).ToList();
            ViewBag.alllisting = new ProviderListingModel().GetAllListing();
            return PartialView(Servicerequest);
        }

    }
}