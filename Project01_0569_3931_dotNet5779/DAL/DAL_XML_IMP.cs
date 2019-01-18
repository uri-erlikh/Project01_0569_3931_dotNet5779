using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{

    class DAL_XML_IMP// : IDal
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
        //------------------------------------------------------------
        private void CreateFiles()
        {
            testerRoot = new XElement("testers");
            testerRoot.Save(testerPath);
        }

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
        //-----------------------------------------------------------------------------
        public void AddTester(DO.Tester tester, bool[,] matrix)
        {
            XElement id = new XElement("id", tester.ID);
            XElement privateName = new XElement("privateName", tester.PrivateName);
            XElement familyName = new XElement("familyName", tester.FamilyName);
            XElement dayOfBirth = new XElement("dayOfBirth", tester.DayOfBirth);
            XElement personGender = new XElement("personGender", tester.PersonGender);
            XElement phone = new XElement("phone", tester.Phone);
            XElement testerExperience = new XElement("testerExperience", tester.TesterExperience);
            XElement maxWeeklyTests = new XElement("maxWeeklyTests", tester.MaxWeeklyTests);
            XElement testerVehicle = new XElement("testerVehicle", tester.TesterVehicle);
            XElement rangeToTest = new XElement("rangeToTest", tester.RangeToTest);
            XElement city = new XElement("city", tester.PersonAddress.City);
            XElement street = new XElement("street", tester.PersonAddress.Street);
            XElement numOfBuilding = new XElement("numOfBuilding", tester.PersonAddress.NumOfBuilding);
            XElement personAddress = new XElement("personAddress", city, street, numOfBuilding);
            XElement person = new XElement("person", id, privateName, familyName, dayOfBirth, personGender, phone, personAddress);
            XElement schdule = new XElement("schdule", matrix);

            testerRoot.Add(new XElement("tester", person, testerExperience, maxWeeklyTests, testerVehicle, rangeToTest));
            testerRoot.Save(testerPath);
        }
        //---------------------------------------------------------------------------------------------------------------------------
    }
}
