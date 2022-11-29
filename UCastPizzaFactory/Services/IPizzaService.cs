namespace UCastPizzaFactory.Services
{
    public interface IPizzaService
    {
        Task InitializeAsync();
        Task MakePizzaAsync();
        Task SaveRecordToFileAsync(string pizza);
    }
}