using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Trainee : Person
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


        public List<TraineeTest> Trainee_Test=new List<TraineeTest>();

        //public override string ToString()
        //{
        //    return this.PrivateName + " " + this.FamilyName + " " + this.ID + "\nvehicle: " + this.TraineeVehicle + " at school: " + this.School
        //        + "\nallready got " + this.DrivingLessonsNum + " lessons";
        //}
        public Trainee(){}

        //private DateTime datelastoftest;
        //public DateTime DateLastOfTest { get; set; }


    }
}
