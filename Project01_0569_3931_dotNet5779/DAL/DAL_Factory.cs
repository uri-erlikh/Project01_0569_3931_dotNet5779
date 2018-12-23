using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DAL_Factory
    {
        public static IDal GetDL(string type)
        {
            switch (type)
            {
                case "lisst": return Dal_imp.Instance;
            }
            return null;
        }
    }
}

