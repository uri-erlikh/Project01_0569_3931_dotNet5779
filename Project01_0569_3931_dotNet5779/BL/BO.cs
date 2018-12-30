using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BO
{


    public enum Vehicle { privateCar, motorcycle, truck, heavyTruck };
    public enum GearBox { auto, manual };
    public enum Gender { male, female };

    public static class Configuration
    {
        public static int MIN_LESSONS;
        public static int MAX_TESTER_AGE;
        public static int MIN_TRAINEE_AGE;
        public static int MIN_GAP_TEST;
        public static int MIN_TESTER_AGE;

        public static bool IfIntialized;
        public static DateTime updateTime;

        static Configuration()
        {
            IDal d_l = DAL_Factory.GetDL("lists");

            IfIntialized = true;
            updateTime = DateTime.Now;
            Dictionary<string, object> config= new Dictionary<string, object>(d_l.getConfig());
            MIN_LESSONS = (int)config["MIN_LESSONS"];
            MAX_TESTER_AGE = (int)config["MAX_TESTER_AGE"];
            MIN_TRAINEE_AGE = (int)config["MIN_TRAINEE_AGE"];
            MIN_GAP_TEST = (int)config["MIN_GAP_TEST"];
            MIN_TESTER_AGE = (int)config["MIN_TESTER_AGE"];
        }

        public static override string  ToString()
        {

        }
    }
    //----------------------------------------------------
    [Serializable]
    public class InvalidDataException : Exception
    {
        public InvalidDataException() : base() { }
        public InvalidDataException(string message) : base(message) { }
        public InvalidDataException(string message, Exception inner) : base(message, inner) { }
        protected InvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
    //---------------------------------------------------
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
}



