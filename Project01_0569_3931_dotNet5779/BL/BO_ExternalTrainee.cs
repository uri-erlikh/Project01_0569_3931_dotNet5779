﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ExternalTrainee:Person
    {
        private Vehicle traineeVehicle;
        public Vehicle TraineeVehicle { get; set; }

        private GearBox traineeGear;
        public GearBox TraineeGear { get; set; }

        private string school;
        public string School { get; set; }

        private string teacher;
        public string Teacher { get; set; }

        private int drivingLessonsNum;
        public int DrivingLessonsNum { get; set; }

        public ExternalTrainee() { }

        public override string ToString()
        {
            return base.ToString()+ " "+TraineeVehicle+" "+traineeGear+" school: " +school+" teacher: " +teacher+" lessons:"+drivingLessonsNum;
        }
    }
}
