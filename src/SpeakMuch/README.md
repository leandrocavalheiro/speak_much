## Speak Much

The name of Speak Much, is based a Brazilian Meme: "Fala muito!".  
This is a lib for translate .NET  applications,
designed to facilitate the localization of applications using JSON files as a translation source. With this library, 
you can add multi-language support to your application in a simple and efficient way.  

### 📦 Features
- Native support for JSON files as translation source.
- Compatible with any type of .NET application (APIs, console, web and desktop applications).
- Speak Much settings can be made through: AppSettings, Environment Variables
- Mensagens de fallback para strings não traduzidas.
- Fallback messages for untranslated strings.

### 🚀 How its work
The basic operations is:
- Default lifetime: Transient
  - Why? This lib, support contexts and the contexts can be changed in each service ( at constructor ).
- Not config required. By Default, this libs use:
  - Language: en
  - Resources Path: Locales
  - Resource File: en.json
    - This is a Key Value file.
  - Context: null
  - Multiples File = false
- In the service constructor that used the lib, can be change the any values of configurations
  - Exists a Withxxxx method for each configuration.
- At the point where you want to use write: service["myKey""]
  - The service will seek, the KEY provided in the json file ( en.json ).
    - If you find the desired key, returns the value correspondent
    - Otherwise, it returns the key provided.

### 🛠️ Hands on

👉 **Step 1** - Install library into project
 
- **Package Manager**

```bash
$ Install-Package SpeakMuch
```

- **.Net CLI**

```bash
$ dotnet add package SpeakMuch
```

👉 **Step 2** - Register Service

- Using AppSettings or Environment Variables configurations
```csharp
builder
    .Services
    .AddSpeakMuch(builder.Configuration);
```

- Using DotNetEnv configurations
```csharp
builder
    .Services
    .AddSpeakMuch();
``` 

👉 **Step 3** - Create Folder and files 
If not exists the Folder "Locales" create at Root of Project.  
Each project must have its locale folder and its files.
Files can be with or without contexts.
Files without contexts are simple Key Value files, and the lib load all keys in memory.
Example:
```json
{
  "greeting": "Hello",
  "farewell": "Goodbye",
  "notFound": "Item not found",
  "unauthorized": "You are not authorized"
}
```
Files with context, can be one level objects,named contexts, and only context is load in memory.
Example
```json
{
  "user": {
      "Greeting": "Hello User",
      "Farewell": "Goodbye User",
      "NotFound": "Item not found",
      "Unauthorized": "You are not authorized"
  },
  "customer": {
    "Greeting": "Hello Customer",
    "Farewell": "Goodbye",
    "NotFound": "Item not found",
    "Unauthorized": "You are not authorized"
  }  
}
```
About file naming.
If you are using single files per language, the file name must follow the following rule:  
*language_name.json. [en.json, pt-BT.json]*  

If you are using multiple files per language, for example, there may be a file for error messages, another for any other purpose. In this case, the rule is as follows:  
*file_name.language_name.json [errors.en.json]*  

Remember that in this case, an environment variable must be configured to allow:  
***SPEAK_MUCH_USE_MULTIPLES_FILES must*** have a value equal to True

👉 **Step 4** - Inject the service
Now, SpeakMuch must be injected into the service that wants to use it.

```csharp
public class DashboardService(ISpeakMuchService speakMuchService)
```
In the example above they use the primary constructor to inject SpeakMuch into the DashboardService.  
For context-free use, nothing else needs to be configured. You can go to step 5.
For use with context, we need to inform which context we want to use.

```csharp
    private readonly ISpeakMuchService _speakMuchService = speakMuchService
        .WithContext("user")
        .Load();
```
If you are using multiple files per language, we need to inform which file we want to use.
```csharp
    private readonly ISpeakMuchService _speakMuchService = speakMuchService
        .WithContext("user")
        .WithFile("errors.en.json")
        .Load();
```

Now, the instance of SpeakMuch, will load only messages from user context.

👉 **Step 4** - Using the service
The use it's very simple:
```csharp
    public void Speak()
    {
        Console.WriteLine($"{speakMuchService["Greeting"]}");
    }
```
This way, SpeakMuch will search for **Greeting** in the **en.json** file and return its **value**, in our example, using the file with context, **"Hello User"**.
So we would have the following output in the console:
```bash
  Hello User
```

Documentations WIP

> **Contact**  
> 📧  leo.cavalheiro.ti@gmail.com

