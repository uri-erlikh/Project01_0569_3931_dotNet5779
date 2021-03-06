﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL
{

    class DAL_XML_IMP : DO.BaseDL
    {
        static DAL_XML_IMP instance = null;
        public volatile bool updated = false;


        public static DAL_XML_IMP GetInstance()
        {
            if (instance == null)
                instance = new DAL_XML_IMP();
            return instance;
        }

        XElement testerRoot;
        string testerPath = @"TesterXml.xml";

        XElement traineeRoot;
        string traineePath = @"TraineeXml.xml";


        XElement testRoot;
        string testPath = @"TestXml.xml";

        XElement scheduleRoot;
        string schedulePath = @"ScheduleXML.xml";

        XElement configRoot;
        string configPath = @"Config.xml";

        private DAL_XML_IMP()
        {
            Thread forConfigThread = new Thread(checkFlag);
            forConfigThread.Start();


            //File.Delete(traineePath);
            //File.Delete(testerPath);
            // File.Delete(testPath);
            // File.Delete(configPath);
            if (!File.Exists(testerPath))
                CreateFiles("testers");

            if (!File.Exists(traineePath))
                CreateFiles("trainees");

            if (!File.Exists(testPath))
                CreateFiles("tests");

            // File.Delete(schedulePath);
            if (!File.Exists(schedulePath))
                CreateFiles("schedule");

            if (!File.Exists(configPath))
                CreateConfig();
        }
        //---------------------------------------------------------
        private void CreateFiles(string identifier)
        {
            switch (identifier)
            {
                case "testers":
                    testerRoot = new XElement("testers");
                    testerRoot.Save(testerPath);
                    break;
                case "trainees":
                    traineeRoot = new XElement("trainees");
                    traineeRoot.Save(traineePath);
                    break;
                case "tests":
                    testRoot = new XElement("tests");
                    testRoot.Save(testPath);
                    break;
                case "schedule":
                    scheduleRoot = new XElement("schedules");
                    scheduleRoot.Save(schedulePath);
                    break;
            }
        }
        //------------------------------------------------------------
        private void CreateConfig()
        {
            XElement configRoot = new XElement("config");
            XElement MIN_LESSONS = new XElement("MIN_LESSONS", new XElement("Readable", true), new XElement("Writable", true), new XElement("value", 28));
            XElement MAX_TESTER_AGE = new XElement("MAX_TESTER_AGE", new XElement("Readable", true), new XElement("Writable", true), new XElement("value", 67));
            XElement MIN_TRAINEE_AGE = new XElement("MIN_TRAINEE_AGE", new XElement("Readable", true), new XElement("Writable", true), new XElement("value", 16));
            XElement MIN_GAP_TEST = new XElement("MIN_GAP_TEST", new XElement("Readable", true), new XElement("Writable", true), new XElement("value", 30));
            XElement MIN_TESTER_AGE = new XElement("MIN_TESTER_AGE", new XElement("Readable", true), new XElement("Writable", true), new XElement("value", 40));
            XElement NumberTest = new XElement("NumberTest", new XElement("Readable", true), new XElement("Writable", false), new XElement("value", 10000000));
            configRoot.Add(MIN_LESSONS, MAX_TESTER_AGE, MIN_TRAINEE_AGE, MIN_GAP_TEST, MIN_TESTER_AGE, NumberTest);
            configRoot.Save(configPath);
        }
        //-----------------------------------------------------------
        private void LoadData(string identifier)
        {
            switch (identifier)
            {
                case "testers":
                    try
                    {
                        testerRoot = XElement.Load(testerPath);
                        break;
                    }
                    catch
                    {
                        throw new KeyNotFoundException("File upload problem - testers");
                    }
                case "trainees":
                    try
                    {
                        traineeRoot = XElement.Load(traineePath);
                        break;
                    }
                    catch (NullReferenceException e) { throw new KeyNotFoundException(e.Message); }//
                    catch
                    {
                        throw new KeyNotFoundException("File upload problem - trainees");
                    }
                case "tests":
                    try
                    {
                        testRoot = XElement.Load(testPath);
                        break;
                    }
                    catch
                    {
                        throw new KeyNotFoundException("File upload problem - tests");
                    }

                case "schedule":
                    try
                    {
                        scheduleRoot = XElement.Load(schedulePath);
                        break;
                    }
                    catch
                    {
                        throw new KeyNotFoundException("File upload problem - schedules");
                    }

                case "config":
                    try
                    {
                        configRoot = XElement.Load(configPath);
                        break;

                    }
                    catch
                    {
                        throw new KeyNotFoundException("File upload problem - config");
                    }
            }
        }
        //-------------------------------------------------------------------       
        private void SaveOneTester(DO.Tester tester)
        {
            XElement ID = new XElement("ID", tester.ID);
            XElement familyName = new XElement("familyName", tester.FamilyName);
            XElement privateName = new XElement("privateName", tester.PrivateName);
            XElement dayOfBirth = new XElement("dayOfBirth", tester.DayOfBirth);
            XElement personGender = new XElement("personGender", tester.PersonGender);
            XElement phone = new XElement("phone", tester.Phone);
            XElement city = new XElement("city", tester.PersonAddress.City);
            XElement street = new XElement("street", tester.PersonAddress.Street);
            XElement numOfBuilding = new XElement("numOfBuilding", tester.PersonAddress.NumOfBuilding);
            XElement personAddress = new XElement("personAddress", city, street, numOfBuilding);
            XElement person = new XElement("person", ID, familyName, privateName, dayOfBirth, personGender, phone, personAddress);
            XElement testerExperience = new XElement("testerExperience", tester.TesterExperience);
            XElement maxWeeklyTests = new XElement("maxWeeklyTests", tester.MaxWeeklyTests);
            XElement testerVehicle = new XElement("testerVehicle", tester.TesterVehicle.ToString());
            XElement rangeToTest = new XElement("rangeToTest", tester.RangeToTest);
            testerRoot.Add(new XElement("tester", person, testerExperience, maxWeeklyTests, testerVehicle, rangeToTest));
        }

        //-------------------------------------------------------------------
        private List<DO.Tester> GetTestersList()
        {
            List<DO.Tester> myTesters = new List<DO.Tester>();
            try
            {
                myTesters = (from tester in testerRoot.Elements()
                             select new DO.Tester(tester.Element("person").Element("ID").Value)
                             {
                                 FamilyName = tester.Element("person").Element("familyName").Value,
                                 PrivateName = tester.Element("person").Element("privateName").Value,
                                 DayOfBirth = DateTime.Parse(tester.Element("person").Element("dayOfBirth").Value),
                                 PersonGender = (DO.Gender)Enum.Parse(typeof(DO.Gender), tester.Element("person").Element("personGender").Value),
                                 Phone = tester.Element("person").Element("phone").Value,
                                 PersonAddress = new DO.Address(tester.Element("person").Element("personAddress").Element("city").Value,
                                 tester.Element("person").Element("personAddress").Element("street").Value,
                                 int.Parse(tester.Element("person").Element("personAddress").Element("numOfBuilding").Value)),
                                 TesterExperience = int.Parse(tester.Element("testerExperience").Value),
                                 MaxWeeklyTests = int.Parse(tester.Element("maxWeeklyTests").Value),
                                 TesterVehicle = (DO.Vehicle)Enum.Parse(typeof(DO.Vehicle), tester.Element("testerVehicle").Value),
                                 RangeToTest = int.Parse(tester.Element("rangeToTest").Value),
                             }).ToList();
            }
            catch (FileLoadException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return myTesters;
        }
        //------------------------------------------------------------------        
        private void SaveOneTrainee(DO.Trainee trainee)
        {
            XElement ID = new XElement("ID", trainee.ID);
            XElement familyName = new XElement("familyName", trainee.FamilyName);
            XElement privateName = new XElement("privateName", trainee.PrivateName);
            XElement dayOfBirth = new XElement("dayOfBirth", trainee.DayOfBirth);
            XElement personGender = new XElement("personGender", trainee.PersonGender);
            XElement phone = new XElement("phone", trainee.Phone);
            XElement city = new XElement("city", trainee.PersonAddress.City);
            XElement street = new XElement("street", trainee.PersonAddress.Street);
            XElement numOfBuilding = new XElement("numOfBuilding", trainee.PersonAddress.NumOfBuilding);
            XElement personAddress = new XElement("personAddress", city, street, numOfBuilding);
            XElement person = new XElement("person", ID, familyName, privateName, dayOfBirth, personGender, phone, personAddress);
            XElement traineeVehicle = new XElement("traineeVehicle", trainee.TraineeVehicle.ToString());
            XElement traineeGear = new XElement("traineeGear", trainee.TraineeGear.ToString());
            XElement school = new XElement("school", trainee.School);
            XElement teacher = new XElement("teacher", trainee.Teacher);
            XElement drivingLessonsNum = new XElement("drivingLessonsNum", trainee.DrivingLessonsNum);
            traineeRoot.Add(new XElement("trainee", person, traineeVehicle, traineeGear, school, teacher, drivingLessonsNum));
        }
        //---------------------------------------------------------------- 
        private List<DO.Trainee> GetTraineesList()
        {
            List<DO.Trainee> myTrainees = new List<DO.Trainee>();
            try
            {
                myTrainees = (from trainee in traineeRoot.Elements()
                              select new DO.Trainee(trainee.Element("person").Element("ID").Value)
                              {
                                  FamilyName = trainee.Element("person").Element("familyName").Value,
                                  PrivateName = trainee.Element("person").Element("privateName").Value,
                                  DayOfBirth = DateTime.Parse(trainee.Element("person").Element("dayOfBirth").Value),
                                  PersonGender = (DO.Gender)Enum.Parse(typeof(DO.Gender), trainee.Element("person").Element("personGender").Value),
                                  Phone = trainee.Element("person").Element("phone").Value,
                                  PersonAddress = new DO.Address(trainee.Element("person").Element("personAddress").Element("city").Value,
                                                   trainee.Element("person").Element("personAddress").Element("street").Value,
                                                   int.Parse(trainee.Element("person").Element("personAddress").Element("numOfBuilding").Value)),
                                  TraineeVehicle = (DO.Vehicle)Enum.Parse(typeof(DO.Vehicle), trainee.Element("traineeVehicle").Value),
                                  TraineeGear = (DO.GearBox)Enum.Parse(typeof(DO.GearBox), trainee.Element("traineeGear").Value),
                                  School = trainee.Element("school").Value,
                                  Teacher = trainee.Element("teacher").Value,
                                  DrivingLessonsNum = int.Parse(trainee.Element("drivingLessonsNum").Value),
                              }).ToList();
            }
            catch (FileLoadException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return myTrainees;
        }
        //---------------------------------------------------------------       
        private void SaveOneTest(DO.Test test)
        {
            XElement testNumber = new XElement("testNumber", test.TestNumber);
            XElement IDTester = new XElement("IDTester", test.TesterId);
            XElement IDTrainee = new XElement("IDTrainee", test.TraineeId);
            XElement vehicle = new XElement("vehicle", test.Vehicle.ToString());
            XElement testDate = new XElement("testDate", test.TestDate);
            XElement testHour = new XElement("testHour", test.TestHour);
            XElement city = new XElement("city", test.TestAddress.City);
            XElement street = new XElement("street", test.TestAddress.Street);
            XElement numOfBuilding = new XElement("numOfBuilding", test.TestAddress.NumOfBuilding);
            XElement testAddress = new XElement("testAddress", city, street, numOfBuilding);
            XElement mirrors = new XElement("mirrors", test.Mirrors);
            XElement brakes = new XElement("brakes", test.Brakes);
            XElement reverseParking = new XElement("reverseParking", test.ReverseParking);
            XElement distance = new XElement("distance", test.Distance);
            XElement vinkers = new XElement("vinkers", test.Vinkers);
            XElement trafficSigns = new XElement("trafficSigns", test.TrafficSigns);
            XElement passedTest = new XElement("passedTest", test.PassedTest);
            XElement testerNote = new XElement("testerNote", test.TesterNote);

            testRoot.Add(new XElement("test", testNumber, IDTester, IDTrainee, vehicle, testDate, testHour,
                testAddress, mirrors, brakes, reverseParking, distance, vinkers, trafficSigns, passedTest, testerNote));
        }
        //---------------------------------------------------------------
        private List<DO.Test> GetTestsList()
        {
            List<DO.Test> myTests = new List<DO.Test>();
            try
            {
                myTests = (from test in testRoot.Elements()
                           select new DO.Test(test.Element("IDTester").Value, test.Element("IDTrainee").Value)
                           {
                               TestNumber = int.Parse(test.Element("testNumber").Value),
                               Vehicle = (DO.Vehicle)Enum.Parse(typeof(DO.Vehicle), test.Element("vehicle").Value),
                               TestDate = DateTime.Parse(test.Element("testDate").Value),
                               TestHour = DateTime.Parse(test.Element("testHour").Value),
                               TestAddress = new DO.Address(test.Element("testAddress").Element("city").Value,
                                 test.Element("testAddress").Element("street").Value,
                                 int.Parse(test.Element("testAddress").Element("numOfBuilding").Value)),
                               Mirrors = bool.Parse(test.Element("mirrors").Value),
                               Brakes = bool.Parse(test.Element("brakes").Value),
                               ReverseParking = bool.Parse(test.Element("reverseParking").Value),
                               Distance = bool.Parse(test.Element("distance").Value),
                               Vinkers = bool.Parse(test.Element("vinkers").Value),
                               TrafficSigns = bool.Parse(test.Element("trafficSigns").Value),
                               PassedTest = bool.Parse(test.Element("passedTest").Value),
                               TesterNote = test.Element("testerNote").Value,
                           }).ToList();
            }
            catch (FileLoadException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return myTests;
        }
        //----------------------------------------------------------------
        private bool IfExist(string ID, string type)
        {
            bool check1 = false;
            switch (type)
            {
                case "tester":
                    if (testerRoot.Elements().Any())
                        foreach (var item in testerRoot.Elements())
                        {
                            if (item.Element("person").Element("ID").Value == ID)
                                check1 = true;
                        }
                    break;
                case "trainee":
                    if (traineeRoot.Elements().Any())
                        foreach (var item in traineeRoot.Elements())
                            if (item.Element("person").Element("ID").Value == ID)
                                check1 = true;                   
                    break;
            }
            return check1;
        }
        //----------------------------------------------------------------------------
        public override void AddTester(DO.Tester tester, bool[,] matrix)
        {
            try
            {
                LoadData("testers");
                if (IfExist(tester.ID, "tester"))
                    throw new DuplicateWaitObjectException("tester allready exist");
            }
            catch (DuplicateWaitObjectException e) { throw; }


            SaveOneTester(tester);
            testerRoot.Save(testerPath);
            SetSchedule(matrix, tester.ID);
        }
        //--------------------------------------------------------------------
        public override void DeleteTester(string testerID)
        {
            LoadData("testers");
            LoadData("tests");
            try
            {
                if (IfExist(testerID, "tester"))
                {
                    (from tester in testerRoot.Elements()
                     where tester.Element("person").Element("ID").Value == testerID
                     select tester).FirstOrDefault().Remove();
                }
                else
                    throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException e) { throw; }

            (from item in testRoot.Elements()
             where item.Element("IDTester").Value == testerID
             select item).Remove();

            testerRoot.Save(testerPath);
            testRoot.Save(testPath);
        }
        //-------------------------------------------------------------------
        public override DO.Tester GetOneTester(string testerID)
        {
            try
            {
                LoadData("testers");
                if (!IfExist(testerID, "tester"))
                    throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException e) { throw; }

            List<DO.Tester> testers = GetTestersList();
            foreach (var item in testers)
            {
                if (item.ID == testerID)
                    return new DO.Tester(item);
            }
            return null;
        }
        //----------------------------------------------------------------------
        public override void UpdateTester(DO.Tester tester)//המטריצה נשלחת לפונקציה בנפרד בשכבה מעל
        {
            try
            {
                LoadData("testers");
                if (!IfExist(tester.ID, "tester"))
                    throw new KeyNotFoundException("ID not found");

                XElement testerElement = (from myTester in testerRoot.Elements()
                                          where myTester.Element("person").Element("ID").Value == tester.ID
                                          select myTester).FirstOrDefault();
                testerElement.Remove();
                SaveOneTester(tester);
                testerRoot.Save(testerPath);
            }
            catch (KeyNotFoundException e) { throw; }
        }
        //---------------------------------------------------------------------
        public override void AddTrainee(DO.Trainee trainee)
        {
            try
            {
                LoadData("trainees");
                bool tr = false;
                foreach (var item in traineeRoot.Elements())
                    if (item.Element("person").Element("ID").Value == trainee.ID && item.Element("traineeVehicle").Value == trainee.TraineeVehicle.ToString())
                        tr = true;
                if (tr == true || IfExist(trainee.ID, "tester"))
                    throw new DuplicateWaitObjectException("allready exist");
                SaveOneTrainee(trainee);
                traineeRoot.Save(traineePath);
            }
            catch (DuplicateWaitObjectException) { throw; }
        }
        //-----------------------------------------------------------------------
        public override DO.Trainee GetOneTrainee(string ID, DO.Vehicle vehicle)
        {
            try
            {

                LoadData("trainees");
                if (!IfExist(ID, "trainee"))
                    throw new KeyNotFoundException("ID not found");
                List<DO.Trainee> trainees = GetTraineesList();
                foreach (var item in trainees)
                {
                    if (item.ID == ID && item.TraineeVehicle == vehicle)
                        return new DO.Trainee(item);
                }
                throw new KeyNotFoundException("no match between trainee and vehicle type");
            }
            catch (KeyNotFoundException e) { throw; }

        }
        //-----------------------------------------------------------------------
        public override void DeleteTrainee(string traineeID, DO.Vehicle vehicle)
        {
            try
            {
                LoadData("trainees");
                LoadData("tests");

                if (!IfExist(traineeID, "trainee"))
                    throw new KeyNotFoundException("ID not found");
                (from trainee in traineeRoot.Elements()
                 where trainee.Element("person").Element("ID").Value == traineeID
                 where trainee.Element("traineeVehicle").Value == vehicle.ToString()
                 select trainee).FirstOrDefault().Remove();
            }
            catch (KeyNotFoundException e) { throw; }
            catch (InvalidOperationException)
            { throw new KeyNotFoundException("trainee doesn't study this vehicle"); }

            (from item in testRoot.Elements()
             where item.Element("IDTrainee").Value == traineeID
             where item.Element("vehicle").Value == vehicle.ToString()
             select item).Remove();
            traineeRoot.Save(traineePath);
            testRoot.Save(testPath);
        }
        //-------------------------------------------------------------------------
        public override void UpdateTrainee(DO.Trainee trainee)
        {
            try
            {
                LoadData("trainees");
                if (!IfExist(trainee.ID, "trainee"))
                    throw new KeyNotFoundException("ID not found");

                (from myTrainee in traineeRoot.Elements()
                 where myTrainee.Element("person").Element("ID").Value == trainee.ID
                 where myTrainee.Element("traineeVehicle").Value == trainee.TraineeVehicle.ToString()
                 select myTrainee).Remove();
                SaveOneTrainee(trainee);

                foreach (var item in traineeRoot.Elements())
                {
                    if (item.Element("person").Element("ID").Value == trainee.ID && item.Element("traineeVehicle").Value != trainee.TraineeVehicle.ToString())
                    {
                        item.Element("person").Element("privateName").Value = trainee.PrivateName;
                        item.Element("person").Element("familyName").Value = trainee.FamilyName;
                        item.Element("person").Element("personAddress").Element("city").Value = trainee.PersonAddress.City;
                        item.Element("person").Element("personAddress").Element("street").Value = trainee.PersonAddress.Street;
                        item.Element("person").Element("personAddress").Element("numOfBuilding").Value = trainee.PersonAddress.NumOfBuilding.ToString();
                        item.Element("person").Element("phone").Value = trainee.Phone;
                    }
                }
                traineeRoot.Save(traineePath);
            }
            catch (KeyNotFoundException e) { throw; }
            catch (NullReferenceException e)
            {
                throw new KeyNotFoundException("the trainee doesn't study this vehicle");
            }
        }
        //---------------------------------------------------------------------------
        public override string AddTest(DO.Test test)
        {
            try
            {
                LoadData("tests");
                LoadData("testers");
                LoadData("trainees");
                if (!IfExist(test.TesterId, "tester"))
                    throw new KeyNotFoundException("TesterID not found");
                if (!IfExist(test.TraineeId, "trainee"))
                    throw new KeyNotFoundException("TraineeID not found");
            }
            catch (KeyNotFoundException e) { throw; }
            LoadData("config");
            test.TestNumber = int.Parse(configRoot.Element("NumberTest").Element("value").Value);
            configRoot.Element("NumberTest").Element("value").SetValue(int.Parse(configRoot.Element("NumberTest").Element("value").Value) + 1);
            SaveOneTest(test);
            testRoot.Save(testPath);
            configRoot.Save(configPath);
            return "your number test is" + test.TestNumber;
        }
        //-------------------------------------------------------------------------
        public override void UpdateTestResult(DO.Test test1)
        {
            try
            {
                LoadData("tests");

                XElement testElement = (from myTest in testRoot.Elements()
                                        where int.Parse(myTest.Element("testNumber").Value) == test1.TestNumber
                                        select myTest).FirstOrDefault();
                if (testElement == null)
                    throw new KeyNotFoundException("test not found in xml");
                testElement.Remove();
                SaveOneTest(test1);
                testRoot.Save(testPath);
            }
            catch (ArgumentNullException e) { throw; }
            catch (KeyNotFoundException e) { throw; }
        }
        //------------------------------------------------------------------------------
        private bool IfTestExist(int numOfTest)
        {
            foreach (var item in testerRoot.Elements())
                if (item.Element("testNumber").Value == numOfTest.ToString())
                    return true;
            return false;
        }
        //------------------------------------------------------------------------------
        public override DO.Test GetOneTest(int testNum)
        {
            try
            {
                LoadData("tests");
                if (!IfTestExist(testNum))
                    throw new KeyNotFoundException("no such test");
            }
            catch (KeyNotFoundException e) { throw; }
            List<DO.Test> tests = GetTestsList();
            foreach (var item in tests)
                if (item.TestNumber == testNum)
                    return new DO.Test(item);
            return null;
        }
        //-------------------------------------------------------------------------------
        public override void DeleteTest(int numOfTest)
        {
            LoadData("tests");
            XElement testElement;
            try
            {
                if (!IfTestExist(numOfTest))
                    throw new KeyNotFoundException("no such test");
                testElement = (from test in testRoot.Elements()
                               where int.Parse(test.Element("testNumber").Value) == numOfTest
                               select test).FirstOrDefault();
                testElement.Remove();
            }
            catch (KeyNotFoundException e) { throw; }
            testRoot.Save(testPath);
        }
        //---------------------------------------------------------------------------------
        public override List<DO.Tester> GetTesters()
        {
            LoadData("testers");
            return GetTestersList();
            //return (from item in this.testers
            //        select new DO.Tester(item)).ToList();
        }
        //-------------------------------------------------------------------------
        public override List<DO.Trainee> GetTrainees()
        {
            LoadData("trainees");
            return GetTraineesList();
            //return (from item in this.trainees
            //        select new DO.Trainee(item)).ToList();
        }
        //----------------------------------------------------
        public override List<DO.Test> GetTests()
        {
            LoadData("tests");
            return GetTestsList();
            //return (from item in this.tests
            //        select new DO.Test(item)).ToList();
        }
        //---------------------------------------------------------
        public override List<DO.Test> GetSomeTests(Predicate<DO.Test> someFunc)
        {
            LoadData("tests");
            List<DO.Test> tests = GetTestsList();
            return (from item in tests
                    where (someFunc(item))
                    select new DO.Test(item)).ToList();
        }
        //--------------------------------------------------
        public override List<DO.Tester> GetSomeTesters(Predicate<DO.Tester> func)
        {
            LoadData("testers");
            List<DO.Tester> testers = GetTestersList();
            var NewList = from item in testers
                          where (func(item))
                          select new DO.Tester(item);
            return NewList.ToList();
        }
        //--------------------------------------------------
        public override List<DO.Trainee> GetSomeTrainies(Predicate<DO.Trainee> func)
        {
            LoadData("trainees");
            List<DO.Trainee> trainees = GetTraineesList();
            var NewList = from item in trainees
                          where (func(item))
                          select new DO.Trainee(item);
            return NewList.ToList();
        }
        //------------------------------------------------------------------
        public override Dictionary<String, Object> GetConfig()
        {
            LoadData("config");
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var newDict = from item in configRoot.Elements()
                          where item.Name.LocalName != "NumberTest"
                          where item.Element("Readable").Value == "true"
                          select new { key = item.Name.LocalName, value = item.Element("value").Value };
            foreach (var item in newDict)
                dictionary.Add(item.key, item.value as object);
            return dictionary;
        }
        //-----------------------------------------------------------------------
        public override void SetConfig(string parm, object value)
        {
            try
            {
                LoadData("config");
                var tempArray = (from item in configRoot.Elements()
                                 where item.Name.LocalName == parm
                                 select item.Name.LocalName).ToList();
                if (tempArray.Count == 0)
                    throw new KeyNotFoundException("key not found");
                if (configRoot.Element(parm).Element("Writable").Value == "false")
                    throw new InvalidOperationException("it is non-writable value");
            }
            catch (InvalidOperationException e) { throw; }
            catch (KeyNotFoundException e) { throw; }
            this.configRoot.Element(parm).Element("value").Value = value.ToString();
            this.configRoot.Save(configPath);
            updated = true;
        }

        private void checkFlag()
        {
            while (true)
            {
                if (updated == true)
                {
                    updated = false;
                    instance?.ConfigUpdated();
                }
                Thread.Sleep(1000);
            }
        }
        //---------------------------------------------------------------------
        public override bool[,] GetSchedule(string ID)
        {
            try
            {
                LoadData("testers");
                if (!IfExist(ID, "tester"))
                    throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException) { throw; }

            LoadData("schedule");

            XElement schedule = (from item in scheduleRoot.Elements()
                                 where item.Element("testerID").Value == ID
                                 select item).FirstOrDefault();

            bool[,] mat = new bool[5, 6];
            if (!(schedule == null))
            {
                for (int i = 0; i < 5; ++i)
                    for (int j = 0; j < 6; ++j)
                        mat[i, j] = bool.Parse(schedule.Element("day_" + i).Element("hour_" + j).Value);
            }
            return mat;
        }
        //-----------------------------------------------------------------------
        public override void SetSchedule(bool[,] matrix, string testerID)
        {
            try
            {
                LoadData("testers");
                LoadData("schedule");

                if (IfExist(testerID, "tester") && scheduleRoot.Elements().Any())
                {
                    XElement sc = (from item in scheduleRoot.Elements()
                                   where item.Element("testerID").Value == testerID
                                   select item).FirstOrDefault();
                    if (!(sc == null))
                        sc.Remove();
                }
                //throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException) { throw; }

            XElement schedule = new XElement("schedule", new XElement("testerID", testerID));
            for (int i = 0; i < 5; ++i)
            {
                XElement day = new XElement("day_" + i);
                for (int j = 0; j < 6; ++j)
                {
                    XElement hour = new XElement("hour_" + j, matrix[i, j]);
                    day.Add(hour);
                }
                schedule.Add(day);
            }
            scheduleRoot.Add(schedule);
            scheduleRoot.Save(schedulePath);
        }


    };
}
