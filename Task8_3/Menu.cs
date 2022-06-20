using System;
using System.Collections.Generic;

namespace Task8_3
{
    internal class Menu
    {
        private List<Option> _options;
        public string Title { get; set; }
        public Menu()
        {
            _options = new List<Option>();
            Title = "Select option: ";
        }
        public Menu(IEnumerable<Option> options, string title = "Select option: ")
        {
            _options = new List<Option>(options);
            Title = title;
        }
        public void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine(Title);
            for (int i = 0; i < _options.Count; i++)
            {
                Console.WriteLine($"{i} > {_options[i]};");
            }
            Console.WriteLine("X > Закінчити");
            Console.Write("Введіть номер пункту > ");
            if (InvokeSelectedOption())
            {
                PrintMenu();
            }
        }
        private bool InvokeSelectedOption()
        {
            string inputedValue = Console.ReadLine();
            Console.Clear();

            if (inputedValue.ToLower() == "x" || inputedValue.ToLower() == "х")
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
