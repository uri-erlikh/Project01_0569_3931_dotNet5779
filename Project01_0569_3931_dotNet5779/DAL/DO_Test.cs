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
            public Address TestAddress { get { return testAddress; } set
                {
                    this.testAddress.City = value.City;
                    this.testAddress.Street = value.Street;
                    this.testAddress.NumOfBuilding = value.NumOfBuilding;
                } }
            public List<Criterion> Criterions;// = new List<Criterion>();
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

            public Test()
            {
                this.Criterions = new List<Criterion>();
                this.Criterions[1].Crit = "mirrors";
                this.Criterions[2].Crit = "brakes";
                this.Criterions[3].Crit = "reverse parking";
                this.Criterions[4].Crit = "distance keeping";
                this.Criterions[5].Crit = "vinkers";
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

        public class Criterion
        {
            public string Crit;
            public bool WasOK;
        }

        

        //------------------------------------------------------------------ 
    }

