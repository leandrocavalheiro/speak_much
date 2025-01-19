namespace SpeakMuch.Example.Services;

public class ExampleService(ISpeakMuchService speakMuchService)
{
    private readonly ISpeakMuchService _speakMuchService = speakMuchService
              //     .WithCulture("pt-BR")
              .Load();
    private ISpeakMuchService _speakMuchServiceUsuario = speakMuchService
            //   .WithCulture("pt-BR")
            .WithContext("user")
            .Load();
    private ISpeakMuchService _speakMuchServiceCliente = speakMuchService
            //  .WithCulture("pt-BR")
            .WithContext("customer")
            .Load();

    public void DisplayMessagesWithoutContext()
    {
        Console.WriteLine("=== Messages Without Context ===");
        Console.WriteLine(_speakMuchService["Greeting"]);
        Console.WriteLine(_speakMuchService["Farewell"]);
        Console.WriteLine();
    }

    public void DisplayMessagesWithUserContext()
    {
        Console.WriteLine("=== User Context Messages ===");
        Console.WriteLine(_speakMuchServiceUsuario["Greeting"]);
        Console.WriteLine(_speakMuchServiceUsuario["Farewell"]);
        Console.WriteLine();
    }

    public void DisplayMessagesWithCustomerContext()
    {
        Console.WriteLine("=== Client Context Messages ===");
        Console.WriteLine(_speakMuchServiceCliente["Greeting"]);
        Console.WriteLine(_speakMuchServiceCliente["Farewell"]);
        Console.WriteLine();
    }
}