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

        public async void DoActions(Barbershop barbershop)
        {
            if (barbershop.Armchair.CurrentClient != null && _barberState != BarberState.Sleep)
            {
                await Task.Factory.StartNew(() => DoWork(barbershop));
            }
            else
            {
                await Task.Factory.StartNew(() => CheckWaitingRoom(barbershop));
            }            
        }

        public async void WakeUp(Barbershop barbershop)
        {
            await Task.Factory.StartNew(() => DoWork(barbershop));
        }

        public void DoWork(Barbershop barbershop)
        {
            barbershop.Mutex.WaitOne();
            Console.WriteLine("Парикмахер начинает свою работу с клиентом {0}...", barbershop.Armchair.CurrentClient.Name);
            Thread.Sleep(5000);
            barbershop.Armchair.CurrentClient.LeaveArmChair(barbershop);
            barbershop.Mutex.ReleaseMutex();
        }

        public void CheckWaitingRoom(Barbershop barbershop)
        {
            barbershop.Mutex.WaitOne();
            if (barbershop.Armchair.CurrentClient == null)
            {                
                Console.WriteLine("Парикмахер закончил работу с клиентом {0} и направляется в комнату ожидания за следующим клиентом...", barbershop.Armchair.CurrentClient?.Name);
                Thread.Sleep(1000);
                if (barbershop.WaitingRoom.Chairs.Count > 0)
                {
                    barbershop.Armchair.CurrentClient = barbershop.WaitingRoom.Chairs.Pop();
                    Console.WriteLine("Парикмахер позвал следующего клиента...", barbershop.Armchair.CurrentClient?.Name);
                    Thread.Sleep(1000);
                }
                else
                {
                    Console.WriteLine("Парикмахер не обнаружил клиентов в комнате ожидания и отправляется спать...");
                    Thread.Sleep(1000);
                    _barberState = BarberState.Sleep;
                }                
            }
            barbershop.Mutex.ReleaseMutex();
        }
    }
}
