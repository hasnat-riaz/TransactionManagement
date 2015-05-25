using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DBAccessFromApplication.DAL.DAO;
using System.Data;

namespace DBAccessFromApplication.DAL.Gateway
{
    public class TraineeGateway
    {
        string connectionString = ConfigurationSettings.AppSettings["connString"];
        private SqlConnection sqlConnectionObj = null;
        private SqlCommand sqlCommand = null;

        public TraineeGateway()
        {
            sqlConnectionObj = new SqlConnection(connectionString);  
            sqlCommand = new SqlCommand();
        }

        public string SaveTraineeWithTakenCourses(Trainee traineeObj, List<Course> selectedCourseList)
        {
            sqlConnectionObj.Open();
            sqlCommand.Connection = sqlConnectionObj;
            SqlTransaction sqlTx = sqlConnectionObj.BeginTransaction();
            sqlCommand.Transaction = sqlTx;
            string traineeSavedMsg = null;
            string messageForUser = null;
            try
            {
                traineeSavedMsg = SaveTrainee(traineeObj);
                messageForUser = SaveTakenCourses(traineeObj, selectedCourseList);
                sqlTx.Commit();
            }
            catch (Exception exObj)
            {
                sqlTx.Rollback();
                throw new Exception("Data is not saved", exObj);
            }
            finally
            {
                if (sqlConnectionObj != null && sqlConnectionObj.State == ConnectionState.Open)
                {
                    sqlConnectionObj.Close();
                }
            }
            return traineeSavedMsg + "\n" + messageForUser;
        }


        private string SaveTrainee(Trainee traineeObj)
        {
            int affectedRows;
            string messageForUser;
            string commandStr = "INSERT INTO Trainee VALUES ('" + traineeObj.Id + "','" + traineeObj.Name + "')";
            sqlCommand.CommandText = commandStr;
            affectedRows = sqlCommand.ExecuteNonQuery();
            if (affectedRows > 0)
            {
                messageForUser = "Trainee updated.";
            }
            else
            {
                messageForUser = "Not saved.";
            }
            return messageForUser;
        }


        private string SaveTakenCourses(Trainee traineeObj, List<Course> selectedCourseList)
        {
            string commandStr = "";
            int affectedRows = 0;
            string messageForUser;
                foreach (Course course in selectedCourseList)
                {
                    commandStr = "INSERT INT courseCompletedByTrainee VALUES ('" + traineeObj.Id + "'," + course.Id + ")";

                    sqlCommand.CommandText = commandStr;
                    affectedRows += sqlCommand.ExecuteNonQuery();
                }

                if (affectedRows > 0)
                {
                    messageForUser = affectedRows + " course(s) has been taken by trainee.";
                }
                else
                {
                    messageForUser = "Not saved.";
                }
            return messageForUser;
        }
    }
}
