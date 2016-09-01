using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AgingInHome.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AgingInHome.Models;
using AgingInHome.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;
using AgingInHome.BLL.Enums;
using System.Configuration;
using System.Net.Mail;
using System.Collections.Generic;
using System.Web.UI;

namespace AgingInHome.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this.RoleManager = roleManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return this.roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set { this.roleManager = value; }
        }


        //
        // GET: /Account/Login
        [AllowAnonymous]
        [OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            var model = new LoginViewModel()
            {
                Roles = GetRoles()
            };
            return View(model);
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    {


                        //if (UserManager.IsInRole(User.Identity.GetUserId(), "admin"))
                        //{

                        //}

                        //var user = new ClaimsPrincipal(AuthenticationManager.AuthenticationResponseGrant.Identity);

                        //if (User.IsInRole(model.UserRole))
                        //{
                        //    if (String.IsNullOrEmpty(returnUrl))
                        //    {
                        //        switch (Convert.ToInt32(model.UserRole))
                        //        {
                        //            case (int)UserRoles.Admin:
                        //                return RedirectToAction("Dashboard", "Admin");
                        //            case (int)UserRoles.Consumer:
                        //                return RedirectToAction("Dashboard","Consumer");
                        //            case (int)UserRoles.Provider:
                        //                return RedirectToAction("ProviderListing","ProviderListing");
                        //            case (int)UserRoles.SaleUser:
                        //                return RedirectToAction("Dashboard","SaleDepartment");
                        //            default:
                        //                return RedirectToAction("index", "Home");
                        //        }
                        //    }
                        //    return RedirectToLocal(returnUrl);
                        //}

                        //else
                        //{

                        //    AuthenticationManager.AuthenticationResponseGrant = null;

                        //    model.Roles = GetRoles();
                        //    ModelState.AddModelError("", "You cannot login as " + model.UserRole);
                        //    return View(model);
                        //}
                        var user = new ClaimsPrincipal(AuthenticationManager.AuthenticationResponseGrant.Identity);
                        //var userIdentity = (ClaimsIdentity)User.Identity;
                        //var claims = userIdentity.Claims;
                        //var roleClaimType = userIdentity.RoleClaimType;
                        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).ToList();
                        model.UserRole = roles[0].Value;


                        if (user.IsInRole(model.UserRole))
                        {
                            if (String.IsNullOrEmpty(returnUrl))
                            {
                                switch (model.UserRole)
                                {
                                    case "Admin":
                                        return RedirectToAction("Dashboard", "Admin");
                                    case "Consumer":
                                        return RedirectToAction("Dashboard", "Consumer");
                                    case "Provider":
                                        return RedirectToAction("Dashboard", "ProviderListing");
                                    case "SaleUser":
                                        return RedirectToAction("Dashboard", "SaleDepartment");
                                    default:
                                        return RedirectToAction("index", "Home");
                                }
                            }
                            return RedirectToLocal(returnUrl);
                        }
                        else
                        {
                            AuthenticationManager.AuthenticationResponseGrant = null;
                            model.Roles = GetRoles();
                            ModelState.AddModelError("", "You cannot login as " + model.UserRole);
                            return View(model);
                        }


                    }
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    {
                        model.Roles = GetRoles();
                        ModelState.AddModelError("", "Invalid login attempt.");
                    }
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            var model = new RegisterViewModel()
            {
                Roles = GetRoles()
            };
            return View(model);
        }
        [AllowAnonymous]
        public ActionResult ConsumerRegister()
        {
            var model = new RegisterViewModel()
            {
                Roles = GetRoles()
            };

            ViewBag.State = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", "Select the State");
            return View(model);
        }
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ConsumerRegister(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserRole = "Consumer";
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                UserManager.AddToRole(user.Id, model.UserRole);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    using (var db = new AgingInHomeContext())
                    {
                        var newconsumer = new ConsumerProfile();
                        newconsumer.FirstName = model.FirstName;
                        newconsumer.Gender = model.Gender;
                        newconsumer.LastName = model.LastName;
                        newconsumer.StateId = model.StateId;
                        newconsumer.UserId = user.Id;
                        newconsumer.Zipcode = model.ZipCode;
                        newconsumer.TypeOfMedicalInsurance = model.TypeOfMedicalInsurance;
                        newconsumer.Relation = model.Relation;
                        newconsumer.PrimaryPhone = model.PrimaryPhone;
                        newconsumer.AlterPhone = model.AlterPhone;
                        newconsumer.Age = model.Age;
                        newconsumer.Address = model.Address;
                        newconsumer.City = model.City;
                        db.ConsumerProfiles.Add(newconsumer);
                        db.SaveChanges();
                        //if (model.StateId==1)
                        //{

                        //    ViewBag.State = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", "Select the State");
                        //    return View(model);
                        //}
                    }
                    return RedirectToAction("Dashboard", "Consumer");


                }
                AddErrors(result);
            }
            ViewBag.State = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", "Select the State");
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult AdminRegister()
        {
            var model = new RegisterViewModel()
            {
                Roles = GetRoles()
            };
            ViewBag.State = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", "Select the State");
            return View(model);
        }
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AdminRegister(RegisterViewModel model)
        {
            ModelState.Remove("Gender");

            if (ModelState.IsValid)
            {
                model.UserRole = "Admin";
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                UserManager.AddToRole(user.Id, model.UserRole);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    using (var db = new AgingInHomeContext())
                    {
                        //var newAdmin = new AdminProfile();
                        //newAdmin.FirstName = model.FirstName;
                        //newAdmin.Gender = model.Gender;
                        //newAdmin.LastName = model.LastName;
                        //newAdmin.Phone = model.Phone;
                        //newAdmin.StateId = model.StateId;
                        //newAdmin.UserId = user.Id;
                        //newAdmin.Zipcode = model.ZipCode;
                        //db.AdminProfiles.Add(newAdmin);
                        //db.SaveChanges();

                    }
                    return RedirectToAction("Dashboard", "Admin");


                }
                AddErrors(result);
            }
            ViewBag.State = new SelectList(new ServicesModel().GetUsStates(), "Id", "State", "Select the State");
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public SelectList GetRoles()
        {

            using (var db = new AgingInHomeContext())
            {

                //var roleStore = new RoleStore<IdentityRole>(db);
                //    var roleMngr = new RoleManager<IdentityRole>(roleStore);

                var Roles = db.AspNetRoles.ToList();


                var roles = Roles
                    .Select(x =>
                        new SelectListItem
                        {
                            Value = x.Name,
                            Text = x.Name
                        });

                return new SelectList(roles, "Value", "Text");
            }
        }


        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                UserManager.AddToRole(user.Id, model.UserRole);

                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    switch (model.UserRole)
                    {
                        case "Admin":
                            return RedirectToAction("Dashboard", "Admin");
                        case "Consumer":
                            return RedirectToAction("Dashboard", "Consumer");
                        case "Provider":
                            return RedirectToAction("MyListing", "ProviderListing");
                        case "SaleUser":
                            return RedirectToAction("Dashboard", "SaleDepartment");
                        default:
                            return RedirectToAction("index", "Home");
                    }

                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> ProviderRegister()
        {

            var model = (RegisterViewModel)TempData["ProviderRegister"];
            var _userexist = UserManager.Users.Where(c => c.Email == model.Email && c.UserName == model.Email).FirstOrDefault();
            if (_userexist == null && model.UserRole=="Provider")
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                UserManager.AddToRole(user.Id, model.UserRole);

                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                EmailSender.SendEmailToRegisterUser(model.Email);

            }
            return RedirectToAction("PromoteYourListing", "ProviderListing");

        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }
        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                //if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                if (user == null)
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                var root = ConfigurationManager.AppSettings["root"];
                MailMessage mail = new MailMessage();
                mail.To.Add(model.Email);
                mail.From = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"]);
                mail.Subject = "Reset Password";
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>";
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["AdminEmail"], ConfigurationManager.AppSettings["Password"]);
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                try
                {
                    client.Send(mail);

                    //Page.RegisterStartupScript("UserMsg","<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
                }
                catch
                {

                }
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }


        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

           [AllowAnonymous]
        public string CheckEmailAvailability(string Email)
        {

            if (UserManager.FindByEmail(Email) != null)
            {
                return "exists";
            }
            else
            {
                return "available";
            }
        }

        //
        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}