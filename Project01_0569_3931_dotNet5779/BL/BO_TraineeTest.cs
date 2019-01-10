using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class TraineeTest
    {
        public int TestNumber { get; set; }

        private ExternalTester tester = new ExternalTester();
        public ExternalTester Tester
        {
            get { return tester; }
            set
            {
                tester.ID = value.ID; tester.FamilyName = value.FamilyName;
                tester.PrivateName = value.PrivateName; tester.TesterVehicle = value.TesterVehicle;
            }
        }

        public BO.Vehicle Vehicle { get; set; }

        private DateTime testdate;
        public DateTime TestDate { get; set; }

        private DateTime testHour;
        public DateTime TestHour { get; set; }

        private Address testAddress = new Address();
        public Address TestAddress
        {
            get { return testAddress; }
            set
            {
                this.testAddress.City = value.City;
                this.testAddress.Street = value.Street; this.testAddress.NumOfBuilding = value.NumOfBuilding;
            }
        }

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

        private string testNode;
        public string TesterNote { get; set; }

        public TraineeTest() { }

        public override string ToString()
        {            
            if (this.TestHour > DateTime.Now)
                return "Test number " + this.TestNumber + " will be at: " + this.TestHour + ". address: " + this.TestAddress.ToString() + " good luck!";
            else
            {
                if (PassedTest)
                    return String.Format("Test number {0}: trainee  passed the test at {1}. {2}", TestNumber, TestHour, TesterNote);
                return String.Format("Test number {0}: trainee  failed at the test at {1}. {2}", TestNumber, TestHour, TesterNote);
            }
        }
    }
}
