﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbershopThreadsSync
{
    class Program
    {
        static void Main(string[] args)
        {
            Barbershop barbershop = new Barbershop();
            barbershop.Working();
        }
    }
}
