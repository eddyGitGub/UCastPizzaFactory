namespace UCastPizzaFactory.Models;

public class Settings
{
    public PizzaBaseCookingTime PizzaBaseCookingTime { get; set; }= null!;
    public int CookingInterval { get; set; }
    public int NoofPizzas { get; set; }
    public string FileName { get; set; } = string.Empty;
}
public class PizzaBaseCookingTime
{
    public int DeepPan { get; set; }
    public int StuffedCrus { get; set; }
    public int ThinAndCrispy { get; set; }
}
