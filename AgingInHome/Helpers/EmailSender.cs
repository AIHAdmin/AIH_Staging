using AgingInHome.DAL;
using AgingInHome.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace AgingInHome.Helpers
{
    public static class EmailSender
    {

        public static bool CustomeEmailToProvider(ProviderListingModel listingmodel, ServiceRequestModel serviceRequestModel)
        {
            var root = ConfigurationManager.AppSettings["root"];
            MailMessage mail = new MailMessage();
            mail.To.Add(listingmodel.PrimaryEmail);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"]);
            mail.Subject = "Service Request";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "<table border='1' rules='all' width='600'><tr><th>Consumer Name:</th><td>" + serviceRequestModel.FirstName + " " + serviceRequestModel.LastName + "</td></tr><tr><th>Address:</th><td>" + serviceRequestModel.Address + "</td></tr><tr><th>Phone No:</th><td>" + serviceRequestModel.PrimaryPhone + "</td></tr><tr><th>Email:</th><td>" + serviceRequestModel.Email + "</td></tr><tr><th>Service Date</th><td>" + serviceRequestModel.ServiceDate + "</td></tr><tr><th>Best Time</th><td>" + serviceRequestModel.BestTime;
            mail.Body += "</td></tr><tr><th>Invitation Options</th><td style='height: 40px;'>";
            //mail.Body += "Consumer wants your services in "+serviceRequestModel.City+" City With Zipcode "+serviceRequestModel.ZipCode+" on  "+serviceRequestModel.ServiceDate+ "<br/><br/>";
            mail.Body += "<a style='margin-right: 3px; padding: 7px;background-color:darkgreen; color: white' href='" + root + "/Consumer/ProviderInvatationStatus?RequestId=" + serviceRequestModel.Id + "&ProviderListingId=" + listingmodel.ProviderListingId + "&Status=true" + "'>Accept Invitation</a>";
            mail.Body += "<a style='margin-right: 3px; padding: 7px;background-color:red; color: white' href='" + root + "/Consumer/ProviderInvatationStatus?RequestId=" + serviceRequestModel.Id + "&ProviderListingId=" + listingmodel.ProviderListingId + "&Status=false" + "'>Cancel Invitation</a>";
            mail.Body += "<a style='margin-right: 3px; padding: 7px;background-color:blue; color: white' href='" + root + "/Consumer/ProviderInvatationStatus?RequestId=" + serviceRequestModel.Id + "&ProviderListingId=" + listingmodel.ProviderListingId + "&Status=true&alter=1" + "'>Alternative Offer</a></td></tr></table>";
            //mail.BodyEncoding = System.Text.Encoding.UTF8;
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
                return true;
                //Page.RegisterStartupScript("UserMsg","<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
            }
            catch
            {
                return false;
            }

        }
        public static bool SendEmail(string EmailSendTo)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(EmailSendTo);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"]);
            mail.Subject = "Confirmation Email";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "You have successfully Add Listing in Aging in home \n \n Thanks \n <a href='aginginhome.net'>aginginhome.net</a>";
            //mail.BodyEncoding = System.Text.Encoding.UTF8;
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
                return true;
                //Page.RegisterStartupScript("UserMsg","<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
            }
            catch
            {
                return false;
            }

        }
        public static bool SendEmailToRegisterUser(string EmailSendTo)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(EmailSendTo);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"]);
            mail.Subject = "Confirmation Email";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "You have successfully Register in Aging in home \n \n Thanks \n <a href='aginginhome.net'>aginginhome.net</a>";
            //mail.BodyEncoding = System.Text.Encoding.UTF8;
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
                return true;
                //Page.RegisterStartupScript("UserMsg","<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
            }
            catch
            {
                return false;
            }

        }
        public static bool SendEmailToServiceProvider(ProviderListingModel listingmodel, ServiceRequestModel serviceRequestModel)
        {
            var root = ConfigurationManager.AppSettings["root"];
            MailMessage mail = new MailMessage();
            mail.To.Add(listingmodel.PrimaryEmail);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"]);
            mail.Subject = "Service Request";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "<table border='1' rules='all' width='600'><tr><th>Consumer Name:</th><td>" + serviceRequestModel.FirstName + " " + serviceRequestModel.LastName + "</td></tr><tr><th>Address:</th><td>" + serviceRequestModel.Address + "</td></tr><tr><th>Phone No:</th><td>" + serviceRequestModel.PrimaryPhone + "</td></tr><tr><th>Email:</th><td>" + serviceRequestModel.Email + "</td></tr><tr><th>Service Date</th><td>" + serviceRequestModel.ServiceDate + "</td></tr><tr><th>Best Time</th><td>" + serviceRequestModel.BestTime;
            mail.Body += "</td></tr><tr><th>Invitation Options</th><td style='height: 40px;'>";
            //mail.Body += "Consumer wants your services in "+serviceRequestModel.City+" City With Zipcode "+serviceRequestModel.ZipCode+" on  "+serviceRequestModel.ServiceDate+ "<br/><br/>";
            mail.Body += "<a style='margin-right: 3px; padding: 7px;background-color:darkgreen; color: white' href='" + root + "/Consumer/ProviderInvatationStatus?RequestId=" + serviceRequestModel.Id + "&ProviderListingId=" + listingmodel.ProviderListingId + "&Status=true" + "'>Accept Invitation</a>";
            mail.Body += "<a style='margin-right: 3px; padding: 7px;background-color:red; color: white' href='" + root + "/Consumer/ProviderInvatationStatus?RequestId=" + serviceRequestModel.Id + "&ProviderListingId=" + listingmodel.ProviderListingId + "&Status=false" + "'>Cancel Invitation</a>";
            mail.Body += "<a style='margin-right: 3px; padding: 7px;background-color:blue; color: white' href='" + root + "/Consumer/ProviderInvatationStatus?RequestId=" + serviceRequestModel.Id + "&ProviderListingId=" + listingmodel.ProviderListingId + "&Status=true&alter=1" + "'>Alternative Offer</a></td></tr></table>";
            //mail.BodyEncoding = System.Text.Encoding.UTF8;
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
                return true;
                //Page.RegisterStartupScript("UserMsg","<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
            }
            catch
            {
                return false;
            }

        }


        public static bool SendEmailFor7daysWait(string EmailSendTo, string Catname, int RequiredListingId)
        {
            var root = ConfigurationManager.AppSettings["root"];
            MailMessage mail = new MailMessage();
            mail.To.Add(EmailSendTo);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"]);
            mail.Subject = "Listing Not Available";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "The listing <b> " + Catname + "</b> Currently Not Available.we will be contacted within 7 days with the available listing provider in your Area";
            mail.Body += "<br/><a href='" + root + "Consumer/IsWait?RequiredListingId=" + RequiredListingId + "&Iswait=true" + "'>Yes </a><br/>";
            mail.Body += "<a href='" + root + "Consumer/IsWait?RequiredListingId=" + RequiredListingId + "&Iswait=false" + "'>No </a>";
            //mail.BodyEncoding = System.Text.Encoding.UTF8;
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
                return true;
                //Page.RegisterStartupScript("UserMsg","<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
            }
            catch
            {
                return false;
            }

        }

        public static bool SendEmailForNewMsg(string EmailSendTo, string UserName)
        {
            var root = ConfigurationManager.AppSettings["root"];
            MailMessage mail = new MailMessage();
            mail.To.Add(EmailSendTo);
            mail.From = new MailAddress(ConfigurationManager.AppSettings["AdminEmail"]);
            mail.Subject = "New Message";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = "You have new message from: " + UserName;
            //mail.BodyEncoding = System.Text.Encoding.UTF8;
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
                return true;
                //Page.RegisterStartupScript("UserMsg","<script>alert('Successfully Send...');if(alert){ window.location='SendMail.aspx';}</script>");
            }
            catch
            {
                return false;
            }

        }
    }
    public static class HelperClass
    {
        public static string SaveImage(string FolderName, HttpPostedFileBase file)
        {
            string pic = Guid.NewGuid().ToString();
            string extention = System.IO.Path.GetExtension(System.IO.Path.GetFileName(file.FileName));

            bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath(FolderName));
            if (!folderExists)
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(FolderName));

            string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(FolderName), pic + extention);
            file.SaveAs(path);
            return pic + extention;
        }
        public static string SaveImage64binarystring(string FolderName, string file)
        {
            string name = Guid.NewGuid().ToString();
            string Based64BinaryBanner = file;
            string str = Based64BinaryBanner.Replace("data:image/jpeg;base64,", " ");//jpg check
            str = str.Replace("data:image/png;base64,", " ");//png check
            str = str.Replace("data:text/plain;base64,", " ");//text file check
            str = str.Replace("data:;base64,", " ");//zip file check
            str = str.Replace("data:application/zip;base64,", " ");//zip file check
                                                                   // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(str);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            string path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(FolderName), name + ".jpg");
            image.Save(path);
            return name + ".jpg";
        }
        public static string ToClientTime(this DateTime dt)
        {
            var timeOffSet = HttpContext.Current.Session["timezoneoffset"];  // read the value from session

            if (timeOffSet != null)
            {
                var offset = int.Parse(timeOffSet.ToString());
                dt = dt.AddMinutes(-1 * offset);

                return dt.ToString("dd/MM/yyyy HH:mm");
            }

            // if there is no offset in session return the datetime in server timezone
            return dt.ToLocalTime().ToString();
        }
        public static DateTime ToUtcTime(this DateTime dt)
        {
            var timeOffSet = HttpContext.Current.Session["timezoneoffset"];  // read the value from session

            if (timeOffSet != null)
            {
                var offset = int.Parse(timeOffSet.ToString());
                dt = dt.AddMinutes(1 * offset);

                return dt;
            }

            // if there is no offset in session return the datetime in server timezone
            return dt.ToLocalTime();
        }


    }
}