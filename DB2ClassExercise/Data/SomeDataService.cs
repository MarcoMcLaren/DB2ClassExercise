using System;
using System.Collections.Generic;
using DB2ClassExercise.Models;
using System.Data.SqlClient;
using DB2ClassExercise.Data;
using System.Linq;

namespace DB2ClassExercise.Data
{
    public class SomeDataService
    {

        private static SomeDataService instance;
        private SqlConnection currConnection;
        public String connectionString;

        public static SomeDataService getInstance() {
            if (instance == null) {
                instance = new SomeDataService();
            }
            return instance;
        }

        public void setConnectionString(String someConnStr) {
            connectionString = someConnStr;
        }

        public bool openConnection()
        {
            bool status = true;
            try
            {
                String conString = connectionString;
                currConnection = new SqlConnection(conString);
                currConnection.Open();
            }
            catch
            {
                status = false;
            }
            return status;
        }

        public bool closeConnection()
        {
            if (currConnection != null)
            {
                currConnection.Close();
            }
            return true;

        }
        public List<Course> getAvailableCourses() {
            List<Course> data = new List<Course>();
            try
            {
                openConnection();
                SqlCommand command = new SqlCommand("Select * from Courses", currConnection);
                using (SqlDataReader reader = command.ExecuteReader()) //lees van databasisse gebruik ExcecuteRader, NA databasis stuur ExcecuteNonQuery
                {
                    while (reader.Read())
                    {
                        Course tmpDest = new Course();
                        tmpDest.Id = Convert.ToInt32(reader["ID"]);
                        tmpDest.Name = reader["Name"].ToString();
                        tmpDest.Description = reader["Description"].ToString();
                        data.Add(tmpDest);
                    }
                }
                closeConnection();
            }
            catch
            {
            }
            return data;
        }

        //TODO: Uncommment and complete this
        public List<DataVM> getAssignmentsOfCourse(int courseID)
        {
            List<DataVM> data = new List<DataVM>();
            try
            {
                openConnection();
                SqlCommand command = new SqlCommand(@"SELECT Courses.Name, Courses.Description, Staff.Name, Students.Name from CourseAssignmentsMarking
                inner join Courses on Courses.ID = CourseAssignmentsMarking.CourseID
                inner join Staff on Staff.ID = CourseAssignmentsMarking.MarkerID
                inner join Students on Students.ID = CourseAssignmentsMarking.StudentID
                where CourseAssignmentsMarking.CourseID = '"+courseID+"'", currConnection);
                using (SqlDataReader reader = command.ExecuteReader()) //lees van databasisse gebruik ExcecuteRader, NA databasis stuur ExcecuteNonQuery
                {
                    while (reader.Read())
                    {
                        DataVM tmpDest = new DataVM();
                        tmpDest.Name = reader[0].ToString();
                        tmpDest.Description = reader[1].ToString();
                        tmpDest.LecturerName = reader[2].ToString();
                        tmpDest.StudentName = reader[3].ToString();
                        data.Add(tmpDest);
                    }
                }
                closeConnection();
            }
            catch
            {
            }
            return data;

        }

    }
}
