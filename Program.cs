using System;
using product_lab3;

namespace Lab3
{
    struct Product
    {
        public string Name;
        public double Price;

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }
    }

    class Program
    {
        static Product[] products = new Product[5];
        static int count = 0;

        static void Main(string[] args)
        {
            LoginMenu();

            int choice;
            do
            {
                Console.WriteLine("\n=== Всі товари === ");
                Console.WriteLine("1. Додати продукт");
                Console.WriteLine("2. Пошук продукту");
                Console.WriteLine("3. Перегляд всіх продуктів");
                Console.WriteLine("4. Статистика");
                Console.WriteLine("5. Оформити замовлення");
                Console.WriteLine("6. Вихід");
                Console.Write("Ваш вибір: ");

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Помилка! Введіть число.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddProduct();
                        break;

                    case 2:
                        FindProduct();
                        break;

                    case 3:
                        PrintAllProducts();
                        break;

                    case 4:
                        ShowStats();
                        break;

                    case 5:
                        PlaceOrder();
                        break;
                    
                    case 6:
                        Console.WriteLine("Вихід...");
                        break;

                    default:
                        Console.WriteLine("Невірний пункт меню.");
                        break;
                }

            } while (choice != 6);
        }

        // Login
        static void LoginMenu()
        {
            const string correctLogin = "admin";
            const string correctPass = "1234";

            int attempts = 3;

            while (attempts > 0)
            {
                Console.Write("Логін: ");
                string login = Console.ReadLine();

                Console.Write("Пароль: ");
                string pass = Console.ReadLine();

                if (login == correctLogin && pass == correctPass)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Успішний вхід!\n");
                    Console.ResetColor();
                    return;
                }
                else
                {
                    attempts--;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Невірний логін або пароль. Залишилось спроб: {attempts}");
                    Console.ResetColor();
                }
            }

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nСпроби Вичерпано! Програма завершує роботу.");
            Console.ResetColor();
            Environment.Exit(0);
        }
        
        // Додати продукт
        static void AddProduct()
        {
            while (true)
            {
                if (count >= products.Length)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nМаксимальна кількість продуктів!");
                    Console.ResetColor();
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Введіть назву продукту: ");
                Console.ResetColor();
                string? input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Помилка: поле не може бути порожнім.");
                    return;
                }
                string name = input!;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Введіть ціну: ");
                Console.ResetColor();
                if (!double.TryParse(Console.ReadLine(), out double price))
                {
                    Console.WriteLine("Помилка: ціна має бути числом.");
                    return;
                }

                products[count] = new Product(name, price);
                count++;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Продукт додано!");
                Console.ResetColor();

                // Запит: додати ще?
                Console.WriteLine("\nДодати ще один продукт?");
                Console.WriteLine("1 — Так, 2 - Ні");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();

                if (choice != "1")
                {
                    Console.WriteLine("Повернення в меню...");
                    break;
                }
            }
        }

        // Знайти продукт
        static void FindProduct()
        {
            if (count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nСписок порожній! Спочатку додайте продукти.");
                Console.ResetColor();
                return;
            }    
            
            Console.Write("Введіть назву для пошуку: ");
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Помилка: поле не може бути порожнім.");
                return;
            }
            string name = input!;

            for (int i = 0; i < count; i++)
            {
                if (products[i].Name.ToLower() == name.ToLower())
                {
                    Console.WriteLine($"Знайдено: {products[i].Name} — {products[i].Price} грн");
                    return;
                }
            }

            Console.WriteLine("\nПродукт не знайдено!");
        }

        // Список продуктів
        static void PrintAllProducts()
        {
            if (count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nСписок порожній!");
                Console.ResetColor();
                return;
            }

            Console.WriteLine("\n=== Список продуктів ===");
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i].Name} — {products[i].Price} грн");
            }
        }

        // Статистика
        static void ShowStats()
        {
            if (count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nНемає даних для статистики!");
                Console.ResetColor();
                return;
            }

            double min = products[0].Price;
            double max = products[0].Price;
            double sum = 0;
            int expensiveCount = 0;

            for (int i = 0; i < count; i++)
            {
                double price = products[i].Price;

                if (price < min) min = price;
                if (price > max) max = price;

                if (price > 100) expensiveCount++;

                sum += price;
            }

            double avg = sum / count;

            Console.WriteLine("\n=== Статистика ===");
            Console.WriteLine($"Загальна сума: {sum} грн");
            Console.WriteLine($"Мінімальна ціна: {min} грн");
            Console.WriteLine($"Максимальна ціна: {max} грн");
            Console.WriteLine($"Середня ціна: {avg:F2} грн");
            Console.WriteLine($"Кількість продуктів дорожчих за 100 грн: {expensiveCount}");
        }

        // Оформлення замовлення
        static void PlaceOrder()
        {
            if (count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nСписок продуктів порожній! Спочатку додайте продукти.");
                Console.ResetColor();
                return;
            }
            
            Console.WriteLine("\n=== Оформлення замовлення ===");
            double total = 0;

            while (true)
            {
                // Показ списку продуктів 
                Console.WriteLine("\nДоступні товари: ");
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine($"{i + 1}. {products[i].Name} - {products[i].Price} грн");
                } 
                
                Console.Write("Введіть номер продукту для замовлення: ");
                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 0 || choice > count)
                {
                    Console.WriteLine("Невірний вибір. Спробуйте ще.");
                    continue;
                }
                
                if (choice == 0)
                    break;

                Product selected = products[choice - 1];
                
                Console.Write($"Введіть кількість для {selected.Name}: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
                {
                    Console.WriteLine("Невірна кількість. Спробуйте ще.");
                    continue;
                }

                double price = selected.Price * quantity;
                total += price;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Додано {quantity} x {selected.Name} - {price} грн до замовлення.");
                Console.ResetColor();
                
                Console.WriteLine("\nБажаєте додати ще продукт до замовлення? (1 - Так, 2 - Ні)");
                string? more = Console.ReadLine();
                if (string.IsNullOrEmpty(more) || more != "1")
                    break;
            }

            double randomDiscount = Math.Round(new Random().NextDouble() * 10);
            double discountTotal = Math.Round(total * (randomDiscount / 100));
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nЗагальна сума замовлення: {total} грн");
            Console.WriteLine($"Ваша знижка: {randomDiscount}%");
            Console.WriteLine($"Сума до оплати зі знижкою: {total - discountTotal} грн");
            Console.ResetColor();
        }
    }
}
