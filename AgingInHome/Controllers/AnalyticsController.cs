using AgingInHome.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Configuration;
using AgingInHome.Helpers;
using Microsoft.AspNet.Identity;
using PagedList;
using PagedList.Mvc;
using AgingInHome.BLL.Enums;
using AgingInHome.DAL;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Data;
using System.Xml;
using System.Security.Claims;
using System.Web.Security;
using DotNetOpenAuth.OAuth2;
using System.Text;
using Google.Apis.Admin.Directory.directory_v1;
using Google.Apis.Admin.Directory.directory_v1.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.Json;
using System.Threading;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2.Responses;

namespace AgingInHome.Controllers
{
    public class AnalyticsController : Controller
    {
        [Authorize(Roles = "Provider")]
        public ActionResult Index()
        {
            GetProfileId();
            GetMarkup();
            getgoogleToken();
            Getbudgetdetails();
            return View();
        }
        [NonAction]
        public void getgoogleToken()
        {
            string[] Scopes = { "https://www.googleapis.com/auth/analytics.readonly" };
            string GoogleFolder = "Google";
            try
            {
                string GoogleFolderpath = Server.MapPath("~/");
                //Set location for Google Token to be locally stored
                var googleTokenLocation = Path.Combine(GoogleFolderpath, GoogleFolder);
                string path = Server.MapPath("~/client_secret.json");

                DirectoryInfo dInfo = new DirectoryInfo(Server.MapPath("~/Google"));
                DirectorySecurity dSecurity = dInfo.GetAccessControl();
                dSecurity.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), FileSystemRights.FullControl, InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.NoPropagateInherit, AccessControlType.Allow));
                dInfo.SetAccessControl(dSecurity);

                //Load the Client Configuration in JSON Format as a stream which is used for API Calls
                var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                //This will create a Token Response User File on the GoogleFolder 
                // indicated on your Application
                var credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                   new ClientSecrets
                   {
                       ClientId = Convert.ToString(ConfigurationManager.AppSettings["ClientId"]),
                       ClientSecret = Convert.ToString(ConfigurationManager.AppSettings["ClientSecret"])
                   },
                    Scopes,
                    "user",
                    CancellationToken.None,
                  new FileDataStore(googleTokenLocation, true)).Result;
                if (credentials.Token.IsExpired(credentials.Flow.Clock))
                {
                    if (credentials.RefreshTokenAsync(CancellationToken.None).Result)
                    {
                        ViewBag.AccessToken = credentials.Token.AccessToken;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    ViewBag.AccessToken = credentials.Token.AccessToken;
                }


            }
            catch (Exception ex)
            {
                CommonClass.ErrorMessage(ex);
            }
            finally
            {
                //ViewBag.AccessToken = "5---" + sb.ToString();
            }
        }

        public void GetProfileId()
        {
            try
            {
                var listing = new ProviderListingModel();
                var UserId = User.Identity.GetUserId();
                var ProviderListingId = listing.GetProviderListingIdbyUserId(UserId);
                ViewBag.ViewId = listing.GetViewId(ProviderListingId);
            }
            catch(Exception ex)
            {
                CommonClass.ErrorMessage(ex);
            }
                
        }

        [HttpGet]
        public ActionResult PromotionalBudget()
        {
            try
            {
                GetPromotionalBudget();
            }
            catch (Exception ex)
            {
                CommonClass.ErrorMessage(ex);
            }
            return View();
        }

        [HttpPost]
        public ActionResult PromotionalBudget(AdwordModel adwordModel)
        {
            var _adwordModel = new AdwordModel();
            try
            {
                if (adwordModel.Id == 0)
                {
                    var listing = new ProviderListingModel();
                    var UserId = User.Identity.GetUserId();
                    var ProviderListingId = listing.GetProviderListingIdbyUserId(UserId);
                    adwordModel.ProviderListingId = ProviderListingId;
                    adwordModel.IsActive = true;
                    if (ModelState.IsValid)
                    {
                        if (adwordModel.Id == 0)
                        {
                            _adwordModel.AddAdwordBudget(adwordModel);
                            return RedirectToAction("MyListing", "providerListing");
                        }
                    }
                }
                else
                {
                    new AdwordModel().UpdateAdwordBudget(adwordModel);
                    return RedirectToAction("MyListing", "providerListing");
                }
            }
            catch (Exception ex)
            {
                CommonClass.ErrorMessage(ex);
            }
            return View(_adwordModel);
        }

        public ActionResult GetPromotionalBudget()
        {
            var listing = new ProviderListingModel();
            var adworddetails = new AdwordModel();
            try
            {
                var UserId = User.Identity.GetUserId();
                var ProviderListingId = listing.GetProviderListingIdbyUserId(UserId);
                adworddetails = adworddetails.GetAdwordByProviderId(ProviderListingId);
            }
            catch(Exception ex){
                CommonClass.ErrorMessage(ex);
            }
            return View(adworddetails);
        }

        public void GetMarkup()
        {
            try
            {
                var _markupModel = new MarkupModel();
                var todoListsResults = _markupModel.GetDefaultMarkup();
                ViewBag.Markup = todoListsResults.Markup1;
            }
            catch (Exception ex)
            {
                CommonClass.ErrorMessage(ex);
            }
        }
        public void Getbudgetdetails()
        {
            try
            {
                var listing = new ProviderListingModel();
                var UserId = User.Identity.GetUserId();
                var ProviderListingId = listing.GetProviderListingIdbyUserId(UserId);
                var adworddetails = new AdwordModel();
                adworddetails = adworddetails.GetAdwordByProviderId(ProviderListingId);
                ViewBag.PromotionalBudget = adworddetails.MonthlyPromotionalBudget;
                ViewBag.RemainingBudget = adworddetails.RemainingPromotionalBudget;
                if ((adworddetails.EndDate.Day - DateTime.Now.Day) > 0)
                {
                    ViewBag.RemainingDays = adworddetails.EndDate.Day - DateTime.Now.Day;
                }
                else
                {
                    ViewBag.RemainingDays = 0;
                }

            }
            catch (Exception ex)
            {
                CommonClass.ErrorMessage(ex);
            }
        }
    }
}

     