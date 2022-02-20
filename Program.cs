using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectOOP
{
    class Style
    {
        OperationStudent opStudent = new OperationStudent();
        OperationCourse opCourse = new OperationCourse();
        OperationScore opScore = new OperationScore();
        public  bool Login(string userName, string password)
        {
            string userN = "Aseel", pass = "1234";
            if (userN == userName && pass == password)
                return true;

            return false;
        }
        public  int GetInputFromUser(string descrip)
        {
            int inp =0;
            try
            {
                Console.Write(descrip);
                 inp = Convert.ToInt32(Console.ReadLine());
            }catch(FormatException e)
            {
                Console.WriteLine("ERR :"+e.Message);
            }

            return inp;
        }
        public  int GetMainMenu()
        {
            Console.WriteLine("\n\t\t\t\t--------------\n\t\t\t\t|  Main Menu  |");
            Console.WriteLine("\t\t\t\t--------------\n\t\t\t\t\n");
            Console.WriteLine("[1] Student Menu\n[2] Course Menu\n[3] Score Menu\n[0] Exit\n");

            int input = GetInputFromUser("Enter Your Choice : ");

            return input;
        }
        public  int DisplyMenuType(int num)
        {
            int x = 0;
            Console.Clear();
            switch (num)
            {
                case 1:
                    x = ManageStudent();
                    while (x != 0)
                    {
                        Console.Clear();
                        ManageStudentSwitch(x);
                        x = ManageStudent();
                    }

                    break;
                case 2:
                    x = ManageCourse();
                    while (x != 0)
                    {
                        Console.Clear();
                        ManageCourseSwitch(x);
                        x = ManageCourse();
                    }
                    break;
                case 3:
                    x = ManageScore();
                    while (x != 0)
                    {
                        Console.Clear();
                        ManageScoreSwitch(x);
                        x = ManageScore();
                    }
                    break;
            }

            Console.Clear();
            int input = GetMainMenu();

            return input;
        }
        public  int ManageStudent()
        {

            Console.WriteLine("\n\t\t\t\t---------------------\n\t\t\t\t| Student Main Menu |");
            Console.WriteLine("\t\t\t\t---------------------\n\t\t\t\t\n");
            Console.WriteLine("[1] Add New Student \n[2] Disply All Students\n[3] Update Student\n" +
                "[4] Delete Student\n[5] Search Student\n[6] Display Statics Infromation\n" +
                "[7] Print Student\n[0] Exit\n");


            int input = GetInputFromUser("Enter Your Choice : ");


            return input;

        }
        public  int ManageCourse()
        {
            Console.WriteLine("\n\t\t\t\t---------------------\n\t\t\t\t| Courses Main Menu |");
            Console.WriteLine("\t\t\t\t---------------------\n\t\t\t\t\n");
            Console.WriteLine("[1] Add New Course \n[2] Disply All Courses\n[3] Update Course\n" +
                "[4] Delete Course\n[5] Print Courses In A Text File\n[0] Exit\n");

            int input = GetInputFromUser("Enter Your Choice : ");

            return input;

        }
        public  int ManageScore()
        {
            Console.WriteLine("\n\t\t\t\t-------------------\n\t\t\t\t| Score Main Menu |");
            Console.WriteLine("\t\t\t\t-------------------\n\t\t\t\t\n");
            Console.WriteLine("[1] Add New Score \n[2] Display All Data\n[3] Update Score\n" +
                "[4] Delete Score\n[5] Print All Information in Text File\n" +
                "[6] Display The Average Score By Course\n" +
                "[7] Display Information For All Score\n[0] Exit\n");
            
            int input = GetInputFromUser("Enter Your Choice : ");

            return input;

        }
        public  void ManageStudentSwitch(int num)
        {

            switch (num)
            {
                case 1: opStudent.Insert(); break;
                case 2: opStudent.DisplayAllData(); break;
                case 3: opStudent.Update(); break;
                case 4: opStudent.Delete(); break;
                case 5: opStudent.SearchStudent(); break;
                case 6: opStudent.DisplayStaticsGender(); break;
                case 7: opStudent.PrintStudent(); break;
            }


        }
        public  void ManageCourseSwitch(int num)
        {

            switch (num)
            {
                case 1: opCourse.Insert(); break;
                case 2: opCourse.DisplayAllData(); break;
                case 3: opCourse.Update(); break;
                case 4: opCourse.Delete(); break;
                case 5: opCourse.PrintCoursesInTextFile(); break;
                default: Console.WriteLine("kenann"); break;
            }


        }
        public  void ManageScoreSwitch(int num)
        {

            switch (num)
            {
                case 1: opScore.Insert(); break;
                case 2: opScore.DisplayAllData(); break;
                case 3: opScore.Update(); break;
                case 4: opScore.Delete(); break;
                case 5: opScore.PrintScoresInTextFile(); break;
                case 6: opScore.DisplayAverageScoreByCourse(); break;
                case 7: opScore.DisplyAllInformation(); break;
            }


        }

    }

    class Program
    {
       
        static void Main(string[] args)
        {


            Style de = new Style();
            int i = 3;
            while (i != 0)
            {

                Console.Write("Enter UserName : ");
                string userName = Console.ReadLine();
                Console.Write("Enter Password : ");
                string password = Console.ReadLine();

                if (de.Login(userName, password))
                {
                    Console.Clear();
                    int numChosen = de.GetMainMenu();
                    while (numChosen != 0)
                    {
                        Console.Clear();
                        numChosen = de.DisplyMenuType(numChosen);
                    }
                    break;
                }
                --i;
                if (i != 0)
                {
                    Console.WriteLine("\nUserNamw Or Passwor 'Not vaild'...Plase try agin\n");

                }
                else
                {
                    Console.WriteLine("\nUserNamw Or Passwor 'Not vaild' and you’ve reached the maximum login attempts.\n");

                }

            }

        }
    }
}
