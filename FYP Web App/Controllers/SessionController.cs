using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FYP_Web_App.Models;
using System.Configuration;
using FYP_Web_App.Repository;

namespace FYP_Web_App.Controllers
{
    public class SessionController : Controller
    {
        private SessionRepository _databaseConnection = new SessionRepository();


        // GET: Session
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
        public JsonResult Insert(SessionModal session)
        {
            if ((int.Parse(session.SessionEndYear) - int.Parse(session.SessionStartYear)) != 4)
            {
                return Json('4', JsonRequestBehavior.AllowGet);

            }
            return Json(_databaseConnection.Insert(session), JsonRequestBehavior.AllowGet);
        }
        public JsonResult List()
        {
            List<SessionModal> listOfSessions = new List<SessionModal>();

            listOfSessions = _databaseConnection.List();

            if (listOfSessions.Count == 0)
            {
                return Json('0', JsonRequestBehavior.AllowGet);
            }

            return Json(listOfSessions, JsonRequestBehavior.AllowGet);

        }
        public JsonResult Delete(String sessionID)
        {
            return Json(_databaseConnection.Delete(sessionID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(SessionModal updatedSession)
        {

          
                if ((int.Parse(updatedSession.SessionEndYear) - int.Parse(updatedSession.SessionStartYear)) != 4)
                {
                    return Json('4', JsonRequestBehavior.AllowGet);

                }
            return Json(_databaseConnection.Update(updatedSession), JsonRequestBehavior.AllowGet);

        }
    }
}