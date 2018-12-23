using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class TraineeTest
    {
        public readonly int TestNumber;
        public ExternalTrainee trainee;
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
    }
}
