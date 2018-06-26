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

        public async void DoActions(Barbershop barbershop)
        {            
            await Task.Factory.StartNew(() => CheckBarber(barbershop));
        }

        private void CheckBarber(Barbershop barbershop)
        {            
            Console.WriteLine("Новый клиент {0} проверяет парикмахера...", _name);
            Thread.Sleep(1000);
            if (barbershop.Barber.BarberState == BarberState.Sleep && barbershop.Armchair.CurrentClient == null)
            {                
                WakeUpBarber(barbershop);
            }
            else if (barbershop.Barber.BarberState == BarberState.Work)
            {
                DoActionIfBarberIsWorking(barbershop);
            }
            else
            {
                throw new Exception("Unknown error! NaN value BarberState variable!");
            }            
        }

        private void WakeUpBarber(Barbershop barbershop)
        {
            Console.WriteLine("Клиент {0} будит парикмахера и занимает кресло...", _name);
            Thread.Sleep(1000);
            barbershop.Armchair.CurrentClient = this;
        }

        private void DoActionIfBarberIsWorking(Barbershop barbershop)
        {
            Console.WriteLine("Клиент {0} разворачивается и идет в комнату ожидания, т.к. парикмахер занят...", _name);
            Thread.Sleep(1000);

            Console.WriteLine("Клиент {0} проверяет, есть ли свободный стул в комнате ожидания...", _name);
            Thread.Sleep(1000);
            if (barbershop.WaitingRoom.Chairs.Count < 3)
            {
                WaitInTheWaitingRoom(barbershop);
            }
            else
            {
                LeaveBarberShop();
            }
        }

        private void WaitInTheWaitingRoom(Barbershop barbershop)
        {
            Console.WriteLine("Клиент {0} нашел свободный стул и занимает его...", _name);
            Thread.Sleep(1000);
            barbershop.WaitingRoom.Chairs.Push(this);
        }

        private void LeaveBarberShop()
        {
            Console.WriteLine("Клиент {0} не нашел свободный стул и уходит...", _name);
            Thread.Sleep(1000);
        }

        public void LeaveArmChair(Barbershop barbershop)
        {
            Console.WriteLine("Клиент {0} подстриженный и довольный идет домой...", _name);
            Thread.Sleep(1000);
            barbershop.Armchair.CurrentClient = null;
        }

        public override string ToString()
        {
            return "№" + _name;
        }
    }
}
