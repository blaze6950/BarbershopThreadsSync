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
        //private Semaphore _semaphore;

        public WaitingRoom()
        {
            _chairs = new Stack<Client>();
            //_semaphore = new Semaphore(_chairs.Count, 3);
        }

        public WaitingRoom(Stack<Client> chairs)
        {
            _chairs = chairs;
        }

        //public WaitingRoom(List<Client> chairs, Semaphore semaphore)
        //{
        //    _chairs = chairs;
        //    _semaphore = semaphore;
        //}

        //public Semaphore Semaphore { get => _semaphore; set => _semaphore = value; }
        internal Stack<Client> Chairs { get => _chairs; set => _chairs = value; }
    }
}
