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
        public event Action<int> OnProcessEnd;

        private Random random = new();
        private int timeCounter = 1000;
        private int localTime;
        private string path;

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
                            OnPersonArrived?.Invoke(person);
                            Cassa tmpCassa = ChooseCassa(person);
                            tmpCassa.Add(person);
                            OnArrivedToCassa?.Invoke(person, tmpCassa, localTime);
                            counter++;
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
                        OnProcessEnd?.Invoke(counter);
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
                OnPersonBackToQueue.Invoke(item);
                Cassa tmpCassa = ChooseCassa(item);
                tmpCassa.Add(item);
                OnArrivedToCassa?.Invoke(item, tmpCassa, localTime);
            }
        }
        private Cassa ChooseCassa(IPerson person)
        {
            List<Cassa> minPersonsCassa = casses.Where(c => c.Count == casses.Select(x => x.Count).Min()).ToList();
            if (minPersonsCassa.Count == 1)
            {
                return minPersonsCassa[0];
            }
            else
            {
                List<Cassa> nearCassa = minPersonsCassa.Where(x => Math.Abs(x.XCoord - person.Coordinate) == minPersonsCassa.Select(x => Math.Abs(x.XCoord - person.Coordinate)).Min()).ToList();
                return nearCassa[0];
            }

        }
    }
}
