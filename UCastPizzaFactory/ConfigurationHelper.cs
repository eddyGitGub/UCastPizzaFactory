using Microsoft.Extensions.Configuration;

namespace UCastPizzaFactory;

public static class ConfigurationHelper
{
    public static IConfiguration? Config;
    public static void Initialize(IConfiguration Configuration)
    {
        Config = Configuration;
    }
}