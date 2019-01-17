using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL
{
   
    class DAL_XML_IMP
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

        private void CreateFiles()
        {
            testerRoot = new XElement("testrs");
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
                throw new Exception("File upload problem");
            }
        }
}
