using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class DAL_Factory
    {
        static string currentDL;

        public static IDal GetDL(string type)
        {
            try
            {
                currentDL = type;
                switch (type)
                {
                    case "lists": return DLObject.GetInstance();
                    case "XML": return DAL_XML_IMP.GetInstance();
                    default: throw new KeyNotFoundException("error");
                }
            }
            catch (KeyNotFoundException) { throw; }
        }

        public static void AddConfigUpdatedObserver(Action act)
        {
            switch (currentDL)
            {
                case "lists":
                    DLObject.GetInstance().ConfigUpdated += act;
                    break;
                case "XML":
                    DAL_XML_IMP.GetInstance().ConfigUpdated += act;
                    break;
                default: break;
            }
        }
    }
}

