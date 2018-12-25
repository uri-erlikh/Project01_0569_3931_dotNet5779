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
        public void AddTester(DO.Tester tester, bool[,] matrix)
        {
            if (!IfExist(tester.ID, "tester"))
            {
                DataSource.Testers.Add(tester);
                DataSource.Schedules.Add(tester.ID, matrix);
            }
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
                        DataSource.Schedules.Remove(TesterID);
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
                            case "amilyName":
                                tester.FamilyName = (string)info[0];//לבדוק למעלה שזה לא נאללל
                                break;
                            case "privateName":
                                tester.PrivateName = (string)info[0];
                                break;
                            case "dayOfBirth":
                                tester.DayOfBirth = (DateTime)info[0];//אולי try
                                break;
                            case "phone":
                                tester.Phone = (string)info[0];
                                break;
                            case "personAddress":
                                tester.PersonAddress = (Address)info[0];
                                break;
                            case "testerExperience":
                                tester.TesterExperience = (int)info[0];
                                break;
                            case "maxWeeklyTesters":
                                tester.MaxWeeklyTests = (int)info[0];
                                break;
                            case "testerVehicle":
                                tester.TesterVehicle = (Vehicle)info[0];
                                break;
                            case "rangeToTest":
                                tester.RangeToTest = (int)info[0];
                                break;
                            case "schedule":
                                DataSource.Schedules[testerID][(int)info[0],(int)info[1]] = (bool)info[2];//chek the data
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
                for (int i = 0; i < DataSource.Trainies.Count; ++i)
                    if (DataSource.Trainies[i].ID == TraineeID)
                    {
                        DataSource.Trainies.Remove(DataSource.Trainies[i]);
                        break;
                    }
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
                            case "familyName":
                                trainee.FamilyName = (string)info[0];//לבדוק למעלה שזה לא נאללל
                                break;
                            case "privateName":
                                trainee.PrivateName = (string)info[0];
                                break;
                            case "dayOfBirth":
                                trainee.DayOfBirth = (DateTime)info[0];//אולי try
                                break;
                            case "phone":
                                trainee.Phone = (string)info[0];
                                break;
                            case "personAddress":
                                trainee.PersonAddress = (Address)info[0];//לבנות למעללה מבנה של כתובת
                                break;
                            case "traineeVehicle":
                                trainee.TraineeVehicle = (Vehicle)info[0];
                                break;
                            case "traineeGear":
                                trainee.TraineeGear = (GearBox)info[0];
                                break;
                            case "school":
                                trainee.School = (string)info[0];
                                break;
                            case "teacher":
                                trainee.Teacher = (string)info[0];
                                break;
                            case "drivingLessonsNum":
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
        public void UpdateTestResult(int numOfTest, string field, object result)
        {
            if (!IfTestExist(numOfTest))
                throw new KeyNotFoundException("Test is not exist");//try
            foreach (var item in DataSource.Tests)
                if (item.TestNumber == numOfTest)
                {
                    if (item.TestHour > DateTime.Now)//try
                        throw new InvalidOperationException("Test didn't occur yet");//change the execption
                    switch (field)
                    {
                        case "mirrors":
                            item.Mirrors = (bool)result;
                            break;
                        case "brakes":
                            item.Brakes = (bool)result;
                            break;
                        case "reverseParking":
                            item.ReverseParking = (bool)result;
                            break;
                        case "distance":
                            item.Distance = (bool)result;
                            break;
                        case "vinkers":
                            item.Vinkers = (bool)result;
                            break;
                        case "trafficSigns":
                            item.TrafficSigns = (bool)result;
                            break;
                        case "passedTest":
                            item.PassedTest = (bool)result;
                            break;
                        case "testerNote":
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
        public DO.Test GetOneTest(int testNum)
        {
            if (!IfTestExist(testNum))
                throw new KeyNotFoundException();
            foreach (var item in DataSource.Tests)
                if (item.TestNumber == testNum)
                    return new Test(item);
            return null;
        }
        //--------------------------------------------------
        public List<DO.Tester> GetTesters()
        {
            if (DataSource.Testers.Count == 0)
                throw new NullReferenceException("empty list");
            List<DO.Tester> newList = new List<DO.Tester>();
            foreach (var item in DataSource.Testers)
            {
                newList.Add(new DO.Tester(item));
            }
            return newList;
        }
        //-----------------------------------------------------
        public List<DO.Trainee> GetTrainees()
        {
            if (DataSource.Trainies.Count == 0)
                throw new NullReferenceException("empty list");
            List<DO.Trainee> newList = new List<DO.Trainee>();
            foreach (var item in DataSource.Trainies)
            {
                newList.Add(new DO.Trainee(item));
            }
            return newList;
        }
        //----------------------------------------------------
        public List<DO.Test> GetTests()
        {
            if (DataSource.Tests.Count == 0)
                throw new NullReferenceException("empty list");
            List<DO.Test> newList = new List<DO.Test>();
            foreach (var item in DataSource.Tests)
            {
                newList.Add(new DO.Test(item));
            }
            return newList;
        }
        //---------------------------------------------------------
    }
}
