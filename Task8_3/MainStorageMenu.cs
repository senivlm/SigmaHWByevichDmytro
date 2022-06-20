using System;
using System.Collections.Generic;

namespace Task8_3
{
    internal class MainStorageMenu
    {
        private Storage _storage;
        private StorageLogHandler _logHandler;
        private FileHandler _storageFileHandler;
        private Menu _mainMenu;

        public MainStorageMenu(string storagePath, string logPath)
        {
            _storage = new Storage();
            StorageLogger.Path = logPath;
            _storageFileHandler = new FileHandler(storagePath);
            _logHandler = new StorageLogHandler(StorageLogger.Path);
            SetMainMenu();
        }
        private void SetMainMenu()
        {
            List<Option> mainMenuOptions = new List<Option>()
            {
                new Option("Зчитати дані у склад з файлу",()=>ReadStorageOption()),
                new Option("Вивести дані складу", ()=>Console.WriteLine(_storage.ToString()??"Склад пустий")),
                new Option("Обробити дані з реєстру хибних записів", ()=>LogHandlerMenu()),
                new Option("Зберегти дані складу", () => _storageFileHandler.WriteFromObject(_storage))
            };
            _mainMenu = new Menu(mainMenuOptions);
        }
        private void ReadStorageOption()
        {
            try
            {
                _storageFileHandler.ReadToObject(_storage);
                Console.WriteLine("Файл успішно прочитан");

            }
            catch (Exception)
            {
                Console.WriteLine("Файл не вдалося зчитати");
                Console.WriteLine("Змінити файл складу?");
                Console.Write("Введіть (y/n) > ");
                string answer = Console.ReadLine();
                if (answer == "y")
                {
                    Console.Write("Введіть назву файла > ");
                    _storageFileHandler = new FileHandler(Console.ReadLine());
                }
            }
        }
        public void PrintMenu()
        {
            _mainMenu.PrintMenu();
        }
        private void LogHandlerMenu()
        {
            Console.Write("Введіть дату починаючи з якої шукати записи > ");
            DateTime start = DateTime.Now;
            if (!DateTime.TryParse(Console.ReadLine(), out start))
            {
                Console.WriteLine("Невірний формат дати");
                return;
            }

            try
            {
                List<Option> options = new List<Option>();
                foreach (var item in _logHandler.GetLogsAfterDate(start))
                {
                    options.Add(new Option(item, () => LogHandler(item)));
                }
                Menu menu = new Menu(options, "Оберіть запис який ви хочете змінити:");
                menu.PrintMenu();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void LogHandler(string log)
        {
            string[] splitedLine = log.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries)[..^2];
            List<Option> options = new List<Option>()
            {
                new Option("Змінити запис", ()=>ChangeProduct(ref log)),
                new Option("Додати до складу", () => AddToStorage(log))
            };
            Menu menu = new Menu(options);
            menu.PrintMenu();
        }
        private void AddToStorage(string line)
        {
            if (_storage.TryAddProductFromLine(line))
            {
                Console.WriteLine("Запис успішно додан");
                return;
            }
            Console.WriteLine(line);
            Console.WriteLine("Запис не додан");
        }
        private void ChangeProduct(ref string line)
        {
            Console.WriteLine($"Змінити {line}");
            Console.WriteLine("Введіть тип продукту > ");

            if (!Enum.TryParse(Console.ReadLine(), out ProductType productType))
            {
                Console.WriteLine("Невідомий тип продукту");
                return;
            }
            try
            {
                Product product = null;
                switch (productType)
                {
                    case ProductType.product:
                        product = new Product();
                        break;
                    case ProductType.meat:
                        product = new Meat();
                        break;
                    case ProductType.dairy:
                        product = new DairyProduct();
                        break;
                    default:
                        break;
                }
                product.ConsoleSet();
                line = productType + " " + product.ToString();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
