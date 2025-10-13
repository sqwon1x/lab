namespace lab1_KN23
{
    class Program
    {
        static void Main(string[] args)
        {
            // Вітання
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to Cafe Bar!");
            Console.ResetColor();

            // Меню
            Console.WriteLine("Ось наші товари: ");
            Console.WriteLine("== Напої ==");
            Console.WriteLine("1. Кава - 25 грн");
            Console.WriteLine("2. Чай - 20 грн");
            Console.WriteLine("3. Сік - 35 грн");
            Console.WriteLine("4. Пиво - 30 грн");

            Console.WriteLine("\n== Страви ==");
            Console.WriteLine("5. Сендвіч - 40 грн");
            Console.WriteLine("6. Кекс - 50 грн");
            Console.WriteLine("7. Салат - 50 грн");
            Console.WriteLine("8. Суп - 50 грн");
            Console.WriteLine("9. Піца - 150 грн");
            Console.WriteLine("10. Бургер - 80 грн");

            // Ввід товарів з клавіатри
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n== Введення кількості напоїв ==");
            Console.ResetColor();

            Console.Write("Введіть кількість чашок кави: ");
            double coffe = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введіть кількість чашок чаю: ");
            double tea = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введіть кількість склянок соку: ");
            double juice = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введіть кількість пляшок пива: ");
            double beer = Convert.ToDouble(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n== Введення кількості страв ==");
            Console.ResetColor();

            Console.Write("Введіть кількість сендвічів: ");
            double sandwich = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введіть кількість кексів: ");
            double cupcake = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введіть кількість тарілок салату: ");
            double salad = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введіть кількість тарілок супу: ");
            double soup = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введіть кількість піц: ");
            double pizza = Convert.ToDouble(Console.ReadLine());

            Console.Write("Введіть кількість бургерів: ");
            double burger = Convert.ToDouble(Console.ReadLine());

            // Ціни на товари
            double priceCoffe = 25;
            double priceTea = 20;
            double priceJuice = 35;
            double priceBeer = 30;
            double priceSandwich = 40;
            double priceCupcake = 50;
            double priceSalad = 50;
            double priceSoup = 50;
            double pricePizza = 150;
            double priceBurger = 80;

            // Обрахунок вартості товарів
            double totalCoffe = coffe * priceCoffe;
            double totalTea = tea * priceTea;
            double totalJuice = juice * priceJuice;
            double totalBeer = beer * priceBeer;
            double totalSandwich = sandwich * priceSandwich;
            double totalCupcake = cupcake * priceCupcake;
            double totalSalad = salad * priceSalad;
            double totalSoup = soup * priceSoup;
            double totalPizza = pizza * pricePizza;
            double totalBurger = burger * priceBurger;

            // Загальна вартість замовлення
            double totalPrice = totalCoffe + totalTea + totalJuice + totalBeer
                + totalSandwich + totalCupcake + totalSalad + totalSoup
                + totalPizza + totalBurger;

            // Знижка
            double randomDiscount = Math.Round(new Random().NextDouble() * 10);
            double discountTotal = Math.Round(totalPrice * (randomDiscount / 100));

            // Вивід підсумків
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n== Вартість замовлення ==");

            Console.WriteLine("\n-- Напої --");
            Console.WriteLine($"Кава: {coffe}, вартість {totalCoffe} грн");
            Console.WriteLine($"Чай: {tea}, вартість {totalTea} грн");
            Console.WriteLine($"Сік: {juice}, вартість {totalJuice} грн");
            Console.WriteLine($"Пиво: {beer}, вартість {totalBeer} грн");

            Console.WriteLine("\n-- Страви --");
            Console.WriteLine($"Сендвіч: {sandwich}, вартість {totalSandwich} грн");
            Console.WriteLine($"Кекс: {cupcake}, вартість {totalCupcake} грн");
            Console.WriteLine($"Салат: {salad}, вартість {totalSalad} грн");
            Console.WriteLine($"Суп: {soup}, вартість {totalSoup} грн");
            Console.WriteLine($"Піца: {pizza}, вартість {totalPizza} грн");
            Console.WriteLine($"Бургер: {burger}, вартість {totalBurger} грн");
            Console.ResetColor();

            // Кінцева сума
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nЗагальна вартість замовлення: {totalPrice} грн");
            Console.WriteLine($"Ваша знижка: {randomDiscount}%");
            Console.WriteLine($"Сума до оплати зі знижкою: {totalPrice - discountTotal} грн");
            Console.ResetColor();

            // Прощання
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nДякуємо за замовлення! Гарного дня!");
            Console.ResetColor();
        }
    }
}    