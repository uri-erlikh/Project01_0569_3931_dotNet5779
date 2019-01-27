using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Test
    {
        public int TestNumber { get; set; }

        private ExternalTester tester = new ExternalTester();
        public ExternalTester Tester 
        {
            get { return tester; }
            set
            {
                this.tester.ID = value.ID; this.tester.FamilyName = value.FamilyName;
                this.tester.PrivateName = value.PrivateName; this.tester.TesterVehicle = value.TesterVehicle;
            }
        }            
        private string traineeId;
        public string TraineeId { get; set; }

        private string traineeName;
        public string TraineeName { get; set; }

        public BO.Vehicle Vehicle { get; set; }

        private DateTime testDate;
        public DateTime TestDate { get; set; }

        private DateTime testHour;
        public DateTime TestHour { get; set; }

        private Address testAddress=new Address();
        public Address TestAddress { get { return testAddress; } set { this.testAddress.City = value.City;
           this.testAddress.Street=value.Street ;this.testAddress.NumOfBuilding = value.NumOfBuilding; } }

        private bool mirrors;
        public bool Mirrors { get; set; }
        private bool brakes;
        public bool Brakes { get; set; }
        private bool reverseParking;
        public bool ReverseParking { get; set; }
        private bool distance;
        public bool Distance { get; set; }
        private bool vinkers;
        public bool Vinkers { get; set; }
        private bool trafficSigns;
        public bool TrafficSigns { get; set; }

        private bool passedTest;
        public bool PassedTest { get; set; }
        private string testNote;
        public string TesterNote { get; set; }

        public Test()
        {
            TestDate = new DateTime().Date;
            TestHour=  new DateTime();
        }

        public override string ToString()
        {
            if (this.TestHour > DateTime.Now)
                return @"Test number: " + this.TestNumber + "\ntester: " + this.tester.ToString();
            //+ "\ntrainee: " + this.TraineeId + " " + this.TraineeName + "\nat: " + this.TestAddress + "\n" + this.TestHour
            //+ "\nYou can do it, Good Luck!!!";
            else
                return @"Test number: " + this.TestNumber + "\ntester: " + this.tester.ToString();
                    //+ "\ntrainee: " + this.TraineeId + " " + this.TraineeName + "\nat: " + this.TestAddress + "\n" + this.TestHour
                    //+ "\npassed? " + this.PassedTest + " note: " + this.TesterNote;
        }

        public static Queue<Test> testsRecentlyOpened = new Queue<Test>();

    }
}
