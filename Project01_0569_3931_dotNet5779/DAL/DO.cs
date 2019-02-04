using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DO
{

    public enum Vehicle { privateCar, motorcycle, truck, heavyTruck };
    public enum GearBox { auto, manual };
    public enum Gender { male, female };
    //-------------------------------------------------------------
    public abstract class BaseDL : IDal
    {
        public Action ConfigUpdated = null;

        public abstract string AddTest(Test test);
        public abstract void AddTester(Tester tester, bool[,] matrix);
        public abstract void AddTrainee(Trainee trainee);
        public abstract void DeleteTest(int numOfTest);
        public abstract void DeleteTester(string TesterID);
        public abstract void DeleteTrainee(string traineeID, Vehicle vehicle);
        public abstract Dictionary<string, object> GetConfig();
        public abstract Test GetOneTest(int TestNum);
        public abstract Tester GetOneTester(string ID);
        public abstract Trainee GetOneTrainee(string ID, Vehicle vehicle);
        public abstract bool[,] GetSchedule(string ID);
        public abstract List<Tester> GetSomeTesters(Predicate<Tester> func);
        public abstract List<Test> GetSomeTests(Predicate<Test> someFunc);
        public abstract List<Trainee> GetSomeTrainies(Predicate<Trainee> func);
        public abstract List<Tester> GetTesters();
        public abstract List<Test> GetTests();
        public abstract List<Trainee> GetTrainees();
        public abstract void SetConfig(string parm, object value);
        public abstract void SetSchedule(bool[,] schedule, string testerID);
        public abstract void UpdateTester(Tester tester);
        public abstract void UpdateTestResult(Test test);
        public abstract void UpdateTrainee(Trainee trainee);
    }

    //-------------------------------------------------------------------
    public struct Address
    {
        private string city;
        public string City { get; set; }
        private string street;
        public string Street { get; set; }
        private int numOfBuilding;
        public int NumOfBuilding { get; set; }

        public Address(string myCity, string myStreet, int myNumOfBuilding)
        {
            this.City = myCity;
            this.city = myCity;
            this.street = myStreet;
            this.Street = myStreet;
            this.numOfBuilding = myNumOfBuilding;
            this.NumOfBuilding = myNumOfBuilding;
        }
        public override string ToString()
        {
            return "" + this.Street + " " + this.NumOfBuilding + " " + this.City;
        }

    }
    //--------------------------------------------------------------


}


