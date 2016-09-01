using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AgingInHome.Helpers
{
    public class CommonClass
    {
        public static void ErrorMessage(Exception ex)
        {
            string filePath = HttpContext.Current.Server.MapPath("~/log.txt");

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                   "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
            }
        }
    }
}