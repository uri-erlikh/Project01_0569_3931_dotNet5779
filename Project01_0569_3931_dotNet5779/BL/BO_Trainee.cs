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

        public List<TraineeTest> Trainee_Test;//לא אתחלתי

        public override string ToString()
        {
            return String.Format("{0}, {1} ,{2} ,at school: {3}", PrivateName, FamilyName, ID, School);
        }
        public Trainee(){}
    }
}
