using FYP_Web_App.Models;
using FYP_Web_App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP_Web_App.Controllers
{

    
    public class MaterialController : Controller
    {

        private MaterialRepository _databaseConnection = new MaterialRepository();
        
        // GET: Material
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
        public JsonResult Insert(MaterialModal material)
        {
            bool isExist = _databaseConnection.CheckDuplication(material);
            if(isExist)
            {

                return Json('0', JsonRequestBehavior.AllowGet);
            }
            return Json(_databaseConnection.Insert(material));
        }
        public JsonResult Delete (int id)
        {



            return Json(_databaseConnection.Delete(id), JsonRequestBehavior.AllowGet);

        }
        public JsonResult List(int sessionId,int semesterId,int courseId)
        {
            List<MaterialModal> listOfMaterials = _databaseConnection.List(sessionId, semesterId, courseId);

            return Json(listOfMaterials, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(int id,string materialName,string materialDescription)
        {
            return Json(_databaseConnection.Update(id, materialName, materialDescription),JsonRequestBehavior.AllowGet);
        }






    }
}