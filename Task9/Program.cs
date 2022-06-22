using System;
using Task9.FIleHandler;
using Task9.Services;
using Task9.Validator;

namespace Task9
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                Console.InputEncoding = System.Text.Encoding.Unicode;
                FileHandler<MenuModel> menuData = new FileHandler<MenuModel>("../../../Files/Menu.txt");
                menuData.ReadToObject(out MenuModel menu, StreamReadersService.MenuReader, null);
                Console.WriteLine(menu);

                Console.WriteLine("Після змін");
                DishModel dish = new DishModel();
                dish.Name = "TestName";
                dish.TryAddIngridient("ingr1", 0.3);
                dish.TryAddIngridient("ingr2", 0.4);
                dish.TryAddIngridient("ingr3", 1.3);
                dish.TryAddIngridient("ingr4", 0.5);

                menuData.WriteToFile(dish, true);
                menuData.ReadToObject(out menu, StreamReadersService.MenuReader, null);
                Console.WriteLine(menu);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
