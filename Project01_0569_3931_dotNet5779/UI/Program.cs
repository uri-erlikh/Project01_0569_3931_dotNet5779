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
            bool[,] matrix = new bool[5, 6];
            string id;
            string city;
            string street;
            int numofbuilding;
            int num;
            int day;
            int hour;
            int month;
            int year;
            IBL bL = BL_Factory.GetBL();
            do
            {
                Console.WriteLine(@"choose what you wants to do:
1: Add Tester
2: Add Trianee
3: Add Test
4: Delete Tester
5: Delete Trianee
6: Update Tester
7: Update Trianee
8: Update Test
9: print all teters
10: print all trianee
11: print all tests
12:exit");
                num = int.Parse(Console.ReadLine());

                switch (num)
                {
                    case 1:
                        Tester tester = new BO.Tester();
                        try
                        {
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
                        Console.WriteLine("enter your specialization: privateCar=0, motorcycle=1, truck=2, heavyTruck=3");
                        tester.TesterVehicle = (Vehicle)int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your day of birth: ");                                                
                            day = int.Parse(Console.ReadLine());                        
                        Console.WriteLine("enter your month of birth: ");
                        month = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your year of birth: ");
                        year = int.Parse(Console.ReadLine());
                        tester.DayOfBirth = new DateTime(year, month, day);
                        Console.WriteLine("enter only true or false for the Schedule: ");

                        hours hours = new hours();
                        days days = new days();
                       
                            for (int i = 0; i < 5; ++i, ++days, hours = 0)
                            {
                                Console.WriteLine("enter for day: " + days);
                                for (int j = 0; j < 6; ++j, ++hours)
                                {
                                    Console.WriteLine("enter for hour: " + hours);
                                    matrix[i, j] = bool.Parse(Console.ReadLine());                                    
                                }
                            }
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        try
                        {
                            bL.AddTester(tester, matrix);
                        }
                        catch (InvalidDataException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (DuplicateWaitObjectException e)
                        {
                            Console.WriteLine(e.Message);
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
                        Console.WriteLine("enter type the GearBox:0-1 ");
                        trainee.TraineeGear = (GearBox)int.Parse(Console.ReadLine());
                        Console.WriteLine("enter type the Vehicle:0-4 ");
                        trainee.TraineeVehicle = (Vehicle)int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your gender: for male: 0 , for female: 1 : ");
                        trainee.PersonGender = (Gender)int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your day of birth: ");
                        day = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your month of birth: ");
                        month = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter your year of birth: ");
                        year = int.Parse(Console.ReadLine());
                        trainee.DayOfBirth = new DateTime(year, month, day);
                        try
                        {
                            bL.AddTrainee(trainee);
                        }
                        catch (InvalidDataException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (DuplicateWaitObjectException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 3:
                        try
                        {
                            Test test = new Test();//לעדכן שדה סוג כלי רכב
                            Console.WriteLine("enter id for trainee: ");
                            test.TraineeId = Console.ReadLine();
                            Console.WriteLine("enter type the Vehicle:0-4 ");
                            Vehicle vehicle = (Vehicle)int.Parse(Console.ReadLine());
                            test.TraineeName = bL.GetOneTrainee(test.TraineeId,vehicle).FamilyName + bL.GetOneTrainee(test.TraineeId,vehicle).PrivateName;
                            // Console.WriteLine("enter name for trainee: ");
                            //  test.TraineeName = Console.ReadLine();
                            Console.WriteLine("enter your day of test: ");
                            day = int.Parse(Console.ReadLine());
                            Console.WriteLine("enter your month of test: ");
                            month = int.Parse(Console.ReadLine());
                            Console.WriteLine("enter your year of test: ");
                            year = int.Parse(Console.ReadLine());
                            test.TestDate = new DateTime(year, month, day);
                            Console.WriteLine("enter the hour for test: ");
                            hour = int.Parse(Console.ReadLine());
                            test.TestHour = new DateTime(year, month, day, hour, 0, 0);
                            Console.WriteLine("enter City of test: ");
                            city = Console.ReadLine();
                            Console.WriteLine("enter Street of test: ");
                            street = Console.ReadLine();
                            Console.WriteLine("enter Num Of Bilding of test: ");
                            numofbuilding = int.Parse(Console.ReadLine());
                            test.TestAddress = new Address(city, street, numofbuilding);

                            bL.AddTest(test);
                        }
                        catch (InvalidDataException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        catch (KeyNotFoundException e)
                        {
                            Console.WriteLine(e.Message);
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
                            Console.WriteLine(e.Message);
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
                            Console.WriteLine(e.Message);
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
                                    bL.UpdateTester(id, "personAddress", city,street,numofbuilding);
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
                                    Console.WriteLine("enter the day in week to update in the number: ");
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
                            Console.WriteLine(e.Message);
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
                                    bL.UpdateTrainee(id, "personAddress", city,street,numofbuilding);
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
                                    Console.WriteLine("enter the Vehicle: ");//לזכור שזה סטרינג וגם למטה היחס אליו כסטרינג ולשנות
                                    infom = Console.ReadLine();
                                    bL.UpdateTrainee(id, "traineeVehicle", infom);
                                    break;
                                case 9:
                                    Console.WriteLine("enter the name of teacher: ");
                                    infom = Console.ReadLine();
                                    bL.UpdateTrainee(id, "teacher", infom);
                                    break;
                                case 10:
                                    Console.WriteLine("enter type the Vehicle:0-4 ");
                                    int info10 = int.Parse(Console.ReadLine());
                                    Console.WriteLine("enter the Number of Lessons ");//שינינו שיקלוט גם סוג רכב דבר ראשון. ראה בשכבה למטה 
                                    info2 = int.Parse(Console.ReadLine());
                                    bL.UpdateTrainee(id, "drivingLessonsNum", info10, info2);
                                    break;
                                default: break;
                            }
                        }
                        catch (KeyNotFoundException e)
                        {
                            Console.WriteLine(e.Message);
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
                            Console.WriteLine(e.Message);
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case 9:
                        foreach (var item in bL.GetTesters())
                            Console.WriteLine(item);
                        break;
                    case 10:
                        foreach (var item in bL.GetTrainees())
                            Console.WriteLine(item);
                        break;
                    case 11:
                        foreach (var item in bL.GetTests())
                            Console.WriteLine(item);
                        break;
                }
            } while (num != 12);
        }
        //--------------------------------------------------------------
        void CheckDate(DateTime time)
        {
            try
            {
                if (time.Day < 1 || time.Day > 31)
                    throw new InvalidDataException("no valid day");
                if (time.Month < 1 || time.Month > 12)
                    throw new InvalidDataException("no valid month");
                if (time.Year < (DateTime.Now.Year - Configuration.MAX_TESTER_AGE)
                    || time.Year > (DateTime.Now.Year - Configuration.MIN_TESTER_AGE))
                    throw new InvalidDataException("no valid year");
            }
            catch (InvalidDataException e) { throw; }
        }
    }
}




