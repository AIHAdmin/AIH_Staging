using AgingInHome.BLL.Enums;
using AgingInHome.Helpers;
using AgingInHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace AgingInHome.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.StateList = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", "Select the State");
            ViewBag.categoryList = new ProviderListingModel().AllCategory();
            var model = new ServiceRequestModel();
            return View(model);
            
        }
        public ActionResult Home()
        {
            ViewBag.StateList = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", "Select the State");
            ViewBag.categoryList = new ProviderListingModel().AllCategory();
            var model = new ServiceRequestModel();
            return View(model);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public async System.Threading.Tasks.Task<string> AddServiceRequest(ServiceRequestModel serviceRequestModel)
        {

            if (serviceRequestModel.IsDirectContact)
            {

                var url = new ServiceRequestModel().AddServiceRequest(serviceRequestModel);
                return url.Split(',')[0];

            }
            else
            {
                serviceRequestModel.UserId = User.Identity.GetUserId();
                if (serviceRequestModel.Email==null)
                {
                    serviceRequestModel.Email = User.Identity.GetUserName();
                }
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
       
        //public async System.Threading.Tasks.Task<bool> ConsumerRegister(ServiceRequestModel model)
        //{
        //    var result=await new AccountController().ConsumerRegister(new RegisterViewModel() {
        //        ConfirmPassword=model.Password,
        //        Password=model.ConfirmPassword,
        //        Email=model.Email,
        //        FirstName=model.FirstName,
        //        Gender=model.Gender,
        //        LastName=model.LastName,
        //        Phone=model.PrimaryPhone,
        //       ZipCode=model.ZipCode,
        //       StateId=00001
        //    });
        //    return true;
        //}
    }
}