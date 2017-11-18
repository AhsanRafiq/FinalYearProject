using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FYP_Web_App.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace FYP_Web_App.Repository
{
    public class MidTermRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public bool CheckDuplication(MidTermModal midterm)
        {

            List<MidTermModal> listOfMidTerm = new List<MidTermModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spListMidterm", connection);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfMidTerm.Add(new MidTermModal
                    {
                        Id = Int32.Parse(rdr["Id"].ToString()),
                        SessionId = Int32.Parse(rdr["SessionId"].ToString()),
                        SemesterId = Int32.Parse(rdr["SemesterId"].ToString()),
                        CourseId = Int32.Parse(rdr["CourseId"].ToString()),
                        StartDay = rdr["StartDay"].ToString(),
                        StartMonth = rdr["StartMonth"].ToString(),
                        StartYear= rdr["MaterialDescription"].ToString(),
                        Time = rdr["Time"].ToString()
                    });
                }
            }

            foreach (MidTermModal midtermInDb in listOfMidTerm)
            {

                if ((midtermInDb.SessionId == midterm.SessionId &&
                    midtermInDb.SemesterId == midterm.SemesterId && midtermInDb.CourseId == midterm.CourseId))
                {

                    return true;

                }


            }

            return false;


        }
        public int Insert(MidTermModal midterm)
        {

            InsertMidterm(midterm);
            int midtermId = GetMidtermId(midterm.SessionId,midterm.SemesterId,midterm.CourseId);

            InsertSections(midterm.Sections, midtermId);

            return 1;
        }
        private int GetMidtermId(int sessionId, int semesterId, int courseId)
        {
            int noOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spGetMidtermId", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SessionId", sessionId);
                command.Parameters.AddWithValue("@SemesterId", semesterId);
                command.Parameters.AddWithValue("@CourseId", courseId);

                try
                {
                    SqlDataReader rdr = command.ExecuteReader();
                    while (rdr.Read())
                    {
                        noOfRowsAffected = int.Parse(rdr["Id"].ToString());
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
        public void InsertMidterm(MidTermModal midterm)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spInsertMidterm", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", midterm.SessionId);
                com.Parameters.AddWithValue("@SemesterId", midterm.SemesterId);
                com.Parameters.AddWithValue("@CourseId", midterm.CourseId);
                com.Parameters.AddWithValue("@StartDay", midterm.StartDay);
                com.Parameters.AddWithValue("@StartMonth", midterm.StartMonth);
                com.Parameters.AddWithValue("@StartYear", midterm.StartYear);
                com.Parameters.AddWithValue("@Time", midterm.Time);
                try
                {
                    rowsAffected = com.ExecuteNonQuery();
                }
                catch (Exception exception)
                {
               
                }
         

            }


        }
        private void InsertSections(string[] sectionList, int midtermId)
        {
            foreach (var section in sectionList)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand("spInsertSectionsForMidterm", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MidtermId ", midtermId);
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
        public List<MidTermModal> List(int sessionId, int semesterId)
        {
            List<MidTermModal> listOfMidterms = new List<MidTermModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spListMidterm", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                com.Parameters.AddWithValue("@SemesterId", semesterId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfMidterms.Add(new MidTermModal
                    {
                        Id = Int32.Parse(rdr["Id"].ToString()),
                        SessionName = GetSessionName(Int32.Parse(rdr["SessionId"].ToString())),
                        SemesterName = GetSemesterName(Int32.Parse(rdr["SemesterId"].ToString())),
                        CourseName = GetCourseName(Int32.Parse(rdr["CourseId"].ToString())),
                        StartDay = rdr["StartDay"].ToString(),
                        StartMonth = rdr["StartMonth"].ToString(),
                        StartYear = rdr["StartYear"].ToString(),
                        Time = rdr["Time"].ToString()
                    });
                }
            }
            return listOfMidterms;
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
        public int Update(int id, string startDay, string startMonth, string startYear, string time)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spUpdateMidterm", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", id);
                com.Parameters.AddWithValue("@StartDay", startDay);
                com.Parameters.AddWithValue("@StartMonth", startMonth);
                com.Parameters.AddWithValue("@StartYear", startYear);
                com.Parameters.AddWithValue("@Time", time);
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
        public int Delete(int id)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spDeleteMidterm", connection);
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