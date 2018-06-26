using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarbershopThreadsSync
{
    class Client
    {
        private String _name;

        public Client()
        {
            _name = Guid.NewGuid().ToString();
        }

        public Client(string name)
        {
            _name = name;
        }

        public Client(string name, Barbershop barbershop)
        {
            _name = name;
            DoActions(barbershop);
        }

        public string Name { get => _name; set => _name = value; }

        public void DoActions(Barbershop barbershop)
        {            
            CheckBarber(barbershop);            
        }

        private void CheckBarber(Barbershop barbershop)
        {
            barbershop.WaitingRoom.MutexRoom.WaitOne();
            Console.WriteLine("C{0}: заходит в парикмахерскую...\n", _name);
            Thread.Sleep(Ping.SmallPing);
            if (barbershop.Armchair.CurrentClient == null && barbershop.WaitingRoom.Chairs.Count == 0 && barbershop.Barber.BarberState == BarberState.Sleep)
            {                
                WakeUpBarber(barbershop);
                barbershop.WaitingRoom.MutexRoom.ReleaseMutex();
            }            
            else
            {
                DoActionIfBarberIsWorking(barbershop);
                barbershop.WaitingRoom.MutexRoom.ReleaseMutex();
            }
        }

        private void WakeUpBarber(Barbershop barbershop)
        {
            Console.WriteLine("C{0}: будит парикмахера и занимает кресло...\n", _name);
            Thread.Sleep(Ping.SmallPing);
            barbershop.Armchair.CurrentClient = this;
            barbershop.Barber.BarberState = BarberState.Work;
        }

        private void DoActionIfBarberIsWorking(Barbershop barbershop)
        {
            Console.WriteLine("C{0}: разворачивается и идет в комнату ожидания, т.к. парикмахер занят...\n", _name);
            Thread.Sleep(Ping.SmallPing);

            Console.WriteLine("C{0}: проверяет, есть ли свободный стул в комнате ожидания...\n", _name);            
            if (barbershop.WaitingRoom.Chairs.Count < 3)
            {
                WaitInTheWaitingRoom(barbershop);
            }
            else
            {
                LeaveBarberShop();
            }
            Thread.Sleep(Ping.SmallPing);
        }

        private void WaitInTheWaitingRoom(Barbershop barbershop)
        {
            Console.WriteLine("C{0}: нашел свободный стул и занимает его...\n", _name);
            barbershop.WaitingRoom.Chairs.Push(this);
            Thread.Sleep(Ping.SmallPing);
        }

        private void LeaveBarberShop()
        {
            Console.WriteLine("C{0}: не нашел свободный стул и уходит...\n", _name);
            Thread.Sleep(Ping.SmallPing);
        }

        public void LeaveArmChair(Barbershop barbershop)
        {
            Console.WriteLine("C{0}: подстриженный и довольный идет домой...\n", _name);            
            barbershop.Armchair.CurrentClient = null;
            Thread.Sleep(Ping.SmallPing);
        }

        public override string ToString()
        {
            return "№" + _name;
        }
    }
}
