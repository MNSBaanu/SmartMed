using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WindowsFormsApp1
{
    class student
    {
        public int id { get; set; }

        public string name { get; set; }
        public int marks { get; set; }

        private static string connectionString = "Data Source=LAB05-PC;Initial Catalog=studentdb;Integrated Security=True";
        public void addStudent()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO student (id, name, marks) VALUES (@id, @name, @marks )";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@marks", marks);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception err)
                {
                }
           
        }
        public void addStudent1(int id, string name, int marks)
        {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO student (id, name, marks) VALUES ('" + id + "','" + name + "','" + marks + "' )";
                    SqlCommand cmd = new SqlCommand(query, con);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

        }
        public void updateStudent(int id, string name, int marks)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE student set name = '"+ name +"', marks = '"+ marks +"', id = '"+ id +"'";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
            }

        }

        public student searchbyID(int id)
        {
            student student = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM student where id = '" + id + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    student = new student
                    {
                      name = reader["name"].ToString(),
                      marks = Convert.ToInt32(reader["marks"]),
                    };
                }
            return student;
            }
        }

        public void delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM student WHERE id =" + id;
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
            }
        }
        }

    }
