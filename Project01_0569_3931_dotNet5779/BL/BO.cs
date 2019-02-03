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
    //------------------------------------------------------
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
            IDal d_l = DAL_Factory.GetDL("XML");

            IfIntialized = true;
            updateTime = DateTime.Now;
            Dictionary<string, object> config= new Dictionary<string, object>(d_l.GetConfig());
            MIN_LESSONS = int.Parse(config["MIN_LESSONS"].ToString());
            MAX_TESTER_AGE = int.Parse(config["MAX_TESTER_AGE"].ToString());
            MIN_TRAINEE_AGE = int.Parse(config["MIN_TRAINEE_AGE"].ToString());
            MIN_GAP_TEST = int.Parse(config["MIN_GAP_TEST"].ToString());
            MIN_TESTER_AGE = int.Parse(config["MIN_TESTER_AGE"].ToString());
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
    public class Address
    {
        private string city;
        public string City { get; set; }
        private string street;
        public string Street { get; set; }
        private int numOfBuilding;
        public int NumOfBuilding { get; set; }

        public Address() { }

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



