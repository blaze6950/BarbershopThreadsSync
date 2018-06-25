using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbershopThreadsSync
{
    class Armchair
    {
        private Client _currentClient;

        public Armchair()
        {
            _currentClient = null;
        }

        public Armchair(Client currentClient)
        {
            _currentClient = currentClient;
        }

        internal Client CurrentClient { get => _currentClient; set => _currentClient = value; }
    }
}
