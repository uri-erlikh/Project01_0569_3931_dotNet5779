using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public partial class BO
    {
        public enum Vehicle { privateCar, motorcycle, truck, heavyTruck };
        public enum GearBox { auto, manual };
        public enum Gender { male, female };

        public class Configuration
        {
            public static int MIN_LESSONS = 28;
            public static int MAX_TESTER_AGE = 67;
            public static int MIN_TRAINEE_AGE = 16;
            public static int MIN_GAP_TEST = 30;
            public static int MIN_TESTER_AGE = 40;
            public static int Number = 1;
        }
        //---------------------------------------------------
        public class Address
        {
            public string City { get; set; }
            public string Street { get; set; }
            public int NumOfBuilding { get; set; }
        }
        //------------------------------------------------------
        public class Person
        {
            public string ID { get; set; }
            public string FamilyName { get; set; }
            public string PrivateName { get; set; }
            private DateTime dayOfBirth = new DateTime();
            public DateTime DayOfBirth { get; set; }
            public int Age;
            public Gender PersonGender { get; set; }
            public string Phone { get; set; }
            public Address PersonAddress { get; set; }
            public Person()
            {
                this.Age = DateTime.Now.Year - DayOfBirth.Year;
            }
        }

        //---------------------------------------------------------
        public class Tester : Person
        {
            public int TesterExperience { get; set; }
            public int MaxWeeklytTests { get; set; }
            public Vehicle TesterVehicle { get; set; }
            public bool[,] Schedule; //= new bool[5, 6];
            public double RangeToTest { get; set; }
            public override string ToString()
            {
                return String.Format("{0} {1} ,{2} ,{3}", PrivateName, FamilyName, PersonAddress, TesterVehicle);
            }

            public Tester()
            {
                Schedule = new bool[5, 6];//לאתחל
            }
        }
        //-----------------------------------------------------------------------
        public class Trainee : Person
        {
            public Vehicle TraineeVehicle { get; set; }
            public GearBox TraineeGear { get; set; }
            public string School { get; set; }
            public string Teacher { get; set; }
            public int DrivingLessonsNum { get; set; }
            public int CurrentTestNum { get; set; }
            public override string ToString()
            {
                return String.Format("{0}, {1} ,{2} ,at school: {3}", PrivateName, FamilyName, ID, School);
            }
            public Trainee()
            {

            }
        }
        //-----------------------------------------------------------
        public class Test
        {
            public readonly int TestNumber;
            public string TesterId { get; set; }
            public string TraineeId { get; set; }
            public DateTime TestDate { get; set; }
            public DateTime TestHour { get; set; }
            public Address TestAddress { get; set; }
            public List<Criterion> Criterions;// = new List<Criterion>();
            public bool PassedTest { get; set; }
            public string TesterNote { get; set; }
            public override string ToString()
            {
                if (PassedTest)
                    return String.Format("Test number {0}: trainee {1} passed the test at {2}. {3}", TestNumber, TraineeId, TestDate, TesterNote);
                return String.Format("Test number {0}: trainee {1} failed at the test at {2}. {3}", TestNumber, TraineeId, TestDate, TesterNote);
            }

            public Test()
            {
                this.Criterions = new List<Criterion>();
                this.Criterions[1].Crit = "mirrors";
                this.Criterions[2].Crit = "brakes";
                this.Criterions[3].Crit = "reverse parking";
                this.Criterions[4].Crit = "distance keeping";
                this.Criterions[5].Crit = "vinkers";
            }
        }

        public class Criterion
        {
            public string Crit;
            public bool WasOK;
        }
    }
    //----------------------------------------------------------
}

