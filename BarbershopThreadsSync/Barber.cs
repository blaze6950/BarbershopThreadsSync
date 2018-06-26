using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarbershopThreadsSync
{
    enum BarberState
    {
        Work,
        Sleep
    }

    class Barber
    {
        private BarberState _barberState;

        public Barber()
        {
            _barberState = BarberState.Sleep;
        }

        public Barber(BarberState barberState)
        {
            _barberState = barberState;
        }

        public BarberState BarberState { get => _barberState; set => _barberState = value; }

        public void DoActions(Barbershop barbershop)
        {
            if (barbershop.Armchair.CurrentClient != null)
            {
                DoWork(barbershop);
            }
            else
            {
                barbershop.WaitingRoom.MutexRoom.WaitOne();
                if (barbershop.Armchair.CurrentClient != null)
                {
                    CheckWaitingRoom(barbershop);
                }
                barbershop.WaitingRoom.MutexRoom.ReleaseMutex();
            }            
        }        

        public void DoWork(Barbershop barbershop)
        {            
            Console.WriteLine("B: начинает свою работу с клиентом {0}...\n", barbershop.Armchair.CurrentClient.Name);
            Thread.Sleep(5000);
            Console.WriteLine("B: закончил работу с клиентом {0} и направляется в комнату ожидания за следующим клиентом...\n", barbershop.Armchair.CurrentClient?.Name);
            barbershop.Armchair.CurrentClient.LeaveArmChair(barbershop);
        }

        public void CheckWaitingRoom(Barbershop barbershop)
        {            
            if (barbershop.Armchair.CurrentClient == null)
            {                
                Thread.Sleep(1000);
                if (barbershop.WaitingRoom.Chairs.Count > 0)
                {
                    barbershop.Armchair.CurrentClient = barbershop.WaitingRoom.Chairs.Pop();
                    Console.WriteLine("B: Парикмахер позвал следующего клиента...\n", barbershop.Armchair.CurrentClient?.Name);
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("B: Парикмахер не обнаружил клиентов в комнате ожидания и отправляется спать...\n");
                    Thread.Sleep(1000);
                    _barberState = BarberState.Sleep;
                }                
            }            
        }
    }
}
