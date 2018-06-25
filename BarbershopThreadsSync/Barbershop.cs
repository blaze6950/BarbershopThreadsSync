using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarbershopThreadsSync
{
    class Barbershop
    {
        private WaitingRoom _waitingRoom;
        private Barber _barber;
        private Armchair _armchair;
        private Mutex _mutex;

        public Barbershop()
        {
            _waitingRoom = new WaitingRoom();
            _barber = new Barber();
            _armchair = new Armchair();
            _mutex = new Mutex();
        }

        public Barbershop(WaitingRoom waitingRoom, Barber barber, Armchair armchair)
        {
            _waitingRoom = waitingRoom;
            _barber = barber;
            _armchair = armchair;
            _mutex = new Mutex();
        }

        public WaitingRoom WaitingRoom { get => _waitingRoom; set => _waitingRoom = value; }
        public Barber Barber { get => _barber; set => _barber = value; }
        public Armchair Armchair { get => _armchair; set => _armchair = value; }
        public Mutex Mutex { get => _mutex; set => _mutex = value; }

        public void NewClient(Client client)
        {
            _mutex.WaitOne();
            client.DoActions(this);
            _mutex.ReleaseMutex();
        }

        public void Working()
        {
            while (true)
            {
                NewClient(new Client());
                _mutex.WaitOne();
                Barber.DoActions(this);
                _mutex.ReleaseMutex();
                Thread.Sleep(1000);
            }
        }
    }
}
