using Microsoft.Extensions.Options;
using UCastPizzaFactory.Extensions;
using UCastPizzaFactory.Models;

namespace UCastPizzaFactory.Services;

public class PizzaService : IPizzaService
{
    public readonly Settings _settings;
    public PizzaService(IOptions<Settings> options)
    {
        _settings = options.Value;
        if (_settings.NoofPizzas == 0)
        {
            Console.WriteLine("Kindly set the initial pizza size");
            Environment.Exit(0);
        }
        if (_settings.FileName == null)
        {
            Console.WriteLine("Kindly provide File full name example.txt");
            Environment.Exit(0);
        }
        if (!_settings.FileName.EndsWith(".txt"))
        {
            Console.WriteLine("Invalid file name (example.txt)");
            Environment.Exit(0);
        }
        if (_settings.CookingInterval == 0 || _settings.PizzaBaseCookingTime.StuffedCrus == 0 ||
            _settings.PizzaBaseCookingTime.ThinAndCrispy == 0 || _settings.PizzaBaseCookingTime.DeepPan ==0)
        {
            Console.WriteLine("Missing setting");
            Environment.Exit(0);
        }
    }
    public async Task InitializeAsync()
    {
        if (File.Exists(_settings.FileName))
        {
            try
            {
                File.Delete(_settings.FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
       

        Console.WriteLine($"{_settings.NoofPizzas} pizza production in progress...");
        var pizzas = new List<string>();
        // randomly pick a pizza base and topping
        int[] pizzaNumbers = { 1, 2, 3 };
        int[] toppingNumbers = { 1, 2, 3 };
        var random = new Random();
        foreach (var index in Enumerable.Range(1, _settings.NoofPizzas))
        {

            int pizzaIndex = random.Next(pizzaNumbers.Length);
            int toppinIndex = random.Next(toppingNumbers.Length);
            var pizzaNos = pizzaNumbers[pizzaIndex];

            var toppingNos = toppingNumbers[toppinIndex];

            //calculate the time it will take to cook a pizza and get the pizza description
            var (cookingTime, mypizza) = PizzaCookingTimes(_settings.CookingInterval, toppingNos, pizzaNos);
            var isPizzaAlreadyCooked = pizzas.Where(x => x.Trim() == mypizza.Trim());
            if (isPizzaAlreadyCooked.Any())
            {
                //Console.WriteLine($"{mypizza} pizza is already cooked", Console.ForegroundColor = ConsoleColor.Red);
                //Console.ResetColor();
                continue;
            }
            //Console.WriteLine(index);
            //Console.WriteLine($"{mypizza} pizza will be ready in {cookingTime}ms");
            await Task.Delay(TimeSpan.FromMilliseconds(cookingTime));
            await SaveRecordToFileAsync(mypizza);
            //Console.WriteLine($"Your pizza order is ready for collection");
            pizzas.Add(mypizza);

        }
        Console.WriteLine("Done!");
    }
    public async Task MakePizzaAsync()
    {
        // Deep Pan, Stuffed Crust, Thin and Crispy
        //“Ham and Mushroom”, “Pepperoni” and “Vegetable”.
        Console.WriteLine("Here are the instruction to make pizza", Console.ForegroundColor = ConsoleColor.Yellow);
        Console.WriteLine();
        Console.WriteLine("For Piza Base enter 1: For Deep Pan, 2: for Stuffed Crust and 3: for Thin and Crispy");
        Console.WriteLine();
        Console.WriteLine("For Pizza Topping enter 1: for  Ham and Mushroom, 2:Pepperoni and 3:for Vegetable");
        Console.WriteLine();
        Console.ResetColor();
        Console.WriteLine("Please enter your pizzabase number:");
        var isValidpizzaBase = int.TryParse(Console.ReadLine(), out int pizzaBase);
        while (!isValidpizzaBase || pizzaBase <= 0 || pizzaBase > 3)
        {
            Console.WriteLine("Please enter a valid pizzabase number:");
            isValidpizzaBase = int.TryParse(Console.ReadLine(), out pizzaBase);
        }

        Console.WriteLine("Please enter your pizza topping number:");
        var isValidpizzaTopping = int.TryParse(Console.ReadLine(), out int pizzaTopping);
        while (!isValidpizzaTopping || pizzaTopping <= 0 || pizzaTopping > 3)
        {
            Console.WriteLine("Please enter a valid topping number:");
            isValidpizzaTopping = int.TryParse(Console.ReadLine(), out pizzaTopping);
        }

        //get the pizza base cooking time in milliseconds
        var pizzaBaseCookingTime = pizzaBase.ToPizzaBaseCookingTime(_settings.PizzaBaseCookingTime);

        string[] lines = File.ReadAllLines(_settings.FileName);
        var pizzas = new List<string>();
        pizzas.AddRange(lines);

        //calculate the time it will take to cook a pizza and get the pizza description
        var (cookingTime, pizza) = PizzaCookingTimes(pizzaBaseCookingTime, pizzaTopping, pizzaBase);
        var isPizzaAlreadyCooked = pizzas.Where(x => x.Trim() == pizza.Trim());
        if (isPizzaAlreadyCooked.Any())
        {
            Console.WriteLine($"{pizza} pizza is already cooked", Console.ForegroundColor = ConsoleColor.Red);
            Console.ResetColor();
            return;
        }
        Console.WriteLine($"{pizza} pizza will be ready in {cookingTime}ms");
        //simulating cooking time
        await Task.Delay(TimeSpan.FromMilliseconds(cookingTime));
        //save pizza description to file
        await SaveRecordToFileAsync(pizza);

        Console.WriteLine($"Your {pizza} pizza order is ready for collection");

    }

    private static (int, string) PizzaCookingTimes(int baseCookingTime, int pizzaTopping, int pizzaBase)
    {
        var (pizzaTime, pizzaType) = GetPizzaBaseCookingTime(baseCookingTime, pizzaBase);
        var (toopingTime, toppingType) = GetToppingCookingTime(pizzaTopping);
        int processingTime = pizzaTime + toopingTime;
        return (processingTime, $"{pizzaType} {toppingType}");
    }
    private static (int, string) GetPizzaBaseCookingTime(int baseCookingTime, int pizzabase)
    {
        var (cookingTime, pizzaType) = (0, string.Empty);
        switch (pizzabase)
        {
            case 1:
                cookingTime = baseCookingTime * 2;
                pizzaType = "Deep Pan";
                break;
            case 2:
                cookingTime = Convert.ToInt32(baseCookingTime * 1.5);
                pizzaType = "Stuffed Crust";
                break;
            case 3:
                cookingTime = baseCookingTime;
                pizzaType = "Thin and Crispy";
                break;
            default:
                break;
        }
        return (cookingTime, pizzaType);
    }
    private static (int, string) GetToppingCookingTime(int topping, int baseCookingTime = 100)
    {
        var (cookingTime, type) = (0, string.Empty);
        switch (topping)
        {
            case 1:
                cookingTime = 11 * baseCookingTime;
                type = "Ham and Mushroom";
                break;
            case 2:
                cookingTime = 9 * baseCookingTime;
                type = "Pepperoni";
                break;
            case 3:
                cookingTime = 9 * baseCookingTime;
                type = "Vegetable";
                break;
            default:
                break;
        }
        return (cookingTime, type);
    }
    // this can be seperated into another repo
    public async Task SaveRecordToFileAsync(string pizza)
    {
        // Append text to an existing file named "Pizza.txt".
        using StreamWriter file = new(_settings.FileName, append: true);
        await file.WriteLineAsync(pizza);
    }
}
