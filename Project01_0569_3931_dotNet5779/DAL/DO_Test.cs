using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class DO
    {

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

        //------------------------------------------------------------------ 
    }
}
