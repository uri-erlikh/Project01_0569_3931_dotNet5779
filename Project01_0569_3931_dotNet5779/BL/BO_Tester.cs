using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Person
    {
        private string id;
        public string ID { get; set; }

        private string familyName;
        public string FamilyName { get; set; }

        private string privateName;
        public string PrivateName { get; set; }

        private DateTime dayOfBirth;
        public DateTime DayOfBirth { get; set; }

        public int age;

        private Gender personGender;
        public Gender PersonGender { get; set; }

        private string phone;
        public string Phone { get; set; }

        private Address personAddress;
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
        public Person()
        {
            DayOfBirth = new DateTime();
            this.age = DateTime.Now.Year - DayOfBirth.Year;
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

        public bool[,] Schedule;//לא אתחלתי

        private double rangeToTest;
        public double RangeToTest { get; set; }

        public List<Test> TesterTest; 

        public override string ToString()
        {
            return String.Format("{0} {1} ,{2} ,{3}", PrivateName, FamilyName, PersonAddress, TesterVehicle);
        }
    //    public Tester(string myId) : base(myId) { }

    //    public Tester(Tester t) : base(t.ID)
    //    {
    //        this.FamilyName = t.FamilyName;
    //        this.PrivateName = t.PrivateName;
    //        this.DayOfBirth = t.DayOfBirth;
    //        this.PersonGender = t.PersonGender;
    //        this.Phone = t.Phone;
    //        this.PersonAddress = t.PersonAddress;
    //        this.TesterExperience = t.TesterExperience;
    //        this.MaxWeeklyTests = t.MaxWeeklyTests;
    //        this.TesterVehicle = this.TesterVehicle;
    //        this.RangeToTest = t.RangeToTest;
    //    }
    //}
    //--------------------------------------------------------------
}
