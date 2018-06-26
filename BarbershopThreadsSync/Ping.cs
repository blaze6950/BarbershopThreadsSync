using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbershopThreadsSync
{
    class Ping
    {
        public static int SmallPing { get; set; }
        public static int BigPing { get; set; }
        public static int VeryBigPing { get; set; }

        static Ping()
        {
            SmallPing = 500;
            BigPing = 2500;
            VeryBigPing = 5000;
        }        
    }
}
