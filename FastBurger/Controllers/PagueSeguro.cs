namespace FastBurger.Controllers;
public class PagueSeguro
{
    public Customer Customer { get; set; }
    public Item[] Items { get; set; }
    public Shipping Shipping { get; set; }
    public string Reference_id { get; set; }
}
public class Customer
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Tax_id { get; set; }
}

public class Item
{
    public string? Name { get; set; }
    public int Quantity { get; set; }
    public decimal Unit_amount { get; set; }
}
public class Address
{
    public string? Street { get; set; }
    public string? Number { get; set; }
    public string? Locality { get; set; }
    public string? City { get; set; }
    public string? Region_code { get; set; }
    public string? Country { get; set; }
    public string? Postal_code { get; set; }
}

public class Shipping
{
    public Address Address { get; set; }
}
