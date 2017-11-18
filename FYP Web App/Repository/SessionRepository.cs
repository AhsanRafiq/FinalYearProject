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
    public class SessionRepository
    {


        private string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;


        // Get data from Session Table
        public List<SessionModal> List()
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




        }//List Method


        //Insert

        public int Insert(SessionModal session)
        {
            if (CheckPrimaryKeyViolation(session.SessionId))
            {

                return 0;
            }





            int noOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("spInsertSession", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SessionId", session.SessionId);
                command.Parameters.AddWithValue("@Program", session.SessionProgram);
                command.Parameters.AddWithValue("@SessionStartDay", session.SessionStartDay);
                command.Parameters.AddWithValue("@SessionStartMonth", session.SessionStartMonth);
                command.Parameters.AddWithValue("@SessionStartYear", session.SessionStartYear);
                command.Parameters.AddWithValue("@SessionEndDay", session.SessionEndDay);
                command.Parameters.AddWithValue("@SessionEndMonth", session.SessionEndMonth);
                command.Parameters.AddWithValue("@SessionEndYear", session.SessionEndYear);

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

        private bool CheckPrimaryKeyViolation(string sessionId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spPkViolationSession", connection);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                   
                    if (sessionId.Equals((rdr["SessionId"].ToString().Trim())))
                    {
                        return true;
                    }


                }
                return false;
            }
        }
        public int Delete(string Id)
        {


            int noOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spDeleteSession", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", Id);


                try
                {

                    noOfRowsAffected = command.ExecuteNonQuery();
                }
                catch (SqlException exc)
                {
                    if (exc.Number == 547)
                    {
                        return 547;

                    }
                   
                }

            }

            return noOfRowsAffected;
        }




        public int Update(SessionModal session)
        {

            int noOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spUpdateSession", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", session.Id);
                command.Parameters.AddWithValue("@SessionId", session.SessionId);
                command.Parameters.AddWithValue("@Program", session.SessionProgram);
                command.Parameters.AddWithValue("@SessionStartDay", session.SessionStartDay);
                command.Parameters.AddWithValue("@SessionStartMonth", session.SessionStartMonth);
                command.Parameters.AddWithValue("@SessionStartYear", session.SessionStartYear);
                command.Parameters.AddWithValue("@SessionEndDay", session.SessionEndDay);
                command.Parameters.AddWithValue("@SessionEndMonth", session.SessionEndMonth);
                command.Parameters.AddWithValue("@SessionEndYear", session.SessionEndYear);


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

    }//Class
}