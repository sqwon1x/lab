using System;
using System.Collections.Generic;
using product_lab4;

namespace lab4
{
    class Program
    {
        //List<T>
        static List<Product> products = new List<Product>();
        static int nextId = 1;

        static void Main(string[] args)
        {
            LoginMenu();
            MainMenu();
        }

        // Головне меню
        static void MainMenu()
        {
            int choice;
            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n=== ГОЛОВНЕ МЕНЮ ===");
                Console.ResetColor();

                Console.WriteLine("1. Продукти");
                Console.WriteLine("2. Оформити замовлення");
                Console.WriteLine("3. Вихід");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Ваш вибір: ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    PrintError("Помилка! Введіть число.");
                    continue;
                }

                switch (choice)
                {
                    case 1: ProductMenu(); break;
                    case 2: PlaceOrder(); break;
                    case 3:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Вихід...");
                        Console.ResetColor();
                        break;
                    default:
                        PrintError("Невірний пункт меню.");
                        break;
                }

            } while (choice != 3);
        }

        // Меню продуктів
        static void ProductMenu()
        {
            int choice;

            do
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n=== ПРОДУКТИ ===");
                Console.ResetColor();

                Console.WriteLine("1. Додати продукт");
                Console.WriteLine("2. Перегляд всіх продуктів");
                Console.WriteLine("3. Пошук продукту");
                Console.WriteLine("4. Видалити продукт");
                Console.WriteLine("5. Сортування");
                Console.WriteLine("6. Статистика");
                Console.WriteLine("7. Назад");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Ваш вибір: ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    PrintError("Помилка!");
                    continue;
                }

                switch (choice)
                {
                    case 1: AddProduct(); break;
                    case 2: PrintAllProducts(); break;
                    case 3: FindProduct(); break;
                    case 4: DeleteProduct(); break;
                    case 5: SortingMenu(); break;
                    case 6: ShowStats(); break;
                    case 7: return;
                    default: PrintError("Невірний пункт меню."); break;
                }

            } while (choice != 7);
        }

        // Додавання
        static void AddProduct()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Назва продукту: ");
                Console.ResetColor();
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    PrintError("Назва не може бути порожньою!");
                    return;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Ціна: ");
                Console.ResetColor();

                if (!double.TryParse(Console.ReadLine(), out double price))
                {
                    PrintError("Ціна повинна бути числом.");
                    return;
                }

                products.Add(new Product(nextId++, name, price));

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✔ Продукт успішно додано!");
                Console.ResetColor();

                // --- Запит на продовження ---
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Додати ще один продукт? (1 — так, 0 — ні): ");
                Console.ResetColor();

                string choice = Console.ReadLine();

                if (choice != "1")
                {
                    PrintSuccess("Повернення в меню продуктів...");
                    return;
                }

                Console.WriteLine(); // відступ
            }
        }
        
        // Видалення
        static void PrintAllProducts()
        {
            if (products.Count == 0)
            {
                PrintError("Список порожній.");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nID    | Назва                |   Ціна   | Опис");
            Console.WriteLine("----------------------------------------------------------");
            Console.ResetColor();

            foreach (var p in products)
                p.PrintRow();
        }

        // Пошук
        static void FindProduct()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Введіть назву: ");
            Console.ResetColor();

            string q = Console.ReadLine().ToLower();

            foreach (var p in products)
            {
                if (p.Name.ToLower().Contains(q))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Знайдено:");
                    Console.ResetColor();

                    p.PrintRow();
                    return;
                }
            }

            PrintError("Нічого не знайдено!");
        }

        // Видалення
        static void DeleteProduct()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Введіть ID для видалення: ");
            Console.ResetColor();

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                PrintError("Помилка!");
                return;
            }

            int index = products.FindIndex(p => p.Id == id);

            if (index == -1)
            {
                PrintError("Товар не знайдено.");
                return;
            }

            products.RemoveAt(index);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("✔ Видалено!");
            Console.ResetColor();
        }

        // Сортування
        static void SortingMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== СОРТУВАННЯ ===");
            Console.ResetColor();

            Console.WriteLine("1. За ціною (вбудоване)");
            Console.WriteLine("2. За назвою");
            Console.WriteLine("3. Бульбашкове сортування");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Ваш вибір: ");
            Console.ResetColor();

            if (!int.TryParse(Console.ReadLine(), out int ch)) return;

            switch (ch)
            {
                case 1:
                    products.Sort((a, b) => a.Price.CompareTo(b.Price));
                    PrintSuccess("Відсортовано!");
                    break;

                case 2:
                    products.Sort((a, b) => a.Name.CompareTo(b.Name));
                    PrintSuccess("Відсортовано!");
                    break;

                case 3:
                    BubbleSort();
                    PrintSuccess("Бульбашкове сортування завершено!");
                    break;

                default:
                    PrintError("Помилка!");
                    break;
            }
        }

        // Бульбашкове сортування
        static void BubbleSort()
        {
            for (int i = 0; i < products.Count - 1; i++)
            {
                for (int j = 0; j < products.Count - i - 1; j++)
                {
                    if (products[j].Price > products[j + 1].Price)
                    {
                        var tmp = products[j];
                        products[j] = products[j + 1];
                        products[j + 1] = tmp;
                    }
                }
            }
        }

        // Статистика
        static void ShowStats()
        {
            if (products.Count == 0)
            {
                PrintError("Список порожній!");
                return;
            }

            double min = products[0].Price;
            double max = products[0].Price;
            double sum = 0;

            foreach (var p in products)
            {
                if (p.Price < min) min = p.Price;
                if (p.Price > max) max = p.Price;
                sum += p.Price;
            }

            double avg = sum / products.Count;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n=== СТАТИСТИКА ===");
            Console.ResetColor();

            Console.WriteLine($"Кількість: {products.Count}");
            Console.WriteLine($"Мінімальна ціна: {min}");
            Console.WriteLine($"Максимальна ціна: {max}");
            Console.WriteLine($"Середня ціна: {avg:F2}");
            Console.WriteLine($"Сума: {sum}");
        }

        // Логін
        static void LoginMenu()
        {
            const string login = "admin";
            const string pass = "1234";
            int attempts = 3;

            while (attempts > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Логін: ");
                Console.ResetColor();
                string l = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Пароль: ");
                Console.ResetColor();
                string p = Console.ReadLine();

                if (l == login && p == pass)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Вхід успішний!\n");
                    Console.ResetColor();
                    return;
                }

                attempts--;
                PrintError($"Невірно! Залишилось спроб: {attempts}");
            }
            
            PrintError("Доступ заборонено.");
            Environment.Exit(0);
        }

        // Замовлення
        static void PlaceOrder()
        {
            if (products.Count == 0)
            {
                PrintError("Немає товарів!");
                return;
            }

            double total = 0;

            while (true)
            {
                PrintAllProducts();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("ID товару (0 - вихід): ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine(), out int id) || id < 0)
                {
                    PrintError("Помилка!");
                    continue;
                }

                if (id == 0) break;

                Product? selected = products.Find(p => p.Id == id);

                if (selected == null)
                {
                    PrintError("Товар не знайдено!");
                    continue;
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Кількість: ");
                Console.ResetColor();

                if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
                {
                    PrintError("Невірна кількість!");
                    continue;
                }

                total += selected.Value.Price * qty;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✔ Додано в замовлення!");
                Console.ResetColor();
            }

            double discount = new Random().Next(0, 11);
            double final = total - total * (discount / 100);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n=== ЧЕК ===");
            Console.ResetColor();

            Console.WriteLine($"Сума: {total} грн");
            Console.WriteLine($"Знижка: {discount}%");
            Console.WriteLine($"До оплати: {final} грн");
        }

        // Друк помилок/успіху
        static void PrintError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }

        static void PrintSuccess(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
    }
}
