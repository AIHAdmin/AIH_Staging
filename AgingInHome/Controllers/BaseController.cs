using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgingInHome.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Request.Cookies.AllKeys.Contains("timezoneoffset"))
            {
                Session["timezoneoffset"] = HttpContext.Request.Cookies["timezoneoffset"].Value;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}