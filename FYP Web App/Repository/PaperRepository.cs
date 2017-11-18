using FYP_Web_App.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FYP_Web_App.Repository
{
    public class PaperRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public string GetCourseName(string courseId)
        {

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string courseName = null;
                SqlCommand com = new SqlCommand("spGetCourseName", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", courseId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {


                    courseName = rdr["CourseName"].ToString();

                }
                return courseName;

            }


        }
        public string GetSessionName(string sessionId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sessionName = null;
                SqlCommand com = new SqlCommand("spGetSessionName", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", sessionId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    sessionName = rdr["SessionId"].ToString();
                }
                return sessionName;
            }


        }
        public string GetSemesterName(string semesterId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string semesterName = null;
                SqlCommand com = new SqlCommand("spGetSemesterName", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", semesterId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {


                    semesterName = rdr["SemesterNo"].ToString();

                }
                return semesterName;

            }
        }
        public List<SemesterModal> GetSemesters(int sessionId)
        {
            List<SemesterModal> listOfSemesters = new List<SemesterModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetSemesters", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfSemesters.Add(new SemesterModal
                    {
                        Id =int.Parse( rdr["Id"].ToString()),
                        SemesterNo = rdr["SemesterNo"].ToString()
                    });
                }


                List<SemesterModal> SortedList = listOfSemesters.OrderBy(o => o.SemesterNo).ToList();
                return SortedList;
            }
        }
        public List<SessionModal> GetSessions()
        {

            List<SessionModal> listOfSessions = new List<SessionModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spListSession", connection);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfSessions.Add(new SessionModal
                    {
                        Id = Int32.Parse(rdr["Id"].ToString()),
                        SessionId = rdr["SessionId"].ToString(),
                        SessionProgram = rdr["Program"].ToString(),
                        SessionStartDay = rdr["SessionStartDay"].ToString(),
                        SessionStartMonth = rdr["SessionStartMonth"].ToString(),
                        SessionStartYear = rdr["SessionStartYear"].ToString(),
                        SessionEndDay = rdr["SessionEndDay"].ToString(),
                        SessionEndMonth = rdr["SessionEndMonth"].ToString(),
                        SessionEndYear = rdr["SessionEndYear"].ToString(),
                    });
                }
                return listOfSessions;

            }



        }
        public List<CourseModal> GetCourse(int sessionId, int semesterId)
        {
            List<CourseModal> listOfSemesters = new List<CourseModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetCourse", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                com.Parameters.AddWithValue("@SemesterId", semesterId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfSemesters.Add(new CourseModal
                    {
                        Id = int.Parse(rdr["Id"].ToString()),
                        CourseName = rdr["CourseName"].ToString()
                    });
                }
                return listOfSemesters;
            }
        }
        public List<SectionModal> GetSection(int sessionId, int semesterId)
        {
            List<SectionModal> listOfSemesters = new List<SectionModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetSection", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                com.Parameters.AddWithValue("@SemesterId", semesterId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfSemesters.Add(new SectionModal
                    {
                        Id = int.Parse(rdr["Id"].ToString()),
                        SectionName = rdr["SectionName"].ToString()
                    });
                }
                return listOfSemesters;
            }







        }
        public List<TeacherModal> GetTeacher(string sessionId, string semesterId, string courseId)
        {
            List<TeacherModal> listOfSemesters = new List<TeacherModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetTeacherBySessionSemesterCourse", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                com.Parameters.AddWithValue("@SemesterId", semesterId);
                com.Parameters.AddWithValue("@CourseId", courseId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfSemesters.Add(new TeacherModal
                    {
                        Id = int.Parse(rdr["Id"].ToString()),
                        EmployeeId = rdr["EmployeeId"].ToString(),
                        FirstName = rdr["FirstName"].ToString(),
                        LastName = rdr["LastName"].ToString()
                    });
                }
                return listOfSemesters;
            }
        }
        public int Insert(PaperDetailsModal paperDetails)
        {
            InsertIntoPaperDetails(paperDetails);
            int paperId =  GetPaperIdForQuestions(paperDetails.SessionId,paperDetails.SemesterId,paperDetails.PaperName);
            InsertSections(paperDetails.Sections, paperId);
            InsertQuestionAndChoices(paperDetails.listOfQuestions, paperId);

            return 1;
        }
        private void InsertSections(string[] sectionList, int paperId)
        {
            foreach (var section in sectionList)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spInsertSections", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@PaperId ", paperId);
                    command.Parameters.AddWithValue("@SectionId", section);


                    try
                    {
                        var noOfRowsAffected = command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627)
                        {
                            
                        }
                    }
                }
            }
        }

        public List<StudentModal> DisplayResult(int paperId)
        {


            List<StudentModal> listOfResult = new List<StudentModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetResultByPaperId", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@PaperId", paperId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfResult.Add(new StudentModal
                    {
                        Id = int.Parse(rdr["Id"].ToString()),
                        SessionName =GetSessionName( rdr["SessionId"].ToString()),
                        SemesterName =GetSemesterName( rdr["SemesterId"].ToString()),
                        CourseName =GetCourseName( rdr["CourseId"].ToString()),
                        PaperName =GetPaperName( rdr["SessionId"].ToString()),
                        RollNumber =GetRollNumber( rdr["SemesterId"].ToString()),
                        ObtainedMarks =  rdr["MarksObtained"].ToString(),
                        TotalMarks = rdr["TotalMarks"].ToString()

                    });
                }
                return listOfResult;
            }






        }
        private string GetRollNumber(string studentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string rollNumber = null;
                SqlCommand com = new SqlCommand("spGetRollNo", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@StudentId", studentId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    rollNumber = rdr["RollNumber"].ToString();
                }
                return rollNumber;
            }
        }
        private string GetPaperName(string paperId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string paperName = null;
                SqlCommand com = new SqlCommand("spGetPaperName", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@PaperId", paperId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    paperName = rdr["PaperName"].ToString();
                }
                return paperName;
            }

        }

        private int GetPaperIdForQuestions(int sessionId, int semesterId, string paperName)
        {
            int noOfRowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("spGetPaperIdForQuestions", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@SessionId", sessionId);
                command.Parameters.AddWithValue("@SemesterId", semesterId);
                command.Parameters.AddWithValue("@PaperName", paperName);

                try
                {
                    SqlDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        noOfRowsAffected =int.Parse( rdr["Id"].ToString());
                    }
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        return 0;
                    }
                }
            }
            return noOfRowsAffected;


        }
        public List<PaperDetailsModal> GetPaperNames(int sessionId, int semesterId, int courseId, int teacherId)
        {
            List<PaperDetailsModal> listOfPapersNames = new List<PaperDetailsModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetPaperNames", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                com.Parameters.AddWithValue("@SemesterId", semesterId);
                com.Parameters.AddWithValue("@CourseId", courseId);
                com.Parameters.AddWithValue("@TeacherId", teacherId);

                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfPapersNames.Add(new PaperDetailsModal
                    {
                        Id = int.Parse(rdr["Id"].ToString()),
                        PaperName =  rdr["PaperName"].ToString()
                    });
                }
                return listOfPapersNames;
            }
        }
        public int GeneratePaper(PaperDetailsModal paperDetial)
        {
            InsertIntoPaperDetails(paperDetial);
            List<PaperQuestionModal> listOfQuestions = GetQuestionsFromDBByQuestionId(paperDetial.QuestionList);
            int paperId = GetPaperIdForQuestions(paperDetial.SessionId, paperDetial.SemesterId, paperDetial.PaperName);
            InsertSections(paperDetial.Sections, paperId);
            listOfQuestions = UpdateListWithOptions(listOfQuestions);
            InsertQuestionAndChoices(listOfQuestions, paperId);
            return 1;
        }
        private List<PaperQuestionModal> GetQuestionsFromDBByQuestionId(string[] questionList)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                List<PaperQuestionModal> questionsFromDb = new List<PaperQuestionModal>();
                foreach (string question in questionList) {
                    SqlCommand com = new SqlCommand("spGetQuestionsById", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@QuestionId", question);


                    SqlDataReader rdr = com.ExecuteReader();
                    while (rdr.Read())
                    {
                        questionsFromDb.Add(new PaperQuestionModal
                        {
                            Id = Int32.Parse(question),
                            Text = rdr["Text"].ToString(),
                            QuestionPicture =(Byte[]) rdr["QuestionPicture"],
                            Resources = rdr["Resources"].ToString(),
                            Marks = rdr["Marks"].ToString()
                        });
                    }
                  
                }
                return questionsFromDb;
            }

        }
        public List<PaperDetailsModal> DisplayPaper(int paperId)
        {
            List<PaperDetailsModal> paper = new List<PaperDetailsModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spDisplayPaper", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@PaperId", paperId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    paper.Add(new PaperDetailsModal
                    {
                        Id = int.Parse(rdr["Id"].ToString()),
                        SectionNames = GetSectionsByPaperId(int.Parse(rdr["Id"].ToString())),
                        NoOfQuestions = rdr["NoOfQuestions"].ToString(),
                        TimeAllowed = rdr["TimeAllowed"].ToString(),
                        Active = rdr["Active"].ToString(),
                        TotalMarks = int.Parse(rdr["TotalMarks"].ToString()),
                        Password = rdr["Password"].ToString()
                    });
                }
                return paper;
            }
        }
        private string GetSectionsByPaperId(int id)
        {
            string sections =null;
      
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetSectionsByPaperId", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", id);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    sections +=( rdr["SectionName"].ToString())+" ";
                    
                }
                return sections;
            }
        }
        private int InsertIntoPaperDetails(PaperDetailsModal paperDetails)
        {

            int noOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("spInsertPaperDetails", connection);
                command.CommandType = CommandType.StoredProcedure;
               
                command.Parameters.AddWithValue("@SessionId", paperDetails.SessionId);
                command.Parameters.AddWithValue("@SemesterId", paperDetails.SemesterId);
                command.Parameters.AddWithValue("@CourseId", paperDetails.CourseId);
                command.Parameters.AddWithValue("@PaperName", paperDetails.PaperName);
                command.Parameters.AddWithValue("@TeacherId", paperDetails.TeacherId);
                command.Parameters.AddWithValue("@NoOfQuestions", paperDetails.NoOfQuestions);
                command.Parameters.AddWithValue("@TimeAllowed", paperDetails.TimeAllowed);
                command.Parameters.AddWithValue("@Active", paperDetails.Active);
                command.Parameters.AddWithValue("@Password", paperDetails.Password);
                command.Parameters.AddWithValue("@TotalMarks", paperDetails.TotalMarks);
                try
                {
                    noOfRowsAffected = command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 2627)
                    {
                        return 0;
                    }
                }
            }
            return noOfRowsAffected;
        }
        public int InsertQuestionAndChoices(List<PaperQuestionModal> questionList,int paperId)
        {

            int noOfRowsAffected = 0;


            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                 
                foreach (PaperQuestionModal question in questionList)
                {
                    SqlCommand command = new SqlCommand("spInsertQuestion", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Text", question.Text);
                    command.Parameters.AddWithValue("@PaperId", paperId);
                    if (question.QuestionPicture == null)
                    {
                        command.Parameters.AddWithValue("@QuestionPicture", new Byte[] { });
                    }
                    else
                    {

                        command.Parameters.AddWithValue("@QuestionPicture", question.QuestionPicture);
                    }
                    command.Parameters.AddWithValue("@Resources", question.Resources);
                    command.Parameters.AddWithValue("@Marks", question.Marks);

                    try
                    {
                        noOfRowsAffected = command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {


                        if (ex.Number == 2627)
                        {
                            return 0;
                        }


                    }


                }
            }
            //sent the QuestionsList
            InsertChoices(questionList,paperId);
            return noOfRowsAffected;
        }
        private void InsertChoices(List<PaperQuestionModal> questionsTextList,int paperId)
        {
            int noOfRowsAffected = 0;

            String id = "";
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                foreach (PaperQuestionModal question in questionsTextList)
                {
                    SqlCommand command = new SqlCommand("spGetQuestionId", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PaperId", paperId);
                    command.Parameters.AddWithValue("@Text", question.Text);
                    try
                    {
                        SqlDataReader rdr = command.ExecuteReader();
                        while (rdr.Read())
                        {
                            id = rdr["Id"].ToString();
                        }
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627)
                        {

                        }
                    }
                    // For First Option
                    SqlCommand com1 = new SqlCommand("spInsertOption", connection);
                    com1.CommandType = CommandType.StoredProcedure;
                    com1.Parameters.AddWithValue("@Text", question.OptionOne.Text);
                    com1.Parameters.AddWithValue("@Correct", question.OptionOne.Correct);
                    com1.Parameters.AddWithValue("@QuestionId", id);

                    try
                    {
                        int rdr = com1.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627)
                        {

                        }
                    }

                    // For Second Option
                    SqlCommand com2 = new SqlCommand("spInsertOption", connection);
                    com2.CommandType = CommandType.StoredProcedure;
                    com2.Parameters.AddWithValue("@Text", question.OptionTwo.Text);
                    com2.Parameters.AddWithValue("@Correct", question.OptionTwo.Correct);
                    com2.Parameters.AddWithValue("@QuestionId", id);
                    try
                    {
                        int rdr = com2.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627)
                        {

                        }
                    }

                    // For 3rd Option
                    SqlCommand com3 = new SqlCommand("spInsertOption", connection);
                    com3.CommandType = CommandType.StoredProcedure;
                    com3.Parameters.AddWithValue("@Text", question.OptionThree.Text);
                    com3.Parameters.AddWithValue("@Correct", question.OptionThree.Correct);
                    com3.Parameters.AddWithValue("@QuestionId", id);
                    try
                    {
                        int rdr = com3.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627)
                        {

                        }
                    }

                    // For 4th Option
                    SqlCommand com4 = new SqlCommand("spInsertOption", connection);
                    com4.CommandType = CommandType.StoredProcedure;
                    com4.Parameters.AddWithValue("@Text", question.OptionFour.Text);
                    com4.Parameters.AddWithValue("@Correct", question.OptionFour.Correct);
                    com4.Parameters.AddWithValue("@QuestionId", id);

                    try
                    {
                        int rdr = com4.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 2627)
                        {

                        }
                    }
                }


            }//Close Connection

        }
        public List<PaperDetailsModal> List(int sessionId, int semesterId, int courseId)
        {

            List<PaperDetailsModal> listOfSemesters = new List<PaperDetailsModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetPaperDetails", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                com.Parameters.AddWithValue("@SemesterId", semesterId);
                com.Parameters.AddWithValue("@CourseId", courseId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfSemesters.Add(new PaperDetailsModal
                    {
                        Id = int.Parse(rdr["Id"].ToString()),
                        SessionId = int.Parse(rdr["SessionId"].ToString()),
                        SemesterId = int.Parse(rdr["SemesterId"].ToString()),
                      //  Sections = 
                    });
                }
                return listOfSemesters;
            }

        }
        public int UpdatePaper(int paperId,string active ,string password)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spUpdatePaper", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@PaperId", paperId);
                com.Parameters.AddWithValue("@Active", active);
                com.Parameters.AddWithValue("@Password", password);

                try
                {
                    rowsAffected= com.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                  
                }

                return rowsAffected;


            }
        }
        public List<PaperQuestionModal> GetQuestionsForGeneration(int paperId)
        {
            List < PaperQuestionModal>  questionList =  new List<PaperQuestionModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("GetPaperQuestions", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@PaperId", paperId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    questionList.Add(new PaperQuestionModal()
                        {   
                            Id =int.Parse( rdr["Id"].ToString()),
                            Text    = rdr["Text"].ToString(),
                            QuestionImage = string.Format("data:image/png;base64,{0}", Convert.ToBase64String((Byte[])rdr["QuestionPicture"])),    
                            Resources =  rdr["Resources"].ToString(),
                            Marks =   rdr["Marks"].ToString()

                        }
                        );
                    
                }


            }
            return questionList;
        }
        public List<PaperQuestionModal> UpdateListWithOptions(List<PaperQuestionModal> listOfQuestions)
        {
           
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
               
                connection.Open();
                foreach (PaperQuestionModal question in listOfQuestions)
                {
                    List<QuestionMCQ> mcqList = new List<QuestionMCQ>();


                    using (SqlCommand com = new SqlCommand("GetQuestionOptions", connection))
                    {



                        com.CommandType = CommandType.StoredProcedure;
                        com.Parameters.AddWithValue("@QuestionId", question.Id);
                        SqlDataReader rdr = com.ExecuteReader();
                        while (rdr.Read())
                        {
                            mcqList.Add(new QuestionMCQ()
                                {
                                    Id = int.Parse(rdr["Id"].ToString()),
                                    Text = rdr["Text"].ToString(),
                                    Correct = int.Parse(rdr["Correct"].ToString())

                                }
                            );

                        }
                    }
                    //First Option
                    question.OptionOne = mcqList[0];
                    //question.OptionOne.Text = mcqList[0].Text;
                    //question.OptionOne.Correct = mcqList[0].Correct;

                    //Second Option
                    question.OptionTwo = mcqList[1];
                   // question.OptionTwo.Text = mcqList[1].Text;
                    //question.OptionTwo.Correct = mcqList[1].Correct;

                    //Third Option
                    question.OptionThree = mcqList[2];
                   // question.OptionThree.Text = mcqList[2].Text;
                   // question.OptionThree.Correct = mcqList[2].Correct;


                    //Fourth Option
                    question.OptionFour = mcqList[3];
                    //question.OptionFour.Text = mcqList[3].Text;
                    //question.OptionFour.Correct = mcqList[3].Correct;

                }
                return listOfQuestions;
            }
          

        }
    }
}