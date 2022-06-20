using System;
using System.Collections.Generic;

namespace Task8_1
{
    internal class Menu
    {
        private List<Option> _options;
        public Menu()
        {
            _options = new List<Option>();
        }
        public Menu(List<Option> options)
        {
            _options = new List<Option>(options);
        }
        public void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Select option: ");
            for (int i = 0; i < _options.Count; i++)
            {
                Console.WriteLine($"{i} > {_options[i]};");
            }
            Console.WriteLine("X > Close menu");
            Console.Write("Input > ");
            if (InvokeSelectedOption())
            {
                PrintMenu();
            }
        }
        private bool InvokeSelectedOption()
        {
            string inputedValue = Console.ReadLine();
            Console.Clear();

            if (inputedValue.ToLower() == "x")
            {
                return false;
            }
            if (!int.TryParse(inputedValue, out int selectedOption) || selectedOption < 0 || selectedOption >= _options.Count)
            {
                Console.WriteLine("Обран невірний пункт");
            }
            else
            {
                _options[selectedOption].Run();
            }
            Console.WriteLine("\nНажміть будь яку кнопку щоб продовжити...");
            Console.ReadKey();
            return true;
        }

    }
}
