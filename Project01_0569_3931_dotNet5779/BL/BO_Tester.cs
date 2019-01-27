using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public Person()
        {
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

        public bool[,] Schedule = new bool[5, 6];


        private double rangeToTest;
        public double RangeToTest { get; set; }

        public List<TesterTest> TesterTests = new List<TesterTest>(); 

        public override string ToString()
        {
            return this.PrivateName + " " + this.FamilyName + " ID: " + this.ID + "\nvehicle: " + this.TesterVehicle;               
        }

        public Tester()
        {
        }

        public static Queue<Tester> testersRecentlyOpened = new Queue<Tester>();
      //  public static ObservableCollection<BO.Tester> testers = new ObservableCollection<BO.Tester>();


    }
}
