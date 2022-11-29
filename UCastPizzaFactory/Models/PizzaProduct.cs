using Microsoft.Extensions.Configuration;

namespace UCastPizzaFactory.Models;

public class PizzaProduct
{
    public string Name { get; set; } = string.Empty;
    public float MultiplicationFactor { get; set; }
    public string Topping{ get; set; } = null!;
}

