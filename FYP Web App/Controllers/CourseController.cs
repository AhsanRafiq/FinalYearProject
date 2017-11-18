using FYP_Web_App.Models;
using FYP_Web_App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP_Web_App.Controllers
{
    public class CourseController : Controller
    {
        private CourseRepository _databaseConnection = new CourseRepository();
        // GET: Course
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

        public JsonResult Delete(string courseId)
        {
            return Json(_databaseConnection.Delete(courseId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(CourseModal courseOject)
        {
            return Json(_databaseConnection.Update(courseOject), JsonRequestBehavior.AllowGet);
        }


        public JsonResult Insert(CourseModal courseOject)
        {
            return Json(_databaseConnection.Insert(courseOject), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSessions()
        {
            List<SessionModal> listOfSessions = new List<SessionModal>();
            listOfSessions = _databaseConnection.GetSessions();
            return Json(listOfSessions, JsonRequestBehavior.AllowGet);

        }

        public JsonResult List(int sessionId,int semesterId)
        {

            return Json(_databaseConnection.List(sessionId, semesterId),JsonRequestBehavior.AllowGet);

        }




        private int GetSemesterId(int semesterNo)
        {
            int id = _databaseConnection.GetSemesterId(semesterNo);
            return id;
        }

        public JsonResult GetSemesters(int sessionId)
        {
            return Json(_databaseConnection.GetSemesters(sessionId), JsonRequestBehavior.AllowGet);
        }
    }
}