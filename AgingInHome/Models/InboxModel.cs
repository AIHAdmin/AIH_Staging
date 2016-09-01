using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgingInHome.BLL.Repositories;
using AutoMapper;
using AgingInHome.DAL;

namespace AgingInHome.Models
{
    public class InboxModel
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public string Subject { get; set; }
        public string MessageBody { get; set; }
        public string ReciptEmail { get; set; }
        public string file { get; set; }
        public Nullable<System.DateTime> SentDate { get; set; }
        public Nullable<int> ServiceRequestDetailsId { get; set; }
        public Nullable<bool> IsArchive { get; set; }
        public Nullable<bool> IsProviderArchive { get; set; }
        public Nullable<bool> IsRead { get; set; }
        public Nullable<bool> IsStarred { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> isAdminArchive { get; set; }
        public int ConversationId { get; set; }
        public Nullable<int> ContactUsId { get; set; }
        public ContactU ContactU { get; set; }

        // public  ServiceRequestDetailModel ServiceRequestDetail { get; set; }
        public List<InboxModel> GetMessageDetailByDetailId(int detailId, string userid)
        {
            var model = new InboxClass().GetMessageDetailByDetailId(detailId, userid);
            var Inboxmessages = Mapper.Map<List<InboxModel>>(model);
            return Inboxmessages;
        }
        public List<InboxModel> GetMessageDetailByConversationId(int conversationId, string Userid, string MsgSenderId)
        {
            var model = new InboxClass().GetMessageDetailByConversationId(conversationId, Userid, MsgSenderId);
            var Inboxmessages = Mapper.Map<List<InboxModel>>(model);
            bool IsReadUpdated = new InboxClass().UpdateIsMsg(model, Userid, MsgSenderId);
            return Inboxmessages;
        }

        public string GetCurrentUserOffSet(string UserId)
        {
            return new InboxClass().GetCurrentUserOffSet(UserId);
        }

        public List<InboxModel> GetSendMessageDetailByDetailId(int detailId, string userid, string type = "")
        {
            var model = new InboxClass().GetSendMessageDetailByDetailId(detailId, userid, type);
            var Inboxmessages = Mapper.Map<List<InboxModel>>(model);
            return Inboxmessages;
        }

        public bool UpdateAchieveStatus(int detailId, string UserId)
        {
            var model = new InboxClass().UpdateAchieveStatus(detailId, UserId);
            return model;
        }
        public bool UpdateAchieveStatusByProvider(int detailId, string UserId)
        {
            var model = new InboxClass().UpdateAchieveStatusByProvider(detailId, UserId);
            return model;
        }
        public bool UpdateAchieveStatusByAdmin(int conversationId, string UserType)
        {
            var model = new InboxClass().UpdateAchieveStatusByAdmin(conversationId, UserType);
            return model;
        }



        public bool SaveMessage(InboxModel message)
        {
            var model = Mapper.Map<Inbox>(message);
            return new InboxClass().SaveMessage(model);
        }
        public bool SaveCustomeMessage(InboxModel message)
        {
            var model = Mapper.Map<Inbox>(message);
            return new InboxClass().SaveCustomeMessage(model);
        }
        public List<InboxModel> GetAllCustomeMessagesByUserid(string UserId)
        {
            var messages = new InboxClass().GetAllCustomeMessagesByUserid(UserId);
            return Mapper.Map<List<InboxModel>>(messages);
        }
    }
}