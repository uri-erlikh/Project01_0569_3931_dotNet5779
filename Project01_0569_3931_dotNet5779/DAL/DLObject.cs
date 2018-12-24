using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DAL
{
    class DLObject : IDal
    {
        static DLObject instance = null;
        private DLObject() { }
        //static DLObject() { }

        //public static DLObject Instance// { get { return instance; } }

        public static DLObject GetInstance()
        {
            if (instance == null)
                instance = new DLObject();
            return instance;
        }

        //-------------------------------------------------------
        public void AddTester(DO.Tester tester)
        {
            if (!IfExist(tester.ID, "tester"))
                DataSource.Testers.Add(tester);
            else throw new DuplicateWaitObjectException("allready exist");
        }
        //--------------------------------------------------------------
        public void DeleteTester(string TesterID)
        {
            if (IfExist(TesterID, "tester"))
            {
                for (int i = 0; i < DataSource.Testers.Count; ++i)
                    if (DataSource.Testers[i].ID == TesterID)
                    {
                        DataSource.Testers.Remove(DataSource.Testers[i]);
                        break;
                    }
            }
            else throw new KeyNotFoundException("ID not found");
        }
        //------------------------------------------------------------------        
        private bool IfExist(string ID, string type)
        {
            bool check1 = false;
            switch (type)
            {
                case "tester":
                    foreach (var tester in DataSource.Testers)
                        if (tester.ID == ID)
                            check1 = true;
                    break;
                case "trainee":
                    foreach (var trainee in DataSource.Trainies)
                        if (trainee.ID == ID)
                            check1 = true;
                    break;
            }
            return check1;
        }
        //----------------------------------------------------------------------------
        public void UpdateTester(string testerID, string field, params object[] info)
        {
            if (!IfExist(testerID, "tester"))
                throw new KeyNotFoundException("ID not found");//try
            else
                foreach (var tester in DataSource.Testers)
                    if (testerID == tester.ID)
                    {
                        switch (field)
                        {
                            case "FamilyName":
                                tester.FamilyName = (string)info[0];//לבדוק למעלה שזה לא נאללל
                                break;
                            case "PrivateName":
                                tester.PrivateName = (string)info[0];
                                break;
                            case "DayOfBirth":
                                tester.DayOfBirth = (DateTime)info[0];//אולי try
                                break;
                            case "Phone":
                                tester.Phone = (string)info[0];
                                break;
                            case "PersonAddress":
                                tester.PersonAddress = (Address)info[0];
                                break;
                            case "TesterExperience":
                                tester.TesterExperience = (int)info[0];
                                break;
                            case "MaxWeeklyTesters":
                                tester.MaxWeeklyTests = (int)info[0];
                                break;
                            case "TesterVehicle":
                                tester.TesterVehicle = (Vehicle)info[0];
                                break;
                            case "RangeToTest":
                                tester.RangeToTest = (int)info[0];
                                break;
                            case "Schedule":
                                DataSource.Schedules[testerID][info[0], info[1]] = (bool)info[2];
                                break;
                            default: break;
                        }
                    }
        }
        //--------------------------------------------------------
        public DO.Tester GetOneTester(string ID)
        {
            if (!IfExist(ID, "tester"))
                throw new KeyNotFoundException("ID not found");
            foreach (var item in DataSource.Testers)
            {
                if (item.ID == ID)
                    return new DO.Tester(item);
            }
            return null;
        }
        //------------------------------------------------------------
        public void AddTrainee(DO.Trainee trainee)
        {
            if (!IfExist(trainee.ID, "trainee"))
                DataSource.Trainies.Add(trainee);
            else throw new DuplicateWaitObjectException("allready exist");
        }
        //------------------------------------------------------------
        public void DeleteTrainee(string TraineeID)
        {
            if (IfExist(TraineeID, "tester"))
            {
                for (int i=0;i<DataSource.Trainies.Count; ++i)
                    if (DataSource.Trainies[i].ID == TraineeID)
                        DataSource.Trainies.Remove(DataSource.Trainies[i]);
            }
            else throw new KeyNotFoundException("ID not found");
        }
        //--------------------------------------------------------------
        public void UpdateTrainee(string traineeID, string field, params object[] info)
        {
            if (!IfExist(traineeID, "trainee"))
                throw new KeyNotFoundException("ID not found");//try
            else
                foreach (var trainee in DataSource.Trainies)
                    if (traineeID == trainee.ID)
                    {
                        switch (field)
                        {
                            case "FamilyName":
                                trainee.FamilyName = (string)info[0];//לבדוק למעלה שזה לא נאללל
                                break;
                            case "PrivateName":
                                trainee.PrivateName = (string)info[0];
                                break;
                            case "DayOfBirth":
                                trainee.DayOfBirth = (DateTime)info[0];//אולי try
                                break;
                            case "Phone":
                                trainee.Phone = (string)info[0];
                                break;
                            case "PersonAddress":
                                trainee.PersonAddress = (Address)info[0];//לבנות למעללה מבנה של כתובת
                                break;
                            case "TraineeVehicle":
                                trainee.TraineeVehicle = (Vehicle)info[0];
                                break;
                            case "TraineeGear":
                                trainee.TraineeGear = (GearBox)info[0];
                                break;
                            case "School":
                                trainee.School = (string)info[0];
                                break;
                            case "Teacher":
                                trainee.Teacher = (string)info[0];
                                break;
                            case "DrivingLessonsNum":
                                trainee.DrivingLessonsNum = (int)info[0];
                                break;
                            default: break;
                        }
                    }
        }
        //--------------------------------------------------
        public DO.Trainee GetOneTrainee(string ID)
        {
            if (!IfExist(ID, "trainee"))
                throw new KeyNotFoundException("ID not found");
            foreach (var item in DataSource.Trainies)
            {
                if (item.ID == ID)
                    return new DO.Trainee(item);
            }
            return null;
        }
        //--------------------------------------------------------
        public void AddTest(DO.Test test)
        {
            if (!IfExist(test.TesterId, "tester"))
                throw new KeyNotFoundException("TesterID not found");//try 
            if (!IfExist(test.TraineeId, "trainee"))
                throw new KeyNotFoundException("TraineeID not found");//try  
            int temp = (int)DataSource.Configuration["Number"].value;
            test.TestNumber = temp;
            DataSource.Tests.Add(test);
            DataSource.Configuration["Number"].value = temp + 1;
        }
        //---------------------------------------------------------
        public void UpdateTestResult(int NumOfTest, string field, object result)
        {
            if (!IfTestExist(NumOfTest))
                throw new KeyNotFoundException("Test is not exist");//try
            foreach (var item in DataSource.Tests)
                if (item.TestNumber == NumOfTest)
                {
                    if (item.TestHour < DateTime.Now)//try
                        throw new InvalidOperationException("Test don't occur yet");//change the execption
                    switch (field)
                    {
                        case "Mirrors":
                            item.Mirrors = (bool)result;
                            break;
                        case "Brakes":
                            item.Brakes = (bool)result;
                            break;
                        case "ReverseParking":
                            item.ReverseParking = (bool)result;
                            break;
                        case "Distance":
                            item.Distance = (bool)result;
                            break;
                        case "Vinkers":
                            item.Vinkers = (bool)result;
                            break;
                        case "TrafficSigns":
                            item.TrafficSigns = (bool)result;
                            break;
                        case "PassedTest":
                            item.PassedTest = (bool)result;
                            break;
                        case "TesterNote":
                            item.TesterNote = (string)result;
                            break;
                        default: throw new InvalidOperationException("you cannot change this field");//the same
                    }
                }

        }
        //--------------------------------------------------
        private bool IfTestExist(int numOfTest)
        {
            if (numOfTest <= (int)DataSource.Configuration["Number"].value && numOfTest >= 10000000)
                return true;
            return false;
        }
        //----------------------------------------------------
    }
}
