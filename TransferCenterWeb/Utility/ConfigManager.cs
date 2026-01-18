namespace TransferCenterWeb.Utility;

public static class ConfigManager
{
        public static string? GetSetting(IServiceProvider services, string key)
        {
            var config = services.GetService(typeof(IConfiguration)) as IConfiguration;
            return config?[key];
        }
        public static HashSet<string> GetExcelExportExcludeFields(this IConfiguration? config, string key)
        {
            if (config == null || string.IsNullOrEmpty(key))
                return [];
            return (config[key] ?? "").Split(',',
                    System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries)
                .ToHashSet(System.StringComparer.OrdinalIgnoreCase);
        }
    }