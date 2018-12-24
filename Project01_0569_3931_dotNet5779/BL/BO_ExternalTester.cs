using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ExternalTester
    {
        private string id;
        public string ID { get; set; }

        private string familyName;
        public string FamilyName { get; set; }

        private string privateName;
        public string PrivateName { get; set; }

        private Vehicle testerVehicle;
        public Vehicle TesterVehicle { get; set; }

        public ExternalTester() { }

        public override string ToString()
        {
            return String.Format(FamilyName + " " + PrivateName + " " + ID + " " + TesterVehicle);
        }
    }
}
