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
                SqlCommand command = new SqlCommand("select * from Courses", currConnection);
                using (SqlDataReader reader = command.ExecuteReader()) //lees van databasisse gebruik ExcecuteRader, NA databasis stuur ExcecuteNonQuery
                {
                    while (reader.Read())
                    {
                        Course tmpDest = new Course();
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

        public List<Course> getAssignmentsOfCourse(int courseID)
        {
            Course dest = null;
            List <Course> assignments = new List<Course> ();
            if (assignments.Any(d => d.Id == courseID))
            {
                int index = assignments.FindIndex(d => d.Id == courseID);
                dest = assignments[index];
            }
            return assignments;
        }

        //public Course getAssignmentsOfCourse(int courseID)
        //{
        //    Course dest = null;
        //    List<Course> assignments = getAvailableCourses();

        //    if (assignments.Any(d => d.Id == courseID))
        //    {
        //        int index = assignments.FindIndex(d => d.Id == courseID);
        //        dest = assignments[index];
        //    }

        //    return dest;
        //}
    }
}
