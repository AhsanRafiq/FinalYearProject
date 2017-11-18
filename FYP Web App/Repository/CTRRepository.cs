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
    public class CTRRepository
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

        internal object GetTeacher()
        {
            List<TeacherModal> listOfSemesters = new List<TeacherModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetTeacher", connection);
                com.CommandType = CommandType.StoredProcedure;

                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfSemesters.Add(new TeacherModal
                    {
                        Id = int.Parse(rdr["Id"].ToString()),
                        EmployeeId = rdr["EmployeeId"].ToString(),
                        FirstName = rdr["FirstName"].ToString()
                    });
                }
                return listOfSemesters;
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

        public int Insert(CTRModal ctr)
        {


            bool isDuplicate = CheckPrimaryKeyViolation( ctr.SessionId,ctr.SemesterId ,ctr.SectionId, ctr.CourseId ,ctr.TeacherId);

            if (isDuplicate)
            {

                return 0;
            }


            int noOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("spInsertCTR", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SessionId", ctr.SessionId);
                command.Parameters.AddWithValue("@SemesterId", ctr.SemesterId);
                command.Parameters.AddWithValue("@SectionId", ctr.SectionId);
                command.Parameters.AddWithValue("@CourseId", ctr.CourseId);
                command.Parameters.AddWithValue("@TeacherId", ctr.TeacherId);

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

        private bool CheckPrimaryKeyViolation(int sessionId, int semesterId, int sectionId, int courseId, int teacherId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spPkViolationCTR", connection);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    int session = int.Parse((rdr["SessionId"].ToString()));
                    int semester = int.Parse((rdr["SemesterId"].ToString()));
                    int section = int.Parse(rdr["SectionId"].ToString());
                    int course = int.Parse(rdr["CourseId"].ToString());
                    int teacher = int.Parse(rdr["TeacherId"].ToString());


                    if (semester == semesterId && session == sessionId && section == sectionId && course==courseId && teacher== teacherId)
                    {
                        return true;
                    }


                }
                return false;
            }


        }
        public List<CTRModal> List(int sessionId, int semesterId, int sectionId)
        {
            List<CTRModal> listOfCTR = new List<CTRModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spListCTR", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                com.Parameters.AddWithValue("@SemesterId", semesterId);
                com.Parameters.AddWithValue("@SectionId", sectionId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfCTR.Add(new CTRModal
                    {


                        Id = Int32.Parse(rdr["Id"].ToString()),
                       CourseName= rdr["CourseName"].ToString(),
                        EmployeeId= rdr["EmployeeId"].ToString(),
                        TeacherName = (rdr["FirstName"].ToString())
                    
                    });
                }
                return listOfCTR;
            }

        } //List Method
        public int Update(CTRModal ctr)
        {

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("spUpdateCTR", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", ctr.Id);
                command.Parameters.AddWithValue("@SessionId", ctr.SessionId);
                command.Parameters.AddWithValue("@SemesterId", ctr.SemesterId);
                command.Parameters.AddWithValue("@SectionId", ctr.SectionId);
                command.Parameters.AddWithValue("@CourseId", ctr.CourseId);
                command.Parameters.AddWithValue("@TeacherId", ctr.TeacherId);

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

        public int Delete(string ctrId)
        {


            int numberOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spDeleteCtr", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", ctrId);

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
    }
}