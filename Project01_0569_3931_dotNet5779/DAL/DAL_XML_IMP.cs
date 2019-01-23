using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
//tVezKd2ywDz9DAzMAwVhzCecXPSErYc4
namespace DAL
{

    class DAL_XML_IMP : IDal
    {
        static DAL_XML_IMP instance = null;

        public static DAL_XML_IMP GetInstance()
        {
            if (instance == null)
                instance = new DAL_XML_IMP();
            return instance;
        }

        XElement testerRoot;
        string testerPath = @"TesterXml.xml";
        List<DO.Tester> testers = new List<DO.Tester>();

        XElement traineeRoot;
        string traineePath = @"TraineeXml.xml";
        List<DO.Trainee> trainees = new List<DO.Trainee>();


        XElement testRoot;
        string testPath = @"TestXml.xml";
        List<DO.Test> tests = new List<DO.Test>();

        XElement scheduleRoot;
        string schedulePath = @"ScheduleXML.xml";

        XElement configRoot;
        string configPath = @"Config.xml";

        private DAL_XML_IMP()
        {
            if (!File.Exists(testerPath))
                CreateFiles("testers");

            if (!File.Exists(traineePath))
                CreateFiles("trainees");

            if (!File.Exists(testPath))
                CreateFiles("tests");

            if (!File.Exists(schedulePath))
                CreateFiles("schedule");

            if (!File.Exists(configPath))
                CreateConfig();
        }
        //---------------------------------------------------------
        private void CreateFiles(string identifier)
        {
            File.Delete(testerPath);
            File.Delete(traineePath);
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
            XElement MIN_LESSONS = new XElement("MIN_LESSONS", new XElement("Readable", true), new XElement("Writable", false), new XElement("value", 28));
            XElement MAX_TESTER_AGE = new XElement("MAX_TESTER_AGE", new XElement("Readable", true), new XElement("Writable", false), new XElement("value", 67));
            XElement MIN_TRAINEE_AGE = new XElement("MIN_TRAINEE_AGE", new XElement("Readable", true), new XElement("Writable", false), new XElement("value", 16));
            XElement MIN_GAP_TEST = new XElement("MIN_GAP_TEST", new XElement("Readable", true), new XElement("Writable", false), new XElement("value", 30));
            XElement MIN_TESTER_AGE = new XElement("MIN_TESTER_AGE", new XElement("Readable", true), new XElement("Writable", false), new XElement("value", 40));
            XElement NumberTest = new XElement("NumberTest", new XElement("Readable", true), new XElement("Writable", true), new XElement("value", 10000000));
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
                        testers = GetTestersList();
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
                        trainees = GetTraineesList();
                        break;
                    }
                    catch
                    {
                        throw new KeyNotFoundException("File upload problem - trainees");
                    }
                // catch (NullReferenceException) { throw new KeyNotFoundException("problem"); }//
                case "tests":
                    try
                    {
                        testRoot = XElement.Load(testPath);
                        tests = GetTestsList();
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
        //private void SaveTestersList(List<DO.Tester> testersList)
        //{
        //    testerRoot = new XElement("testers");

        //    foreach (var tester in testersList)
        //    {
        //        SaveOneTester(tester);
        //    }
        //    testerRoot.Save(testerPath);
        //}

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
            //XElement schdule = new XElement("schedule",tester.matrix);
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
                                 //schedule
                             }).ToList();
            }
            catch (FileLoadException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return myTesters;
        }
        //------------------------------------------------------------------
        //private void SaveTraineeList(List<DO.Trainee> traineeList)
        //{
        //    traineeRoot = new XElement("trainees");

        //    foreach (var trainee in traineeList)
        //    {
        //        SaveOneTrainee(trainee);
        //    }
        //    traineeRoot.Save(traineePath);
        //}

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
                //if (myTrainees == null)
                //  throw (new NullReferenceException("problem"));
            }
            catch (FileLoadException e)
            {
                throw new KeyNotFoundException(e.Message);
            }
            return myTrainees;
        }
        //---------------------------------------------------------------
        //private void SaveTestList(List<DO.Test> testsList)
        //{
        //    testRoot = new XElement("tests");
        //    foreach (var test in testsList)
        //    {
        //        SaveOneTest(test);
        //    }
        //    testRoot.Save(testPath);
        //}

        private void SaveOneTest(DO.Test test)
        {
            XElement testNumber = new XElement("testNumber", test.TestNumber);
            XElement IDTester = new XElement("IDTester", test.TesterId);
            XElement IDTrainee = new XElement("IDTrainee", test.TraineeId);
            XElement vehicle = new XElement("Vehicle", test.Vehicle.ToString());
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
            XElement testerNote = new XElement("testNote", test.TesterNote);
            // testRoot.Add(new XElement("test",))

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
                    if (testers.Any())
                        foreach (var tester in this.testers)
                            if (tester.ID == ID)
                                check1 = true;
                    break;
                case "trainee":
                    if (trainees.Any())
                        foreach (var trainee in this.trainees)
                            if (trainee.ID == ID)
                                check1 = true;
                    break;
            }
            return check1;
        }
        //----------------------------------------------------------------------------
        public void AddTester(DO.Tester tester, bool[,] matrix)
        {
            try
            {
                LoadData("testers");
                if (testers != null && IfExist(tester.ID, "tester"))
                    throw new DuplicateWaitObjectException("tester allready exist");
                //DO.Tester myTester = GetOneTester(tester.ID);
                //DO.Trainee myTrainee = GetOneTrainee(tester.ID, tester.TesterVehicle);
                //    if (myTester != null || myTrainee != null)
                //        throw new DuplicateWaitObjectException("Tester with the same ID already exists...");
            }
            catch (DuplicateWaitObjectException e) { throw; }

            SaveOneTester(tester);
            SetSchedule(matrix, tester.ID);
            testerRoot.Save(testerPath);
        }
        //--------------------------------------------------------------------
        public void DeleteTester(string testerID)
        {
            LoadData("testers");
            LoadData("tests");
            //XElement testerElement;
            try
            {
                if (IfExist(testerID, "tester"))
                {
                    (from tester in testerRoot.Elements()
                     where tester.Element("ID").Value == testerID
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
        public DO.Tester GetOneTester(string testerID)
        {
            try
            {
                LoadData("testers");
                if (!IfExist(testerID, "tester"))
                    throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException e) { throw; }
            foreach (var item in this.testers)
            {
                if (item.ID == testerID)
                    return new DO.Tester(item);
            }
            return null;
        }
        //----------------------------------------------------------------------
        public void UpdateTester(DO.Tester tester)//המטריצה נשלחת לפונקציה בנפרד בשכבה מעל
        {
            try
            {
                LoadData("testers");
                if (!IfExist(tester.ID, "tester"))
                    throw new KeyNotFoundException("ID not found");

                XElement testerElement = (from myTester in testerRoot.Elements()
                                          where myTester.Element("ID").Value == tester.ID
                                          select myTester).FirstOrDefault();
                //if (testerElement == null)
                //    throw new KeyNotFoundException("ID not found");
                testerElement.Remove();
                SaveOneTester(tester);
                testerRoot.Save(testerPath);
            }
            catch (KeyNotFoundException e) { throw; }
        }
        //---------------------------------------------------------------------
        public void AddTrainee(DO.Trainee trainee)
        {
            try
            {
                LoadData("trainees");
                if (GetOneTrainee(trainee.ID, trainee.TraineeVehicle) != null)
                    throw new DuplicateWaitObjectException("ID allready exist");

                //if (IfExist(trainee.ID, "trainee"))
                //    throw new DuplicateWaitObjectException("ID allready exist");
                // this.trainees.Add(trainee);
                SaveOneTrainee(trainee);
                traineeRoot.Save(traineePath);
            }
            catch (DuplicateWaitObjectException) { throw; }
            //  LoadTrainees();
            //FileStream file = new FileStream(traineePath, FileMode.Create);
            //XmlSerializer xmlSerializer = new XmlSerializer(trainee.GetType());
            //xmlSerializer.Serialize(file, trainee);
            //traineeRoot.Save(traineePath);//???
            //file.Close();
        }
        //-----------------------------------------------------------------------
        public DO.Trainee GetOneTrainee(string ID, DO.Vehicle vehicle)
        {
            try
            {
                LoadData("trainees");
                if (!IfExist(ID, "trainee"))
                    throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException e) { throw; }
            foreach (var item in this.trainees)
            {
                if (item.ID == ID)
                    return new DO.Trainee(item);
            }
            return null;
            //FileStream file = new FileStream(traineePath, FileMode.Open);
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(DO.Trainee));

            //DO.Trainee _trainee;
            //try
            //{
            //    // LoadTrainees();
            //}
            //catch (KeyNotFoundException)
            //{
            //    throw;
            //}
            //try
            //{
            //    _trainee = (from trainee in traineeRoot.Elements()
            //                where trainee.Element("id").Value == ID
            //                select (DO.Trainee)xmlSerializer.Deserialize(file)

            //               //FamilyName = trainee.Element("FamilyName").Value,
            //               //PrivateName = trainee.Element("PrivateName").Value,
            //               //DayOfBirth= trainee.Element

            //               ).FirstOrDefault();
            //}
            //catch
            //{
            //    throw new InvalidOperationException("ID not found");
            //}
            //file.Close();
            //return _trainee;


            //try
            //{
            //    _trainee = (from trainee in traineeRoot.Elements()
            //                      where trainee.Element("id").Value == ID
            //                      where trainee.Element("TraineeVehicle").Value == vehicle.ToString()
            //                      select new  DO.Trainee(trainee)).FirstOrDefault();
            //    traineeRoot.Save(traineePath);
            //    return _trainee;
            //}
            //catch
            //{
            //    throw new InvalidOperationException("ID not found");
            //}



            //FileStream file = new FileStream(traineePath, FileMode.Open);
            //XmlSerializer xmlSerializer = new XmlSerializer(typeof(DO.Trainee));
            //DO.Trainee trainee = (DO.Trainee)xmlSerializer.Deserialize(file);
            //traineeRoot.Save(traineePath);//???
            //file.Close();
            //return trainee;
        }
        //-----------------------------------------------------------------------
        public void DeleteTrainee(string traineeID, DO.Vehicle vehicle)
        {
            try
            {
                LoadData("trainees");
                LoadData("tests");
                //if(GetOneTrainee(traineeID, vehicle)==null)
                //    throw new KeyNotFoundException("ID not found");
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

            //this.trainees.RemoveAll(x => x.ID == traineeID);
            // this.tests.RemoveAll(x => x.TraineeId == traineeID);
            //SaveTestList();

            (from item in testRoot.Elements()
             where item.Element("IDTrainee").Value == traineeID
             where item.Element("Vehicle").Value == vehicle.ToString()
             select item).FirstOrDefault().Remove();
            traineeRoot.Save(traineePath);
            testRoot.Save(testPath);
        }
        //-------------------------------------------------------------------------
        public void UpdateTrainee(DO.Trainee trainee)
        {
            try
            {
                LoadData("trainee");

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
            catch (NullReferenceException e) { throw new KeyNotFoundException("the trainee doesn't study this vehicle"); }
            //traineeRoot.Save(traineePath);
            //List<DO.Trainee> trainees = this.trainees.FindAll(x => x.ID == trainee.ID && x.TraineeVehicle != trainee.TraineeVehicle);
            //if (trainees.Any())
            //    foreach (var _trainee in trainees)
            //    {
            //        _trainee.PrivateName = trainee.PrivateName;
            //        _trainee.FamilyName = trainee.FamilyName;
            //        _trainee.PersonAddress = trainee.PersonAddress;
            //        _trainee.Phone = trainee.Phone;
            //    }
            // SaveTraineeList(trainees);

            //try
            //{
            //    // LoadTrainees();
            //}
            //catch (KeyNotFoundException)
            //{
            //    throw;
            //}
            //FileStream file = new FileStream(traineePath, FileMode.Create);
            //XmlSerializer xmlSerializer = new XmlSerializer(trainee.GetType());

            //XElement traineeElement = (from _trainee in traineeRoot.Elements()
            //                           where _trainee.Element("id").Value == trainee.ID
            //                           where _trainee.Element("TraineeVehicle").Value == trainee.TraineeVehicle.ToString()
            //                           select _trainee).FirstOrDefault();
            //traineeElement.Remove();
            //xmlSerializer.Serialize(file, trainee);
            //file.Close();

            // traineeRoot.Save(traineePath);
        }
        //---------------------------------------------------------------------------
        public string AddTest(DO.Test test)
        {
            try
            {
                LoadData("tests");
                if (!IfExist(test.TesterId, "tester"))
                    throw new KeyNotFoundException("TesterID not found");
                if (!IfExist(test.TraineeId, "trainee"))
                    throw new KeyNotFoundException("TraineeID not found");
            }
            catch (KeyNotFoundException e) { throw; }
            LoadData("config");
            test.TestNumber = int.Parse(configRoot.Element("NumberTest").Element("value").Value);
            this.tests.Add(test);
            configRoot.Element("NumberTest").Element("value").SetValue(int.Parse(configRoot.Element("NumberTest").Element("value").Value) + 1);
            SaveOneTest(test);
            testRoot.Save(testPath);
            return "your number test is" + test.TestNumber;
        }
        //-------------------------------------------------------------------------
        public void UpdateTestResult(DO.Test test1)
        {
            try
            {
                LoadData("tests");
                //int index = this.tests.FindIndex(x => x.TestNumber == test1.TestNumber);
                //this.tests[index] = test1;

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
            if (this.tests.Find(x => x.TestNumber == numOfTest) == null)
                return false;
            return true;
        }
        //------------------------------------------------------------------------------
        public DO.Test GetOneTest(int testNum)
        {
            try
            {
                LoadData("tests");
                if (!IfTestExist(testNum))
                    throw new KeyNotFoundException("no such test");
            }
            catch (KeyNotFoundException e) { throw; }
            foreach (var item in this.tests)
                if (item.TestNumber == testNum)
                    return new DO.Test(item);
            return null;
        }
        //-------------------------------------------------------------------------------
        public void DeleteTest(int numOfTest)
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
        public List<DO.Tester> GetTesters()
        {
            LoadData("testers");
            return (from item in this.testers
                    select new DO.Tester(item)).ToList();
        }
        //-------------------------------------------------------------------------
        public List<DO.Trainee> GetTrainees()
        {
            LoadData("trainees");
            return (from item in this.trainees
                    select new DO.Trainee(item)).ToList();
        }
        //----------------------------------------------------
        public List<DO.Test> GetTests()
        {
            LoadData("tests");
            return (from item in this.tests
                    select new DO.Test(item)).ToList();
        }
        //---------------------------------------------------------
        public List<DO.Test> GetSomeTests(Predicate<DO.Test> someFunc)
        {
            LoadData("tests");
            return (from item in this.tests
                    where (someFunc(item))
                    select new DO.Test(item)).ToList();
        }
        //--------------------------------------------------
        public List<DO.Tester> GetSomeTesters(Predicate<DO.Tester> func)
        {
            LoadData("testers");
            var NewList = from item in this.testers
                          where (func(item))
                          select new DO.Tester(item);
            return NewList.ToList();
        }
        //--------------------------------------------------
        public List<DO.Trainee> GetSomeTrainies(Predicate<DO.Trainee> func)
        {
            LoadData("trainees");
            var NewList = from item in this.trainees
                          where (func(item))
                          select new DO.Trainee(item);
            return NewList.ToList();
        }
        //------------------------------------------------------------------
        public Dictionary<String, Object> GetConfig()
        {
            LoadData("config");
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var newDict = from item in configRoot.Elements()
                          where bool.Parse(item.Element("Readable").Value) == true
                          select new { key = item.Name.LocalName, value = item.Element("value").Value };
            foreach (var item in newDict)
                dictionary.Add(item.key, item.value as object);
            return dictionary;
        }
        //-----------------------------------------------------------------------
        public void SetConfig(string parm, object value)
        {
            try
            {
                LoadData("config");
                var tempArray = (from item in configRoot.Elements()
                                 where item.Name.LocalName == parm
                                 select item.Name.LocalName).ToList();
                if (tempArray.Count == 0)
                    throw new KeyNotFoundException("key not found");
                if (bool.Parse(configRoot.Element(parm).Element("Writable").Value) == false)
                    throw new InvalidOperationException("it is non-writeable value");
            }
            catch (InvalidOperationException e) { throw; }
            catch (KeyNotFoundException e) { throw; }
            this.configRoot.Element(parm).Element("value").SetValue(value);
        }
        //---------------------------------------------------------------------
        public bool[,] GetSchedule(string ID)
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

            for (int i = 0; i < 5; ++i)
                for (int j = 0; j < 6; ++j)
                    mat[i, j] = bool.Parse(schedule.Element("day_" + i).Element("hour_" + j).Value);

            return mat;
        }
        //-----------------------------------------------------------------------
        public void SetSchedule(bool[,] matrix, string testerID)
        {
            try
            {
                LoadData("testers");
                if (IfExist(testerID, "tester"))
                    (from item in scheduleRoot.Elements()
                     where item.Element("testerID").Value == testerID
                     select item).Remove();
                //throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException) { throw; }

            LoadData("schedule");
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
