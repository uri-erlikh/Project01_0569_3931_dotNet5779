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
            try
            {
                switch (type)
                {
                    case "lists": return DLObject.GetInstance();
                    case "XML": return DAL_XML_IMP.GetInstance();
                    default: throw new KeyNotFoundException("error");
                }
            }
            catch (KeyNotFoundException) { throw; }           
        }       
    }
}

