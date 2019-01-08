using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DO;
using BO;
using System.Globalization;

namespace BL
{
    class BLImplementation : IBL
    {
        IDal dl = DAL_Factory.GetDL("lists");
        static Random r = new Random();
        //-------------------------------------------------------------
        public void AddTester(BO.Tester tester, bool[,] matrix)
        {
            tester.age = DateTime.Now.Year - tester.DayOfBirth.Year;
            try
            {
                CheckID(tester.ID);
                CheckAgeTester(tester.age);
                CheckName(tester.FamilyName);
                CheckName(tester.PrivateName);
                CheckDate(tester.DayOfBirth);
                CheckPhone(tester.Phone);
                CheckTesterExperience(tester.TesterExperience);
                CheckMaxWeeekltTests(tester.MaxWeeklyTests);
                CheckVehicle(tester.TesterVehicle.ToString());
            }
            catch (BO.InvalidDataException e) { throw; }
            try
            {
                dl.AddTester(Convert(tester), matrix);
            }
            catch (DuplicateWaitObjectException e)
            {
                throw;
            }
        }
        //---------------------------------------------------------
        public void DeleteTester(string TesterID)
        {
            try
            {
                dl.DeleteTester(TesterID);
            }
            catch (KeyNotFoundException e)
            { throw; }
        }
        //--------------------------------------------------------------------
        public void UpdateTester(string testerID, string field, params object[] info)
        {
            try
            {
                switch (field)
                {
                    case "familyName":
                        CheckName((string)info[0]);
                        break;
                    case "privateName":
                        CheckName((string)info[0]);
                        break;
                    case "dayOfBirth":
                        CheckDate(DateTime.Parse((string)info[0]));
                        break;
                    case "phone":
                        CheckPhone((string)info[0]);
                        break;
                    case "personAddress": break;
                    case "testerExperience":
                        CheckTesterExperience((int)info[0]);
                        break;
                    case "maxWeeklyTests":
                        CheckMaxWeeekltTests((int)info[0]);
                        break;
                    case "testerVehicle":
                        CheckVehicle((string)info[0]);
                        break;
                    case "rangeToTest": break;
                    case "schedule":
                        CheckScheduale((int)info[0], (int)info[1]);
                        break;
                    default: throw new InvalidDataException("no such field");
                }
            }
            catch (InvalidDataException e) { throw; }
            try
            {
                dl.UpdateTester(testerID, field, info);
            }
            catch (KeyNotFoundException e) { throw; }
        }
        //---------------------------------------------------------------
        public BO.Tester GetOneTester(string ID)
        {
            try
            {
                return Convert(dl.GetOneTester(ID));
            }
            catch (KeyNotFoundException e)
            {
                throw;
            }
        }
        //----------------------------------------------------------------
        public void AddTrainee(BO.Trainee trainee)
        {
            trainee.age = DateTime.Now.Year - trainee.DayOfBirth.Year;
            try
            {
                if (trainee.age < Configuration.MIN_TRAINEE_AGE)
                    throw new InvalidDataException("Too young trainee");
                CheckID(trainee.ID);
                CheckName(trainee.FamilyName);
                CheckName(trainee.PrivateName);
                CheckDateTrainee(trainee.DayOfBirth);
                CheckPhone(trainee.Phone);
                CheckVehicle(trainee.TraineeVehicle.ToString());
                CheckGear(trainee.TraineeGear.ToString());
                CheckDrivingLessonsNum(trainee.DrivingLessonsNum);
            }
            catch (InvalidDataException e)
            {
                throw;
            }

            try
            {
                dl.AddTrainee(Convert(trainee));
            }
            catch
                (DuplicateWaitObjectException e)
            { throw; }
        }
        //------------------------------------------------------------------------
        public void DeleteTrainee(string traineeID, BO.Vehicle vehicle)
        {
            try
            {
                if (!dl.GetSomeTrainies(x => x.ID == traineeID && x.TraineeVehicle == (DO.Vehicle)vehicle).Any())
                    throw new InvalidDataException("no match between trainee and vehicle");
                dl.DeleteTrainee(traineeID, (DO.Vehicle)vehicle);
            }
            catch (KeyNotFoundException e)
            { 
                throw;
            }
        }
        //--------------------------------------------------------------------
        public void UpdateTrainee(string traineeID, string field, params object[] info)
        {
            try
            {
                switch (field)
                {
                    case "familyName":
                        CheckName((string)info[0]);
                        break;
                    case "privateName":
                        CheckName((string)info[0]);
                        break;
                    case "dayOfBirth":
                        CheckDateTrainee(DateTime.Parse((string)(info[0])));
                        break;
                    case "phone":
                        CheckPhone((string)info[0]);
                        break;
                    case "personAddress": break;
                    case "traineeVehicle":
                        CheckVehicle((string)info[0]);
                        break;
                    case "traineeGear":
                        CheckGear((string)info[0]);
                        break;
                    case "school": break;
                    case "teacher": break;
                    case "drivingLessonsNum":
                        CheckVehicle((string)info[0]);
                        CheckDrivingLessonsNum((int)info[1]);
                        break;
                }
            }
            catch (InvalidDataException e) { throw; }

            try
            {
                dl.UpdateTrainee(traineeID, field, info);
            }
            catch (KeyNotFoundException e) { throw; }
        }
        //---------------------------------------------------------------------
        public BO.Trainee GetOneTrainee(string ID, BO.Vehicle vehicle)
        {
            try
            {
                return Convert(dl.GetOneTrainee(ID, (DO.Vehicle)vehicle));
            }
            catch (KeyNotFoundException e)
            {
                throw;
            }
        }
        //------------------------------------------------------------------
        public string AddTest(BO.Test test)
        {
            try
            {
                ChecksToAddTest(test);
            }
            catch (KeyNotFoundException e) { throw; }
            catch (InvalidDataException e) { throw; }
            try
            {
                DateTime newDate = test.TestHour;
                List<BO.Tester> closeTester = GetCloseTester(test.TestAddress, 30);
                if (!closeTester.Any())
                    throw new InvalidDataException("no close tester");

                List<BO.Tester> whoTest = GetTestersByDate(test.TestHour);
                if (!whoTest.Any())
                {
                    newDate = GetNewDate(test.TestHour, test);
                    whoTest = GetTestersByDate(newDate);
                }

                bool flag = false;
                if (whoTest.Any() && closeTester.Any())
                {
                    var finalList = (from item in whoTest
                             from item1 in closeTester
                             where item.ID == item1.ID
                             select item).ToList();
                    foreach (var item in finalList)
                        if (item.TesterVehicle == test.Vehicle)
                        {
                            test.Tester.ID = item.ID;
                            test.TestDate = newDate.Date;
                            test.TestHour = newDate;
                            flag = true;
                        }
                }
                if (!flag)                    
                 throw new InvalidDataException("no match between vehicles");
            }
            catch (InvalidDataException e) { throw; }
            catch (KeyNotFoundException e) { throw; }
                       
            try
            {
                return dl.AddTest(Convert(test))+ "\n your test date: "+ test.TestHour+"\n good luck!";                
            }
            catch (KeyNotFoundException e) { throw; }
        }
        //-------------------------------------------------------------------
        public List<BO.Tester> GetTestersByDate(DateTime date)
        {
            try
            {
                List<DO.Tester> whoWork = (from item in dl.GetTesters()
                                           where dl.GetSchedule(item.ID)[(int)date.DayOfWeek, date.Hour - 9] == true
                                           select item).ToList();
                if (!whoWork.Any())
                    return new List<BO.Tester>();

                var newList = (from item in whoWork
                               from itemTest in Convert(item).TesterTests
                               where itemTest.TestHour == date
                               select item).ToList();

                foreach (var item in newList)
                    whoWork.Remove(item);
                //List<BO.Tester> temp = (from item in whoWork.Except(newList)//containes
                //                        select Convert(item)).ToList();
                for (int i = 0; i < whoWork.Count; ++i)
                    if (!CheckHoursInWeek(Convert(whoWork[i]), date))
                    {
                        whoWork.Remove(whoWork[i]);
                        --i;
                    }
                return (from item in whoWork
                       select Convert(item)).ToList();
            }
            catch (KeyNotFoundException e) { throw; }
            catch (IndexOutOfRangeException e) { throw new InvalidDataException("don't choose friday-saturday"); }
        }
        //-----------------------------------------------------------------
        private DateTime GetNewDate(DateTime date, BO.Test test)
        {
            DateTime temp = date.AddDays(1);
            while (!GetTestersByDate(temp).Any() || !IfTraineeDoTest(temp, test))
                if (temp.Hour < 14)
                    temp.AddHours(1);
                else
                {
                    temp.AddDays(1);
                    temp.AddHours(-5);
                    if ((int)temp.DayOfWeek > 4)
                        temp.AddDays(2);
                }
            return temp;
        }
        //--------------------------------------------------------------------------
        private bool IfTraineeDoTest(DateTime date, BO.Test test)
        {
            if ((from item in dl.GetSomeTests(x => x.TestHour == date && x.TraineeId == test.TraineeId)
                 select item).ToList().Any())
                return false;
            return true;
        }
        //--------------------------------------------------------------------
        private void ChecksToAddTest(BO.Test test)
        {
            try
            {
                CheckID(test.TraineeId);
                CheckDateTrainee(test.TestDate);
                CheckHour(test.TestHour);
            }
            catch (InvalidDataException e) { throw; }
            //------------
            /*var tspan = test.TestDate -*/ try
            {
                if ((from item in dl.GetSomeTests(x => x.TraineeId == test.TraineeId)
                    let temp = test.TestDate - item.TestDate
                    where temp.Days < BO.Configuration.MIN_GAP_TEST && temp.Days > -(BO.Configuration.MIN_GAP_TEST)
                    select item).ToList().Any())
                    throw new InvalidDataException("test too close");
            }
        
                                         //where item.TestDate < DateTime.Now
                                         // orderby item.TestDate descending
                                          //select item.TestDate).ToList().FirstOrDefault();//same vehicle
            //try
            //{
            //    if (tspan.Days < BO.Configuration.MIN_GAP_TEST)
            //        throw new InvalidDataException("test too close");
            //}
            catch (InvalidDataException e) { throw; }
            catch (KeyNotFoundException e) { throw; }
            //-------------
            try
            {
                if (dl.GetOneTrainee(test.TraineeId,(DO.Vehicle)test.Vehicle).DrivingLessonsNum < Configuration.MIN_LESSONS)
                    throw new InvalidDataException("not enough lessons");
            }
            catch (InvalidDataException e) { throw; }
            catch (KeyNotFoundException e) { throw; }
            //-------------
            try
            {
                if (!dl.GetSomeTrainies(x => x.ID == test.TraineeId && x.TraineeVehicle == (DO.Vehicle)test.Vehicle).Any())
                    throw new InvalidDataException("you don't study this vehicle");
                if ((from item in dl.GetSomeTests(x => x.TraineeId == test.TraineeId)
                     where item.PassedTest == true
                     where item.Vehicle == (DO.Vehicle)test.Vehicle 
                     select item).ToList().Any())
                    throw new InvalidDataException("trainee passed a test on this vehicle");
            }
            catch (InvalidDataException e) { throw; }
        }
        //---------------------------------------------------------------------
        public List<DateTime> GetDateOfTests(DateTime fromDate, DateTime untilDate)
        {
            List<DateTime> dateTimes = new List<DateTime>();

            for (int i = fromDate.DayOfYear + 365 * fromDate.Year; i < untilDate.DayOfYear + 365 * untilDate.Year; ++i)
            {
                for (int j = 0; j <= 23; ++j)
                {
                    if (fromDate.Hour <= 14 && fromDate.Hour >= 9 && (int)fromDate.DayOfWeek < 5)
                        if (GetTestersByDate(fromDate).Count > 0)
                            dateTimes.Add(fromDate);
                    fromDate.AddHours(1);
                }
                fromDate.AddDays(1);
            }
            return dateTimes;
        }
        //------------------------------------------------------------------
        private bool CheckHoursInWeek(BO.Tester tester, DateTime hour)
        {
            int counter = 0;
            DateTime sunday = hour.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int)hour.DayOfWeek);
            DateTime friday = sunday.AddDays(4);
            //List<DateTime> times = (from test in tester.TesterTests
            //                        where test.TestDate > DateTime.Now
            //                        select test.TestHour).ToList();
            //foreach (var date in times)//all tests
            foreach (var test in tester.TesterTests)
                if (test.TestHour < friday && test.TestHour > sunday)
                    ++counter;

            if (counter == tester.MaxWeeklyTests)
                return false;
            return true;
        }
        //-------------------------------------------------------------------
        public void UpdateTestResult(int NumOfTest, bool[] result, string note = "")
        {
            try
            {
                bool summary = true;
                DO.Test test = dl.GetOneTest(NumOfTest);
                if (test.TestHour > DateTime.Now)
                    throw new InvalidDataException("Test didn't occur yet");
                for (int i = 0; i < result.Length - 1; ++i)
                    if (result[i] == false)
                        summary = false;
                if (summary != result[result.Length - 1])
                    throw new InvalidDataException("data and result are not matched");
            }
            catch (InvalidDataException e) { throw; }
            catch (KeyNotFoundException e) { throw; }
            dl.UpdateTestResult(NumOfTest, result, note);
        }
        //------------------------------------------------------------
        public BO.Test GetOneTest(int TestNum)
        {
            try
            {
                return Convert(dl.GetOneTest(TestNum));
            }
            catch (KeyNotFoundException e)
            {
                throw;
            }
        }
        //---------------------------------------------------------------------
        public List<BO.Tester> GetTesters()
        {
            try
            {
                List<DO.Tester> list = dl.GetTesters();
                var newList = list.Select(x => Convert(x)).ToList();
                return newList;
            }
            catch (ArgumentNullException e) { throw; }
        }
        //---------------------------------------------------------------------
        public List<BO.Trainee> GetTrainees()
        {
            try
            {
                List<DO.Trainee> list = dl.GetTrainees();
                var newList = list.Select(x => Convert(x)).ToList();
                return newList;
            }
            catch (ArgumentNullException e) { throw; }
        }
        //-------------------------------------------------------------------------
        public List<BO.Test> GetTests()
        {
            try
            {
                List<DO.Test> list = dl.GetTests();
                var newList = list.Select(x => Convert(x)).ToList();
                return newList;
            }
            catch (ArgumentNullException e) { throw; }
        }
        //--------------------------------------------------------------------------
        public Dictionary<string, object> GetConfig()
        {
            return dl.getConfig();
        }
        //-----------------------------------------------------------------------------
        public void SetConfig(string parm, object value)
        {
            try
            {
                dl.SetConfig(parm, value);
            }
            catch (InvalidOperationException e) { throw; }
            catch (KeyNotFoundException e) { throw; }
        }
        //-----------------------------------------------------------------------------

        public List<BO.Tester> GetCloseTester(BO.Address address, double x)
        {
            try
            {
                return (from item in dl.GetTesters()
                        where x < r.Next()
                        select Convert(item)).ToList();
            }
            catch (ArgumentNullException e) { throw; }
        }
        //---------------------------------------------------------------------        
        public List<BO.Test> GetSomeTests(Predicate<BO.Test> someFunc)
        {
            try
            {
                List<BO.Test> list = (from item in dl.GetTests()
                                      select Convert(item)).ToList();
                return (list.Where(x => someFunc(x)).ToList());
            }
            catch (ArgumentNullException e) { throw; }
        }
        //------------------------------------------------------------------------
        public int NumOfTest(string id, BO.Vehicle vehicle)
        {
            try
            {
                return Convert(dl.GetOneTrainee(id, (DO.Vehicle)vehicle)).Trainee_Test.Count;
            }
            catch (KeyNotFoundException e) { throw; }
        }
        //--------------------------------------------------------------------------
        public bool IfPassed(string id, BO.Vehicle vehicle)
        {
            try
            {
                return (from item in (Convert(dl.GetOneTrainee(id,(DO.Vehicle)vehicle))).Trainee_Test
                        where item.PassedTest == true && item.Vehicle==vehicle
                        select item).ToList().Any();
            }
            catch (KeyNotFoundException e) { throw; }
        }
        //--------------------------------------------------------------------
        public List<BO.Test> TestsPerDate(DateTime date)
        {
            try
            {
                return (from item in dl.GetTests()
                        where item.TestDate == date
                        select Convert(item)).ToList();
            }
            catch (ArgumentNullException e) { throw; }
        }

        public List<BO.Test> TestsPerMonth(DateTime date)
        {
            try
            {
                return (from item in dl.GetTests()
                        where item.TestDate.Year == date.Year && item.TestDate.Month == date.Month
                        select Convert(item)).ToList();
            }
            catch (ArgumentNullException e) { throw; }
        }
        //------------------------------------------------------------------
        public List<BO.TesterTest> GetFutureTestForTester(string ID)
        {
            return (from item in Convert(dl.GetOneTester(ID)).TesterTests
                    where item.TestHour > DateTime.Now
                    orderby item.TestHour
                    select item).ToList();
        }
        //----------------------------------------------------------------
        public List<IGrouping<BO.Vehicle, BO.Tester>> TestersByVehicle(bool flag)
        {
            if (flag)
            {
                return (from item in dl.GetTesters()
                        orderby item.FamilyName
                        group Convert(item) by Convert(item).TesterVehicle).ToList();
            }
            else
                return (from item in dl.GetTesters()
                        group Convert(item) by Convert(item).TesterVehicle).ToList();
        }
        //----------------------------------------------------------------------------
        public List<IGrouping<string, BO.Trainee>> TraineesBySchool(bool flag)
        {
            if (flag)
            {
                return (from item in dl.GetTrainees()
                        orderby item.FamilyName
                        group Convert(item) by Convert(item).School).ToList();
            }
            else
                return (from item in dl.GetTrainees()
                        group Convert(item) by Convert(item).School).ToList();

        }
        //-----------------------------------------------------------------------
        public List<IGrouping<string, BO.Trainee>> TraineesByTeacher(bool flag)
        {
            if (flag)
            {
                return (from item in dl.GetTrainees()
                        orderby item.FamilyName
                        group Convert(item) by Convert(item).Teacher).ToList();// into g
                                                                               //   select new { teacher = g.Key, name = g };

            }
            else
                return (from item in dl.GetTrainees()
                        group Convert(item) by Convert(item).Teacher).ToList();
        }
        //------------------------------------------------------------------------
        public List<IGrouping<int, BO.Trainee>> TraineesByTests(bool flag)
        {
            if (flag)
            {
                return (from item in dl.GetTrainees()
                        orderby item.FamilyName
                        group Convert(item) by Convert(item).Trainee_Test.Count).ToList();
            }
            else
                return (from item in dl.GetTrainees()
                        group Convert(item) by Convert(item).Trainee_Test.Count).ToList();
        }
        //-----------------------------------------------------------------
        private DO.Tester Convert(BO.Tester tester)
        {
            return new DO.Tester(tester.ID)
            {
                FamilyName = tester.FamilyName,
                PrivateName = tester.PrivateName,
                Phone = tester.Phone,
                DayOfBirth = tester.DayOfBirth,
                PersonGender = (DO.Gender)tester.PersonGender,
                PersonAddress = new DO.Address(tester.PersonAddress.City, tester.PersonAddress.Street, tester.PersonAddress.NumOfBuilding),
                TesterExperience = tester.TesterExperience,
                TesterVehicle = (DO.Vehicle)tester.TesterVehicle,
                MaxWeeklyTests = tester.MaxWeeklyTests,
                RangeToTest = tester.RangeToTest
            };
        }
        //------------------------------------------------------------------
        private DO.Trainee Convert(BO.Trainee trainee)
        {
            return new DO.Trainee(trainee.ID)
            {
                FamilyName = trainee.FamilyName,
                PrivateName = trainee.PrivateName,
                Phone = trainee.Phone,
                DayOfBirth = trainee.DayOfBirth,
                PersonGender = (DO.Gender)trainee.PersonGender,
                PersonAddress = new DO.Address(trainee.PersonAddress.City, trainee.PersonAddress.Street, trainee.PersonAddress.NumOfBuilding),
                TraineeVehicle = (DO.Vehicle)trainee.TraineeVehicle,
                TraineeGear = (DO.GearBox)trainee.TraineeGear,
                School = trainee.School,
                Teacher = trainee.Teacher,
                DrivingLessonsNum = trainee.DrivingLessonsNum,
            };
        }
        //------------------------------------------------------------------            
        private DO.Test Convert(BO.Test test)
        {
            return new DO.Test(test.Tester.ID, test.TraineeId)
            {
                Vehicle = (DO.Vehicle)test.Vehicle,
                TestDate = test.TestDate,
                TestHour = test.TestHour,
                TestAddress = new DO.Address(test.TestAddress.City, test.TestAddress.Street, test.TestAddress.NumOfBuilding),
                Mirrors = test.Mirrors,
                Brakes = test.Brakes,
                ReverseParking = test.ReverseParking,
                Distance = test.Distance,
                Vinkers = test.Vinkers,
                TrafficSigns = test.TrafficSigns,
                PassedTest = test.PassedTest,
                TesterNote = test.TesterNote,
            };
        }
        //--------------------------------------------------------------------------
        private BO.Tester Convert(DO.Tester tester)
        {
            return new BO.Tester
            {
                ID = tester.ID,
                FamilyName = tester.FamilyName,
                PrivateName = tester.PrivateName,
                DayOfBirth = tester.DayOfBirth,
                PersonGender = (BO.Gender)tester.PersonGender,
                Phone = tester.Phone,
                PersonAddress = new BO.Address(tester.PersonAddress.City, tester.PersonAddress.Street, tester.PersonAddress.NumOfBuilding),
                TesterExperience = tester.TesterExperience,
                MaxWeeklyTests = tester.MaxWeeklyTests,
                TesterVehicle = (BO.Vehicle)tester.TesterVehicle,
                RangeToTest = tester.RangeToTest,
                Schedule = dl.GetSchedule(tester.ID),
                TesterTests = GetTesterTests(dl.GetSomeTests(x => x.TesterId == tester.ID)),
            };
        }

        private List<BO.TesterTest> GetTesterTests(List<DO.Test> list)
        {
            List<BO.TesterTest> newList = new List<BO.TesterTest>();
            foreach (var item in list)
                newList.Add(new TesterTest()
                {
                    TestNumber = item.TestNumber,
                    TraineeId = item.TraineeId,
                    TestDate = item.TestDate,
                    TestHour = item.TestHour,
                    TestAddress = new BO.Address(item.TestAddress.City, item.TestAddress.Street, item.TestAddress.NumOfBuilding),
                    Mirrors = item.Mirrors,
                    Brakes = item.Brakes,
                    ReverseParking = item.ReverseParking,
                    Distance = item.Distance,
                    Vinkers = item.Vinkers,
                    TrafficSigns = item.TrafficSigns,
                    PassedTest = item.PassedTest,
                    TesterNote = item.TesterNote,
                });
            return newList;
        }
        //---------------------------------------------------------------------------
        private BO.Trainee Convert(DO.Trainee trainee)
        {
            return new BO.Trainee()
            {
                ID = trainee.ID,
                FamilyName = trainee.FamilyName,
                PrivateName = trainee.PrivateName,
                DayOfBirth = trainee.DayOfBirth,
                Phone = trainee.Phone,
                PersonGender = (BO.Gender)trainee.PersonGender,
                PersonAddress = new BO.Address(trainee.PersonAddress.City, trainee.PersonAddress.Street, trainee.PersonAddress.NumOfBuilding),
                TraineeVehicle = (BO.Vehicle)trainee.TraineeVehicle,
                TraineeGear = (BO.GearBox)trainee.TraineeGear,
                School = trainee.School,
                Teacher = trainee.Teacher,
                DrivingLessonsNum = trainee.DrivingLessonsNum,
                Trainee_Test = GetTraineeTests(dl.GetSomeTests(x => x.TraineeId == trainee.ID)),
            };
        }

        private List<BO.TraineeTest> GetTraineeTests(List<DO.Test> list)
        {
            List<BO.TraineeTest> newList = new List<TraineeTest>();
            foreach (var item in list)
                newList.Add(new TraineeTest()
                {
                    TestNumber = item.TestNumber,
                    Tester = new ExternalTester()
                    {
                        ID = item.TesterId,
                        PrivateName = dl.GetOneTester(item.TesterId).PrivateName,
                        FamilyName = dl.GetOneTester(item.TesterId).FamilyName,
                        TesterVehicle = (BO.Vehicle)dl.GetOneTester(item.TesterId).TesterVehicle,
                    },
                    Vehicle = (BO.Vehicle)item.Vehicle,
                    TestDate = item.TestDate,
                    TestHour = item.TestHour,
                    TestAddress = new BO.Address(item.TestAddress.City, item.TestAddress.Street, item.TestAddress.NumOfBuilding),
                    Mirrors = item.Mirrors,
                    Brakes = item.Brakes,
                    ReverseParking = item.ReverseParking,
                    Distance = item.Distance,
                    Vinkers = item.Vinkers,
                    TrafficSigns = item.TrafficSigns,
                    PassedTest = item.PassedTest,
                    TesterNote = item.TesterNote,
                });
            return newList;
        }
        //--------------------------------------------------------------------------
        private BO.Test Convert(DO.Test test)
        {
            return new BO.Test()
            {
                TestNumber = test.TestNumber,
                Tester = new ExternalTester()
                {
                    ID = test.TesterId,
                    PrivateName = dl.GetOneTester(test.TesterId).PrivateName,
                    FamilyName = dl.GetOneTester(test.TesterId).FamilyName,
                    TesterVehicle = (BO.Vehicle)dl.GetOneTester(test.TesterId).TesterVehicle,
                },
                TraineeId = test.TraineeId,
                TraineeName = string.Format(dl.GetOneTrainee(test.TraineeId, test.Vehicle).FamilyName + " " + dl.GetOneTrainee(test.TraineeId, test.Vehicle).PrivateName),
                Vehicle = (BO.Vehicle)test.Vehicle,
                TestDate = test.TestDate,
                TestHour = test.TestHour,
                TestAddress = new BO.Address(test.TestAddress.City, test.TestAddress.Street, test.TestAddress.NumOfBuilding),
                Mirrors = test.Mirrors,
                Brakes = test.Brakes,
                ReverseParking = test.ReverseParking,
                Distance = test.Distance,
                Vinkers = test.Vinkers,
                TrafficSigns = test.TrafficSigns,
                PassedTest = test.PassedTest,
                TesterNote = test.TesterNote,
            };
        }
        //-------------------------------------------------------------------------------------
        private void CheckName(string name)
        {
            try
            {
                if (name == "")
                    throw new InvalidDataException("name cannot be empty string");
            }
            catch (InvalidDataException e) { throw; }
        }
        //------------------------------------------------------------------------------
        private void CheckDate(DateTime time)
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
        //-------------------------------------------------------------------------
        private void CheckPhone(string phone)
        {
            try
            {
                if (phone[0] != '0' || phone.Length != 10)
                    throw new InvalidDataException("no valid phone number");
            }
            catch (InvalidDataException e) { throw; }
        }
        //--------------------------------------------------------------------
        private void CheckTesterExperience(int testerExperience)
        {
            try
            {
                if (testerExperience > Configuration.MAX_TESTER_AGE - Configuration.MIN_TESTER_AGE || testerExperience < 0)
                    throw new InvalidDataException("no valid num of tster's experience years");
            }
            catch (InvalidDataException e) { throw; }
        }
        //--------------------------------------------------------------------------
        private void CheckMaxWeeekltTests(int maxWeeklyTests)
        {
            try
            {
                if (maxWeeklyTests < 1 || maxWeeklyTests > 30)
                    throw new InvalidDataException("no valid maxWeeklyTests number");
            }
            catch (InvalidDataException e) { throw; }
        }
        //------------------------------------------------------------------------
        private void CheckVehicle(string vehicle)
        {
            try
            {
                if (vehicle != "privateCar" && vehicle != "motorcycle"
                            && vehicle != "truck" && vehicle != "heavyTruck")
                    throw new InvalidDataException("no valid vehicle");
            }
            catch (InvalidDataException e) { throw; }
        }
        //------------------------------------------------------------------------
        private void CheckScheduale(int day, int hour)
        {
            try
            {
                if (day < 1 || day > 5 || hour < 9 || hour > 14)
                    throw new InvalidDataException("hours operation are not matched");
            }
            catch (InvalidDataException e) { throw; }
        }
        //--------------------------------------------------------------------
        private void CheckGear(string gear)
        {
            try
            {
                if (gear != "auto" && gear != "manual")
                    throw new InvalidDataException("no valid gear");
            }
            catch (InvalidDataException e) { throw; }
        }
        //----------------------------------------------------------------------------
        private void CheckDrivingLessonsNum(int num)
        {
            try
            {
                if (num < Configuration.MIN_LESSONS)
                    throw new InvalidDataException("too few lessons");
            }
            catch (InvalidDataException e) { throw; }

        }
        //--------------------------------------------------------------------------------
        private void CheckAgeTester(int age)
        {
            try
            {
                if (age >= Configuration.MAX_TESTER_AGE)
                    throw new InvalidDataException("too old tester");
                if (age < Configuration.MIN_TESTER_AGE)
                    throw new InvalidDataException("too young tester");
            }
            catch (InvalidDataException e) { throw; }
        }
        //--------------------------------------------------------------------------------
        private void CheckDateTrainee(DateTime time)
        {
            try
            {
                if (time.Day < 1 || time.Day > 31)
                    throw new InvalidDataException("no valid day");
                if (time.Month < 1 || time.Month > 12)
                    throw new InvalidDataException("no valid month");
            }
            catch (InvalidDataException e) { throw; }
        }
        //------------------------------------------------------------------------------
        private void CheckID(string ID)
        {
            try
            {
                if (int.Parse(ID) > 999999999 || int.Parse(ID) < 10000000)
                    throw new InvalidDataException("no valid ID");
            }
            catch (InvalidDataException e) { throw; }
        }
        //---------------------------------------------------------------------------------
        private void CheckHour(DateTime hour)
        {
            try
            {
                if (hour.Hour < 9 || hour.Hour > 14)
                    throw new InvalidDataException("not at operation time");
                if ((int)hour.DayOfWeek > 4 || (int)hour.DayOfWeek < 0)
                    throw new InvalidDataException("we aren't working at weekend");
            }
            catch (InvalidDataException e) { throw; }
        }
        //-------------------------------------------------------------------------------
    }
}
