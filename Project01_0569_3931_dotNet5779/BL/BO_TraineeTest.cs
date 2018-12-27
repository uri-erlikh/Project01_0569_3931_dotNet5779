using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class TraineeTest
    {
        public int TestNumber;

        private ExternalTester tester;
        public ExternalTester Tester { get { return tester; } set { tester = value; } }

        private DateTime testdate;
        public DateTime TestDate { get; set; }

        private DateTime testHour;
        public DateTime TestHour { get; set; }

        private Address testAddress;
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
            if (PassedTest)
                return String.Format("Test number {0}: trainee {1} passed the test at {2}. {3}", TestNumber, trainee.ToString(), TestDate, TesterNote);
            return String.Format("Test number {0}: trainee {1} failed at the test at {2}. {3}", TestNumber, trainee.ToString(), TestDate, TesterNote);
        }
    }
}
