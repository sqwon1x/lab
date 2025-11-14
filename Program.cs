namespace lab2_KN23
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderIntro();
            ShowMainMenu();
        }

        // Інтро
        public static void RenderIntro()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("===========================================");
            Console.WriteLine("========== Ласкаво просимо до Cafe Bar =====");
            Console.WriteLine("===========================================");
            Console.ResetColor();
        }

        // Гет ввід користувача
        public static double GetUserInput(string prompt = "Введіть число:")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(prompt + " ");

            bool isNumber = Double.TryParse(Console.ReadLine(), out double choice);

            if (!isNumber)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("❌ Ви ввели не число!");
                Console.ResetColor();
                return GetUserInput(prompt);
            }

            Console.ResetColor();
            return choice;
        }

        // Головне меню
        public static void ShowMainMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("========== ГОЛОВНЕ МЕНЮ ==========");
            Console.WriteLine("1. Напої");
            Console.WriteLine("2. Страви");
            Console.WriteLine("3. Оформити замовлення");
            Console.WriteLine("4. Пошук");
            Console.WriteLine("5. Статистика");
            Console.WriteLine("6. Вихід");
            Console.WriteLine("==================================");
            Console.ResetColor();

            double choice = GetUserInput("Вибреіть пункт меню:");

            switch (choice)
            {
                case 1: ShowDrinkMenu(); break;
                case 2: ShowFoodMenu(); break;
                case 3: MakeOrder(); break;
                case 4:
                    Console.WriteLine("Функція в розробці...");
                    ReturnToMenu();
                    break;
                case 5:
                    Console.WriteLine("Функція в розробці...");
                    ReturnToMenu();
                    break;
                case 6:
                    Console.WriteLine("До зустрічі!");
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("❌ Неправильний вибір. Спробуйте ще раз.");
                    ShowMainMenu();
                    break;
            }
        }

        // Меню напоїв
        public static void ShowDrinkMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("======= МЕНЮ НАПОЇВ =======");
            Console.WriteLine("Кава – 25 грн");
            Console.WriteLine("Чай – 20 грн");
            Console.WriteLine("Сік – 35 грн");
            Console.WriteLine("Пиво – 30 грн");
            Console.ResetColor();

            ReturnToMenu();
        }

        // Меню страв
        public static void ShowFoodMenu()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("======= МЕНЮ СТРАВ =======");
            Console.WriteLine("Сендвіч – 40 грн");
            Console.WriteLine("Кекс – 50 грн");
            Console.WriteLine("Салат – 50 грн");
            Console.WriteLine("Суп – 50 грн");
            Console.WriteLine("Піца – 150 грн");
            Console.WriteLine("Бургер – 80 грн");
            Console.ResetColor();

            ReturnToMenu();
        }

        // Оформлення замовлення
        public static void MakeOrder()
        {
            // Ціни
            double priceCoffee = 25;
            double priceTea = 20;
            double priceJuice = 35;
            double priceBeer = 30;

            double priceSandwich = 40;
            double priceCupcake = 50;
            double priceSalad = 50;
            double priceSoup = 50;
            double pricePizza = 150;
            double priceBurger = 80;

            Console.WriteLine("=== Введіть кількість товарів ===");

            // Напої
            double coffee = GetUserInput("Кава (чашок):");
            double tea = GetUserInput("Чай (чашок):");
            double juice = GetUserInput("Сік (склянок):");
            double beer = GetUserInput("Пиво (пляшок):");

            // Страви
            double sandwich = GetUserInput("Сендвічі (шт):");
            double cupcake = GetUserInput("Кекси (шт):");
            double salad = GetUserInput("Салати (тарілок):");
            double soup = GetUserInput("Супи (тарілок):");
            double pizza = GetUserInput("Піци (шт):");
            double burger = GetUserInput("Бургери (шт):");

            // Підрахунок вартості
            double totalPrice =
                coffee * priceCoffee +
                tea * priceTea +
                juice * priceJuice +
                beer * priceBeer +
                sandwich * priceSandwich +
                cupcake * priceCupcake +
                salad * priceSalad +
                soup * priceSoup +
                pizza * pricePizza +
                burger * priceBurger;

            // Рандомна знижка
            double randomDiscount = Math.Round(new Random().NextDouble() * 10);
            double discountTotal = Math.Round(totalPrice * (randomDiscount / 100));

            // Вивід підсумку
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n=== ПІДСУМОК ЗАМОВЛЕННЯ ===");
            Console.WriteLine($"Загальна вартість: {totalPrice} грн");
            Console.WriteLine($"Знижка: {randomDiscount}%");
            Console.WriteLine($"До оплати: {totalPrice - discountTotal} грн");
            Console.ResetColor();

            ReturnToMenu();
        }

        // Повернення в меню
        public static void ReturnToMenu()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу, щоб повернутися в меню...");
            Console.ReadKey();
            ShowMainMenu();
        }
    }
}
