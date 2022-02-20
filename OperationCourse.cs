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
    class OperationCourse : ICrud
    {
        string conString = @"Data Source=SCS\SQLEXPRESS;Initial Catalog=UniversityDB;Integrated Security=True;Pooling=False";

        public bool Insert()
        {
            Course course = new Course();
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                Console.Write("please enter 'CourseID' :");
                course.id = Convert.ToInt32(Console.ReadLine());

                Console.Write("please enter 'CourseName' :");
                course.name = Console.ReadLine();


                string query = "insert into Course(id, name ) values('" + course.id + "','" + course.name + "')";

                SqlCommand insert = new SqlCommand(query, con);
                insert.ExecuteNonQuery();
                Console.WriteLine("\n\t( Successfully...Inserted Course )");
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

                Console.Write("please Enter 'CourseID' you want to change : ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.Write("please enter new course 'Name' that went : ");
                string name = Console.ReadLine();

                string query = "update Course set name = '" + name + "' where id ='" + id + "'";

                SqlCommand update = new SqlCommand(query, con);
                update.ExecuteNonQuery();

                Console.WriteLine("\n\t( Successfully...Updated CourseName )");
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

                Console.Write("Please Enter 'CourseID' you want to delete : ");
                int id = Convert.ToInt32(Console.ReadLine());


                string query = @"delete from Course where id ='" + id + "'";

                SqlCommand update = new SqlCommand(query, con);
                update.ExecuteNonQuery();
                Console.WriteLine("\n\t( Successfully...Deleted Course )");
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

                string query = @"select * from Course";

                SqlCommand getData = new SqlCommand(query, con);

                SqlDataReader dr = getData.ExecuteReader();

                if (dr.HasRows)
                {
                    Console.WriteLine();

                    ConsoleDisplayFormatter.PrintRow("ID", "Name");
                    ConsoleDisplayFormatter.PrintSeperatorLine();
                    while (dr.Read())
                    {
                        ConsoleDisplayFormatter.PrintSeperatorLine();
                        ConsoleDisplayFormatter.PrintRow(dr.GetInt32(0).ToString(), dr.GetString(1));
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

        public void PrintCoursesInTextFile()
        {
            Console.Write("Enter Name File : ");
            string nameFile = Console.ReadLine();
            string path = @"C:\Users\Mohammad\Desktop\TestFile\"+nameFile+".txt";
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                string query = @"select * from Course";

                SqlCommand getData = new SqlCommand(query, con);

                SqlDataReader dr = getData.ExecuteReader();

                if (dr.HasRows)
                {
                    
                    Course course = new Course();
                    using(StreamWriter st = File.AppendText(path))
                    {
                        
                        while (dr.Read())
                        {
                            course.id = dr.GetInt32(0);
                            course.name = dr.GetString(1);
                            string output = JsonConvert.SerializeObject(course);
                            st.WriteLine(output);
                        }

                        ConsoleDisplayFormatter.PrintSeperatorLine();
                        Console.WriteLine($"\n\t( Successfully...Printed Courses In {nameFile} File )");
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

    }
}
