using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Task13.CassaFolder;
using Task13.FileHandler;
using Task13.Parsers;
using Task13.Persons;
using Task13.Readers;

namespace Task13
{
    internal class TimeCordinator
    {
        public event Action<IPerson, Cassa, int> OnArrivedToCassa;
        public event Action<Cassa> OnCassaClosed;
        public event Action<Cassa, IPerson> OnPersonServiced;
        public event Action<IPerson> OnPersonBackToQueue;
        public event Action<IPerson> OnPersonArrived;
        public event Action<int, PriorityQueue<IPerson, int>> OnProcessEnd;
        public event Action<PriorityQueue<IPerson, int>, Cassa> OnCassaMaxAmount;

        private Random random = new();
        private int timeCounter = 1000;
        private int localTime;
        private string path;
        private PriorityQueue<IPerson, int> _mainQueue = new(Comparer<int>.Create((x, y) => y - x));
        private List<Cassa> casses;
        public TimeCordinator(List<Cassa> casses, string path)
        {
            this.casses = new(casses);
            this.path = path;
        }

        public void Process()
        {
            localTime = 1;
            bool isProcess = true;
            int counter = 0;
            foreach (Cassa item in casses)
            {
                item.OnCassaClosed += OnCassaClosedAction;
                item.OnCassaMaxAmount += OnCassaMaxAmountAction;
            }

            using (StreamReader sr = new(path))
            {
                while (isProcess)
                {
                    localTime++;
                    if (casses.Count > 1 && localTime % 150 == 0 && random.Next(0, 2) == 1)
                    {
                        casses[random.Next(0, casses.Count)].Close();
                    }

                    if (localTime % 10 == 0)
                    {
                        if (!sr.EndOfStream)
                        {
                            FileHandlerService.TryReadToObject(
                                out Person person,
                                new TXTSerializedPersonReader<Person>(),
                                new PersonParser<Person>(),
                                sr
                            );
                            _mainQueue.Enqueue(person, person.Priority);
                            OnPersonArrived?.Invoke(person);
                        }
                        if (_mainQueue.Count > 0)
                        {
                            if (TryChooseCassa(_mainQueue.Peek()))
                            {
                                counter++;
                                _mainQueue.Dequeue();
                            }
                            else
                            {
                                OnPersonBackToQueue?.Invoke(_mainQueue.Peek());
                            }

                        }
                    }
                    foreach (Cassa person in casses)
                    {
                        if (person.Count > 0 && --person.Peek().TimeService <= 0)
                        {
                            if (OnPersonServiced is not null)
                            {
                                OnPersonServiced.Invoke(person, person.Dequeue());
                            }
                            else
                            {
                                person.Dequeue();
                            }
                        }
                    }

                    Thread.Sleep(10);

                    if (localTime == timeCounter)
                    {
                        isProcess = false;
                        OnProcessEnd?.Invoke(counter, _mainQueue);
                    }

                }
            }
        }
        private void OnCassaClosedAction(Cassa cassa)
        {
            OnCassaClosed?.Invoke(cassa);
            casses.Remove(cassa);
            foreach (IPerson item in cassa)
            {
                _mainQueue.Enqueue(item, item.Priority);
                OnPersonBackToQueue.Invoke(item);
                if (TryChooseCassa(_mainQueue.Peek()))
                {
                    _mainQueue.Dequeue();
                }
                else
                {
                    OnPersonBackToQueue?.Invoke(_mainQueue.Peek());
                }
            }
        }
        private bool TryChooseCassa(IPerson person)
        {

            List<Cassa> availableCasses = casses.Where(x => x.IsAvailable).ToList();
            List<Cassa> filtered = availableCasses.Where(x => x.Filter(person)).ToList();
            List<Cassa> minPersonsCassa = filtered.Where(c => c.Count == filtered.Select(x => x.Count).Min()).ToList();
            if (minPersonsCassa.Count == 1)
            {
                minPersonsCassa[0].Add(person);
                OnArrivedToCassa?.Invoke(person, minPersonsCassa[0], localTime);
                return true;
            }
            else if (minPersonsCassa.Count > 0)
            {
                var tmp = minPersonsCassa.Select(x => Math.Abs(x.XCoord - person.Coordinate)).Min();
                List<Cassa> nearCassa = minPersonsCassa.Where(x => Math.Abs(x.XCoord - person.Coordinate) == minPersonsCassa.Select(x => Math.Abs(x.XCoord - person.Coordinate)).Min()).ToList();
                nearCassa[0].Add(person);
                OnArrivedToCassa?.Invoke(person, nearCassa[0], localTime);
                return true;

            }
            else return false;
        }
        private void OnCassaMaxAmountAction(Cassa cassa)
        {
            if (cassa.IsWasMax == false)
            {
                OnCassaMaxAmount?.Invoke(_mainQueue, cassa);
                cassa.IsWasMax = true;
            }
            else
            {
                cassa.IsAvailable = false;
            }
        }
    }
}
