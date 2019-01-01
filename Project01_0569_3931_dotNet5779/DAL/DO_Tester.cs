using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{

    public class Person
    {
        private string Id;
        public string ID { get; }
        private string familyName;
        public string FamilyName { get; set; }

        private string privateName;
        public string PrivateName { get; set; }

        private DateTime dayOfBirth;
        public DateTime DayOfBirth { get; set; }

        private Gender personGender;
        public Gender PersonGender { get; set; }

        private string phone;
        public string Phone { get; set; }

        private Address personAddress=new Address();
        public Address PersonAddress
        {
            get { return personAddress; }
            set
            {
                this.personAddress.City = value.City;
                this.personAddress.Street = value.Street;
                this.personAddress.NumOfBuilding = value.NumOfBuilding;
            }
        }
        public Person(string myId)
        {
            this.ID = myId;
            DayOfBirth = new DateTime().Date;
        }
    }
    //-----------------------------------------------------------
    public class Tester : Person
    {
        private int testerExperience;
        public int TesterExperience { get; set; }

        private int maxWeeklyTests;
        public int MaxWeeklyTests { get; set; }

        private Vehicle testerVehicle;
        public Vehicle TesterVehicle { get; set; }

        private double rangeToTest;
        public double RangeToTest { get; set; }

        public override string ToString()
        {
            return String.Format("{0} {1} ,{2} ,{3}", PrivateName, FamilyName, PersonAddress, TesterVehicle);
        }
        public Tester(string myId) : base(myId) { }

        public Tester(Tester t) : base(t.ID)
        {
            this.FamilyName = t.FamilyName;
            this.PrivateName = t.PrivateName;
            this.DayOfBirth = t.DayOfBirth;
            this.PersonGender = t.PersonGender;
            this.Phone = t.Phone;
            this.PersonAddress = t.PersonAddress;
            this.TesterExperience = t.TesterExperience;
            this.MaxWeeklyTests = t.MaxWeeklyTests;
            this.TesterVehicle = t.TesterVehicle;
            this.RangeToTest = t.RangeToTest;
        }
    }
    //--------------------------------------------------------------
}

