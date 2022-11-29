namespace UCastPizzaFactory;

public static class HelperMethod
{
    public static string fileName = ConfigurationHelper.Config.GetSection("Settings:FileName")?.Value;
    static async Task SaveRecordToFileAsync(string pizza)
    {
        string destPath = AppDomain.CurrentDomain.BaseDirectory + fileName;
        // Append text to an existing file named "Pizza.txt".
        using StreamWriter file = new(fileName, append: true);
        await file.WriteLineAsync(pizza);
    }
    public static async Task Initialize(int noOfPizza, int baseCookingTime)
    {


        if (fileName == null)
        {
            Console.WriteLine("Kindly provide File full name example.txt");
            Environment.Exit(0);
        }
        if (!fileName.EndsWith(".txt"))
        {
            Console.WriteLine("Invalid file name (example.txt)");
            Environment.Exit(0);
        }
        if (File.Exists(fileName))
        {
            try
            {
                File.Delete(fileName);
            }
            catch (Exception ex)
            {
                //Do something
            }
        }
        var pizzas = new List<string>();
        int[] pizzaNumbers = { 1, 2, 3 };
        int[] toppingNumbers = { 1, 2, 3 };
        var random = new Random();
        foreach (var index in Enumerable.Range(1, noOfPizza))
        {

            int pizzaIndex = random.Next(pizzaNumbers.Length);
            int toppinIndex = random.Next(toppingNumbers.Length);
            var pizzaNos = pizzaNumbers[pizzaIndex];

            var toppingNos = toppingNumbers[toppinIndex];
            var (cookingTime, mypizza) = PizzaCookingTimes(baseCookingTime, toppingNos, pizzaNos);
            var isPizzaAlreadyCooked = pizzas.Where(x => x.Trim() == mypizza.Trim());
            if (isPizzaAlreadyCooked.Any())
            {
                Console.WriteLine($"Pizza already cooked", Console.ForegroundColor = ConsoleColor.Red);
                Console.ResetColor();
                continue;
            }
            Console.WriteLine(index);
            Console.WriteLine($"Pizza will be ready in {cookingTime}ms");
            await Task.Delay(TimeSpan.FromMilliseconds(cookingTime));

            Console.WriteLine($"Your Pizza order is ready for collection");
            pizzas.Add(mypizza);

        }
        foreach (var item in pizzas)
        {
            await SaveRecordToFileAsync(item);
            Console.WriteLine(item);
        }
        //string[] lines = File.ReadAllLines("Pizza.txt");
    }
    public static async Task MakePizza(int baseCookingTime, int pizzaTopping, int pizzaBase)
    {
        string[] lines = File.ReadAllLines(fileName);
        var pizzas = new List<string>();
        pizzas.AddRange(lines);
        var (cookingTime, pizza) = PizzaCookingTimes(baseCookingTime, pizzaTopping, pizzaBase);
        var isPizzaAlreadyCooked = pizzas.Where(x => x.Trim() == pizza.Trim());
        if (isPizzaAlreadyCooked.Any())
        {
            Console.WriteLine($"Pizza already cooked", Console.ForegroundColor = ConsoleColor.Red);
            Console.ResetColor();
            return;
        }
        Console.WriteLine($"Pizza will be ready in {cookingTime}ms");
        await Task.Delay(TimeSpan.FromMilliseconds(cookingTime));
        await SaveRecordToFileAsync(pizza);
        Console.WriteLine($"Your Pizza order is ready for collection");

    }
    private static (int, string) PizzaCookingTimes(int baseCookingTime, int pizzaTopping, int pizzaBase)
    {
        var (pizzaTime, pizzaType) = GetPizzaBaseCookingTime(baseCookingTime, pizzaBase);
        var (toopingTime, toppingType) = GetToppingCookingTime(pizzaTopping);
        int processingTime = pizzaTime + toopingTime;
        return (processingTime, $"A {pizzaType} {toppingType} pizza took {processingTime}ms to proccess");
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
}
