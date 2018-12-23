using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ExternalTrainee:Person
    {
        public Vehicle TraineeVehicle { get; set; }
        public GearBox TraineeGear { get; set; }
        public string School { get; set; }
        public string Teacher { get; set; }
        public int DrivingLessonsNum { get; set; }
    }
}
