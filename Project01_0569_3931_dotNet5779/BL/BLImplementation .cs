﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DO;
using BO;

namespace BL
{
    public class BLImplementation : IBL
    {
        IDal dl = DAL_Factory.GetDL("lists");
        static Random r = new Random();
        //-------------------------------------------------------------
        public void AddTester(BO.Tester tester, bool[,] matrix)
        {
            tester.age = DateTime.Now.Year - tester.DayOfBirth.Year;
            try
            {
                if (tester.age < (int)BO.Configuration.MIN_TESTER_AGE)
                    throw new BO.InvalidDataException("Too young tester");
                if (tester.age > Configuration.MAX_TESTER_AGE)
                    throw new BO.InvalidDataException("Too old tester");//if if if
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
            {   // try { }//בדיקות תקינות
                switch (field)//מתי בודקים קלט
                {
                    case "familyName":
                        dl.UpdateTester(testerID, field, info);
                        break;
                    case "privateName":
                        dl.UpdateTester(testerID, field, info);
                        break;
                    case "dayOfBirth":
                        dl.UpdateTester(testerID, field, info);
                        break;
                    case "phone":
                        dl.UpdateTester(testerID, field, info);
                        break;
                    case "personAddress":
                        dl.UpdateTester(testerID, field, info);
                        break;
                    case "testerExperience":
                        dl.UpdateTester(testerID, field, info);
                        break;
                    case "maxWeeklyTests":
                        dl.UpdateTester(testerID, field, info);
                        break;
                    case "testerVehicle":
                        dl.UpdateTester(testerID, field, info);
                        break;
                    case "rangeToTest":
                        dl.UpdateTester(testerID, field, info);
                        break;
                    case "schedule":
                        dl.UpdateTester(testerID, field, info);
                        break;
                    default: throw new InvalidDataException("no such field");//למה הוספנו את זה???
                }
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
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
        public void DeleteTrainee(string TraineeID)
        {
            try
            {
                dl.DeleteTrainee(TraineeID);
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
                dl.UpdateTrainee(traineeID, field, info);
            }
            catch (KeyNotFoundException e) { throw; }
        }
        //---------------------------------------------------------------------
        public BO.Trainee GetOneTrainee(string ID)
        {
            try
            {
                return Convert(dl.GetOneTrainee(ID));
            }
            catch (KeyNotFoundException e)
            {
                throw;
            }
        }
        //------------------------------------------------------------------
        public void AddTest(BO.Test test)
        {
            var t = test.TestDate - ((from item in dl.GetSomeTests(x => x.TraineeId == test.TraineeId)
                                      where item.TestDate < DateTime.Now
                                      orderby item.TestDate descending
                                      select item.TestDate).ToList().FirstOrDefault());
            try
            {
                if (t.Days < BO.Configuration.MIN_GAP_TEST)
                    throw new InvalidDataException("test too close");
            }
            catch (InvalidDataException e) { throw; }
            try
            {
                if (dl.GetOneTrainee(test.TraineeId).DrivingLessonsNum < Configuration.MIN_LESSONS)
                    throw new InvalidDataException("not enough lessons");
            }
            catch (InvalidDataException e) { throw; }

            List<BO.Tester> optionaltesters = new List<BO.Tester>();
            var m = (from item in dl.GetTesters()
                     where dl.GetSchedule(item.ID)[test.TestHour.Day - 1, test.TestHour.Hour - 9]==true
                     select item).ToList();
            if (!m.Any())
                throw new InvalidDataException("bad time to test");
            else

                try
                {
                    dl.AddTest(Convert(test));
                }
                catch (KeyNotFoundException e) { throw; }
        }


        //------------------------------------------------------------------
        public void UpdateTestResult(int NumOfTest, string field, object result)
        {

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
        public Dictionary<string, object> getConfig()
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
        public List<BO.Tester> GetCloseTester(BO.Address address, double x)
        {             
            return(from item in dl.GetTesters()
            where x < r.Next()
            select Convert(item)).ToList();            
        }
        //---------------------------------------------------------------------
        public List<BO.Test> GetSomeTests(Predicate<BO.Test> someFunc)
        {

            List<BO.Test> list = (from item in dl.GetTests()
                                  select Convert(item)).ToList();
            return (list.Where(x => someFunc(x)).Select(x => x)).ToList();                                     
        }
        //------------------------------------------------------------------------
        public int NumOfTest(string id)
        {
            return Convert(dl.GetOneTrainee(id)).Trainee_Test.Count;
        }
        //--------------------------------------------------------------------------
        public bool IfPassed(string id)
        {
            return (from item in Convert(dl.GetOneTrainee(id)).Trainee_Test
                    where item.PassedTest == true
                    select item).ToList().Any();
        }
        //--------------------------------------------------------------------
        public List<BO.Test> TestsPerDate(DateTime date)
        {
            return (from item in dl.GetTests()
                     where item.TestDate == date
                     select Convert(item)).ToList();
        }

        public List<BO.Test> TestsPerMonth(DateTime date)
        {
            return (from item in dl.GetTests()
                    where item.TestDate.Year == date.Year && item.TestDate.Month==date.Month
                    select Convert(item)).ToList();
        }
        //-----------------------------------------------------------------
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
            List<BO.TesterTest> newList = new List<TesterTest>();
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
                TraineeName = string.Format(dl.GetOneTrainee(test.TraineeId).FamilyName + " " + dl.GetOneTrainee(test.TraineeId).PrivateName),
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
    }
}

