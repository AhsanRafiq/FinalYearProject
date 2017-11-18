using FYP_Web_App.Models;
using FYP_Web_App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP_Web_App.Controllers
{
    public class SemesterController : Controller
    {
        private SemesterRepository _databaseConnection = new SemesterRepository();



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


        public JsonResult Insert(SemesterModal semester)
        {

            return Json(_databaseConnection.Insert(semester), JsonRequestBehavior.AllowGet);

        }

        public JsonResult List()
        {


            List<SemesterModal> listOfSemester = new List<SemesterModal>();

            listOfSemester = _databaseConnection.List();

            if (listOfSemester.Count == 0)
            {
                return Json('0', JsonRequestBehavior.AllowGet);
            }

            return Json(listOfSemester, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetSessions()
        {
            List<SessionModal> listOfSessions = new List<SessionModal>();

            listOfSessions = _databaseConnection.GetSessions();

            return Json(listOfSessions, JsonRequestBehavior.AllowGet);


        }


        public JsonResult Delete(string semesterId)
        {

            return Json(_databaseConnection.Delete(semesterId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Update(SemesterModal semester)
        {



            return Json(_databaseConnection.Update(semester), JsonRequestBehavior.AllowGet);
        }
    }
}