namespace BuildingWebApisWithAspNet.Appsettings
{
    public class AppSettingsOptions
    {
        public static string Options = "Options";

        public LoggingOptions Logging { get; set; }
        public string AllowedHosts { get; set; }
        public bool UseDeveloperExceptionPage { get; set; }
        public bool UseSwagger { get; set; }
        public string AllowedOrigins { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class LoggingOptions
    {
        public LogLevelOptions LogLevel { get; set; }
    }

    public class LogLevelOptions
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }

    public class ConnectionStrings
    {
        public string MyBgList { get; set; }
    }
}
