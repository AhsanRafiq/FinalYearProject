using FYP_Web_App.Models;
using FYP_Web_App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP_Web_App.Controllers
{
    public class SectionController : Controller
    {
        private SectionRepository _databaseConnection = new SectionRepository();
        // GET: Section
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


        public JsonResult Insert(SectionModal sectionObject)
        {
            return Json(_databaseConnection.Insert(sectionObject), JsonRequestBehavior.AllowGet);
        }  
        public JsonResult GetSemesters(int sessionId)
        {
            return Json(_databaseConnection.GetSemesters(sessionId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult List()
        {
            List<SectionModal> listOfSections = _databaseConnection.List();

            if (listOfSections.Count == 0)
            {

                return Json('0', JsonRequestBehavior.AllowGet);
            }


            return Json(_databaseConnection.List(), JsonRequestBehavior.AllowGet);

        }
        public JsonResult Delete(int sectionId)
        { 
            return Json(_databaseConnection.Delete(sectionId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(SectionModal section)
        {

            section.SemesterId = GetSemesterId(Int32.Parse(section.SemesterNo));


            return Json(_databaseConnection.Update(section), JsonRequestBehavior.AllowGet);


        }
        private int GetSemesterId(int semesterNo)
        {
            int id = _databaseConnection.GetSemesterId(semesterNo);
            return id;
        }




    }//Class
}