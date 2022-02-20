using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectOOP
{
    class OperationScore:ICrud
    {
        string conString = @"Data Source=SCS\SQLEXPRESS;Initial Catalog=UniversityDB;Integrated Security=True;Pooling=False";

        public bool Insert()
        {
            Score score = new Score();

            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                Console.Write("please enter 'ScoreID' :");
                score.id = Convert.ToInt32(Console.ReadLine());

                Console.Write("please enter 'Mark' :");
                score.mark = Convert.ToDouble(Console.ReadLine());

                Console.Write("please enter 'StudentID' :");
                score.stdID = Convert.ToInt32(Console.ReadLine());

                Console.Write("please enter 'CourseID' :");
                score.couID = Convert.ToInt32(Console.ReadLine());

                

                string query = "insert into Score(id, mark, stdID, couID) values('" + score.id + "','" + score.mark + "','" + score.stdID + "','" + score.couID + "')";

                SqlCommand insert = new SqlCommand(query, con);
                insert.ExecuteNonQuery();
                Console.WriteLine("\n\t( Successfully...Inserted Score )");
                con.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine($"ERR : {e.Message}");
                return false;
            }
            catch (FormatException e)
            {
                Console.WriteLine($"ERR : {e.Message}");
                return false;
            }
            return true;

        }
        public bool Update()
        {
            DisplayAllData();
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                Console.Write("please Enter 'ScoreID' you want to change : ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.Write("please enter 'newMark' that went chenge : ");
                double newMark = Convert.ToDouble(Console.ReadLine());

                string query = "update Score set mark = '" + newMark + "' where id ='" + id + "'";

                SqlCommand update = new SqlCommand(query, con);
                update.ExecuteNonQuery();
                Console.WriteLine("\n\t( Successfully...Updated ScoreMark )");
                con.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine($"ERR : {e.Message}");
                return false;
            }
            catch (FormatException e)
            {
                Console.WriteLine($"ERR : {e.Message}");
                return false;
            }
            return true;
        }
        public bool Delete()
        {
            DisplayAllData();
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                Console.Write("Please Enter 'ScoreID' you want to delete : ");
                int id = Convert.ToInt32(Console.ReadLine());


                string query = @"delete from Score where id ='" + id + "'";

                SqlCommand update = new SqlCommand(query, con);
                update.ExecuteNonQuery();
                Console.WriteLine("\n\t( Successfully...Deleted Score )");
                con.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine($"ERR : {e.Message}");
                return false;
            }
            catch (FormatException e)
            {
                Console.WriteLine($"ERR : {e.Message}");
                return false;
            }
            return true;
        }

        public void DisplayAllData()
        {
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                string query = @"select sc.id, st.fName,st.lName,co.name ,
                               sc.mark from Score sc
                               join Student st on sc.stdID = st.id
                               join Course co on sc.couID = co.id";

                SqlCommand getData = new SqlCommand(query, con);

                SqlDataReader dr = getData.ExecuteReader();

                if (dr.HasRows)
                {
                    Console.WriteLine();

                    ConsoleDisplayFormatter.PrintRow("ID", "First Name", "Last Name","Course Name", "Mark" );
                    ConsoleDisplayFormatter.PrintSeperatorLine();
                    while (dr.Read())
                    {
                        ConsoleDisplayFormatter.PrintSeperatorLine();
                        ConsoleDisplayFormatter.PrintRow(dr.GetInt32(0).ToString(), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetDouble(4).ToString());
                    }
                    ConsoleDisplayFormatter.PrintSeperatorLine();

                }

                dr.Close();
                con.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("ERR :" + e.Message);
            }
        }

        public void DisplayAverageScoreByCourse()
        {
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                string query = @"select avg(s.mark)as AVG,c.name from Score s
                                 join Course c
                                 on s.couID = c.id
                                 group by c.name";

                SqlCommand getData = new SqlCommand(query, con);

                SqlDataReader dr = getData.ExecuteReader();

                if (dr.HasRows)
                {
                    Console.WriteLine();

                    ConsoleDisplayFormatter.PrintRow("Course Name", "Average");
                    ConsoleDisplayFormatter.PrintSeperatorLine();
                    while (dr.Read())
                    {
                        ConsoleDisplayFormatter.PrintSeperatorLine();
                        ConsoleDisplayFormatter.PrintRow(dr.GetString(1), dr.GetDouble(0).ToString());
                    }
                    ConsoleDisplayFormatter.PrintSeperatorLine();

                }

                dr.Close();
                con.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("ERR :" + e.Message);
            }
        }

        public void PrintScoresInTextFile()
        {
            Console.Write("Enter Name File : ");
            string nameFile = Console.ReadLine();
            string path = @"C:\Users\Mohammad\Desktop\TestFile\" + nameFile + ".txt";

            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                string query = @"select sc.id, st.fName+' '+st.lName as [Full Name],co.name as [Course Name],sc.mark from Score sc
                                 join Student st
                                 on sc.stdID = st.id
                                 join Course co
                                 on sc.couID = co.id";

                SqlCommand getData = new SqlCommand(query, con);

                SqlDataReader dr = getData.ExecuteReader();

                if (dr.HasRows)
                {
                    using (StreamWriter st = File.AppendText(path))
                    {

                        while (dr.Read())
                        {                
                            string output = JsonConvert.SerializeObject(new {id = dr.GetInt32(0),
                                                                             FullName = dr.GetString(1),
                                                                             CourseName =dr.GetString(2),
                                                                             Mark = dr.GetDouble(3)});
                            st.WriteLine(output);
                            
                        }
                        ConsoleDisplayFormatter.PrintSeperatorLine();
                        Console.WriteLine($"\n\t( Successfully...Printed Scores In {nameFile} File )");
                        ConsoleDisplayFormatter.PrintSeperatorLine();
                    }
                }

                dr.Close();
                con.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("ERR :" + e.Message);
            }
        }

        public void DisplyAllInformation()
        {
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();


                using (SampleDataContext dataC = new SampleDataContext())
                {
                    foreach (Course course in dataC.Courses)
                    {
                        Console.WriteLine("\nCourse Name " +"[ "+ course.name+" ]"+"[ "+course.id+" ]");
                        ConsoleDisplayFormatter.PrintSeperatorLine();
                        ConsoleDisplayFormatter.PrintRow("StudentId", "FirstName", "LastName", "Mark");
                        ConsoleDisplayFormatter.PrintSeperatorLine();
                        foreach (Score sc in course.Scores)
                        {
                            
                            ConsoleDisplayFormatter.PrintRow(sc.Student.id.ToString(), sc.Student.fName, sc.Student.lName, sc.mark.ToString());
                        }
                        ConsoleDisplayFormatter.PrintSeperatorLine();
                        Console.WriteLine("Course Average = " + GetAvgToOneCourse(course.id));
                        ConsoleDisplayFormatter.PrintSeperatorLine();

                    }
                }




                con.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("ERR :" + e.Message);
            }
        }

        public double GetAvgToOneCourse(int id)
        {
            double avg = 0;
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                string query = $"select avg(s.mark)as AVG,c.name from Score s join Course c on s.couID = c.id where c.id = {id} group by c.name";

                SqlCommand getData = new SqlCommand(query, con);

                SqlDataReader dr = getData.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        avg = dr.GetDouble(0);
                    }
                    
                }

                dr.Close();
                con.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine("ERR :" + e.Message);
            }

            return avg;
        }
    }
}
