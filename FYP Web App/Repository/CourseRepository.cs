using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FYP_Web_App.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace FYP_Web_App.Repository
{
    public class CourseRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;


        public List<CourseModal> List(int sessionId, int semesterId)
        {
            List<CourseModal> listOfSubjects = new List<CourseModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spListCourse", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                com.Parameters.AddWithValue("@SemesterId", semesterId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfSubjects.Add(new CourseModal
                    {


                        Id = Int32.Parse(rdr["Id"].ToString()),
                        CourseCode = rdr["CourseCode"].ToString(),
                        CourseName = rdr["CourseName"].ToString(),
                        CreditHour = Int32.Parse(rdr["CreditHour"].ToString())


                    });
                }
                return listOfSubjects;
            }

        } //List Method

        public int Insert(CourseModal coursetOject)
        {
            //So that no white space
            coursetOject.CourseCode = coursetOject.CourseCode.Trim();
            coursetOject.CourseName = coursetOject.CourseName.Trim();


            bool isDuplicate = CheckPrimaryKeyViolation(coursetOject.SessionId, coursetOject.SemesterId, coursetOject.CourseName);

            if (isDuplicate)
            {

                return 0;
            }


            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand com = new SqlCommand("spInsertCourse", connection);

                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", coursetOject.SessionId);
                com.Parameters.AddWithValue("@Semesterid", coursetOject.SemesterId);
                com.Parameters.AddWithValue("@CourseCode", coursetOject.CourseCode);
                com.Parameters.AddWithValue("@CourseName", coursetOject.CourseName);
                com.Parameters.AddWithValue("@CreditHour", coursetOject.CreditHour);


                try
                {
                    rowsAffected = com.ExecuteNonQuery();
                }
                catch (Exception e)
                {


                    return 0;
                }

                return rowsAffected;

            }
        }



        public int Update(CourseModal coursetOject)
        {

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand com = new SqlCommand("spUpdateCourse", connection);

                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", coursetOject.Id);
                com.Parameters.AddWithValue("@SessionId", coursetOject.SessionId);
                com.Parameters.AddWithValue("@Semesterid", coursetOject.SemesterId);
                com.Parameters.AddWithValue("@CourseCode", coursetOject.CourseCode);
                com.Parameters.AddWithValue("@CourseName", coursetOject.CourseName);
                com.Parameters.AddWithValue("@CreditHour", coursetOject.CreditHour);


                try
                {
                    rowsAffected = com.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    if (ex.Number == 547)
                    {
                        return 547;
                    }

                    return 0;
                }

                return rowsAffected;

            }
        }

        public int Delete(string courseId)
        {


            int numberOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spDeleteCourse", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", courseId);

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

        public int GetSemesterId(int semesterNo)
        {
            int semesterId = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetSemesterId", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SemesterNo", semesterNo);

                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    semesterId = Int32.Parse(rdr["Id"].ToString());

                }
                return semesterId;
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
                        Id = int.Parse(rdr["Id"].ToString()),
                        SemesterNo = rdr["SemesterNo"].ToString()
                    });
                }
                return listOfSemesters;
            }
        }
        private bool CheckPrimaryKeyViolation(int sessionId, int semesterId, string courseName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spPkViolationCourse", connection);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    int session = int.Parse((rdr["SessionId"].ToString()));
                    int semester = int.Parse((rdr["SemesterId"].ToString()));
                    string course = rdr["CourseName"].ToString().Trim();

                    if (semester == semesterId && session == sessionId && courseName.Equals(course))
                    {
                        return true;
                    }


                }
                return false;
            }
        }

    }
}