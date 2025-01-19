var builder = Host.CreateDefaultBuilder(args);
builder.ConfigureServices((context, services) =>
{
    var baseDir = AppDomain.CurrentDomain.BaseDirectory;
    Environment.SetEnvironmentVariable("SPEAK_MUCH_RESOURCES_PATH", Path.Combine(baseDir, "Locales"));
    Environment.SetEnvironmentVariable("SPEAK_MUCH_DEFAULT_LANGUAGE", "pt-BR");

    services.AddSpeakMuch();
    services.AddTransient<ExampleService>();
});

var host = builder.Build();
var service = host.Services.GetRequiredService<ExampleService>();

service.DisplayMessagesWithoutContext();
service.DisplayMessagesWithUserContext();
service.DisplayMessagesWithCustomerContext();
