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
            try
            {
                if (IfExist(tester.ID, "tester"))
                    throw new DuplicateWaitObjectException("allready exist");
            }
            catch (DuplicateWaitObjectException e) { throw; }
            DataSource.Testers.Add(tester);
            DataSource.Schedules.Add(tester.ID, matrix);
        }
        //--------------------------------------------------------------
        public void DeleteTester(string TesterID)
        {
            try
            {
                if (!IfExist(TesterID, "tester"))
                    throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException e) { throw; }

            for (int i = 0; i < DataSource.Testers.Count; ++i)
                if (DataSource.Testers[i].ID == TesterID)
                {
                    DataSource.Testers.Remove(DataSource.Testers[i]);
                    DataSource.Schedules.Remove(TesterID);
                    return;
                }
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
            try
            {
                if (!IfExist(testerID, "tester"))
                    throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException e)
            { throw; }
            foreach (var tester in DataSource.Testers)
                if (testerID == tester.ID)
                {
                    switch (field)
                    {
                        case "familyName":
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
                            DataSource.Schedules[testerID][(int)info[0] - 1, (int)info[1] - 9] = (bool)info[2];//chek the data
                            break;
                    }
                }
        }
        //--------------------------------------------------------
        public DO.Tester GetOneTester(string ID)
        {
            try
            {
                if (!IfExist(ID, "tester"))
                    throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException e) { throw; }
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
            try
            {
                if (IfExist(trainee.ID, "trainee"))
                    throw new DuplicateWaitObjectException("allready exist");
            }
            catch (DuplicateWaitObjectException e) { throw; }
            DataSource.Trainies.Add(trainee);
        }
        //------------------------------------------------------------
        public void DeleteTrainee(string TraineeID)
        {
            try
            {
                if (!IfExist(TraineeID, "tester"))
                    throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException e) { throw; }
            for (int i = 0; i < DataSource.Trainies.Count; ++i)
                if (DataSource.Trainies[i].ID == TraineeID)
                {
                    DataSource.Trainies.Remove(DataSource.Trainies[i]);
                    return;
                }
        }
        //--------------------------------------------------------------
        public void UpdateTrainee(string traineeID, string field, object info)
        {
            try
            {
                if (!IfExist(traineeID, "trainee"))
                    throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException e) { throw; }
            foreach (var trainee in DataSource.Trainies)
                if (traineeID == trainee.ID)
                {
                    switch (field)
                    {
                        case "familyName":
                            trainee.FamilyName = (string)info;//לבדוק למעלה שזה לא נאללל
                            break;
                        case "privateName":
                            trainee.PrivateName = (string)info;
                            break;
                        case "dayOfBirth":
                            trainee.DayOfBirth = (DateTime)info;//אולי try
                            break;
                        case "phone":
                            trainee.Phone = (string)info;
                            break;
                        case "personAddress":
                            trainee.PersonAddress = (Address)info;//לבנות למעללה מבנה של כתובת
                            break;
                        case "traineeVehicle":
                            trainee.TraineeVehicle = (Vehicle)info;
                            break;
                        case "traineeGear":
                            trainee.TraineeGear = (GearBox)info;
                            break;
                        case "school":
                            trainee.School = (string)info;
                            break;
                        case "teacher":
                            trainee.Teacher = (string)info;
                            break;
                        case "drivingLessonsNum":
                            trainee.DrivingLessonsNum = (int)info;
                            break;
                    }
                }
        }
        //--------------------------------------------------
        public DO.Trainee GetOneTrainee(string ID)
        {
            try
            {
                if (!IfExist(ID, "trainee"))
                    throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException e) { throw; }
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
            try
            {
                if (!IfExist(test.TesterId, "tester"))
                    throw new KeyNotFoundException("TesterID not found");
                if (!IfExist(test.TraineeId, "trainee"))
                    throw new KeyNotFoundException("TraineeID not found");
            }
            catch (KeyNotFoundException e) { throw; }
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
            try
            {
                if (!IfTestExist(testNum))
                    throw new KeyNotFoundException();
            }
            catch (KeyNotFoundException e) { throw; }
            foreach (var item in DataSource.Tests)
                if (item.TestNumber == testNum)
                    return new Test(item);
            return null;
        }
        //--------------------------------------------------
        public List<DO.Tester> GetTesters()
        {
            try
            {
                if (DataSource.Testers.Count == 0)
                    throw new ArgumentNullException("empty list");
            }
            catch (ArgumentNullException e) { throw; }
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
            try
            {
                if (DataSource.Trainies.Count == 0)
                    throw new NullReferenceException("empty list");
            }
            catch (ArgumentNullException e) { throw; }
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
            try
            {
                if (DataSource.Tests.Count == 0)
                    throw new NullReferenceException("empty list");
            }
            catch (ArgumentNullException e) { throw; }
            List<DO.Test> newList = new List<DO.Test>();
            foreach (var item in DataSource.Tests)
            {
                newList.Add(new DO.Test(item));
            }
            return newList;
        }
        //---------------------------------------------------------
        public List<DO.Test> GetSomeTests(Predicate<DO.Test> someFunc)
        {
            try
            {
                if (DataSource.Tests.Count == 0)
                    throw new ArgumentNullException("empty list");
            }
            catch (ArgumentNullException e) { throw; }
            var NewList = from item in DataSource.Tests
                          where (someFunc(item))
                          select item;
            return NewList.ToList();
        }
        //--------------------------------------------------
        public List<DO.Tester> GetSomeTesters(Predicate<DO.Tester> func)
        {
            try
            {
                if (DataSource.Testers.Count == 0)
                    throw new NullReferenceException("empty list");
            }
            catch (ArgumentNullException e) { throw; }
            var NewList = from item in DataSource.Testers
                          where (func(item))
                          select item;
            return NewList.ToList();
        }
        //--------------------------------------------------
        public List<DO.Trainee> GetSomeTrainies(Predicate<DO.Trainee> func)
        {
            try
            {
                if (DataSource.Testers.Count == 0)
                    throw new NullReferenceException("empty list");
            }
            catch (ArgumentNullException e) { throw; }
            var NewList = from item in DataSource.Trainies
                          where (func(item))
                          select item;
            return NewList.ToList();
        }
        //------------------------------------------------------------------
        public Dictionary<String, Object> getConfig()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            var newDict = from item in DataSource.Configuration
                          where (item.Value.Readable == true)
                          select new { key = item.Key, item.Value.value };
            foreach (var item in newDict)
                dictionary.Add(item.key, item.value);
            return dictionary;
        }
        //-----------------------------------------------------------------------
        public void SetConfig(string parm, object value)
        {
            try
            {
                var asd = DataSource.Configuration.Keys.Select(x => x==parm).ToArray();
                
                if (DataSource.Configuration[parm].Writable == false)
                    throw new InvalidOperationException("it is non-writeable value");
            }
            catch (InvalidOperationException e) { throw; }
            DataSource.Configuration[parm].value = value;
        }
        //-----------------------------------------------------
        public bool[,] GetSchedule(string ID)
        {
            var matrix = (from item in DataSource.Schedules
                          where (item.Key == ID)
                          select item.Value).ToArray();
            return matrix.FirstOrDefault();
        }
    }
}

