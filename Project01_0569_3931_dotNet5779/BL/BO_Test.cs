﻿using System;
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
            TestDate = new DateTime();
            TestHour = new DateTime();
        }

        public override string ToString()
        {
            return @"Test number: "+this.TestNumber+ this.tester.ToString()
                + " trainee: "+this.traineeId+" "+this.traineeName+" at:"+this.TestAddress+" "+this.TestHour
                + " passed? "+this.PassedTest+" note: "+this.TesterNote;
        }
    }
}