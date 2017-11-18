using FYP_Web_App.Models;
using FYP_Web_App.Repository;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace FYP_Web_App.Controllers
{
    public class StudentController : Controller
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        private StudentRepository _databaseConnection = new StudentRepository();
        private static List<PaperQuestionModal> listOfQuestions = new List<PaperQuestionModal>();
        private static StudentModal studentData = new StudentModal();

        private static int _index = -1;
        private static int _position = -1;
        private static int PaperId { get; set; }
        private static string SessionName { get; set; }
        private static string SemesterName { get; set; }
        private static string CourseName { get; set; }
        private static string PaperName { get; set; }
        private static string NoOfQuestions { get; set; }
        private static string TotalMarks { get; set; }
        private static string TimeAllowed { get; set; }
        public ActionResult Dashboard()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("Login","Student");

            }
            ViewBag.StudentName = studentData.Name + " " + studentData.FatherName;
            ViewBag.RollNumber = studentData.RollNumber;
            return View("Dashboard");
        }
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
        public JsonResult Insert(StudentModal student)
        {
            return Json(_databaseConnection.Insert(student), JsonRequestBehavior.AllowGet);
        }
        public JsonResult List(int sessionId, int semesterId,int sectionId)
        {
            return Json(_databaseConnection.List(sessionId, semesterId, sectionId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Update(StudentModal studentOject)
        {
            return Json(_databaseConnection.Update(studentOject), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(string studentId)
        {
            return Json(_databaseConnection.Delete(studentId), JsonRequestBehavior.AllowGet);
        }
        //////////---Student Realated Operations-----------///////////
        public ActionResult ListPaper()
        {
           return RedirectToAction("PapersList", "Student");
        }
        public ActionResult PapersList()
        {
            return View("ListPapers");
        }
        public JsonResult GetPapers(int sessionId,int semesterId)
        {
            SessionName = _databaseConnection.GetSessionName(sessionId);
            SemesterName = "" + semesterId;

          
            return Json(_databaseConnection.GetPapers(sessionId, semesterId),JsonRequestBehavior.AllowGet);
        }
        public JsonResult StartTest(int id,string password,string courseName,string paperName,string noOfQuestions,string totalMarks,string timeAllowed)
        {
            CourseName = courseName;
            PaperName = paperName;
            NoOfQuestions = noOfQuestions;
            TotalMarks = totalMarks;
            TimeAllowed = timeAllowed;
            PaperId = id;
            bool isMatched =  _databaseConnection.MatchPassword(id, password);

            if (isMatched == true)
            {
                return Json('1', JsonRequestBehavior.AllowGet);
            }
            return Json('0', JsonRequestBehavior.AllowGet);
        }
        //Start from here
        public ActionResult Test()
        {
            listOfQuestions = _databaseConnection.GetQuestions(PaperId);
            listOfQuestions = _databaseConnection.UpdateListWithOptions(listOfQuestions);

            @ViewBag.Session = SessionName;
            @ViewBag.Semester = SemesterName;
            @ViewBag.Subject = CourseName;
            @ViewBag.TotalQuestions = NoOfQuestions;
            @ViewBag.PaperTime = TimeAllowed;

            PaperQuestionModal question = listOfQuestions.ElementAt(0);
            @ViewBag.Text = question.Text;
            @ViewBag.QuestionPicture = question.QuestionPicture;
            @ViewBag.OptionOneText = question.OptionOne.Text;
            @ViewBag.OptionTwoText = question.OptionTwo.Text;
            @ViewBag.OptionThreeText = question.OptionThree.Text;
            @ViewBag.OptionFourText = question.OptionFour.Text;


            return View("Exam");
        }
        public JsonResult Next()
        {
            if (_index < listOfQuestions.Count - 1)
            {
                ++_index;
                ++_position;
            }


            if (_index >= 0 && _index < listOfQuestions.Count)
            {

                PaperQuestionModal question = listOfQuestions.ElementAt(_index);
              
                question.Index = _index; 

                return Json(question, JsonRequestBehavior.AllowGet);
            }
            return Json("None", JsonRequestBehavior.AllowGet);


        }
        public JsonResult Previous()
        {
            if (_index > 0) { --_index; --_position; }


            if (_index >= 0 && _index < listOfQuestions.Count)
            {
                PaperQuestionModal question = listOfQuestions.ElementAt(_index);
              
                question.Index = _index;
                return Json(question, JsonRequestBehavior.AllowGet);
            }
            return Json("None", JsonRequestBehavior.AllowGet);

        }
        public JsonResult SaveAnswer(PaperQuestionModal question)
        {
            listOfQuestions[_position] = question;          
            return Json(listOfQuestions.Count + 1, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Submit()
        {
            
            int obtainedMarks = 0;
            List<PaperQuestionModal> questionsList = _databaseConnection.GetQuestions(PaperId);
            int[] indexesOfQuestions = new int[questionsList.Count];
            questionsList = _databaseConnection.UpdateListWithOptionsCharacteristics(questionsList);
            for(int index = 0; index < questionsList.Count; index++)
            {
               if(listOfQuestions[index].OptionOne.Correct == questionsList[index].OptionOne.Correct)
                {
                    if (listOfQuestions[index].OptionTwo.Correct == questionsList[index].OptionTwo.Correct)
                    {
                        if (listOfQuestions[index].OptionThree.Correct == questionsList[index].OptionThree.Correct)
                        {
                            if (listOfQuestions[index].OptionFour.Correct == questionsList[index].OptionFour.Correct)
                            {
                                obtainedMarks +=int.Parse(questionsList[index].Marks);
                                indexesOfQuestions[index] = 1;
                            }

                        }

                    }

                }

            }
         
         FileStream fs = new FileStream(Server.MapPath("/Pdf") +"\\"+ ""+CourseName+".pdf", FileMode.Create);
         // Create an instance of the document class which represents the PDF document itself.
         Document document = new Document(PageSize.A4,25,25,30,30);
         // Create an instance to the PDF file by creating an instance of the PDF 
         // Writer class using the document and the filestrem in the constructor.
         PdfWriter writer = PdfWriter.GetInstance(document, fs);

         // Add meta information to the document
         document.AddAuthor("Ahsan Rafiq");//Teacher Name
         document.AddCreator("Sample application using iTextSharp");
         document.AddKeywords("PDF tutorial education");
         document.AddSubject("Document subject - Describing the steps creating a PDF document");
         document.AddTitle(PaperName);

         // Open the document to enable you to write to the document
         document.Open();

          Phrase phrase1 = new Phrase();
          phrase1.Add(new Chunk("2013-2017" +"    "+ 1 +"    "+ "ITC", FontFactory.GetFont("New Times Roman", 10, iTextSharp.text.Font.NORMAL)));
          phrase1.Add(new Chunk(Environment.NewLine));

            // Add a simple and wellknown phrase to the document in a flow layout manner

            foreach (PaperQuestionModal question in questionsList)
         {
             Phrase phrase = new Phrase();
             phrase.Add(new Chunk(questionsList.IndexOf(question)+1 +"). "+question.Text +"     "+ "(Marks : "+ question.Marks+")", FontFactory.GetFont("New Times Roman", 10, iTextSharp.text.Font.NORMAL)));


                if (question.OptionOne.Correct == 1)
                {
                    phrase.Add(new Chunk(Environment.NewLine));
                    phrase.Add(new Chunk("(a) " + question.OptionOne.Text, FontFactory.GetFont("New Times Roman", 8, iTextSharp.text.Font.BOLDITALIC)));
                }
                else
                {
                    phrase.Add(new Chunk(Environment.NewLine));
                    phrase.Add(new Chunk("(a) " + question.OptionOne.Text, FontFactory.GetFont("New Times Roman", 8, iTextSharp.text.Font.NORMAL)));
                }
                if(question.OptionTwo.Correct==1)
                {
                    phrase.Add(new Chunk(Environment.NewLine));
                    phrase.Add(new Chunk("(b) " + question.OptionTwo.Text, FontFactory.GetFont("New Times Roman", 8, iTextSharp.text.Font.BOLDITALIC)));
                }
                else
                {
                    phrase.Add(new Chunk(Environment.NewLine));
                    phrase.Add(new Chunk("(b) " + question.OptionTwo.Text, FontFactory.GetFont("New Times Roman", 8, iTextSharp.text.Font.NORMAL)));
                }
                if (question.OptionThree.Correct ==1)
                {
                    phrase.Add(new Chunk(Environment.NewLine));
                    phrase.Add(new Chunk("(c) " + question.OptionThree.Text, FontFactory.GetFont("New Times Roman", 8, iTextSharp.text.Font.BOLDITALIC)));
                }
                else
                {
                    phrase.Add(new Chunk(Environment.NewLine));
                    phrase.Add(new Chunk("(c) " + question.OptionThree.Text, FontFactory.GetFont("New Times Roman", 8, iTextSharp.text.Font.NORMAL)));
                }
                if(question.OptionFour.Correct == 1)
                {
                    phrase.Add(new Chunk(Environment.NewLine));
                    phrase.Add(new Chunk("(d) " + question.OptionFour.Text, FontFactory.GetFont("New Times Roman", 8, iTextSharp.text.Font.BOLDITALIC)));
                }
                else
                {
                    phrase.Add(new Chunk(Environment.NewLine));
                    phrase.Add(new Chunk("(d) " + question.OptionFour.Text, FontFactory.GetFont("New Times Roman", 8, iTextSharp.text.Font.NORMAL)));
                }

                phrase.Add(new Chunk(Environment.NewLine));
                if (indexesOfQuestions[questionsList.IndexOf(question)] == 1)
                {
                    phrase.Add(new Chunk("Answer : Correct"));
                }
                else
                {
                    phrase.Add(new Chunk("Answer :Not Correct", FontFactory.GetFont("New Times Roman", 10, iTextSharp.text.Font.NORMAL)));
                    phrase.Add(new Chunk(Environment.NewLine));
                    phrase.Add(new Chunk("Resources : "));
                    phrase.Add(new Chunk(Environment.NewLine));
                    phrase.Add(new Chunk(question.Resources, FontFactory.GetFont("New Times Roman", 10, iTextSharp.text.Font.NORMAL)));
                    phrase.Add(new Chunk(Environment.NewLine));
                }

                phrase.Add(new Chunk(Environment.NewLine));
             document.Add(phrase);
         }
         // Close the document
         document.Close();

         // Close the writer instance
         writer.Close();

         // Always close open filehandles explicity
         fs.Close();



        

         // add result to database table
         return null;


        }
        public JsonResult ChangePassword(string oldPassword, string newPassword)
        {
            return Json(_databaseConnection.ChangePassword(oldPassword, newPassword),JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditPassword()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("Login", "Student");

            }
            return View("EditPassword");
        }
        public ActionResult Timetable()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("Login", "Student");

            }
            return View("Timetable");
        }
        public ActionResult Result()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("Login", "Student");

            }
            return View("Result");
        }
        public JsonResult GetPaperNames(int sessionId,int semesterId,int courseId)
        {
            return Json(_databaseConnection.GetPaperNames(sessionId, semesterId, courseId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetResult(int sessionId, int semesterId, int courseId,int paperId,string rollNumber)
        {
            return Json(_databaseConnection.GetResult(sessionId, semesterId, courseId, paperId, rollNumber), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMidterm(int sessionId,int semesterId)
        {
            return Json(_databaseConnection.GetMidterm(sessionId, semesterId), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMaterial(int sessionId, int semesterId)
        {
            return Json(_databaseConnection.Folder(sessionId, semesterId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Folder()
        {
             if (Session["Student"] == null)
            {
                return RedirectToAction("Login", "Student");

            }
            
            return View("Folder");

        }
        public ActionResult Notification()
        {
            if (Session["Student"] == null)
            {
                return RedirectToAction("Login", "Student");

            }
            return View("Notification");

        }
        public JsonResult GetNotification(int sessionId, int semesterId)
        {
            return Json(_databaseConnection.GetNotification(sessionId, semesterId), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login()
        {
            return View("Login");

        }
        public RedirectToRouteResult CheckCredentials(string rollNumber, string password)
        {
            int exits = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spVerifyStudent", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@RollNumber", rollNumber);
                com.Parameters.AddWithValue("@Password", password);
                try
                {
                    exits = (int)com.ExecuteScalar();
                }
                catch (Exception e)
                {
                    return RedirectToAction("Login", "Student");

                }
            }
            if (exits == 1)
            {
                GetStudentData(rollNumber);

                Session["Student"] = "Student";
                return RedirectToAction("Dashboard", "Student");
            }
            else
            {
                return RedirectToAction("Login", "Student");

            }


            

        }
        private void GetStudentData(string rollNumber)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetStudent", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@RollNumber", rollNumber);
                
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    studentData.Name = rdr["Name"].ToString();
                    studentData.RollNumber = rdr["RollNumber"].ToString();
                    studentData.FatherName = rdr["FatherName"].ToString();
                }

            }
        }
        public ActionResult Logout()
        {

            Session["Student"] = null;
        

            return View("Login");
        }



    }
}