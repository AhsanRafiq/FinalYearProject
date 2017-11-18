using FYP_Web_App.Models;
using FYP_Web_App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP_Web_App.Controllers
{
    public class NotificationController : Controller
    {


        private NotificationRepository _databaseConnection = new NotificationRepository();
        // GET: Notification
        public ActionResult Index()
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("Login", "TeacherDashboard");

            }
            return View("List");
        }
        public ActionResult Add()
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("Login", "TeacherDashboard");

            }
            return View();
        }
        public JsonResult List(int sessionId, int semesterId, int courseId)
        {
            return Json(_databaseConnection.List(sessionId, semesterId, courseId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Insert(NotificationModal notification)
        {
            return Json(_databaseConnection.Insert(notification),JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(int id, string notificationName, string notificationDescription)
        {
            return Json(_databaseConnection.Update(id, notificationName, notificationDescription), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int id)
        {
            return Json(_databaseConnection.Delete(id), JsonRequestBehavior.AllowGet);
        }

    }
}