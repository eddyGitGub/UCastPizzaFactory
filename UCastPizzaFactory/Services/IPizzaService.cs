namespace UCastPizzaFactory.Services
{
    public interface IPizzaService
    {
        Task Initialize();
        Task MakePizza();
        Task SaveRecordToFileAsync(string pizza);
    }
}