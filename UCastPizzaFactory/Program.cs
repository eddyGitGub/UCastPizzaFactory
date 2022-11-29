// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UCastPizzaFactory.Models;
using UCastPizzaFactory.Services;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddScoped<IPizzaService, PizzaService>();
        var configurationRoot = _.Configuration;
        services.Configure<Settings>(
            configurationRoot.GetSection(nameof(Settings)));

    }).Build();


var pizzaService = host.Services.GetService<IPizzaService>();
if (pizzaService == null)
{
    throw new ArgumentNullException(nameof(pizzaService));
    //Environment.Exit(Environment.ExitCode);
}

await pizzaService.InitializeAsync();
await pizzaService.MakePizzaAsync();


