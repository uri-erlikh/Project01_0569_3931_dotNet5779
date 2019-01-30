using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class BL_Factory
    {
        public static IBL GetBL ()
        {
            return BLImplementation.GetInstance();
        }
    }
}
