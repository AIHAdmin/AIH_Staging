using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgingInHome.BLL.Enums
{
    public enum Enums
    {

    }
    public enum ListingStatus
    {
        Pending,
        Accepted,
        Rejected,
        Suspended
    }
    public enum UserRoles
    {
        Admin=1,
        Consumer=2,
        Provider=3,
        SaleUser=4
    }
}
