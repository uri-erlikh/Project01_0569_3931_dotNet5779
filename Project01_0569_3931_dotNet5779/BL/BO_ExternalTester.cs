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
        public string FamilyName { get; set; }
        public string PrivateName { get; set; }
        public Vehicle TesterVehicle { get; set; }
    }
}
