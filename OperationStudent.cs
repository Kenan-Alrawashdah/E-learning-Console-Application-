using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectOOP
{
    class OperationStudent : ICrud
    {
        string conString = @"Data Source=SCS\SQLEXPRESS;Initial Catalog=UniversityDB;Integrated Security=True;Pooling=False";

        public bool Insert()
        {
            Student student = new Student();
            try {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                Console.Write("please enter 'StudentID' :");
                student.id = Convert.ToInt32(Console.ReadLine());

                Console.Write("please enter 'firstName' :");
                student.fName = Console.ReadLine();

                Console.Write("please enter 'lastName' :");
                student.lName = Console.ReadLine();

                Console.Write("please enter 'Major' :");
                student.major = Console.ReadLine();

                Console.Write("please enter 'Gender' :");
                 student.gender = Console.ReadLine();

                string query = "insert into Student(id, fName, lName, major,gender ) values('"+student.id+"','"+student.fName+ "','" + student.lName + "','" + student.major + "','" + student.gender + "')";

                SqlCommand insert = new SqlCommand(query,con);
                insert.ExecuteNonQuery();
                Console.WriteLine("\n\t( Successfully...Inserted Student )");
                con.Close();
            }
            catch (SqlException e)
            {
                Console.WriteLine($"ERR : {e.Message}");
                return false;
            }
            catch(FormatException e)
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

                Console.Write("please Enter 'StydentID' you want to change : ");
                int id = Convert.ToInt32(Console.ReadLine());

                Console.Write("please enter new 'FirstName' that : ");
                string fName = Console.ReadLine();

                string query = "update Student set fName = '" + fName + "' where id ='" + id + "'";

                SqlCommand update = new SqlCommand(query, con);
                update.ExecuteNonQuery();
                Console.WriteLine("\n\t( Successfully...Updated Student )");
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
        public  bool Delete()
        {
            DisplayAllData();
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                Console.Write("Please Enter 'StudentID' you want to delete : ");
                int id = Convert.ToInt32(Console.ReadLine());


                string query = @"delete from Student where id ='" + id + "'";

                SqlCommand update = new SqlCommand(query, con);
                update.ExecuteNonQuery();
                Console.WriteLine("\n\t( Successfully...Deleted Student )");
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

                string query = @"select * from Student";

                SqlCommand getData = new SqlCommand(query, con);

                SqlDataReader dr = getData.ExecuteReader();

                if (dr.HasRows)
                {
                    Console.WriteLine();

                    ConsoleDisplayFormatter.PrintRow("ID", "First Name", "Last Name", "Major", "Gender");
                    ConsoleDisplayFormatter.PrintSeperatorLine();
                    while (dr.Read())
                    {
                        ConsoleDisplayFormatter.PrintSeperatorLine();
                        ConsoleDisplayFormatter.PrintRow(dr.GetInt32(0).ToString(), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4));
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
        public void SearchStudent()
        {
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                Console.Write("Enter the name you are looking for : ");
                string name = Console.ReadLine();
                string query = "select * from Student where fName like '"+name+ "' or lName like '" + name + "'";

                SqlCommand getData = new SqlCommand(query, con);

                SqlDataReader dr = getData.ExecuteReader();

                if (dr.HasRows)
                {
                    Console.WriteLine();

                    ConsoleDisplayFormatter.PrintRow("ID", "First Name", "Last Name", "Major", "Gender");
                    ConsoleDisplayFormatter.PrintSeperatorLine();
                    while (dr.Read())
                    {
                        ConsoleDisplayFormatter.PrintSeperatorLine();
                        ConsoleDisplayFormatter.PrintRow(dr.GetInt32(0).ToString(), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4));
                    }
                    ConsoleDisplayFormatter.PrintSeperatorLine();

                }
                else
                {
                    ConsoleDisplayFormatter.PrintSeperatorLine();
                    Console.WriteLine("No Result Found ...");
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
        public void PrintStudent()
        {
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                Console.Write("Enter the StudentID you are looking for : ");
                int id = Convert.ToInt32(Console.ReadLine());
                string query = "select * from Student where id = '" + id + "' ";

                SqlCommand getData = new SqlCommand(query, con);

                SqlDataReader dr = getData.ExecuteReader();

                if (dr.HasRows)
                {
                    Console.WriteLine();

                    ConsoleDisplayFormatter.PrintRow("ID", "First Name", "Last Name", "Major", "Gender");
                    ConsoleDisplayFormatter.PrintSeperatorLine();
                    while (dr.Read())
                    {
                        ConsoleDisplayFormatter.PrintSeperatorLine();
                        ConsoleDisplayFormatter.PrintRow(dr.GetInt32(0).ToString(), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4));
                    }
                    ConsoleDisplayFormatter.PrintSeperatorLine();

                }
                else
                {
                    ConsoleDisplayFormatter.PrintSeperatorLine();
                    Console.WriteLine("No Result Found ...");
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
        public void DisplayStaticsGender()
        {
            try
            {
                SqlConnection con = new SqlConnection(conString);
                con.Open();

                string query = "select count(id),gender from Student group by gender;";

                SqlCommand getData = new SqlCommand(query, con);

                SqlDataReader dr = getData.ExecuteReader();

                if (dr.HasRows)
                {
                    Console.WriteLine();

                    while (dr.Read())
                    {
                        ConsoleDisplayFormatter.PrintSeperatorLine();
                        ConsoleDisplayFormatter.PrintRow(dr.GetString(1), dr.GetInt32(0).ToString());
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

       




    }
}
