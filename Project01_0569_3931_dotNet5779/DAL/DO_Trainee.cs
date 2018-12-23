using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
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
            private int currentTestNum;
            public int CurrentTestNum { get; set; }
            public override string ToString()
            {
                return String.Format("{0}, {1} ,{2} ,at school: {3}", PrivateName, FamilyName, ID, School);
            }

            public Trainee(string myId):base(myId) { }

            public Trainee(Trainee tr) : base(tr.ID)
            {
                this.FamilyName = tr.FamilyName;
                this.PrivateName = tr.PrivateName;
                this.DayOfBirth = tr.DayOfBirth;
                this.PersonGender = tr.PersonGender;
                this.Phone = tr.Phone;
                this.PersonAddress = tr.PersonAddress;
                this.TraineeVehicle = tr.TraineeVehicle;
                this.TraineeGear = tr.TraineeGear;
                this.School = tr.School;
                this.Teacher = tr.Teacher;
                this.DrivingLessonsNum = tr.DrivingLessonsNum;
                this.CurrentTestNum = tr.CurrentTestNum;
            }
    }
    //---------------------------------------------------------------

