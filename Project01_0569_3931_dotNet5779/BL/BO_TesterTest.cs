using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class TesterTest
    {
        public readonly int TestNumber;
        public string TraineeId { get; set; }
        public DateTime TestDate { get; set; }
        public DateTime TestHour { get; set; }
        public Address TestAddress { get; set; }
        public List<Criterion> Criterions;// = new List<Criterion>();
        public bool PassedTest { get; set; }
        public string TesterNote { get; set; }
    }
}
