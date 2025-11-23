namespace product_lab3;

public struct Product
{
    public int Id;
    public int Price;
    public string Name = "";
    public string Description;
    public int Quantity;

    public Product(int id, string name, int price, string description, int quantity)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
        Quantity = quantity;
    }

    public bool IsEmpty()
    {
        return String.IsNullOrWhiteSpace(Name);
    }
}