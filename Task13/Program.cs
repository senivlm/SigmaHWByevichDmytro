using System;
using Task13.CassaFolder;
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
                //personGenerator.RandomPersonsIntoFIleGenerate("../../../Files/Data.txt", 100, minTimeServiceBound: 10,maxTimeServiceBound:200);

                TimeCordinator timeCoordinator = new(
                    casses: new()
                    {
                        new Cassa(1),
                        new Cassa(2),
                        new Cassa(9),
                        new Cassa(5)
                    },
                    path: "../../../Files/Data.txt");
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
            Console.WriteLine(person+" arrived");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void OnProcessEndLog(int count)
        {
            Logger.Instance.Log($"the persons amount passed through casses: {count}");

        }
        private static void OnProcessEnd(int count)
        {
            Console.WriteLine($"the persons amount passed through casses: {count}");
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
            Console.ForegroundColor = ConsoleColor.Red;
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
    }
}
