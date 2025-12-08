namespace product_lab4;

public struct Product
{
    public int Id;
    public string Name;
    public double Price;
    public string Description;

    public Product(int id, string name, double price, string description = "")
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
    }

    public void PrintRow()
    {
        Console.WriteLine($"{Id,-5} | {Name,-20} | {Price,8:F2} грн | {Description,-20}");
    }
}