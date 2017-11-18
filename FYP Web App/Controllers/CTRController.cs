using FYP_Web_App.Models;
using FYP_Web_App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP_Web_App.Controllers
{
    public class CTRController : Controller
    {
        private CTRRepository _databaseConnection = new CTRRepository();
        // GET: CTR
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
        public JsonResult GetSessions()
        {
            List<SessionModal> listOfSessions = new List<SessionModal>();

            listOfSessions = _databaseConnection.GetSessions();

            return Json(listOfSessions, JsonRequestBehavior.AllowGet);


        }
        public JsonResult GetSemesters(int sessionId)
        {
            return Json(_databaseConnection.GetSemesters(sessionId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSection(int sessionId, int semesterId)
        {
            return Json(_databaseConnection.GetSection(sessionId, semesterId), JsonRequestBehavior.AllowGet);
        }   
        public JsonResult GetCourse(int sessionId, int semesterId)
        {
            return Json(_databaseConnection.GetCourse(sessionId, semesterId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTeacher()
        {
            return Json(_databaseConnection.GetTeacher(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Insert(CTRModal ctr)
        {
            return Json(_databaseConnection.Insert(ctr), JsonRequestBehavior.AllowGet);
        }
        public JsonResult List(int sessionId, int semesterId, int sectionId)
        {

            return Json(_databaseConnection.List(sessionId, semesterId, sectionId), JsonRequestBehavior.AllowGet);

        }
        public JsonResult Update(CTRModal ctrOject)
        {
            return Json(_databaseConnection.Update(ctrOject), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string ctrId)
        {
            return Json(_databaseConnection.Delete(ctrId), JsonRequestBehavior.AllowGet);
        }

    }
}