namespace SpeakMuch.Configurations;
public static class SpeakMuchConfigurations
{
    public static IServiceCollection AddSpeakMuch(
        this IServiceCollection services,
        IConfiguration configurations,
        ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        var options = new SpeakMuchOptions()
            .WithResourcePath(configurations.GetValue(EnvResourcesPath, "Locales"))
            .WithUseMultiplesFiles(configurations.GetValue(EnvUseMultiplesFiles, false))
            .WithResourceName(configurations.GetValue(EnvResourcesFile, ""))
            .WithLanguage(configurations.GetValue(EnvDefaultLanguage, "en"))
            .WithContext(configurations.GetValue(EnvDefaultContext, ""));

        return services.ConfigureTranslator(options, serviceLifetime);
        
                
    }
    public static IServiceCollection AddSpeakMuch(this IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {
        var options = new SpeakMuchOptions()
            .WithResourcePath(EnvResourcesPath.GetFromEnvironmentVariable("Locales"))
            .WithUseMultiplesFiles(EnvUseMultiplesFiles.GetFromEnvironmentVariable(false))
            .WithResourceName(EnvDefaultLanguage.GetFromEnvironmentVariable(""))
            .WithLanguage(EnvDefaultLanguage.GetFromEnvironmentVariable("en"))
            .WithContext(EnvDefaultContext.GetFromEnvironmentVariable(""));
        
        return services.ConfigureTranslator(options, serviceLifetime);        
    }

    private static IServiceCollection ConfigureTranslator(this IServiceCollection services, SpeakMuchOptions optionsTranslator, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
    {

        
        services.Configure(delegate (SpeakMuchOptions options)
        {
            options.ResourcesPath = optionsTranslator.ResourcesPath;
            options.ResourceName = optionsTranslator.ResourceName;
            options.Languages = optionsTranslator.Languages;
            options.Language = optionsTranslator.Language;
            options.Context = optionsTranslator.Context;
            options.UseMultiplesFiles = optionsTranslator.UseMultiplesFiles;
        });
        
        switch (serviceLifetime)
        {
            case ServiceLifetime.Scoped:
                return services.AddScoped<ISpeakMuchService, SpeakMuchService>();
            case ServiceLifetime.Singleton:
                return services.AddSingleton<ISpeakMuchService, SpeakMuchService>();
            case ServiceLifetime.Transient:
            default:
                return services.AddTransient<ISpeakMuchService, SpeakMuchService>();
        }        
    }
}