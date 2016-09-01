using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class combinedInboxMessagesModel
    {
        public int UnreadMsg { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string SenderId { get; set; }
        public string RecipientId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool ConversationMessage { get; set; }
        public int ConversationId { get; set; }
        public int ServiceRequestDetailsId { get; set; }
    }
}