using FYP_Web_App.Models;
using FYP_Web_App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP_Web_App.Controllers
{
    public class TeacherController : Controller
    {
        private TeacherRepository _databaseConnection = new TeacherRepository();
        // GET: Teacher
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View("List");
        }

        public ActionResult Add()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View("Create");
        }

        public JsonResult Insert(TeacherModal teacher)
        {
            return Json(_databaseConnection.Insert(teacher),JsonRequestBehavior.AllowGet);
        }


        public JsonResult List()
        {

            return Json(_databaseConnection.List(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult Update(TeacherModal teacher)
        {

            return Json(_databaseConnection.Update(teacher), JsonRequestBehavior.AllowGet);

        }

        public JsonResult Delete(string teacherId)
        {

            return Json(_databaseConnection.Delete(teacherId), JsonRequestBehavior.AllowGet);

        }

    }
}