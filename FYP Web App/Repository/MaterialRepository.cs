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
    public class MaterialRepository
    {
        private string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int Insert(MaterialModal material)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spInsertMaterial", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@SessionId", material.SessionId);
                com.Parameters.AddWithValue("@SemesterId", material.SemesterId);
                com.Parameters.AddWithValue("@CourseId", material.CourseId);
                com.Parameters.AddWithValue("@TeacherId", material.TeacherId);
                com.Parameters.AddWithValue("@MaterialName", material.MaterialName);
                com.Parameters.AddWithValue("@MaterialDescription", material.MaterialDescription);
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
        public List<MaterialModal> List(int sessionId, int semesterId, int courseId)
        {
            List<MaterialModal> listOfMaterials = new List<MaterialModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spListMaterial", connection);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfMaterials.Add(new MaterialModal
                    {
                        Id = Int32.Parse(rdr["Id"].ToString()),
                        SessionName = GetSessionName(Int32.Parse(rdr["SessionId"].ToString())),
                        SemesterName =GetSemesterName( Int32.Parse(rdr["SemesterId"].ToString())),
                        CourseName = GetCourseName(Int32.Parse(rdr["CourseId"].ToString())),
                        TeacherName = GetTeacherName(Int32.Parse(rdr["TeacherId"].ToString())),
                        MaterialName = rdr["MaterialName"].ToString(),
                        MaterialDescription = rdr["MaterialDescription"].ToString()
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
                return teacherName = firstName +"-"+ lastName;
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
                SqlCommand com = new SqlCommand("spDeleteMaterial", connection);
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
        public bool CheckDuplication(MaterialModal material)
        {

            List<MaterialModal> listOfMaterials = new List<MaterialModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spListMaterial", connection);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfMaterials.Add(new MaterialModal
                    {
                        Id = Int32.Parse(rdr["Id"].ToString()),
                        SessionId = Int32.Parse(rdr["SessionId"].ToString()),
                        SemesterId = Int32.Parse(rdr["SemesterId"].ToString()),
                        CourseId = Int32.Parse(rdr["CourseId"].ToString()),
                        TeacherId = Int32.Parse(rdr["TeacherId"].ToString()),
                        MaterialName = rdr["MaterialName"].ToString(),
                        MaterialDescription =rdr["MaterialDescription"].ToString()
                    });
                }
            }

            foreach(MaterialModal materialOfSubject in listOfMaterials)
            {

                if( (materialOfSubject.SessionId == material.SessionId &&
                    materialOfSubject.SemesterId == material.SemesterId && materialOfSubject.CourseId == material.CourseId &&
                    materialOfSubject.TeacherId == material.TeacherId && materialOfSubject.MaterialName.Equals(material.MaterialName))&&
                    materialOfSubject.MaterialDescription.Equals(material.MaterialDescription))
                {

                    return true;

                }


            }

            return false;


        }
        public int Update(int id, string materialName, string materialDescription)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spUpdateMaterial", connection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@Id", id);
                com.Parameters.AddWithValue("@MaterialName", materialName);
                com.Parameters.AddWithValue("@MaterialDescription", materialDescription);
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