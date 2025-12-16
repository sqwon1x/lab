using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using product_lab5;

namespace lab5
{
    class Program
    {
        static string dataDir = "data";
        static string productsFile = "data/products.csv";
        static string usersFile = "data/users.csv";
        
        static void Main(string[] args)
        {
            Directory.CreateDirectory(dataDir);

            if (!File.Exists(productsFile))
                File.WriteAllText(productsFile, "Id;Name;Price;Description\n");

            if (!File.Exists(usersFile))
                File.WriteAllText(usersFile, "Id;Email;Salt;Hash\n");

            LoginMenu();
            MainMenu();
        }

        // CSV
        static List<Product> LoadProducts()
        {
            var list = new List<Product>();
            foreach (var line in File.ReadAllLines(productsFile).Skip(1))
                if (Product.TryFromCsv(line, out Product p))
                    list.Add(p);
            return list;
        }

        static void RewriteProducts(List<Product> products)
        {
            var lines = new List<string> { "Id;Name;Price;Description" };
            lines.AddRange(products.Select(p => p.ToCsv()));
            File.WriteAllLines(productsFile, lines);
        }

        static int GenerateNextProductId()
        {
            int max = 0;
            foreach (var l in File.ReadAllLines(productsFile).Skip(1))
            {
                var p = l.Split(';');
                if (int.TryParse(p[0], out int id) && id > max)
                    max = id;
            }
            return max + 1;
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
            Console.Write("Назва: ");
            string name = Console.ReadLine();

            Console.Write("Ціна: ");
            if (!double.TryParse(Console.ReadLine(), out double price))
            {
                PrintError("Некоректна ціна");
                return;
            }

            Console.Write("Опис: ");
            string desc = Console.ReadLine();

            int id = GenerateNextProductId();
            var p = new Product(id, name, price, desc);

            File.AppendAllText(productsFile, p.ToCsv() + "\n");
            PrintSuccess("Продукт додано");
        }
        
        // Список
        static void PrintAllProducts()
        {
            var products = LoadProducts();
            if (products.Count == 0)
            {
                PrintError("Список порожній");
                return;
            }

            Console.WriteLine("\nID | Назва | Ціна | Опис");
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

            var products = LoadProducts();
            foreach (var p in products)
                if (p.Name.ToLower().Contains(q))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Знайдено:");
                    Console.ResetColor();
                    
                    p.PrintRow();
                    return;
                }

            PrintError("Нічого не знайдено");
        }

        // Видалення
        static void DeleteProduct()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Введіть ID для видалення: ");
            Console.ResetColor();

            if (!int.TryParse(Console.ReadLine(), out int id)) return;
           
            var products = LoadProducts();
            int before = products.Count;
            
            products = products.Where(p => p.Id != id).ToList();
            
            if (before == products.Count)
            {
                PrintError("Товар не знайдено.");
                return;
            }
            
            RewriteProducts(products);
            PrintSuccess("Видалено");
        }

        // Сортування
        static void SortingMenu()
        {
            var products = LoadProducts();
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
            var products = LoadProducts();
            for (int i = 0; i < products.Count - 1; i++)
            {
                for (int j = 0; j < products.Count - i - 1; j++)
                {
                    if (products[j].Price > products[j + 1].Price)
                    {
                        (products[j], products[j + 1]) = (products[j + 1], products[j]);
                    }
                }
            }
        }

        // Статистика
        static void ShowStats()
        {
            var products = LoadProducts();
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

        // Авторизація
        static void LoginMenu()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n=== АВТОРИЗАЦІЯ ===");
                Console.ResetColor();
                
                Console.WriteLine("1. Вхід");
                Console.WriteLine("2. Реєстрація");
                Console.WriteLine("3. Вихід");
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Ваш вибір: ");
                Console.ResetColor();
                
                string ch = Console.ReadLine();

                if (ch == "3")
                    Environment.Exit(0);
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Email: ");
                Console.ResetColor();
                string email = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Пароль: ");
                Console.ResetColor();
                string pass = Console.ReadLine();

                if (ch == "1")
                {
                    if (Login(email, pass))
                    {
                        PrintSuccess("Успішний вхід!");
                        return;
                    }
                    else
                    {
                        PrintError("Невірний email або пароль!");
                    }
                }
                else if (ch == "2")
                {
                    Register(email, pass);
                }
                else
                {
                    PrintError("Невірний пункт меню!");
                }
            }
        }

        static void Register(string email, string password)
        {
            var lines = File.ReadAllLines(usersFile);
            if (lines.Any(l => l.Split(';').Length > 1 && l.Split(';')[1] == email))
            {
                PrintError("Email вже існує!");
                return;
            }
            
            int id = lines.Length;
            string salt = Guid.NewGuid().ToString();
            string hash = Hash(password + salt);

            File.AppendAllText(usersFile, $"{id};{email};{salt};{hash}\n");
            PrintSuccess("Зареєстровано!");
        }
        

        static bool Login(string email, string password)
        {
            foreach (var l in File.ReadAllLines(usersFile).Skip(1))
            {
                var p = l.Split(';');
                if (p.Length != 4) continue;
                if (p[1] == email)
                    return Hash(password + p[2]) == p[3];
            }
            return false;
        }

        static string Hash(string input)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(input)));
        }
        
        // Замовлення
        static void PlaceOrder()
        {
            var products = LoadProducts();
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

                if (!int.TryParse(Console.ReadLine(), out int id) || id == 0) break;

                var p = products.FirstOrDefault(x => x.Id == id);
                if (p == null)
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

                total += p.Price * qty;

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