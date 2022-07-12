using System;
using System.Collections.Generic;
using Task13.CassaFolder;
using Task13.Enums;
using Task13.Persons;

namespace Task13
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            try
            {

                Logger.Instance.Path = "../../../Files/Logs.txt";

                //PersonGenerator personGenerator = new PersonGenerator();
                //personGenerator.RandomPersonsIntoFIleGenerate("../../../Files/Data.txt", 50, minTimeServiceBound: 10, maxTimeServiceBound: 50);

                TimeCordinator timeCoordinator = new(
                    casses: new()
                    {
                        new Cassa(1, (x) => x.Status == Status.MILITARY, 5),
                        new Cassa(3, (x) => x.Status == Status.GP_1_DISABILITY, 5),
                        new Cassa(5, (x) => x.Status == Status.GP_2_DISABILITY, 5),
                        new Cassa(7, (x) => true, 5),
                        new Cassa(9, (x) => true, 5),

                    },
                    path: "../../../Files/Data.txt",
                    timeCounter: 1000
                    );
                Logger.Instance.Path = "../../../Files/Result.txt";
                timeCoordinator.OnArrivedToCassa += OnArrivedToCassaAction;
                timeCoordinator.OnArrivedToCassa += OnArrivedToCassaActionLog;
                timeCoordinator.OnCassaClosed += OnCassaClosedAction;
                timeCoordinator.OnCassaClosed += OnCassaClosedActionLog;
                timeCoordinator.OnPersonServiced += OnPersonServicedAtion;
                timeCoordinator.OnPersonServiced += OnPersonServicedAtionLog;
                timeCoordinator.OnPersonBackToQueue += OnPersonBackToQueueAction;
                timeCoordinator.OnPersonBackToQueue += OnPersonBackToQueueActionLog;
                timeCoordinator.OnProcessEnd += OnProcessEnd;
                timeCoordinator.OnProcessEnd += OnProcessEndLog;
                timeCoordinator.OnPersonArrived += OnPersonArrived;
                timeCoordinator.OnPersonArrived += OnPersonArrivedLog;
                timeCoordinator.OnCassaMaxAmount += OnCassaMaxAmountAction;


                timeCoordinator.Process();

                Console.WriteLine("Finish");


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                Console.WriteLine($"Програма заверешeна, у логи було занесено: {Logger.Instance.ExCount} запис(ів)");
            }
        }
        private static void OnPersonArrivedLog(IPerson person)
        {
            Logger.Instance.Log(person + " arrived");
        }
        private static void OnPersonArrived(IPerson person)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(person + " arrived");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void OnProcessEndLog(int count, PriorityQueue<IPerson, int> mainQueue)
        {
            Logger.Instance.Log($"the persons amount passed through casses: {count}");
            Logger.Instance.Log($"In queue last {mainQueue.Count} persons");
        }
        private static void OnProcessEnd(int count, PriorityQueue<IPerson, int> mainQueue)
        {
            Console.WriteLine($"the persons amount passed through casses: {count}");
            Console.WriteLine($"In queue last {mainQueue.Count} persons");            
        }
        private static void OnPersonServicedAtionLog(Cassa cassa, IPerson person)
        {
            Logger.Instance.Log(person + " has been serviced on cassa on coords: " + cassa.XCoord);

        }
        private static void OnPersonServicedAtion(Cassa cassa, IPerson person)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(person + " has been serviced on cassa on coords: " + cassa.XCoord);
            Console.ForegroundColor = ConsoleColor.Gray;

        }
        private static void OnArrivedToCassaActionLog(IPerson person, Cassa cassa, int time)
        {
            Logger.Instance.Log($"{person} arrived to cassa at coord:{cassa.XCoord}, at time: {time}");
        }
        private static void OnArrivedToCassaAction(IPerson person, Cassa cassa, int time)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{person} arrived to cassa at coord:{cassa.XCoord}, at time: {time}, service time: {person.TimeService}, priority: {person.Priority}");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void OnCassaClosedActionLog(Cassa cassa)
        {
            Logger.Instance.Log($"Cassa on coord: {cassa.XCoord} closed");
        }
        private static void OnCassaClosedAction(Cassa cassa)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Cassa on coord: {cassa.XCoord} closed");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void OnPersonBackToQueueActionLog(IPerson person)
        {
            Logger.Instance.Log($": {person} back to queue");
        }
        private static void OnPersonBackToQueueAction(IPerson person)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($": {person} back to queue");
            Console.ForegroundColor = ConsoleColor.Magenta;
        }
        private static void OnCassaMaxAmountAction(PriorityQueue<IPerson, int> mainQueue, Cassa cassa)
        {
            var status = (Status)new Random().Next(0, 5);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"cassa on coord {cassa.XCoord} reprofiled to {status}");
            Logger.Instance.Log($"cassa on coord {cassa.XCoord} reprofiled to {status}");
            Console.ForegroundColor = ConsoleColor.Gray;
            cassa.Filter = x => x.Status == status;            
            List<IPerson> filteredPersons = new List<IPerson>();
            if (cassa.Filter(cassa.Peek()))
            {
                filteredPersons.Add(cassa.Dequeue());
            }
            else
            {
                var tmpPerson = cassa.Dequeue();
                mainQueue.Enqueue(tmpPerson, tmpPerson.Priority);
            }
            foreach (var person in filteredPersons)
            {
                cassa.Add(person);
            }            
        }
    }
}
