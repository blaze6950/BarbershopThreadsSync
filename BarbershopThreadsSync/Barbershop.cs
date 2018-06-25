using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbershopThreadsSync
{
    class Barbershop
    {
        private WaitingRoom _waitingRoom;
        private Barber _barber;
        private Armchair _armchair;

        public Barbershop()
        {
        }

        public Barbershop(WaitingRoom waitingRoom, Barber barber, Armchair armchair)
        {
            _waitingRoom = waitingRoom;
            _barber = barber;
            _armchair = armchair;
        }

        internal WaitingRoom WaitingRoom { get => _waitingRoom; set => _waitingRoom = value; }
        internal Barber Barber { get => _barber; set => _barber = value; }
        internal Armchair Armchair { get => _armchair; set => _armchair = value; }
    }
}
