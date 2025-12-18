
using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Business
{
    public class clsInitializer
    {
        public static void InitializeDatabase()
        {
            clsDBInitializer.InitializeDatabase();
        }
    }
}
