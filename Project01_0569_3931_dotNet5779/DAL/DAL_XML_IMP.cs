using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL
{

    class DAL_XML_IMP//:IDal
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

        private DAL_XML_IMP()
        {
            if (!File.Exists(testerPath))
                CreateFiles("testers");
            else
                LoadData("testers");

            if (!File.Exists(traineePath))
                CreateFiles("trainees");
            else
                LoadData("trainees");

            if (!File.Exists(testPath))
                CreateFiles("tests");
            else
                LoadData("tests");
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
            }
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
            }
        }
        //-------------------------------------------------------------------
        private void SaveTestersList(List<DO.Tester> testersList)
        {
            testerRoot = new XElement("testers");

            foreach (var tester in testersList)
            {
                SaveOneTester(tester);
            }
            testerRoot.Save(testerPath);
        }

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
            XElement maxWeeklyTests = new XElement("mawWeeklyTests", tester.MaxWeeklyTests);
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
                             select new DO.Tester(tester.Element("ID").Value)
                             {
                                 FamilyName = tester.Element("person").Element("familyName").Value,
                                 PrivateName = tester.Element("person").Element("privateName").Value,
                                 DayOfBirth = DateTime.Parse(tester.Element("person").Element("dayOfBirth").Value),
                                 PersonGender = (DO.Gender)Enum.Parse(typeof(DO.Gender), tester.Element("person").Element("personGender").Value),
                                 Phone = tester.Element("phone").Value,
                                 PersonAddress = new DO.Address(tester.Element("person").Element("personAddress").Element("city").Value,
                                 tester.Element("person").Element("personAddress").Element("street").Value,
                                 int.Parse(tester.Element("person").Element("personAddress").Element("numOfBuilding").Value)),
                                 TesterExperience = int.Parse(tester.Element("testerExperience").Value),
                                 MaxWeeklyTests = int.Parse(tester.Element("maxWeeklyTests").Value),
                                 TesterVehicle = (DO.Vehicle)Enum.Parse(typeof(DO.Gender), tester.Element("testerVehicle").Value),
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
        private void SaveTraineeList(List<DO.Trainee> traineeList)
        {
            traineeRoot = new XElement("trainees");

            foreach (var trainee in traineeList)
            {
                SaveOneTrainee(trainee);
            }
            traineeRoot.Save(traineePath);
        }

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
            XElement traineeGear = new XElement("traineeVehicle", trainee.TraineeGear.ToString());
            XElement school = new XElement("school", trainee.School);
            XElement teacher = new XElement("teacher", trainee.Teacher);
            XElement drivingLessonsNum = new XElement("drivingLessonsNum", trainee.DrivingLessonsNum);
            traineeRoot.Add(new XElement("trainee", person,, traineeVehicle, traineeGear, school, teacher, drivingLessonsNum));
        }
        //---------------------------------------------------------------- 
        private List<DO.Trainee> GetTraineesList()
        {
            List<DO.Trainee> myTrainees = new List<DO.Trainee>();
            try
            {
                myTrainees = (from trainee in traineeRoot.Elements()
                              select new DO.Trainee(trainee.Element("ID").Value)
                              {
                                  FamilyName = trainee.Element("person").Element("familyName").Value,
                                  PrivateName = trainee.Element("person").Element("privateName").Value,
                                  DayOfBirth = DateTime.Parse(trainee.Element("person").Element("dayOfBirth").Value),
                                  PersonGender = (DO.Gender)Enum.Parse(typeof(DO.Gender), trainee.Element("person").Element("personGender").Value),
                                  Phone = trainee.Element("phone").Value,
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
        private bool IfExist(string ID, string type)
        {
            bool check1 = false;
            switch (type)
            {
                case "tester":
                    foreach (var tester in this.testers)
                        if (tester.ID == ID)
                            check1 = true;
                    break;
                case "trainee":
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
                // DO.Tester myTester = GetOneTester(tester.ID);
                // DO.Trainee myTrainee = GetOneTrainee(tester.ID);
                //if (myTester != null || myTrainee!=null)
                //    throw new DuplicateWaitObjectException("Tester with the same ID already exists...");
            }
            catch (DuplicateWaitObjectException e) { throw; }

            this.testers.Add(tester);
            SaveOneTester(tester);
            //schedule;
            testerRoot.Save(testerPath);
        }
        //--------------------------------------------------------------------
        public void DeleteTester(string testerID)
        {
            XElement testerElement;
            try
            {
                testerElement = (from tester in testerRoot.Elements()
                                 where tester.Element("ID").Value == testerID
                                 select tester).FirstOrDefault();
                if (testerElement == null)
                    throw new KeyNotFoundException("ID not found");
                testerElement.Remove();

                if (!IfExist(testerID, "tester"))
                    throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException e) { throw; }
            this.testers.RemoveAll(x => x.ID == testerID);
            this.tests.RemoveAll(x => x.TesterId == testerID);
            //SaveTestList();
            testerRoot.Save(testerPath);
        }
        //-------------------------------------------------------------------
        public DO.Tester GetOneTester(string testerID)
        {
            try
            {
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
        public void UpdateTester(DO.Tester tester)
        {
            try
            {
                if (!IfExist(tester.ID, "tester"))
                    throw new KeyNotFoundException("ID not found");
                int index = this.testers.FindIndex(x => x.ID == tester.ID);
                this.testers[index] = tester;

                XElement testerElement = (from myTester in testerRoot.Elements()
                                          where myTester.Element("ID").Value == tester.ID
                                          select myTester).FirstOrDefault();
                if (testerElement == null)
                    throw new KeyNotFoundException("ID not found");
                testerElement.Remove();
                SaveOneTester(tester);
            }
            catch (KeyNotFoundException e) { throw; }
        }
        //---------------------------------------------------------------------
        public void AddTrainee(DO.Trainee trainee)
        {
            try
            {
                if (IfExist(trainee.ID, "trainee"))
                    throw new DuplicateWaitObjectException("ID allready exist");
                this.trainees.Add(trainee);
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
                (from trainee in traineeRoot.Elements()
                 where trainee.Element("ID").Value == traineeID
                 where trainee.Element("traineeVehicle").Value == vehicle.ToString()
                 select trainee).FirstOrDefault().Remove();

                if (!IfExist(traineeID, "trainee"))
                    throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException e) { throw; }
            this.trainees.RemoveAll(x => x.ID == traineeID);
            this.tests.RemoveAll(x => x.TraineeId == traineeID);
            //SaveTestList();
            traineeRoot.Save(traineePath);
        }
        //-------------------------------------------------------------------------
        public void UpdateTrainee(DO.Trainee trainee)
        {
            try
            {
                if (!IfExist(trainee.ID, "trainee"))
                    throw new KeyNotFoundException("ID not found");
                int index = this.trainees.FindIndex(x => x.ID == trainee.ID && x.TraineeVehicle == trainee.TraineeVehicle);
                this.trainees[index] = trainee;
                List<DO.Trainee> trainees = this.trainees.FindAll(x => x.ID == trainee.ID && x.TraineeVehicle != trainee.TraineeVehicle);
                if (trainees.Any())
                    foreach (var _trainee in trainees)
                    {
                        _trainee.PrivateName = trainee.PrivateName;
                        _trainee.FamilyName = trainee.FamilyName;
                        _trainee.PersonAddress = trainee.PersonAddress;
                        _trainee.Phone = trainee.Phone;
                    }
                SaveTraineeList(trainees);
            }
            catch (KeyNotFoundException e) { throw; }
            catch (NullReferenceException e) { throw new KeyNotFoundException("the trainee don't study this vehicle"); }
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

            //XElement _traineeElement = (from _trainee in traineeRoot.Elements()
            //                            where _trainee.Element("id").Value == trainee.ID
            //                            where _trainee.Element("TraineeVehicle").Value != trainee.TraineeVehicle.ToString()
            //                            select _trainee).FirstOrDefault();
            //_traineeElement.Element("PrivateName").Value = trainee.PrivateName;
            //_traineeElement.Element("FamilyName").Value = trainee.FamilyName;
            //_traineeElement.Element("City").Value = trainee.PersonAddress.City;
            //_traineeElement.Element("Street").Value = trainee.PersonAddress.Street;
            //// _traineeElement.Element("NumOfBuilding").Value = trainee.PersonAddress.NumOfBuilding;
            //_traineeElement.Element("Phone").Value = trainee.Phone;

            //traineeRoot.Save(traineePath);
        }
        //---------------------------------------------------------------------------





    };
}
