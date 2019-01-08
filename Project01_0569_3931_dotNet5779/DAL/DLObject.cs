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
            DataSource.Testers.RemoveAll(x => x.ID == TesterID);
            DataSource.Tests.RemoveAll(x => x.TesterId == TesterID);
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

                Tester tester = DataSource.Testers.Find(x => x.ID == testerID);
                if (tester == null)
                    throw new KeyNotFoundException("ID not found");
                switch (field)
                {
                    case "familyName":
                        tester.FamilyName = (string)info[0];
                        break;
                    case "privateName":
                        tester.PrivateName = (string)info[0];
                        break;
                    case "dayOfBirth":
                        tester.DayOfBirth = DateTime.Parse((string)info[0]);
                        break;
                    case "phone":
                        tester.Phone = (string)info[0];
                        break;
                    case "personAddress":
                        tester.PersonAddress = new Address((string)info[0], (string)info[1], (int)info[2]);
                        break;
                    case "testerExperience":
                        tester.TesterExperience = (int)info[0];
                        break;
                    case "maxWeeklyTests":
                        tester.MaxWeeklyTests = (int)info[0];
                        break;
                    case "testerVehicle":
                        tester.TesterVehicle = (Vehicle)Enum.Parse(typeof(Vehicle), (string)info[0]);
                        break;
                    case "rangeToTest":
                        tester.RangeToTest = (int)info[0];
                        break;
                    case "schedule":
                        DataSource.Schedules[testerID][(int)info[0] - 1, (int)info[1] - 9] = (bool)info[2];
                        break;
                }
            }
            catch (KeyNotFoundException e)
            { throw; }
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
                DO.Trainee tr = DataSource.Trainies.Find(x => x.ID == trainee.ID && x.TraineeVehicle == trainee.TraineeVehicle);
                if (tr != null)
                    throw new DuplicateWaitObjectException("allready exist");
                else
                    DataSource.Trainies.Add(trainee);
            }
            catch (DuplicateWaitObjectException e) { throw; }
        }
        //------------------------------------------------------------
        public void DeleteTrainee(string TraineeID)
        {
            try
            {
                if (!IfExist(TraineeID, "trainee"))
                    throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException e) { throw; }
            DataSource.Trainies.RemoveAll(x => x.ID == TraineeID);
            DataSource.Tests.RemoveAll(x => x.TraineeId == TraineeID);
        }
        //--------------------------------------------------------------
        public void UpdateTrainee(string traineeID, string field, params object[] info)
        {
            try
            {
                if (!IfExist(traineeID, "trainee"))
                    throw new KeyNotFoundException("ID not found");
                List<Trainee> trainies = DataSource.Trainies.FindAll(x => x.ID == traineeID);
                foreach (var trainee in trainies)
                    switch (field)
                    {
                        case "familyName":
                            trainee.FamilyName = (string)info[0];
                            break;
                        case "privateName":
                            trainee.PrivateName = (string)info[0];
                            break;
                        case "dayOfBirth":
                            trainee.DayOfBirth = DateTime.Parse((string)info[0]);
                            break;
                        case "phone":
                            trainee.Phone = (string)info[0];
                            break;
                        case "personAddress":
                            trainee.PersonAddress = new Address((string)info[0], (string)info[1], (int)info[2]);
                            break;
                        case "traineeVehicle":
                            trainee.TraineeVehicle = (Vehicle)Enum.Parse(typeof(Vehicle), (string)(info[0]));
                            break;
                        case "traineeGear":
                            trainee.TraineeGear = (GearBox)Enum.Parse(typeof(GearBox), (string)(info[0]));
                            break;
                        case "school":
                            trainee.School = (string)info[0];
                            break;
                        case "teacher":
                            trainee.Teacher = (string)info[0];
                            break;
                        case "drivingLessonsNum":
                            if (trainee.TraineeVehicle == (Vehicle)Enum.Parse(typeof(Vehicle), (string)info[0]))
                                trainee.DrivingLessonsNum = (int)info[1];
                            break;
                    }
            }
            catch (KeyNotFoundException e) { throw; }
        }
        //--------------------------------------------------
        public DO.Trainee GetOneTrainee(string ID, DO.Vehicle vehicle)
        {
            try
            {
                if (!IfExist(ID, "trainee"))
                    throw new KeyNotFoundException("ID not found");
            }
            catch (KeyNotFoundException e) { throw; }
            try
            {
                foreach (var item in DataSource.Trainies)
                {
                    if (item.ID == ID && item.TraineeVehicle == vehicle)
                        return new DO.Trainee(item);
                }
                throw new KeyNotFoundException("no match between trainee and vehicle type");
            }
            catch (KeyNotFoundException e) { throw; }
        }
        //--------------------------------------------------------
        public string AddTest(DO.Test test)
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
            return "your number test is" + temp;
        }
        //---------------------------------------------------------
        public void UpdateTestResult(DO.Test test)
        {
            try
            {
                int index = DataSource.Tests.FindIndex(x => x.TestNumber == test.TestNumber);
                DataSource.Tests[index] = test;
                //Test test = DataSource.Tests.Find(x => x.TestNumber == numOfTest);
                //test.Mirrors = result[0];
                //test.Brakes = result[1];
                //test.ReverseParking = result[2];
                //test.Distance = result[3];
                //test.Vinkers = result[4];
                //test.TrafficSigns = result[5];
                //test.PassedTest = result[6];
                //test.TesterNote = note;
            }
            catch (KeyNotFoundException e) { throw; }
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
                    throw new KeyNotFoundException("no such test");
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
            return (from item in DataSource.Tests
                    where (someFunc(item))
                    select new DO.Test(item)).ToList();
        }
        //--------------------------------------------------
        public List<DO.Tester> GetSomeTesters(Predicate<DO.Tester> func)
        {
            var NewList = from item in DataSource.Testers
                          where (func(item))
                          select new DO.Tester(item);
            return NewList.ToList();
        }
        //--------------------------------------------------
        public List<DO.Trainee> GetSomeTrainies(Predicate<DO.Trainee> func)
        {
            var NewList = from item in DataSource.Trainies
                          where (func(item))
                          select new DO.Trainee(item);
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
                var tempArray = DataSource.Configuration.Keys.Where(x => x == parm).Select(x => x).ToArray();
                if (tempArray.Length == 0)
                    throw new KeyNotFoundException("key not found");
                if (DataSource.Configuration[parm].Writable == false)
                    throw new InvalidOperationException("it is non-writeable value");
            }
            catch (InvalidOperationException e) { throw; }
            catch (KeyNotFoundException e) { throw; }
            DataSource.Configuration[parm].value = value;
        }
        //-----------------------------------------------------
        public bool[,] GetSchedule(string ID)
        {
            try
            {
                if (!IfExist(ID, "tester"))
                    throw new KeyNotFoundException();
            }
            catch (KeyNotFoundException e) { throw; }
            var matrix = (from item in DataSource.Schedules
                          where (item.Key == ID)
                          select item.Value).ToArray();
            return matrix.FirstOrDefault();
        }
    }
}

