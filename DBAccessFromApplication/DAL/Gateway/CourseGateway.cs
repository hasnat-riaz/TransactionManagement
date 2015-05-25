using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DBAccessFromApplication.DAL.DAO;

namespace DBAccessFromApplication.DAL.Gateway
{
    public class CourseGateway
    {
        public List<Course> GetCourseList()
        {
            List<Course> courses = new List<Course>();
            int affectedRows;
            string messageForUser;
            string connectionString = ConfigurationSettings.AppSettings["connString"];
            try
            {
                SqlConnection sqlConnectionObj = new SqlConnection(connectionString);
                string commandStr = "SELECT * FROM Course";
                sqlConnectionObj.Open();
                SqlCommand sqlCommand = new SqlCommand(commandStr, sqlConnectionObj);
                SqlDataReader myDataReader = sqlCommand.ExecuteReader();
                Course course = null;

                while(myDataReader.Read())
                {
                    course = new Course(Convert.ToInt32(myDataReader[0]), myDataReader[1].ToString());
                    courses.Add(course);
                }
            }
            catch (SqlException sqlException)
            {
                throw sqlException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return courses;
        }
    }
}
