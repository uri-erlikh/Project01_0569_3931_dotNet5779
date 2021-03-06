﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{

    public class Test
    {
        private int testNumber;
        public int TestNumber { get; set; }

        private string testerId;
        public string TesterId { get; set; }

        private string traineeId;
        public string TraineeId { get; set; }

        public DO.Vehicle Vehicle { get; set; }

        private DateTime testDate;
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
            this.TestDate = new DateTime().Date;
            this.TestHour = new DateTime();
        }

        public Test(Test te)
        {
            this.TestNumber = te.TestNumber;
            this.TesterId = te.TesterId;
            this.TraineeId = te.TraineeId;
            this.Vehicle = te.Vehicle;
            this.TestDate = te.TestDate;
            this.TestHour = te.TestHour;
            this.testAddress = te.testAddress;
            this.Mirrors = te.Mirrors;
            this.Brakes = te.Brakes;
            this.ReverseParking = te.ReverseParking;
            this.Distance = te.Distance;
            this.Vinkers = te.Vinkers;
            this.TrafficSigns = te.TrafficSigns;
            this.PassedTest = te.PassedTest;
            this.TesterNote = te.TesterNote;
        }
    }





    //------------------------------------------------------------------ 
}

