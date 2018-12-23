using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class DO
    {
        public enum Vehicle { privateCar, motorcycle, truck, heavyTruck };
        public enum GearBox { auto, manual };
        public enum Gender { male, female };
        //-------------------------------------------------------------
        public class Configuration
        {
            public static int MIN_LESSONS = 28;
            public static int MAX_TESTER_AGE = 67;
            public static int MIN_TRAINEE_AGE = 16;
            public static int MIN_GAP_TEST = 30;
            public static int MIN_TESTER_AGE = 40;
            public static int Number = 10000000;
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
        }
        //--------------------------------------------------------------
       
            
    }
}

