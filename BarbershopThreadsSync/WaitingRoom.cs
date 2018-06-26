using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarbershopThreadsSync
{
    class WaitingRoom
    {
        private Stack<Client> _chairs;
        private Mutex _mutexRoom;

        public WaitingRoom()
        {
            _chairs = new Stack<Client>();
            _mutexRoom = new Mutex();
        }

        public WaitingRoom(Stack<Client> chairs)
        {
            _chairs = chairs;
            _mutexRoom = new Mutex();
        }

        public Mutex MutexRoom { get => _mutexRoom; set => _mutexRoom = value; }
        internal Stack<Client> Chairs { get => _chairs; set => _chairs = value; }
    }
}
