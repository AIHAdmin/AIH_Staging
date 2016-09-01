using AgingInHome.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgingInHome.Controllers
{
    public class SQLAnalyzerController : Controller
    {
        SQLAnalyzerModel sqlAnalyzerM = new SQLAnalyzerModel();
        //
        // GET: /SQLAnalyzer/
        public ActionResult Index()
        {
            return View();
        }
        [NonAction]
         private string GetConnectionString()
        {

            //sets the connection string from your web config file. "DBConnection" is the name of your Connection String

            return System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

         }
        public ActionResult AddAboutUs(int listingId)
        {
            ViewBag.listingId = listingId;
            return View();
        }

        public ActionResult CreateSQLObjects(string body)
        {
            if (!string.IsNullOrEmpty(body))
            {
                SqlConnection connection = new SqlConnection(GetConnectionString());
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                string sqlStatement = body;
                SqlCommand sqlCmd = new SqlCommand(sqlStatement, connection);
                sqlCmd.ExecuteNonQuery();
                connection.Close();
            }
            return View();
        }
	}
}