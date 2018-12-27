using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class TesterTest
    {
        public int TestNumber { get; set; }

        private string traineeId;
        public string TraineeId { get; set; }

        private DateTime testDate;
        public DateTime TestDate { get; set; }

        private DateTime testHour;
        public DateTime TestHour { get; set; }

        private Address testAddress=new Address();
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

        private string testerNote;
        public string TesterNote { get; set; }

        public TesterTest() { }

        public override string ToString()
        {
            return @"Test number: " + this.TestNumber + " traineeID: " + this.TraineeId 
                + " at: " + this.TestAddress + " " + this.TestHour 
                + " passed? "
                + this.PassedTest + " note: " + this.TesterNote; 
        }
    }
}
