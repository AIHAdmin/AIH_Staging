using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgingInHome.DAL;

namespace AgingInHome.BLL.Repositories
{
    public interface IinboxRepository
    {
        List<Inbox> GetMessageDetailByDetailId(int detailId, string Userid);
        List<Inbox> GetMessageDetailByConversationId(int ConversationId, string Userid, string MsgSenderId);
        List<Inbox> GetSendMessageDetailByDetailId(int detailId, string Userid, string type = "");
        bool SaveMessage(Inbox message);
        bool UpdateAchieveStatus(int detailId, string UserId);
        bool UpdateAchieveStatusByProvider(int detailId, string UserId);
        bool SaveCustomeMessage(Inbox message);
        List<Inbox> GetAllCustomeMessagesByUserid(string Userid);
        bool UpdateAchieveStatusByAdmin(int conversationId, string UserType);
        bool UpdateIsMsg(List<Inbox> listofMsg, string Userid, string MsgSenderId);
        string GetCurrentUserOffSet(string UserId);
    }
    public class InboxClass : IinboxRepository
    {
        public List<Inbox> GetAllCustomeMessagesByUserid(string Userid)
        {
            using (var db = new AgingInHomeContext())
            {
                var GetInboxMessages = db.Inboxes.Include("ContactU").Where(s => s.ConversationId > 0).ToList();
                GetInboxMessages = GetInboxMessages.Where(r => r.SenderId == Userid || r.RecipientId == Userid).OrderByDescending(s => s.SentDate).ToList();
                return GetInboxMessages;
            }
        }

        public List<Inbox> GetMessageDetailByDetailId(int detailId, string Userid)
        {
            using (var db = new AgingInHomeContext())
            {
                //include use for to inforce dont mis navigation property
                var listofMsg = db.Inboxes
                    .Include("ServiceRequestDetail.ProviderListing")
                    .Include("ServiceRequestDetail.ProviderListing.FavoriteDetails")
                    .Include("ServiceRequestDetail.ProviderListing.ListingAboutUs")
                    .Include("ServiceRequestDetail.ProviderListing.ListingArticles")
                    .Include("ServiceRequestDetail.ProviderListing.ListingBlogs")
                    .Include("ServiceRequestDetail.ProviderListing.ProviderImageGalaries")
                    .Include("ServiceRequestDetail.ProviderListing.ProviderVideos")
                    .Include("ServiceRequestDetail.ProviderListing.RatingTables")
                    .Include("ServiceRequestDetail.ProviderListing.ServiceAreas")
                    .Include("ServiceRequestDetail.ProviderListing.ServiceRequestDetails")
                    .Include("ContactU").ToList()
                    .Where(s => s.ServiceRequestDetailsId == detailId).ToList();
                if (listofMsg.Count == 0)
                {
                    listofMsg = db.Inboxes
                    .Include("ServiceRequestDetail.ProviderListing")
                    .Include("ServiceRequestDetail.ProviderListing.FavoriteDetails")
                    .Include("ServiceRequestDetail.ProviderListing.ListingAboutUs")
                    .Include("ServiceRequestDetail.ProviderListing.ListingArticles")
                    .Include("ServiceRequestDetail.ProviderListing.ListingBlogs")
                    .Include("ServiceRequestDetail.ProviderListing.ProviderImageGalaries")
                    .Include("ServiceRequestDetail.ProviderListing.ProviderVideos")
                    .Include("ServiceRequestDetail.ProviderListing.RatingTables")
                    .Include("ServiceRequestDetail.ProviderListing.ServiceAreas")
                    .Include("ServiceRequestDetail.ProviderListing.ServiceRequestDetails")
                    .Include("ContactU").ToList()
                    .Where(s => s.ConversationId == detailId).ToList();
                }
                //change message Status unread to Read
                foreach (var eachmsg in listofMsg.Where(d => d.RecipientId == Userid).ToList())
                {
                    eachmsg.IsRead = true;
                }
                db.SaveChanges();
                return listofMsg;
            }
        }
        public List<Inbox> GetMessageDetailByConversationId(int ConversationId, string Userid, string MsgSenderId)
        {
            using (var db = new AgingInHomeContext())
            {
                var listofMsg = db.Inboxes
                    .Include("ServiceRequestDetail.ProviderListing")
                    .Include("ServiceRequestDetail.ProviderListing.FavoriteDetails")
                    .Include("ServiceRequestDetail.ProviderListing.ListingAboutUs")
                    .Include("ServiceRequestDetail.ProviderListing.ListingArticles")
                    .Include("ServiceRequestDetail.ProviderListing.ListingBlogs")
                    .Include("ServiceRequestDetail.ProviderListing.ProviderImageGalaries")
                    .Include("ServiceRequestDetail.ProviderListing.ProviderVideos")
                    .Include("ServiceRequestDetail.ProviderListing.RatingTables")
                    .Include("ServiceRequestDetail.ProviderListing.ServiceAreas")
                    .Include("ServiceRequestDetail.ProviderListing.ServiceRequestDetails")
                    .Include("ContactU")
                    .Where(s => (s.SenderId == MsgSenderId && s.RecipientId == Userid) || (s.SenderId == Userid && s.RecipientId == MsgSenderId)).ToList();
                ////change message Status unread to Read
                //foreach (var eachmsg in listofMsg.Where(d => d.RecipientId == Userid).ToList())
                //{
                //    eachmsg.IsRead = true;
                //}
                // db.SaveChanges();
                return listofMsg;
            }
        }

        public string GetCurrentUserOffSet(string UserId)
        {
            using (var db = new AgingInHomeContext())
            {
                var offset = "0";
                var _user = db.ConsumerProfiles.Where(c => c.UserId == UserId).FirstOrDefault();
                if (_user != null)
                    offset = db.UsStates.Where(c => c.Id == _user.StateId).FirstOrDefault().Offset;
                return offset;
            }
        }

        public bool UpdateIsMsg(List<Inbox> listofMsg, string Userid, string MsgSenderId)
        {
            using (var db = new AgingInHomeContext())
            {
                foreach (var eachmsg in listofMsg.Where(d => d.RecipientId == Userid && d.SenderId == MsgSenderId && d.IsRead == false).ToList())
                {
                    Inbox _Inbox = db.Inboxes.Where(d => d.Id == eachmsg.Id).FirstOrDefault();
                    _Inbox.IsRead = true;
                    db.SaveChanges();
                }

                return true;
            }
        }

        public List<Inbox> GetSendMessageDetailByDetailId(int detailId, string Userid, string type = "")
        {
            using (var db = new AgingInHomeContext())
            {
                var listofMsg = db.Inboxes
                    .Include("ServiceRequestDetail.ProviderListing")
                    .Include("ServiceRequestDetail.ProviderListing.FavoriteDetails")
                    .Include("ServiceRequestDetail.ProviderListing.ListingAboutUs")
                    .Include("ServiceRequestDetail.ProviderListing.ListingArticles")
                    .Include("ServiceRequestDetail.ProviderListing.ListingBlogs")
                    .Include("ServiceRequestDetail.ProviderListing.ProviderImageGalaries")
                    .Include("ServiceRequestDetail.ProviderListing.ProviderVideos")
                    .Include("ServiceRequestDetail.ProviderListing.RatingTables")
                    .Include("ServiceRequestDetail.ProviderListing.ServiceAreas")
                    .Include("ServiceRequestDetail.ProviderListing.ServiceRequestDetails")
                    .Include("ContactU")
                    .Where(s => s.ServiceRequestDetailsId == detailId).ToList();
                if (listofMsg.Count == 0 || type == "Conversation")
                {
                    listofMsg = db.Inboxes
                    .Include("ServiceRequestDetail.ProviderListing")
                    .Include("ServiceRequestDetail.ProviderListing.FavoriteDetails")
                    .Include("ServiceRequestDetail.ProviderListing.ListingAboutUs")
                    .Include("ServiceRequestDetail.ProviderListing.ListingArticles")
                    .Include("ServiceRequestDetail.ProviderListing.ListingBlogs")
                    .Include("ServiceRequestDetail.ProviderListing.ProviderImageGalaries")
                    .Include("ServiceRequestDetail.ProviderListing.ProviderVideos")
                    .Include("ServiceRequestDetail.ProviderListing.RatingTables")
                    .Include("ServiceRequestDetail.ProviderListing.ServiceAreas")
                    .Include("ServiceRequestDetail.ProviderListing.ServiceRequestDetails")
                    .Include("ContactU")
                    .Where(s => s.ConversationId == detailId).ToList();
                }
                //change message Status unread to Read
                //foreach (var eachmsg in listofMsg.Where(d=>d.RecipientId==Userid).ToList())
                //{
                //    eachmsg.IsRead = true;
                //}
                db.SaveChanges();
                return listofMsg;
            }
        }

        public bool SaveCustomeMessage(Inbox message)
        {
            using (var db = new AgingInHomeContext())
            {
                if (message.ConversationId > 0)
                {
                    if (message.Subject == null)
                    {
                        message.Subject = db.Inboxes.Where(s => s.ConversationId == message.ConversationId && s.Subject != null).FirstOrDefault().Subject;
                    }
                }
                else
                {
                    var getdetail = db.Inboxes.Max(s => s.ConversationId);
                    if (getdetail != null)
                    {
                        //conversation id is use to handle chat when anyone chat without submit service request
                        message.ConversationId = getdetail + 1;
                    }
                    else
                    {
                        message.ConversationId = 1;
                    }

                }


                message.IsRead = false;
                message.IsArchive = false;
                message.IsProviderArchive = false;
                message.SentDate = DateTime.UtcNow;
                var saveMessage = db.Inboxes.Add(message);
                db.SaveChanges();
                return true;
            }
        }

        public bool SaveMessage(Inbox message)
        {
            using (var db = new AgingInHomeContext())
            {
                message.IsRead = false;
                message.IsArchive = false;
                message.IsProviderArchive = false;
                message.SentDate = DateTime.Now;
                if (message.Subject == null)
                {
                    message.Subject = db.Inboxes.Where(s => s.ServiceRequestDetailsId == message.ServiceRequestDetailsId && s.Subject != null).FirstOrDefault().Subject;
                }
                var saveMessage = db.Inboxes.Add(message);
                //update date in categoryDetail table *NOTE: updation is neccessory for sorting inbox message
                var getdetail = db.ServiceRequestDetails.Find(message.ServiceRequestDetailsId);
                if (getdetail != null)
                {
                    getdetail.LastUpdation = DateTime.Now;
                    var _relatedcat = db.ServiceRequestCategories.Find(getdetail.ServiceRequestCatId);
                    _relatedcat.LastUpdation = DateTime.Now;
                    var servicerequest = db.ServiceRequests.Find(_relatedcat.ServiceRequestId);
                    servicerequest.LastUpdation = DateTime.Now;
                }
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateAchieveStatus(int detailId, string UserId)
        {
            using (var db = new AgingInHomeContext())
            {
                var Recipeintid = db.Inboxes.Where(d => (d.ServiceRequestDetailsId == detailId || d.ConversationId == detailId) && d.RecipientId != UserId).FirstOrDefault();
                if (Recipeintid == null)
                {
                    Recipeintid = db.Inboxes.Where(d => (d.ServiceRequestDetailsId == detailId || d.ConversationId == detailId) && d.SenderId != UserId).FirstOrDefault();
                }
                var _RecipeintidId = Recipeintid.SenderId == UserId ? Recipeintid.RecipientId : Recipeintid.SenderId;

                var AllMessages = db.Inboxes.Where(s => (s.SenderId == _RecipeintidId && s.RecipientId == UserId) || (s.SenderId == UserId && s.RecipientId == _RecipeintidId)).ToList();
                foreach (var updateStatus in AllMessages)
                {
                    updateStatus.IsArchive = true;
                }
                db.SaveChanges();
                return true;
            }
        }

        public bool UpdateAchieveStatusByProvider(int detailId, string UserId)
        {
            using (var db = new AgingInHomeContext())
            {
                var Recipeintid = db.Inboxes.Where(d => (d.ServiceRequestDetailsId == detailId || d.ConversationId == detailId) && d.RecipientId != UserId).FirstOrDefault();
                if (Recipeintid == null)
                {
                    Recipeintid = db.Inboxes.Where(d => (d.ServiceRequestDetailsId == detailId || d.ConversationId == detailId) && d.SenderId != UserId).FirstOrDefault();
                }
                var _RecipeintidId = Recipeintid.SenderId == UserId ? Recipeintid.RecipientId : Recipeintid.SenderId;


                var AllMessages = db.Inboxes.Where(s => (s.SenderId == _RecipeintidId && s.RecipientId == UserId) || (s.SenderId == UserId && s.RecipientId == _RecipeintidId)).ToList();
                foreach (var updateStatus in AllMessages)
                {
                    updateStatus.IsProviderArchive = true;
                }
                db.SaveChanges();
                return true;
            };
        }
        public bool UpdateAchieveStatusByAdmin(int conversationId, string UserType)
        {
            using (var db = new AgingInHomeContext())
            {
                var AllMessages = db.Inboxes.Where(d => d.ConversationId == conversationId).ToList();
                foreach (var updateStatus in AllMessages)
                {
                    if (UserType == "Admin")
                    {
                        updateStatus.isAdminArchive = true;
                    }
                    else if (UserType == "Provider")
                    {
                        updateStatus.IsProviderArchive = true;
                    }
                    else if (UserType == "Consumer")
                    {
                        updateStatus.IsArchive = true;
                    }

                }
                db.SaveChanges();
                return true;
            };
        }
    }
}
