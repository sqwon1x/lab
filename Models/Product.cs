namespace product_lab5
{
    using System;
    using System.Globalization;

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public Product(int id, string name, double price, string description = "")
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }

        // CSV
        public string ToCsv()
        {
            return $"{Id};{Sanitize(Name)};{Price.ToString(CultureInfo.InvariantCulture)};{Sanitize(Description)}";
        }

        public static bool TryFromCsv(string line, out Product product)
        {
            product = null;

            try
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    return false;
                }

                var p = line.Split(';');
                if (p.Length != 4)
                {
                    return false;
                }

                if (!int.TryParse(p[0], out int id))
                {
                    return false;
                }

                if (!double.TryParse(p[2], NumberStyles.Float, CultureInfo.InvariantCulture, out double price))
                {
                    return false;
                }

                product = new Product(id, p[1], price, p[3]);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string Sanitize(string s)
        {
            return (s ?? string.Empty).Replace(";", ",").Trim();
        }

        // Вивід
        public void PrintRow()
        {
            Console.WriteLine($"{Id,-4} | {Name,-20} | {Price,8:F2} грн | {Description,-20}");
        }
    }
}
