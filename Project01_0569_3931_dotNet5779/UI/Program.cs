using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BO;
namespace UI
{
    class Program
    {

        static void Main(string[] args)
        {
            //11
            DateTime dateTime;
            bool[,] matrix = new bool[5, 6];
            string city;
            string street;
            int numofilding;
            int num;
            IBL bL = BL_Factory.GetBL();
            Console.WriteLine(@"choose what you wants to do:
1:Add Tester
2:Add Trianee
3:Add Test");
            num = int.Parse(Console.ReadLine());
            switch (num)
            {
                case 1:
                    BO.Tester tester = new BO.Tester();
                    Console.WriteLine("enter id:");
                    tester.ID = Console.ReadLine();
                    Console.WriteLine("enter Family Name:");
                    tester.FamilyName = Console.ReadLine();
                    Console.WriteLine("enter Private Name:");
                    tester.PrivateName = Console.ReadLine();
                    Console.WriteLine("enter City:");
                    city = Console.ReadLine();
                    Console.WriteLine("enter Street:");
                    street = Console.ReadLine();
                    Console.WriteLine("enter NumOfBilding:");
                    numofilding = int.Parse(Console.ReadLine());
                    tester.PersonAddress = new Address(city, street, numofilding);
                    Console.WriteLine("enter number phone:");
                    tester.Phone = Console.ReadLine();
                    Console.WriteLine("enter your sex: for male: 0 , for female: 1 :");
                    tester.PersonGender = (Gender)int.Parse(Console.ReadLine());
                    Console.WriteLine("enter max tests in week: ");
                    tester.MaxWeeklyTests = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter the range from home to do test: ");
                    tester.RangeToTest = double.Parse(Console.ReadLine());
                    Console.WriteLine("enter your Experience: ");
                    tester.TesterExperience = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter your specialization: ");
                    tester.TesterVehicle = (Vehicle)int.Parse(Console.ReadLine());
                    Console.WriteLine("enter your date of birth: ");
                    tester.DayOfBirth.Day = int.Parse(Console.ReadLine());
                    dateTime = new DateTime();
                    for (int i = 0; i < 5; ++i)
                    {
                        for (int j = 0; j < 6; ++j)
                        {

                        }
                    }
                    try
                    {
                        bL.AddTester(tester, matrix);
                    }
                    catch (InvalidDataException e)
                    {
                        Console.WriteLine(e);
                    }
                    catch (DuplicateWaitObjectException e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
                case 2:
                    BO.Trainee trainee = new Trainee();
                    Console.WriteLine("enter id:");
                    trainee.ID = Console.ReadLine();
                    Console.WriteLine("enter Family Name:");
                    trainee.FamilyName = Console.ReadLine();
                    Console.WriteLine("enter Private Name:");
                    trainee.PrivateName = Console.ReadLine();
                    Console.WriteLine("enter City:");
                    city = Console.ReadLine();
                    Console.WriteLine("enter Street:");
                    street = Console.ReadLine();
                    Console.WriteLine("enter NumOfBilding:");
                    numofilding = int.Parse(Console.ReadLine());
                    trainee.PersonAddress = new Address(city, street, numofilding);
                    Console.WriteLine("enter num the Lessons: ");
                    trainee.DrivingLessonsNum = int.Parse(Console.ReadLine());
                    Console.WriteLine("enter name your School: ");
                    trainee.School = Console.ReadLine();
                    Console.WriteLine("enter name your Teacher: ");
                    trainee.Teacher = Console.ReadLine();
                    Console.WriteLine("enter number phone:");
                    trainee.Phone = Console.ReadLine();
                    Console.WriteLine("enter type the GearBox: ");
                    trainee.TraineeGear = (GearBox)int.Parse(Console.ReadLine());
                    Console.WriteLine("enter type the Vehicle: ");
                    trainee.TraineeVehicle = (Vehicle)int.Parse(Console.ReadLine());
                    Console.WriteLine("enter your sex: for male: 0 , for female: 1 : ");
                    trainee.PersonGender = (Gender)int.Parse(Console.ReadLine());
                    Console.WriteLine("enter your date of birth: ");
                    trainee.DayOfBirth = ;
                    try
                    {
                        bL.AddTrainee(trainee);
                    }
                    catch ()
                    {

                    }
            }
        }
    }
}
}
