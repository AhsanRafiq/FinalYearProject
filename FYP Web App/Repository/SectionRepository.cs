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
    public class SectionRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;


        // List
        public List<SectionModal> List()
        {
            List<SectionModal> listOfSemesters = new List<SectionModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spListSection", connection);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfSemesters.Add(new SectionModal
                    {
                        Id = Int32.Parse(rdr["Id"].ToString()),
                        SessionName = rdr["SessionId"].ToString(),
                        SemesterNo = rdr["SemesterNo"].ToString(),
                        SectionName = rdr["SectionName"].ToString(),
                        Shift = rdr["Shift"].ToString(),
                    });
                }
                return listOfSemesters;
            }

        } //List Method


        // Insert 
        public int Insert(SectionModal section)
        {

            bool isDuplicate = CheckPrimaryKeyViolation(section.SemesterId, section.SessionId,section.SectionName);

            if (isDuplicate)
            {

                return 0;
            }


            int noOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("spInsertSection", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SessionId", section.SessionId);
                command.Parameters.AddWithValue("@SemesterId", section.SemesterId);
                command.Parameters.AddWithValue("@SectionName", section.SectionName);
                command.Parameters.AddWithValue("@Shift", section.Shift);


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

        public object Update(SectionModal section)
        {



            int noOfRowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spUpdateSection", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", section.Id);
                command.Parameters.AddWithValue("@SessionId", section.SessionId);
                command.Parameters.AddWithValue("@SemesterId", section.SemesterId);
                command.Parameters.AddWithValue("@SectionName", section.SectionName);
                command.Parameters.AddWithValue("@Shift", section.Shift);



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






        private bool CheckPrimaryKeyViolation(int semesterId, int sessionId,string sectionName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spPkViolationSection", connection);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    int session = int.Parse((rdr["SessionId"].ToString()));
                    int semester = int.Parse((rdr["SemesterId"].ToString()));
                    string section = rdr["SectionName"].ToString().Trim();

                     if (semester == semesterId && session == sessionId && sectionName.Equals(section))
                    {
                        return true;
                    }


                }
                return false; 
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
                        SemesterNo = rdr["SemesterNo"].ToString()
                    });
                }
                return listOfSemesters;
            }







        }



        public int Delete(int sectionId)
        {

            int noOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spDeleteSection", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", sectionId);

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