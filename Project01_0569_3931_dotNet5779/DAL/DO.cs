using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    
        public enum Vehicle { privateCar, motorcycle, truck, heavyTruck };
        public enum GearBox { auto, manual };
        public enum Gender { male, female };
        //-------------------------------------------------------------
        
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


