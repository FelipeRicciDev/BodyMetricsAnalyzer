namespace CrossCutting.Configuration;

public sealed class AppSettings
{
    public static AppSettings Settings => AppSettingsConfiguration.AppSettingsLoader.Load();

    [JsonPropertyName("connectionSettings")]
    public ConnectionSettings ConnectionSettings { get; set; } = new();
}

