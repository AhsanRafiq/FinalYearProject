using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP_Web_App.Controllers
{
    public class AdminController : Controller
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public ActionResult Login()
        {
           
            return View("Login");
        }
        public RedirectToRouteResult CheckCredentials(string adminUserName, string adminPassword)
        {
            int exits = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spVerifyAdmin", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@UserName", adminUserName);
                com.Parameters.AddWithValue("@Password", adminPassword);
                try
                {
                    exits =(int) com.ExecuteScalar();
                }
                catch (Exception e)
                {
                  return  RedirectToAction("Login", "Admin");

                }
            }
            if(exits == 1)
            {
                Session["Admin"] = "Admin";
                return RedirectToAction("Index","Admin");
            }
            else
            {
                return RedirectToAction("Login","Admin");

            }
           



        }
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View("Admindashboard");
        }
        public JsonResult ChangePassword(string oldPassword,string newPassword)
        {
            int matched = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spMatchPassword", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@OldPassword", oldPassword);
                try
                {
                    matched =(int)com.ExecuteScalar();
                }
                catch (Exception exception)
                {
                }
            }
            if(matched == 0)
            {
                return Json('0', JsonRequestBehavior.AllowGet);
            }
            int added = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spSetPassword", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@NewPassword", newPassword);
                try
                {
                    added = com.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
                   

                }
            }
            return Json(added,JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditPassword()
        {
            return View("EditPassword");
        }
        public ActionResult Logout()
        {
            Session["Admin"] = null;
            return View("Login");
        }
    }
}