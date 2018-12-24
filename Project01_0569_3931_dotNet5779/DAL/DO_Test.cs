using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{

    public class Test
    {
        public readonly int TestNumber;
        private string testerId;
        public string TesterId { get; set; }
        private string traineeId;
        public string TraineeId { get; set; }
        private DateTime testDate;
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
                this.testAddress.Street = value.Street;
                this.testAddress.NumOfBuilding = value.NumOfBuilding;
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
        private string testNote;
        public string TesterNote { get; set; }

        public override string ToString()
        {
            if (PassedTest)
                return String.Format("Test number {0}: trainee {1} passed the test at {2}. {3}", TestNumber, TraineeId, TestDate, TesterNote);
            return String.Format("Test number {0}: trainee {1} failed at the test at {2}. {3}", TestNumber, TraineeId, TestDate, TesterNote);
        }

        public Test(string IDTester, string IDTrainee)
        {
            this.TesterId = IDTester;
            this.TraineeId = IDTrainee;
        }

        public Test(Test te)
        {
            this.TestNumber = te.TestNumber;
            this.TesterId = te.TesterId;
            this.TraineeId = te.TraineeId;
            this.TestDate = te.TestDate;
            this.TestHour = te.TestHour;
            this.testAddress = te.testAddress;
            this.PassedTest = this.PassedTest;
            //crit
            this.TesterNote = te.TesterNote;
        }
    }





    //------------------------------------------------------------------ 
}

