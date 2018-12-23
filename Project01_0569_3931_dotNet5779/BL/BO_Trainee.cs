using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Trainee : Person
    {
        public Vehicle TraineeVehicle { get; set; }
        public GearBox TraineeGear { get; set; }
        public string School { get; set; }
        public string Teacher { get; set; }
        public int DrivingLessonsNum { get; set; }
        public List<TraineeTest> Trainee_Test;//לא אתחלתי
        public override string ToString()
        {
            return String.Format("{0}, {1} ,{2} ,at school: {3}", PrivateName, FamilyName, ID, School);
        }
        public Trainee()
        {

        }
    }
}
