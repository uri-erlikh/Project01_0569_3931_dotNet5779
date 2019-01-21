using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using DO;

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

        XElement traineeRoot;
        string traineePath = @"TraineeXml.xml";

        XElement testRoot;
        string testPath = @"TestXml.xml";


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
        //----------------------------------------------------------------       
        public void AddTester(DO.Tester tester, bool[,] matrix)
        {
            try
            {
                LoadData("testers");
                // DO.Tester myTester = GetOneTester(tester.ID);
                // DO.Trainee myTrainee = GetOneTrainee(tester.ID);
                //if (myTester != null || myTrainee!=null)
                //    throw new DuplicateWaitObjectException("Tester with the same ID already exists...");
            }
            catch (DuplicateWaitObjectException e) { throw; }

            XElement iD = new XElement("ID", tester.ID);
            XElement familyName = new XElement("familyName", tester.FamilyName);
            XElement privateName = new XElement("privateName", tester.PrivateName);
            XElement dayOfBirth = new XElement("dayOfBirth", tester.DayOfBirth);
            XElement personGender = new XElement("personGender", tester.PersonGender);
            XElement phone = new XElement("phone", tester.Phone);
            XElement city = new XElement("city", tester.PersonAddress.City);
            XElement street = new XElement("street", tester.PersonAddress.Street);
            XElement numOfBuilding = new XElement("numOfBuilding", tester.PersonAddress.NumOfBuilding);
            XElement personAddress = new XElement("personAddress", city, street, numOfBuilding);
            XElement person = new XElement("person", iD, familyName, privateName, dayOfBirth, personGender, phone, personAddress);
            XElement testerExperience = new XElement("testerExperience", tester.TesterExperience);
            XElement maxWeeklyTests = new XElement("mawWeeklyTests", tester.MaxWeeklyTests);
            XElement testerVehicle = new XElement("testerVehicle", tester.TesterVehicle);
            XElement rangeToTest = new XElement("rangeToTest", tester.RangeToTest);
            XElement schdule = new XElement("schedule", matrix);

            testerRoot.Add(new XElement("tester", person, testerExperience, maxWeeklyTests, testerVehicle, rangeToTest));
            testerRoot.Save(testPath);
        }
        //--------------------------------------------------------------------
        public void DeleteTester(string testerID)
        {
            LoadData("testers");
            XElement testerElement;
            try
            {
                testerElement = (from tester in testerRoot.Elements()
                                 where tester.Element("ID").Value == testerID
                                 select tester).FirstOrDefault();
                if (testerElement == null)
                    throw new KeyNotFoundException("ID not found");
                testerElement.Remove();
                testerRoot.Save(testerPath);
            }
            catch(KeyNotFoundException)
            {
                throw; 
            }
        }
        //-------------------------------------------------------------------
        public DO.Tester GetOneTester(string testerID)
        {
            LoadData("testers");
            DO.Tester myTester;
            try
            {
                myTester = (from tester in testerRoot.Elements()
                           where tester.Element("ID").Value == testerID
                           select new DO.Tester(testerID)
                           {
                               FamilyName = tester.Element("person").Element("familyName").Value,
                               PrivateName=tester.Element("person").Element("privateName").Value,
                               DayOfBirth=DateTime.Parse(tester.Element("person").Element("dayOfBirth").Value),
                               PersonGender= (DO.Gender)Enum.Parse(typeof(DO.Gender),tester.Element("person").Element("personGender").Value),
                               Phone=tester.Element("Phone").Value,
                               PersonAddress=new DO.Address(tester.Element("person").Element("personAddress").Element("city").Value,
                               tester.Element("person").Element("personAddress").Element("street").Value,
                               int.Parse(tester.Element("person").Element("personAddress").Element("numOfBuilding").Value)),

                           }).FirstOrDefault();
             //   if (myTester.)
            }
            catch
            {
                throw new KeyNotFoundException("ID not found");
            }
            return myTester;
        }
        //----------------------------------------------------------------------
        public void AddTrainee(DO.Trainee trainee)
        {
            LoadData("trainees");
            FileStream file = new FileStream(traineePath, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(trainee.GetType());
            xmlSerializer.Serialize(file, trainee);
            traineeRoot.Save(traineePath);//????
            file.Close();
        }
        //-----------------------------------------------------------------------
        public DO.Trainee GetOneTrainee(string ID, DO.Vehicle vehicle)
        {           
            FileStream file = new FileStream(traineePath, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DO.Trainee));

            DO.Trainee _trainee;
            try
            {
                LoadData("trainees");
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            try
            {
                _trainee = (from trainee in traineeRoot.Elements()
                            where trainee.Element("id").Value == ID
                            select (DO.Trainee)xmlSerializer.Deserialize(file)

                           //FamilyName = trainee.Element("FamilyName").Value,
                           //PrivateName = trainee.Element("PrivateName").Value,
                           //DayOfBirth= trainee.Element

                           ).FirstOrDefault();
            }
            catch
            {
                throw new InvalidOperationException("ID not found");
            }
            file.Close();
            return _trainee;


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
        public void DeleteTrainee(string TraineeID, DO.Vehicle vehicle)
        {
            LoadData("trainees");
            XElement traineeElement;
            try
            {
                traineeElement = (from trainee in traineeRoot.Elements()
                                  where trainee.Element("ID").Value == TraineeID
                                  where trainee.Element("TraineeVehicle").Value == vehicle.ToString()
                                  select trainee).FirstOrDefault();
                traineeElement.Remove();
                traineeRoot.Save(traineePath);
            }
            catch
            {
                throw new InvalidOperationException("ID not found");
            }
        }
        //-------------------------------------------------------------------------
        public void UpdateTrainee(DO.Trainee trainee)
        {
            try
            {
                LoadData("trainees");
            }
            catch (KeyNotFoundException)
            {
                throw;
            }

            XElement traineeElement = (from _trainee in traineeRoot.Elements()
                                       where _trainee.Element("id").Value == trainee.ID
                                       where _trainee.Element("TraineeVehicle").Value == trainee.TraineeVehicle.ToString()
                                       select _trainee).FirstOrDefault();
            traineeElement.SetValue(trainee);

            //IEnumerable <XElement> _traineeElement = (from _trainee in traineeRoot.Elements()
            //                            where _trainee.Element("id").Value == trainee.ID
            //                            where _trainee.Element("TraineeVehicle").Value != trainee.TraineeVehicle.ToString()
            //                            select _trainee).FirstOrDefault();
            ////foreach(var tra in _traineeElement)
            //_traineeElement.Element("PrivateName").Value = trainee.PrivateName;
            //_traineeElement.Element("FamilyName").Value = trainee.FamilyName;
            //_traineeElement.Element("City").Value = trainee.PersonAddress.City;
            //_traineeElement.Element("Street").Value = trainee.PersonAddress.Street;
            //// _traineeElement.Element("NumOfBuilding").Value = trainee.PersonAddress.NumOfBuilding;
            //_traineeElement.Element("Phone").Value = trainee.Phone;

            traineeRoot.Save(traineePath);
        }




    };
}
