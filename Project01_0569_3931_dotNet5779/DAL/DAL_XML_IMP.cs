using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
                CreateFiles();
            else
                LoadData();
        }
        //---------------------------------------------------------
        private void CreateFiles()
        {
            testerRoot = new XElement("testers");
            testerRoot.Save(testerPath);
        }
        //-----------------------------------------------------------
        private void LoadData()
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
        public void AddTester(DO.Tester tester, bool[,] matrix)//
        {
            try
            {
               // DO.Tester myTester = GetOneTester(tester.ID);
               // DO.Trainee myTrainee = GetOneTrainee(tester.ID);
                //if (myTester != null || myTrainee!=null)
                //    throw new DuplicateWaitObjectException("Tester with the same ID already exists...");
            }
            catch(DuplicateWaitObjectException e) { throw; }

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
             XElement person = new XElement("person", iD, familyName, privateName, dayOfBirth, personGender, phone,personAddress);           
            XElement testerExperience = new XElement("testerExperience", tester.TesterExperience);
            XElement maxWeeklyTests = new XElement("mawWeeklyTests", tester.MaxWeeklyTests);
            XElement testerVehicle = new XElement("testerVehicle", tester.TesterVehicle);
            XElement rangeToTest = new XElement("rangeToTest", tester.RangeToTest);
            XElement schdule = new XElement("schedule", matrix); 

            testerRoot.Add(new XElement("tester", person,testerExperience,maxWeeklyTests,testerVehicle,rangeToTest));
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
    };


    
}
