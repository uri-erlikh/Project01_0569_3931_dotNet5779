using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DAL
{
    class DLObject:IDal
    {
        static  DLObject instance = null;
        private DLObject() { }
        //static DLObject() { }

        //public static DLObject Instance// { get { return instance; } }

        public static DLObject GetInstance()
        {
            if (instance == null)
                instance = new DLObject();
            return instance;
        }



    }
}
