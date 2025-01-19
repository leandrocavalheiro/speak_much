using System.Text.Json;
using Microsoft.Extensions.Options;
using SpeakMuch.Dtos;
using SpeakMuch.Extensions;
using SpeakMuch.Interfaces;

namespace SpeakMuch.Implementations;

public class SpeakMuchService : ISpeakMuchService
{
    private Dictionary<string, string> _localizations;
    private readonly SpeakMuchOptions _options;
    private string _resourceFullPath;
    
    public string this[string name]
    {
        get
        {
            var value = _localizations.GetValueOrDefault(name, name);
            return value;
        }
    }
    public string this[string name, params object[] arguments]
    {
        get
        {
            var value = _localizations.TryGetValue(name, out var localization) ? string.Format(localization, arguments) : name;
            return value;
        }
    }     
    public SpeakMuchService(IOptions<SpeakMuchOptions> options)
    {
        _options = options.Value;
        Load();
    }
    
    public SpeakMuchService WithPath(string path)
    {
        _options.WithResourcePath(path);
        return this;
    }
    public SpeakMuchService WithFile(string file)
    {
        _options.WithResourceName(file);
        return this;
    }
    public SpeakMuchService WithContext(string context)
    {
        _options.WithContext(context);
        return this;
    }

    public SpeakMuchService WithCulture(string culture)
    {
        _options.WithLanguage(culture);
        return this;
    }

    public SpeakMuchService WithMultipleFiles(bool multipleFiles)
    {
        _options.WithUseMultiplesFiles(multipleFiles);
        return this;
    }

    public SpeakMuchService Load()
    {
        SetFullPath(_options.ResourcesPath, _options.ResourceName, _options.Language);
        _ = LoadLocalizations();
        return this;
    }

    public SpeakMuchService WithFileResource(string path, string name, string context = null, string culture = null)
    {
        SetFullPath(path, name, culture);
        return 
            WithContext(context)
            .Load();
    }
    
    #region Private Methods
    private void SetFullPath(string resourcePath, string resourceName, string culture = null)
    { 
        _options.WithResourcePath(resourcePath);
        _options.WithResourceName(resourceName);
        if (!string.IsNullOrWhiteSpace(culture))
            _options.WithLanguage(culture);

        if (_options.UseMultiplesFiles)
            _resourceFullPath = Path.Combine((_options.ResourcesPath), $"{_options.ResourceName}.{_options.Language}.json");
        else
            _resourceFullPath = Path.Combine((_options.ResourcesPath), $"{_options.Language}.json");
    }
    private Dictionary<string, string> LoadLocalizations()
    {
        if (_options is null)
            return [];

        if (!File.Exists(_resourceFullPath))
            throw new FileNotFoundException($"Localization file not found: {_resourceFullPath}");

        return string.IsNullOrWhiteSpace(_options.Context) 
            ? LoadWithoutContext() 
            : LoadWithContext();
    }
    private Dictionary<string, string> LoadWithContext()
    {
        var jsonDocument = JsonDocument.Parse(File.ReadAllText(_resourceFullPath));
        if (!jsonDocument.RootElement.TryGetProperty(_options.Context, out var contextResult))
            return [];
        
        var result = contextResult.GetRawText().Deserialize<Dictionary<string, string>>();
        _localizations = result;
        return result;
    }
    private Dictionary<string, string> LoadWithoutContext()
    {
        var json = File.ReadAllText(_resourceFullPath);
        if (string.IsNullOrWhiteSpace(json))
            return [];

        var result = json.Deserialize<Dictionary<string, string>>();        
        _localizations = result;
        return result;
    }    
    #endregion    
}