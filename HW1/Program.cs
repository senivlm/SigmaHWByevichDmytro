using ProductsProj;

Product product1 = new Product();
Product product2 = new Product("someProduct", 100, 0.3);

DairyProduct dairyProduct1 = new DairyProduct("someDairyProduct1", 100, 3.2, 8);
DairyProduct dairyProduct2 = new DairyProduct("someDairyProduct2", 100, 1, 6);
DairyProduct dairyProduct3 = new DairyProduct("someDairyProduct3", 100, 4.1, 4);
DairyProduct dairyProduct4 = new DairyProduct("someDairyProduct4", 100, 5.2, 2);
DairyProduct dairyProduct5 = new DairyProduct("someDairyProduct5", 100, 7.2, 1);


List<Product> products = new List<Product>();
products.Add(product1);
products.Add(product2);
products.Add(dairyProduct1);
products.Add(dairyProduct2);
products.Add(dairyProduct3);
products.Add(dairyProduct4);
products.Add(dairyProduct5);

//Buy buy = new Buy(products);
//Check.PrintCheck(buy);
dairyProduct1.ChangePrice(10);
dairyProduct2.ChangePrice(50);
dairyProduct3.ChangePrice(100);
dairyProduct4.ChangePrice(-10);
dairyProduct5.ChangePrice(1000);
//Check.PrintCheck(buy);

Storage storage = new StorageBuilder().AddProductList(products)
                                    .AddProduct(product2)
                                    .Build();
for (int i = 0; i < storage.Count; i++)
{
    Console.WriteLine(storage[i]);
}
