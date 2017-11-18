using FYP_Web_App.Models;
using FYP_Web_App.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace FYP_Web_App.Controllers
{
    public class PaperController : Controller
    {

        private PaperRepository _databaseConnection = new PaperRepository();

        private static List<PaperQuestionModal> listOfQuestionsAndMcqs = new List<PaperQuestionModal>();

        private static PaperDetailsModal paperDetails = new PaperDetailsModal();

        private static  byte[] _questionImage = null;

        private static int _index = 0;
        private static int _position = 0;
        // GET: Paper
        public ActionResult Index()
        {
            if (Session["Teacher"] == null)
            {
                return RedirectToAction("Login", "TeacherDashboard");

            }
            ViewBag.TeacherId = Session["TeacherId"];
            return View("List");
        }
        public ActionResult SetPaper()
        {
            return View("SetPaper");
        }
       //Working
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
        public JsonResult GetCourse(int sessionId, int semesterId)
        {
            return Json(_databaseConnection.GetCourse(sessionId, semesterId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSection(int sessionId, int semesterId)
        {
            return Json(_databaseConnection.GetSection(sessionId, semesterId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetTeacher(string sessionId, string semesterId, string courseId)
        {
            return Json(_databaseConnection.GetTeacher(sessionId, semesterId, courseId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreatePaper(string sessionId, string semesterId, string sectionNames, string courseId, int teacherId, string noOfQuestions, string paperName, string timeAllowed,string active,string password,string totalMarks) 
        {
            ViewBag.Session = GetSessionName(sessionId);
            ViewBag.Semester =GetSemesterName(semesterId);
            ViewBag.Subject = GetCourseName(courseId);
            ViewBag.TotalQuestions = noOfQuestions;
            string[] sectionNamesList = sectionNames.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            paperDetails.SessionId = int.Parse(sessionId);
            paperDetails.SemesterId =int.Parse(semesterId);
            paperDetails.CourseId = int.Parse(courseId);
            paperDetails.Sections = sectionNamesList;
            paperDetails.TeacherId = teacherId;
            paperDetails.NoOfQuestions = noOfQuestions;
            paperDetails.PaperName = paperName;
            paperDetails.TimeAllowed = timeAllowed;
            paperDetails.Active = active;
            paperDetails.Password = password;
            paperDetails.TotalMarks = int.Parse(totalMarks);
            return View("~/Views/Paper/CreatePaper.cshtml");
        }
       //just for testing
        public ActionResult GetPaper()
        {
            return View("CreatePaper");
        }
        private string GetCourseName(string courseId)
        {
            return _databaseConnection.GetCourseName(courseId);

        }
        private string GetSemesterName(string semesterId)
        {
            return  _databaseConnection.GetSemesterName(semesterId);

        }
        private string GetSessionName(string sessionId)
        {
            return _databaseConnection.GetSessionName(sessionId);
        }
        public void UploadImage()
        {

            string directory = @"C:\Temp\";

           HttpPostedFileBase file = Request.Files[0];

            string fileName =null;
            if (file != null && file.ContentLength > 0)
            {
               fileName = Path.GetFileName(file.FileName);



                file.SaveAs(Path.Combine(directory, fileName));
            }
            FileInfo filepath = new FileInfo("" + directory + "\\" + fileName);
            FileStream stream = new FileStream(filepath.FullName, FileMode.Open, FileAccess.ReadWrite);

            Bitmap srcImage =(Bitmap)Image.FromStream(stream);

            ImageConverter converter = new ImageConverter();

            _questionImage =(byte[])converter.ConvertTo(srcImage, typeof(byte[]));

            stream.Close();
            filepath.Delete();


        }
        public JsonResult AddQuestion(PaperQuestionModal question)
        {
            question.QuestionPicture = PaperController._questionImage;
            question.Text=question.Text.Trim();
            question.Marks = question.Marks.Trim();
            question.Resources = question.Resources.Trim();
            question.OptionOne.Text = question.OptionOne.Text.Trim();
            question.OptionTwo.Text = question.OptionTwo.Text.Trim();
            question.OptionThree.Text = question.OptionThree.Text.Trim();
            question.OptionFour.Text = question.OptionFour.Text.Trim();
            if (!listOfQuestionsAndMcqs.Contains(question))
            {
                listOfQuestionsAndMcqs.Add(question);
                _position = listOfQuestionsAndMcqs.Count ;
                _index = listOfQuestionsAndMcqs.Count ;
                PaperController._questionImage = null;
            }
            else
            {
                listOfQuestionsAndMcqs[_position]= question;
           
            }
            return Json(listOfQuestionsAndMcqs.Count + 1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Next()
        {
            if (_index < listOfQuestionsAndMcqs.Count - 1) { ++_index;
                ++_position;
            }


            if (_index >= 0 && _index < listOfQuestionsAndMcqs.Count)
            {

                PaperQuestionModal question = listOfQuestionsAndMcqs.ElementAt(_index);
                int indexNo = listOfQuestionsAndMcqs.IndexOf(question);
                question.Index = indexNo;

                return Json(question, JsonRequestBehavior.AllowGet);
            }
            return Json("None", JsonRequestBehavior.AllowGet);


        }
        public JsonResult Previous()
        {
            if (_index > 0) { --_index; --_position; }


            if (_index >= 0 && _index < listOfQuestionsAndMcqs.Count)
            {

                PaperQuestionModal question = listOfQuestionsAndMcqs.ElementAt(_index);
                int indexNo = listOfQuestionsAndMcqs.IndexOf(question);
                question.Index = indexNo;

                return Json(question, JsonRequestBehavior.AllowGet);


            }
            return Json("None", JsonRequestBehavior.AllowGet);

        }
        public JsonResult Submit()
        {
            paperDetails.listOfQuestions = listOfQuestionsAndMcqs;
            return Json( _databaseConnection.Insert(paperDetails), JsonRequestBehavior.AllowGet);
         }
        public JsonResult List(int sessionId,int semesterId,int courseId)
        {
            return Json(_databaseConnection.List(sessionId,semesterId,courseId),JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPaperNames(int sessionId, int semesterId, int courseId, int teacherId)
        {
            return Json(_databaseConnection.GetPaperNames(sessionId, semesterId, courseId, teacherId),JsonRequestBehavior.AllowGet);
        }
        public JsonResult DisplayPaper(int paperId)
        {
            return Json(_databaseConnection.DisplayPaper(paperId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePaper(int paperId,string active, string password)
        {
            return Json(_databaseConnection.UpdatePaper(paperId, active, password),JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPaperForGeneration(int paperId)
        {

            List<PaperQuestionModal> listOfQuestions = _databaseConnection.GetQuestionsForGeneration(paperId);
            listOfQuestions = _databaseConnection.UpdateListWithOptions(listOfQuestions);


            return Json(listOfQuestions, JsonRequestBehavior.AllowGet);


        }
        public JsonResult GeneratePaper(PaperDetailsModal paperDetail)
        { 
            return Json(_databaseConnection.GeneratePaper(paperDetail));
        }


        public JsonResult DisplayResult(int paperId)
        {
            return Json(_databaseConnection.DisplayResult(paperId), JsonRequestBehavior.AllowGet);
        }


    }
   
    }