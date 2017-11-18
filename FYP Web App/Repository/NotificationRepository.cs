using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FYP_Web_App.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace FYP_Web_App.Repository
{
    public class NotificationRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int Insert(NotificationModal notification)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spInsertNotification", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", notification.SessionId);
                com.Parameters.AddWithValue("@SemesterId", notification.SemesterId);
                com.Parameters.AddWithValue("@CourseId", notification.CourseId);
                com.Parameters.AddWithValue("@TeacherId", notification.TeacherId);
                com.Parameters.AddWithValue("@NotificationName", notification.NotificationName);
                com.Parameters.AddWithValue("@NotificationDescription", notification.NotificationDescription);
                try
                {
                    rowsAffected = com.ExecuteNonQuery();
                }
                catch (Exception excp)
                {
                    return 0;
                }
                return rowsAffected;

            }
        }
        public int Update(int id, string notificationName, string notificationDescription)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spUpdateNotification", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", id);
                com.Parameters.AddWithValue("@NotificationName", notificationName);
                com.Parameters.AddWithValue("@NotificationDescription", notificationDescription);
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
        public List<NotificationModal> List(int sessionId, int semesterId, int courseId)
        {
            List<NotificationModal> listOfMaterials = new List<NotificationModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spListNotification", connection);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfMaterials.Add(new NotificationModal
                    {
                        Id = Int32.Parse(rdr["Id"].ToString()),
                        SessionName = GetSessionName(Int32.Parse(rdr["SessionId"].ToString())),
                        SemesterName = GetSemesterName(Int32.Parse(rdr["SemesterId"].ToString())),
                        CourseName = GetCourseName(Int32.Parse(rdr["CourseId"].ToString())),
                        TeacherName = GetTeacherName(Int32.Parse(rdr["TeacherId"].ToString())),
                        NotificationName = rdr["NotificationName"].ToString(),
                        NotificationDescription = rdr["NotificationDescription"].ToString()
                    });
                }
            }
            return listOfMaterials;
        }
        private string GetTeacherName(int id)
        {
            string firstName = null;
            string lastName = null;
            string teacherName = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetTeacherNameOnly", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", id);
                SqlDataReader rdr = com.ExecuteReader();

                while (rdr.Read())
                {
                    firstName = rdr["FirstName"].ToString();
                    lastName = rdr["LastName"].ToString();



                }
                return teacherName = firstName + "-" + lastName;
            }
        }
        private string GetSemesterName(int id)
        {
            string semesterName = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetSemesterNameOnly", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", id);
                try
                {
                    semesterName = (string)com.ExecuteScalar();
                }
                catch (Exception e)
                {
                    return null;
                }
                return semesterName;
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
        private string GetSessionName(int id)
        {
            string sessionName = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetSessionNameOnly", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", id);
                try
                {
                    sessionName = (string)com.ExecuteScalar();
                }
                catch (Exception e)
                {
                    return null;
                }
                return sessionName;
            }
        }
        public int Delete(int id)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spDeleteNotification", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", id);

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
    }
}