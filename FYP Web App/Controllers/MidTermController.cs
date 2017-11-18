using FYP_Web_App.Models;
using FYP_Web_App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP_Web_App.Controllers
{
    public class MidTermController : Controller
    {
        private MidTermRepository _databaseConnection = new MidTermRepository();
        // GET: MidTerm
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
        public JsonResult Insert(MidTermModal midterm)
        {
            midterm.Sections= midterm.SectionNames.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            bool isExist = _databaseConnection.CheckDuplication(midterm);
            if (isExist)
            {

                return Json('0', JsonRequestBehavior.AllowGet);
            }
            return Json(_databaseConnection.Insert(midterm)); 

        }
        public JsonResult List(int sessionId, int semesterId)
        {

            return Json(_databaseConnection.List(sessionId, semesterId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(int id, string startDay, string startMonth, string startYear,string time)
        {
            return Json(_databaseConnection.Update(id, startDay, startMonth, startYear, time), JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            return Json(_databaseConnection.Delete(id), JsonRequestBehavior.AllowGet);
        }



    }
}