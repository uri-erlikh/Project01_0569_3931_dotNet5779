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

        XElement traineeRoot;
        string traineePath = @"TraineeXml.xml";

        XElement testRoot;
        string testPath = @"TestXml.xml";


        private DAL_XML_IMP()
        {
            if (!File.Exists(testerPath))
                CreateFilesTesers();
            else
                LoadTesters();
            if (!File.Exists(traineePath))
                CreateFilesTesers();
            else
                LoadTesters();
            if (!File.Exists(testPath))
                CreateFilesTesers();
            else
                LoadTesters();
        }
        //---------------------------------------------------------
        private void CreateFilesTesers()
        {
            testerRoot = new XElement("testers");
            testerRoot.Save(testerPath);
        }
        //-----------------------------------------------------------
        private void LoadTesters()
        {
            try
            {
                testerRoot = XElement.Load(testerPath);
            }
            catch
            {
                throw new KeyNotFoundException("File upload problem");
            }
        }
        //-----------------------------------------------------------------
        private void CreateFilesTrainees()
        {
            traineeRoot = new XElement("trainees");
            traineeRoot.Save(testerPath);
        }
        //----------------------------------------------------------------
        private void LoadTrainees()
        {
            try
            {
                traineeRoot = XElement.Load(testerPath);
            }
            catch
            {
                throw new KeyNotFoundException("File upload problem");
            }
        }
        //---------------------------------------------------------------
        public void AddTester(DO.Tester tester, bool[,] matrix)
        {
            try
            {
                LoadTesters();
                // DO.Tester myTester = GetOneTester(tester.ID);
                // DO.Trainee myTrainee = GetOneTrainee(tester.ID);
                //if (myTester != null || myTrainee!=null)
                //    throw new DuplicateWaitObjectException("Tester with the same ID already exists...");
            }
            catch (DuplicateWaitObjectException e) { throw; }

            XElement iD = new XElement("ID", tester.ID);
            XElement familyName = new XElement("FamilyName", tester.FamilyName);
            XElement privateName = new XElement("PrivateName", tester.PrivateName);
            XElement dayOfBirth = new XElement("DayOfBirth", tester.DayOfBirth);
            XElement personGender = new XElement("PersonGender", tester.PersonGender);
            XElement phone = new XElement("Phone", tester.Phone);
            XElement city = new XElement("City", tester.PersonAddress.City);
            XElement street = new XElement("Street", tester.PersonAddress.Street);
            XElement numOfBuilding = new XElement("NomOfBuilding", tester.PersonAddress.NumOfBuilding);
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
        public void DeleteTester(string TesterID)
        {
            XElement testerElement;
            try
            {
                testerElement = (from tester in testerRoot.Elements()
                                 where tester.Element("id").Value == TesterID
                                 select tester).FirstOrDefault();
                testerElement.Remove();
                testerRoot.Save(testerPath);
            }
            catch
            {
                throw new InvalidOperationException("ID not found");
            }
        }
        //-------------------------------------------------------------------
        void AddTrainee(DO.Trainee trainee)
        {
            FileStream file = new FileStream(traineePath, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(trainee.GetType());
            xmlSerializer.Serialize(file, trainee);
            traineeRoot.Save(traineePath);//???
            file.Close();
        }
        //-----------------------------------------------------------------------
        DO.Trainee GetOneTrainee(string ID, DO.Vehicle vehicle)
        {
            FileStream file = new FileStream(traineePath, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(DO.Trainee));

            DO.Trainee _trainee;
            try
            {
                LoadTrainees();
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
        void DeleteTrainee(string TraineeID, DO.Vehicle vehicle)
        {
            XElement traineeElement;
            try
            {
                traineeElement = (from trainee in traineeRoot.Elements()
                                  where trainee.Element("id").Value == TraineeID
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
        void UpdateTrainee(DO.Trainee trainee)
        {
            try
            {
                LoadTrainees();
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            FileStream file = new FileStream(traineePath, FileMode.Create);
            XmlSerializer xmlSerializer = new XmlSerializer(trainee.GetType());

            XElement traineeElement = (from _trainee in traineeRoot.Elements()
                                       where _trainee.Element("id").Value == trainee.ID
                                       where _trainee.Element("TraineeVehicle").Value == trainee.TraineeVehicle.ToString()
                                       select _trainee).FirstOrDefault();
            traineeElement.Remove();
            xmlSerializer.Serialize(file, trainee);
            file.Close();

            XElement _traineeElement = (from _trainee in traineeRoot.Elements()
                                        where _trainee.Element("id").Value == trainee.ID
                                        where _trainee.Element("TraineeVehicle").Value != trainee.TraineeVehicle.ToString()
                                        select _trainee).FirstOrDefault();
            _traineeElement.Element("PrivateName").Value = trainee.PrivateName;
            _traineeElement.Element("FamilyName").Value = trainee.FamilyName;
            _traineeElement.Element("City").Value = trainee.PersonAddress.City;
            _traineeElement.Element("Street").Value = trainee.PersonAddress.Street;
            // _traineeElement.Element("NumOfBuilding").Value = trainee.PersonAddress.NumOfBuilding;
            _traineeElement.Element("Phone").Value = trainee.Phone;

            traineeRoot.Save(traineePath);
        }




    };
}
