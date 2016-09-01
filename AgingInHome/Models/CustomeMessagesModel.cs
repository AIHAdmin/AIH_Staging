using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class CustomeMessagesModel
    {
        public string Name { get; set; }
        public List<InboxModel> AllMessages{ get; set; }
    }
}