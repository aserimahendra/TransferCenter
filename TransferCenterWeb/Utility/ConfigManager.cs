namespace TransferCenterWeb.Utility;

public static class ConfigManager
{
        public static string? GetSetting(IServiceProvider services, string key)
        {
            var config = services.GetService(typeof(IConfiguration)) as IConfiguration;
            return config?[key];
        }
    }