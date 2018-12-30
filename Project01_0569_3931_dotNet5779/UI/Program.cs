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
        enum hours { nine, ten, Eleven, Twelve, one, tow, trhee }
        enum days { sunday, monday, Tuesday, Wednesday, Thursday }
        static void Main(string[] args)
        {

            DateTime dateTime;
            bool[,] matrix = new bool[5, 6];
            string id;
            string city;
            string street;
            int numofbuilding;
            int num;
            int day;
            int hour;
            IBL bL = BL_Factory.GetBL();

            Console.WriteLine(@"choose what you wants to do:
1:Add Tester
2:Add Trianee
3:Add Test
4:Delete Tester
5:Delete Trianee
6:Update Tester
7:Update Trianee
8:Update Test
9:exit");
            num = int.Parse(Console.ReadLine());
            do
            {
                switch (num)
                {
                    case 1:
                        Tester tester = new BO.Tester();
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
                        numofbuilding = int.Parse(Console.ReadLine());
                        tester.PersonAddress = new Address(city, street, numofbuilding);
                        Console.WriteLine("enter number phone:");
                        tester.Phone = Console.ReadLine();
                        Console.WriteLine("enter your gender: for male: enter 0 , for female: enter 1 :");
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
                        Console.WriteLine("");

                        hours hours = new hours();
                        days days = new days();
                        int t = 9;
                        for (int i = 0; i < 5; ++i)
                        {
                            Console.WriteLine("enter for day: " + days);
                            ++days;
                            for (int j = 0; j < 6; ++j)
                            {
                                Console.WriteLine("enter for hour: " + hours);
                                ++hours;
                                do
                                { 
                                    matrix[i, j] = bool.Parse(Console.ReadLine());
                                    if (matrix[i, j] != true && matrix[i, j] != false)
                                        Console.WriteLine("enter only 1 or 0");
                                } while (matrix[i, j] != true && matrix[i, j] != false);
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
                        Trainee trainee = new Trainee();
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
                        numofbuilding = int.Parse(Console.ReadLine());
                        trainee.PersonAddress = new Address(city, street, numofbuilding);
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
                        Console.WriteLine("enter your gender: for male: 0 , for female: 1 : ");
                        trainee.PersonGender = (Gender)int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your date of birth: ");
                        trainee.DayOfBirth = ;
                        try
                        {
                            bL.AddTrainee(trainee);
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
                    case 3:
                        Test test = new BO.Test();
                        
                        Console.WriteLine("enter id for trainee: ");
                        test.TraineeId = Console.ReadLine();
                        Console.WriteLine("enter name for trainee: ");
                        test.TraineeName = Console.ReadLine();
                        Console.WriteLine("enter the date for test: ");
                        test.TestDate =  ;
                        Console.WriteLine("enter the hour for test: ");
                        test.TestHour = ;
                        Console.WriteLine("enter City of test: ");
                        city = Console.ReadLine();
                        Console.WriteLine("enter Street of test: ");
                        street = Console.ReadLine();
                        Console.WriteLine("enter Num Of Bilding of test: ");
                        numofbuilding = int.Parse(Console.ReadLine());
                        test.TestAddress = new Address(city, street, numofbuilding);
                        try
                        {
                            bL.AddTest(test);
                        }
                        catch (InvalidDataException e)
                        {
                            Console.WriteLine(e);
                        }
                        catch (KeyNotFoundException e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 4:
                        Console.WriteLine("Enter the ID Tester's ");
                        id = Console.ReadLine();
                        try
                        {
                            bL.DeleteTester(id);
                        }
                        catch (KeyNotFoundException e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 5:
                        Console.WriteLine("Enter the ID Trainee's ");
                        id = Console.ReadLine();
                        try
                        {
                            bL.DeleteTrainee(id);
                        }
                        catch (KeyNotFoundException e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 6:
                        object info = new object();
                        object info1 = new object();
                        object info2 = new object();
                        int number;
                        Console.WriteLine("Enter the ID Tester's ");
                        id = Console.ReadLine();
                        Console.WriteLine(@"select what you want to update:
1:first name
2:last name
3:day Of Birth
4:phone
5:Address
6:tester Experience
7:max Weekly Tests
8:tester Vehicle
9:range To Test
10:schedule
");
                        number = int.Parse(Console.ReadLine());
                        try
                        {

                            switch (number)
                            {
                                case 1:
                                    Console.WriteLine("enter the first name ");
                                    info = Console.ReadLine();
                                    bL.UpdateTester(id, "privateName", info);
                                    break;
                                case 2:
                                    Console.WriteLine("enter the last name ");
                                    info = Console.ReadLine();
                                    bL.UpdateTester(id, "familyName", info);
                                    break;
                                case 3:
                                    Console.WriteLine("enter the dayOfBirth");
                                    info = Console.ReadLine();
                                    bL.UpdateTester(id, "dayOfBirth", info);
                                    break;
                                case 4:
                                    Console.WriteLine("enter the phone");
                                    info = Console.ReadLine();
                                    bL.UpdateTester(id, "phone", info);
                                    break;
                                case 5:
                                    Console.WriteLine("enter City: ");
                                    city = Console.ReadLine();
                                    Console.WriteLine("enter Street: ");
                                    street = Console.ReadLine();
                                    Console.WriteLine("enter Num Of Bilding: ");
                                    numofbuilding = int.Parse(Console.ReadLine());
                                    info = new Address(city, street, numofbuilding);
                                    bL.UpdateTester(id, "personAddress", info);
                                    break;
                                case 6:
                                    Console.WriteLine("enter the Experience in years: ");
                                    info = Console.ReadLine();
                                    bL.UpdateTester(id, "testerExperience", info);
                                    break;
                                case 7:
                                    Console.WriteLine("enter the max Weekly Tests: ");
                                    info = Console.ReadLine();
                                    bL.UpdateTester(id, "maxWeeklyTests", info);
                                    break;
                                case 8:
                                    Console.WriteLine("enter the Vehicle: ");
                                    info = Console.ReadLine();
                                    bL.UpdateTester(id, "testerVehicle", info);
                                    break;
                                case 9:
                                    Console.WriteLine("enter the Vehicle: ");
                                    info = Console.ReadLine();
                                    bL.UpdateTester(id, "testerVehicle", info);
                                    break;
                                case 10:
                                    Console.WriteLine("enter the day to update in the number: ");
                                    info = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter the hour to update in the number: ");
                                    info1 = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter the update for true: enter 1 , for false: enter 0 ");
                                    info2 = int.Parse(Console.ReadLine());
                                    bL.UpdateTester(id, "schedule", info, info1, info2);
                                    break;
                                default: break;
                            }
                        }
                        catch (KeyNotFoundException e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 7:
                        object infom = new object();
                        int number1;
                        Console.WriteLine("Enter the ID Trainee's ");
                        id = Console.ReadLine();
                        Console.WriteLine(@"select what you want to update:
1:first name
2:last name
3:day Of Birth
4:phone
5:Address
6:Gear
7:school
8: Vehicle
9:teacher
10:Number of Lessons
");
                        number1 = int.Parse(Console.ReadLine());
                        try
                        {
                            switch (number1)
                            {
                                case 1:
                                    Console.WriteLine("enter the first name ");
                                    infom = Console.ReadLine();
                                    bL.UpdateTrainee(id, "privateName", infom);
                                    break;
                                case 2:
                                    Console.WriteLine("enter the last name ");
                                    infom = Console.ReadLine();
                                    bL.UpdateTrainee(id, "familyName", infom);
                                    break;
                                case 3:
                                    Console.WriteLine("enter the dayOfBirth");
                                    infom = Console.ReadLine();
                                    bL.UpdateTrainee(id, "dayOfBirth", infom);
                                    break;
                                case 4:
                                    Console.WriteLine("enter the phone");
                                    infom = Console.ReadLine();
                                    bL.UpdateTrainee(id, "phone", infom);
                                    break;
                                case 5:
                                    Console.WriteLine("enter City: ");
                                    city = Console.ReadLine();
                                    Console.WriteLine("enter Street: ");
                                    street = Console.ReadLine();
                                    Console.WriteLine("enter Num Of Bilding: ");
                                    numofbuilding = int.Parse(Console.ReadLine());
                                    infom = new Address(city, street, numofbuilding);
                                    bL.UpdateTrainee(id, "personAddress", infom);
                                    break;
                                case 6:
                                    Console.WriteLine("enter the Gear: ");
                                    infom = Console.ReadLine();
                                    bL.UpdateTrainee(id, "traineeGear", infom);
                                    break;
                                case 7:
                                    Console.WriteLine("enter the name of school: ");
                                    infom = Console.ReadLine();
                                    bL.UpdateTrainee(id, "school", infom);
                                    break;
                                case 8:
                                    Console.WriteLine("enter the Vehicle: ");
                                    infom = Console.ReadLine();
                                    bL.UpdateTrainee(id, "traineeVehicle", infom);
                                    break;
                                case 9:
                                    Console.WriteLine("enter the name of teacher: ");
                                    infom = Console.ReadLine();
                                    bL.UpdateTrainee(id, "teacher", infom);
                                    break;
                                case 10:

                                    Console.WriteLine("enter the Number of Lessons ");
                                    info2 = int.Parse(Console.ReadLine());
                                    bL.UpdateTrainee(id, "drivingLessonsNum", infom);
                                    break;
                                default: break;
                            }
                        }
                        catch (KeyNotFoundException e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case 8:


                        bool[] result = new bool[7];
                        string note; ;
                        int numOfTest;
                        Console.WriteLine("enter the numOfTest: ");
                        numOfTest = int.Parse(Console.ReadLine());
                        Console.WriteLine(@"enter your update:
1:mirrors
2:brakes
3:reverseParking
4:distance
5:vinkers
6:trafficSigns
7:passedTest");
                        for (int i = 0; i < 7; ++i)
                        {
                            result[i] = bool.Parse(Console.ReadLine());
                        }
                        Console.WriteLine("enter note: ");
                        note = Console.ReadLine();
                        try
                        {
                            bL.UpdateTestResult(numOfTest, result, note);
                        }
                        catch (KeyNotFoundException e)
                        {
                            Console.WriteLine(e);
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e);
                        }
                        break;

                }
            } while (num != 9);
        }
    }
}

