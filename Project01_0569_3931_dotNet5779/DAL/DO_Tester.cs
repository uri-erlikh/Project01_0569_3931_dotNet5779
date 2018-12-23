using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class DO
    {
        public class Person
        {
            protected readonly string id;
            public string ID { get;  }
            public string FamilyName { get; set; }
            public string PrivateName { get; set; }
            public DateTime DayOfBirth { get; set; }
            public Gender PersonGender { get; set; }
            public string Phone { get; set; }
            public Address PersonAddress { get; set; }
            //public override string ToString()
            //{
            //    return this.;
            //}                 
        }
        //-----------------------------------------------------------
        public class Tester : Person
        {
            public int TesterExperience { get; set; }
            public int MaxWeeklytTests { get; set; }
            public Vehicle TesterVehicle { get; set; }           
            public double RangeToTest { get; set; }
            public override string ToString()
            {
                return String.Format("{0} {1} ,{2} ,{3}", PrivateName, FamilyName, PersonAddress, TesterVehicle);
            }
            public Tester(string myId)
            {
                //id= myId;          
            }
        }
        //--------------------------------------------------------------
    }
}
