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
    public class TeacherRepository
    {

        private string _connectionString = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        public int Insert(TeacherModal teacher)
        {
            teacher.EmployeeId = teacher.EmployeeId.Trim();
            teacher.FirstName = teacher.FirstName.Trim();
            teacher.LastName = teacher.LastName.Trim();
            teacher.Designation = teacher.Designation.Trim();
            teacher.Education = teacher.Education.Trim();
            teacher.ContactNumber = teacher.ContactNumber.Trim();
            teacher.PostalAddress = teacher.PostalAddress.Trim();
            teacher.Email = teacher.Email.Trim();
            teacher.Password = teacher.Password.Trim();
            bool isDuplicate = CheckPrimaryKeyViolation(teacher.EmployeeId);

            if (isDuplicate)
            {

                return 0;
            }


            int noOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("spInsertTeacher", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@EmployeeId", teacher.EmployeeId);
                command.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                command.Parameters.AddWithValue("@LastName", teacher.LastName);
                command.Parameters.AddWithValue("@Designation", teacher.Designation);
                command.Parameters.AddWithValue("@Education", teacher.Education);
                command.Parameters.AddWithValue("@ContactNumber", teacher.ContactNumber);
                command.Parameters.AddWithValue("@PostalAddress", teacher.PostalAddress);
                command.Parameters.AddWithValue("@Email", teacher.Email);
                command.Parameters.AddWithValue("@Password", teacher.Password);

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


        private bool CheckPrimaryKeyViolation(string employeeId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spPkViolationTeacher", connection);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    string empId = rdr["EmployeeId"].ToString();

                    if ( employeeId.Equals(empId))
                    {
                        return true;
                    }
                }
                return false;
            }
        }


        public List<TeacherModal> List()
        {
            List<TeacherModal> listOfSubjects = new List<TeacherModal>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand com = new SqlCommand("spListTeacher", connection);
                com.CommandType = CommandType.StoredProcedure;

                SqlDataReader rdr = com.ExecuteReader();
                while (rdr.Read())
                {
                    listOfSubjects.Add(new TeacherModal
                    {


                        Id = Int32.Parse(rdr["Id"].ToString().Trim()),
                        EmployeeId = rdr["EmployeeId"].ToString().Trim(),
                        FirstName = rdr["FirstName"].ToString().Trim(),
                        LastName = rdr["LastName"].ToString().Trim(),
                        Designation = rdr["Designation"].ToString().Trim(),
                        Education = rdr["Education"].ToString().Trim(),
                        ContactNumber = rdr["ContactNumber"].ToString().Trim(),
                        PostalAddress = rdr["PostalAddress"].ToString().Trim(),
                        Email = rdr["Email"].ToString().Trim(),
                        Password = rdr["Password"].ToString().Trim()



                    });
                }
                return listOfSubjects;
            }

        }


        public int Update(TeacherModal teacher)
        {

            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("spUpdateTeacher", connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", teacher.Id);
                command.Parameters.AddWithValue("@EmployeeId", teacher.EmployeeId);
                command.Parameters.AddWithValue("@FirstName", teacher.FirstName);
                command.Parameters.AddWithValue("@LastName", teacher.LastName);
                command.Parameters.AddWithValue("@Designation", teacher.Designation);
                command.Parameters.AddWithValue("@Education", teacher.Education);
                command.Parameters.AddWithValue("@ContactNumber", teacher.ContactNumber);
                command.Parameters.AddWithValue("@PostalAddress", teacher.PostalAddress);
                command.Parameters.AddWithValue("@Email", teacher.Email);
                command.Parameters.AddWithValue("@Password", teacher.Password);


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
        public int Delete(string Id)
        {


            int noOfRowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("spDeleteTeacher", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", Id);


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
    }
}