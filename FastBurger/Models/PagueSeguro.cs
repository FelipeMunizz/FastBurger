namespace FastBurger.Models;
public class PagueSeguro
{
    public Customer Customer { get; set; }
    public Item[] Items { get; set; }
    public int Reference_id { get; set; }
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
    public int Unit_amount { get; set; }
}

public class charges
{
    public string reference_id { get; set; }
    public string description { get; set; }
    public Amount amount { get; set; }
    public payment_method payment_method { get; set; }
}

public class Amount
{
    public int value { get; set; }
    public string currency { get; } = "BRL";
}

public class payment_method
{
    public type type { get; set; }
    public int installments { get; set; }
    public bool capture { get; set; } = true;
    public string soft_descriptor { get; set; }
}

public class card
{
    public string id { get; set; }
    public string encrypted { get; set; }
    public string number { get; set; }
    public string network_token { get; set; }
    public int exp_month { get; set; }
    public int exp_year { get; set; }
    public string security_code { get; set; }
    public bool store { get; set; } = false;
}

public class ResponsePedidoPagSeguro
{
    public string id { get; set; }
    public string public_key { get; set; }
}

public enum type
{
    CREDIT_CARD,
    DEBIT_CARD,
    BOLETO
}