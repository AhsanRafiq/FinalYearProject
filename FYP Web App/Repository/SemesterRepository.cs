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
    public class SemesterRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

        // List
        public List<SemesterModal> List()
        {
            List<SemesterModal> listOfSemesters = new List<SemesterModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spListSemester", connection);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfSemesters.Add(new SemesterModal
                    {
                        Id = Int32.Parse(rdr["Id"].ToString()),
                        SessionName = rdr["SessionId"].ToString(),
                        SemesterNo = rdr["SemesterNo"].ToString(),
                        SemesterStartDay = rdr["SemesterStartDay"].ToString(),
                        SemesterStartMonth = rdr["SemesterStartMonth"].ToString(),
                        SemesterStartYear = rdr["SemesterStartYear"].ToString(),
                        SemesterEndDay = rdr["SemesterEndDay"].ToString(),
                        SemesterEndMonth = rdr["SemesterEndMonth"].ToString(),
                        SemesterEndYear = rdr["SemesterEndYear"].ToString(),
                        Active = rdr["Active"].ToString()
                    });
                }
                return listOfSemesters;
            }

        } //List Method


        // Insert 
        public int Insert(SemesterModal semester)
        {

            bool isDuplicate = CheckPrimaryKeyViolation(semester.SemesterNo, semester.SessionId);
            bool isAnyOtherIsActive = false;

            if (semester.Active.Equals("Yes"))
            {
                isAnyOtherIsActive = CheckActive(semester.SessionId);
            }

            if (isDuplicate)
            {

                return 0;
            }
            else if (isAnyOtherIsActive)
            {
                return 2;
            }

            int noOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("spInsertSemester", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SessionId", semester.SessionId);
                command.Parameters.AddWithValue("@SemesterNo", semester.SemesterNo);
                command.Parameters.AddWithValue("@SemesterStartDay", semester.SemesterStartDay);
                command.Parameters.AddWithValue("@SemesterStartMonth", semester.SemesterStartMonth);
                command.Parameters.AddWithValue("@SemesterStartYear", semester.SemesterStartYear);
                command.Parameters.AddWithValue("@SemesterEndDay", semester.SemesterEndDay);
                command.Parameters.AddWithValue("@SemesterEndMonth", semester.SemesterEndMonth);
                command.Parameters.AddWithValue("@SemesterEndYear", semester.SemesterEndYear);
                command.Parameters.AddWithValue("@Active", semester.Active);

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

        private bool CheckActive(int sessionId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spGetSemestersForActive", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", sessionId);
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {


                    if (rdr["Active"].Equals("Yes"))
                    {


                        return true;

                    }

                }

                return false;
            }
        }

        //For Checking the PrimaryKeyViolation
        private bool CheckPrimaryKeyViolation(string semesterNo, int sessionId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spCheckPkViolation", connection);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {


                    if (Int32.Parse(rdr["SessionId"].ToString()) == (sessionId) &&
                        rdr["SemesterNo"].ToString().Equals(semesterNo))
                    {


                        return true;

                    }

                }

                return false;
            }

        }



        // Delete 
        public int Delete(string id)
        {


            String semesterId = id;




            int noOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spDeleteSemester", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", semesterId);

                try
                {

                    noOfRowsAffected = command.ExecuteNonQuery();
                }
                catch (SqlException exc)
                {
                    if(exc.Number == 547)
                    {
                        return 547;
                    }
                    
                }

            }

            return noOfRowsAffected;
        }



        // Update
        public int Update(SemesterModal semester)
        {


            bool isAnyOtherIsActive = false;
            if (semester.Active.Equals("Yes"))
            {
                 isAnyOtherIsActive = CheckActive(semester.SessionId);
            }

            if (isAnyOtherIsActive)
            {

                return 2;
            }

            int noOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spUpdateSemester", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", semester.Id);
                command.Parameters.AddWithValue("@SessionId", semester.SessionId);
                command.Parameters.AddWithValue("@SemesterNo", semester.SemesterNo);
                command.Parameters.AddWithValue("@SemesterStartDay", semester.SemesterStartDay);
                command.Parameters.AddWithValue("@SemesterStartMonth", semester.SemesterStartMonth);
                command.Parameters.AddWithValue("@SemesterStartYear", semester.SemesterStartYear);
                command.Parameters.AddWithValue("@SemesterEndDay", semester.SemesterEndDay);
                command.Parameters.AddWithValue("@SemesterEndMonth", semester.SemesterEndMonth);
                command.Parameters.AddWithValue("@SemesterEndYear", semester.SemesterEndYear);
                command.Parameters.AddWithValue("@Active", semester.Active);


                try
                {

                    noOfRowsAffected = command.ExecuteNonQuery();
                }
                catch (SqlException exc)
                {
                    return 0;
                }

            }

            return noOfRowsAffected;


        }

        // Get Sessions
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

    }
}