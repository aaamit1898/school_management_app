using school_management_app.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mail;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace school_management_app.Service
{
    public class StudentDat
    {

        public List<StudentRagistrationModel> GetAllDetails()
        {
            string connection = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connection);

            SqlCommand cmd = new SqlCommand("GetAllDetails", sqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<StudentRagistrationModel> st = new List<StudentRagistrationModel>();
            while (reader.Read())
            {
                st.Add(new StudentRagistrationModel()
                {
                    id = Convert.ToInt32(reader["StudentId"]),
                    name = reader["StudentName"].ToString(),
                    email = reader["StudentEmail"].ToString(),
                    phone = reader["StudentPhoneNumber"].ToString(),
                    gender = reader["Gender"].ToString(),
                    dob = reader["DateOfBirth"].ToString()


                });

            }

            return st;

        }




        public bool CheckLogin(AdminLoginModel adminLoginModel)
        {
            bool result = false;
            string connection = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand("GetIdPassword", sqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlConnection.Open();
            cmd.Parameters.AddWithValue("@username", adminLoginModel.Name);
            cmd.Parameters.AddWithValue("@password", adminLoginModel.Password);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                result = true;
            }

            return result;
        }

        public bool UserRagistration(StudentRagistrationModel studentRagistrationModel)
        {
            bool result = false;
            string connection = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand("UspRagistration", sqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            sqlConnection.Open();
            cmd.Parameters.AddWithValue("@name", studentRagistrationModel.name);
            cmd.Parameters.AddWithValue("@email", studentRagistrationModel.email);
            cmd.Parameters.AddWithValue("@phone", studentRagistrationModel.phone);
            cmd.Parameters.AddWithValue("@gender", studentRagistrationModel.gender);
            cmd.Parameters.AddWithValue("@dob", studentRagistrationModel.dob);

            int result1 = cmd.ExecuteNonQuery();
            if (result1 < 0)
            {
                result = true;
            }

            return result;
        }
        public StudentRagistrationModel Deletestudent(int id)
        {
            StudentRagistrationModel student = new StudentRagistrationModel();
            string connection = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                string query = "select * from StudentRagistrationForm where StudentId = @id";

                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    sqlConnection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {


                            student.name = reader["StudentName"].ToString();
                            student.email = reader["StudentEmail"].ToString();
                            student.phone = reader["StudentPhoneNumber"].ToString();
                            student.gender = reader["Gender"].ToString();
                            student.dob = reader["DateOfBirth"].ToString();



                        }

                    }

                }
                catch
                {

                }




            }
            return student;
        }

        public StudentRagistrationModel EditGetByid(int id)
        {
            string connection = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            SqlConnection sqlConnection = new SqlConnection(connection);

            //string query = "select * from StudentRagistrationForm where StudentId = @id";

            SqlCommand cmd = new SqlCommand("GetDetailsById", sqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            sqlConnection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            StudentRagistrationModel st = new StudentRagistrationModel();

            if (reader.Read())
            {


                st.name = reader["StudentName"].ToString();
                st.email = reader["StudentEmail"].ToString();
                st.phone = reader["StudentPhoneNumber"].ToString();
                st.gender = reader["Gender"].ToString();
                st.dob = reader["DateOfBirth"].ToString();


            }

            return st;

        }
        public bool UpadteRow(StudentRagistrationModel studentRagistrationModel)
        {
            bool result = false;
            string connection = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;

            SqlConnection sqlConnection = new SqlConnection(connection);


            SqlCommand cmd = new SqlCommand("Usp_UpadateStudentRagistration", sqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", studentRagistrationModel.id);
            cmd.Parameters.AddWithValue("@name", studentRagistrationModel.name);
            cmd.Parameters.AddWithValue("@email", studentRagistrationModel.email);
            cmd.Parameters.AddWithValue("@phone", studentRagistrationModel.phone);
            cmd.Parameters.AddWithValue("@gender", studentRagistrationModel.gender);
            cmd.Parameters.AddWithValue("@dob", studentRagistrationModel.dob);
            sqlConnection.Open();
            int result1 = cmd.ExecuteNonQuery();
            if (result1 < 0)
            {
                result = true;
            }
            return result;
        }
        public bool DeleteByiD(int id)
        {
            bool result = false;
            string connection = ConfigurationManager.ConnectionStrings["sqlconnection"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(connection);



            SqlCommand cmd = new SqlCommand("Usp_DeleteById", sqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);

            sqlConnection.Open();
            int result1 = cmd.ExecuteNonQuery();
            if (result1 > 0)
            {
                result = true;
            }
            return result;
        }
    }
}
