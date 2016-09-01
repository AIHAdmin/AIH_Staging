using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgingInHome.Models
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public List<int> ServiceSelectedIds{ get; set; }
        public int BudgetAmount { get; set; }
        public string PromoteType { get; set; }
    }
}