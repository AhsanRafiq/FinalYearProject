using FYP_Web_App.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FYP_Web_App.Repository
{
    public class StudentRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
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
                        SemesterNo = rdr["SemesterNo"].ToString(),
                        Id = int.Parse(rdr["Id"].ToString())
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
        public string GetSessionName(int sessionId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string courseName = null;
                SqlCommand com = new SqlCommand("spGetSessionName", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", sessionId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {


                    courseName = rdr["SessionId"].ToString();

                }
                return courseName;

            }
        }
        public int Insert(StudentModal student)
        {
            student.RollNumber = student.RollNumber.Trim();
            student.Name = student.Name.Trim();
            student.FatherName = student.FatherName.Trim();
            student.Password = student.Password.Trim();

            bool isDuplicate = CheckPrimaryKeyViolation(student.SemesterId, student.SessionId, student.SectionId, student.RollNumber);

            if (isDuplicate)
            {

                return 0;
            }


            int noOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("spInsertStudent", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SessionId", student.SessionId);
                command.Parameters.AddWithValue("@SemesterId", student.SemesterId);
                command.Parameters.AddWithValue("@SectionId", student.SectionId);
                command.Parameters.AddWithValue("@RollNumber", student.RollNumber);
                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@FatherName", student.FatherName);
                command.Parameters.AddWithValue("@Password", student.Password);
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

        } // Insert Method
        private bool CheckPrimaryKeyViolation(int semesterId, int sessionId, int sectionId, string rollNumber)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spPkViolationStudent", connection);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    int session = int.Parse((rdr["SessionId"].ToString()));
                    int semester = int.Parse((rdr["SemesterId"].ToString()));
                    int section = int.Parse(rdr["SectionId"].ToString());
                    string roll = rdr["RollNumber"].ToString();

                    if (semester == semesterId && session == sessionId && section == sectionId && rollNumber.Equals(roll))
                    {
                        return true;
                    }


                }
                return false;
            }
        }
        public List<StudentModal> List(int sessionId, int semesterId, int sectionId)
        {
            List<StudentModal> listOfSubjects = new List<StudentModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spListStudent", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                com.Parameters.AddWithValue("@SemesterId", semesterId);
                com.Parameters.AddWithValue("@SectionId", sectionId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfSubjects.Add(new StudentModal
                    {


                        Id = Int32.Parse(rdr["Id"].ToString()),
                        SessionId = Int32.Parse(rdr["SessionId"].ToString()),
                        SemesterId = Int32.Parse(rdr["SemesterId"].ToString()),
                        SectionId = Int32.Parse(rdr["SectionId"].ToString()),
                        RollNumber = rdr["RollNumber"].ToString(),
                        Name = rdr["Name"].ToString(),
                        FatherName = (rdr["FatherName"].ToString()),
                        Password = (rdr["Password"].ToString()),

                    });
                }
                return listOfSubjects;
            }

        } //List Method
        public int Update(StudentModal student)
        {

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("spUpdateStudent", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", student.Id);
                command.Parameters.AddWithValue("@SessionId", student.SessionId);
                command.Parameters.AddWithValue("@SemesterId", student.SemesterId);
                command.Parameters.AddWithValue("@SectionId", student.SectionId);
                command.Parameters.AddWithValue("@RollNumber", student.RollNumber);
                command.Parameters.AddWithValue("@Name", student.Name);
                command.Parameters.AddWithValue("@FatherName", student.FatherName);
                command.Parameters.AddWithValue("@Password", student.Password);


                try
                {
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception e)
                {


                    return 0;
                }

                return rowsAffected;

            }
        }
        public int Delete(string studentId)
        {


            int numberOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spDeleteStudent", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", studentId);

                try
                {
                    numberOfRowsAffected = com.ExecuteNonQuery();
                }
                catch (Exception e)
                {

                }

                return numberOfRowsAffected;


            }
        }
        ////---------------Student Exam Related tasks------------------//////
        public List<PaperDetailsModal> GetPapers(int sessionId, int semesterId)
        {
            List<PaperDetailsModal> paper = new List<PaperDetailsModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spDisplayPaperForExam", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                com.Parameters.AddWithValue("@SemesterId", semesterId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    paper.Add(new PaperDetailsModal
                    {
                        Id = int.Parse(rdr["Id"].ToString()),
                        CourseName = GetCourseName(rdr["CourseId"].ToString().Trim()),
                        PaperName = rdr["PaperName"].ToString().Trim(),
                        NoOfQuestions = rdr["NoOfQuestions"].ToString().Trim(),
                        TimeAllowed = rdr["TimeAllowed"].ToString().Trim(),
                        TotalMarks = int.Parse(rdr["TotalMarks"].ToString().Trim()),
                    });
                }
                return paper;
            }
        }
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
        public bool MatchPassword(int id, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
             
                SqlCommand com = new SqlCommand("spMtachPassword", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@PaperId", id);
                com.Parameters.AddWithValue("@Password", password);
                bool isMatched =(bool) com.ExecuteScalar();
                return isMatched;
            }
           

            }
        public List<PaperQuestionModal> GetQuestions(int paperId)
        {
            List<PaperQuestionModal> questionList = new List<PaperQuestionModal>();
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
                            Id = int.Parse(rdr["Id"].ToString()),
                            Text = rdr["Text"].ToString(),
                            QuestionPicture = (Byte[])rdr["QuestionPicture"],
                            QuestionImage = string.Format("data:image/png;base64,{0}", Convert.ToBase64String((Byte[])rdr["QuestionPicture"])),
                            Resources = rdr["Resources"].ToString(),
                            Marks = rdr["Marks"].ToString()

                        }
                    );

                }


            }
            return questionList;
        }


        public List<PaperDetailsModal> GetPaperNames(int sessionId, int semesterId, int courseId)
        {
            List<PaperDetailsModal> listOfPapersNames = new List<PaperDetailsModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetPaperNamesForStudent", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                com.Parameters.AddWithValue("@SemesterId", semesterId);
                com.Parameters.AddWithValue("@CourseId", courseId);

                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfPapersNames.Add(new PaperDetailsModal
                    {
                        Id = int.Parse(rdr["Id"].ToString()),
                        PaperName = rdr["PaperName"].ToString()
                    });
                }
                return listOfPapersNames;
            }
        }

        public List<NotificationModal> GetNotification(int sessionId, int semesterId)
        {
            List<NotificationModal> listOfNotification = new List<NotificationModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetNotification", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                com.Parameters.AddWithValue("@SemesterId", semesterId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfNotification.Add(new NotificationModal
                    {

                        CourseName = GetCourseName(rdr["CourseId"].ToString()),
                        TeacherName = GetTeacherName(rdr["TeacherId"].ToString()),
                        NotificationName = rdr["NotificationName"].ToString(),
                        NotificationDescription = rdr["NotificationDescription"].ToString(),
                    });
                }
                return listOfNotification;
            }
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
        public List<PaperQuestionModal> UpdateListWithOptionsCharacteristics(List<PaperQuestionModal> listOfQuestions)
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
                                Correct =int.Parse( rdr["Correct"].ToString())
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
        public int ChangePassword(string oldPassword, string newPassword)
        {
            int matched = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spMatchPasswordStudent", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@OldPassword", oldPassword);
                try
                {
                    matched = (int)com.ExecuteScalar();
                }
                catch (Exception exception)
                {
                }
            }
            if (matched == 0)
            {
                return 0;
            }
            int added = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spSetPasswordStudent", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@NewPassword", newPassword);
                try
                {
                    added = com.ExecuteNonQuery();
                }
                catch (Exception exception)
                {


                }
            }
            return added;
        }
        public PaperResultModal GetResult(int sessionId, int semesterId, int courseId, int paperId, string rollNumber)
        {

            string studentId = GetStudentId(rollNumber);

            PaperResultModal paperResultModal = new PaperResultModal();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand com = new SqlCommand("spGetResultByRollNumber", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                com.Parameters.AddWithValue("@SemesterId", semesterId);
                com.Parameters.AddWithValue("@CourseId", courseId);
                com.Parameters.AddWithValue("@PaperId", paperId);
                com.Parameters.AddWithValue("@StudentId", studentId);

               
                try
                {
                    SqlDataReader rdr = com.ExecuteReader();

                    while (rdr.Read())
                    {
                        paperResultModal.MarksObtained = int.Parse(rdr["MarksObtained"].ToString());
                        paperResultModal.TotalMarks = int.Parse(rdr["TotalMarks"].ToString());
                    }
                }
                catch (Exception e)
                {

                }
                return paperResultModal;
            }
        }
        private string GetStudentId(string rollNumber)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spStudentRollNo", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@RollNumber", rollNumber);
                string stId =(string) com.ExecuteScalar();
                return stId;
            }
        }
        public List<MidTermModal> GetMidterm(int sessionId, int semesterId)
        {

            List<MidTermModal> listOfMidterms = new List<MidTermModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetMidterm", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                com.Parameters.AddWithValue("@SemesterId", semesterId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfMidterms.Add(new MidTermModal
                    {
                      
                        CourseName = GetCourseName(rdr["CourseId"].ToString()),
                        StartDay = rdr["StartDay"].ToString(),
                        StartMonth = rdr["StartMonth"].ToString(),
                        StartYear =  rdr["StartYear"].ToString(),
                        Time = rdr["Time"].ToString()
                    });
                }
                return listOfMidterms;
            }

        }
        private string GetCourseName(int id)
        {
            string courseName = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetCourseNameOnly", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", id);
                try
                {
                    courseName = (string)com.ExecuteScalar();
                }
                catch (Exception e)
                {
                    return null;
                }
                return courseName;
            }
        }


        public List<MaterialModal>  Folder(int sessionId, int semesterId)
        {

            List<MaterialModal> listOfMaterial = new List<MaterialModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetMaterial", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                com.Parameters.AddWithValue("@SemesterId", semesterId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfMaterial.Add(new MaterialModal
                    {

                        CourseName = GetCourseName(rdr["CourseId"].ToString()),
                        TeacherName = GetTeacherName(rdr["TeacherId"].ToString()),
                        MaterialName = rdr["MaterialName"].ToString(),
                        MaterialDescription = rdr["MaterialDescription"].ToString(),
                    });
                }
                return listOfMaterial;
            }
        }







        public string GetTeacherName(string id)
        {
            string teacherName = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetTeacherNameOnly", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", id);
                try
                {
                    teacherName = (string)com.ExecuteScalar();
                }
                catch (Exception e)
                {
                    return null;
                }
                return teacherName;
            }


        }

    }
}
