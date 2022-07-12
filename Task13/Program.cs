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
                //personGenerator.RandomPersonsIntoFIleGenerate("../../../Files/Data.txt", 100);

                TimeCordinator timeCoordinator = new(
                    casses: new()
                    {
                        new Cassa(1),
                        new Cassa(2),
                        new Cassa(9),
                        new Cassa(5)
                    },
                    path: "../../../Files/Data.txt");
                timeCoordinator.OnArrivedToCassa += PrintToConsole;


                foreach (var item in timeCoordinator.Process())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(item);
                    Console.ForegroundColor = ConsoleColor.Gray;

                };
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
        private static void PrintToConsole(IPerson person, Cassa cassa,int time)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{person} arrived to cassa at coord:{cassa.XCoord}, at time: {time}");
            Console.ForegroundColor = ConsoleColor.Gray;

        }
    }
}
