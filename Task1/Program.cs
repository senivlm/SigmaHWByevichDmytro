using System;
using System.Collections.Generic;

namespace Task1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Product product1 = new Product();
            Product product2 = new Product("someProduct", 20.5, 3.4);
            Product product3 = new Product("Tomato", 15.3, 1);
            Product product4 = new Product("Milk", 34, 3);
            try
            {
                Product product5 = new Product("NegativPriceProduct", -2, 0);
                Product product6 = new Product("NegativWeightProduct", 0, -2);

            }
            catch (ArgumentException e)
            {

                Console.WriteLine(e.Message);
            }
            List<Product> products = new List<Product>();

            products.Add(product1);
            products.Add(product2);
            products.Add(product3);
            products.Add(product4);


            Buy buy = new Buy();
            Buy buy1 = new Buy(products);
            Buy buy2 = new Buy(products);

            Console.WriteLine(Equals(buy1, buy2));
            buy2.ProductList.Add(product1);
            Console.WriteLine(Equals(buy1, buy2));

            buy2.AddProduct(product1);
            Console.WriteLine(Equals(buy1, buy2));

            Check.PrintCheck(buy);
            Check.PrintCheck(buy1);
            Console.WriteLine();
            Console.WriteLine(buy2);
        }
    }
}

