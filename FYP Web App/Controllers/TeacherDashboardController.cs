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
    public class TeacherDashboardController : Controller
    {
        private static string getTeacherId = null;
        private static string  getTeacherName = null;
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        // GET: TeacherDashboard
        public ActionResult Index()
        {
            if(Session["Teacher"] == null)
            {

                return RedirectToAction("Login", " TeacherDashboard");
            }

            Session["TeacherId"] = getTeacherId;
            ViewBag.TeacherName = getTeacherName;
            return View("Dashboard");
        }
        public ActionResult Login()
        {
            return View("Login");
        }
        public RedirectToRouteResult CheckCredentials(string teacherId, string password)
        {           
                
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetTeacherForLogin", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@EmployeeId", teacherId);
                com.Parameters.AddWithValue("@Password", password);
                try
                {
                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        getTeacherId = rdr["Id"].ToString();
                        getTeacherName = rdr["FirstName"].ToString() + " " + rdr["LastName"].ToString();
                    }

                   
                }
                catch (Exception exception)
                {
                    return RedirectToAction("Login", "TeacherDashboard");
                }
            }
            if (getTeacherId!=null)
            {
                Session["Teacher"] = "Teacher";
                return RedirectToAction("Index", "TeacherDashboard");
            }
            else
            {
                return RedirectToAction("Login", "TeacherDashboard");

            }

            


        }

        public ActionResult Logout()
        {
            Session["Teacher"] = null;

            return View("Logins");


        }
    }
}