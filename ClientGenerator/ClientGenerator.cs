using System.Text.Json;
using NJsonSchema;
using NJsonSchema.CodeGeneration.TypeScript;
using NSwag;
using NSwag.CodeGeneration.CSharp;
using NSwag.Generation;

namespace UserManagementApi.ClientGenerator;

public class ClientGenerator
{
    public async Task GenerateCsharp()
    {
        var httpClient = new HttpClient();

        var swaggerJson = await httpClient.GetStringAsync("http://localhost:5092/swagger/v1/swagger.json");

        var document = await OpenApiDocument.FromJsonAsync(swaggerJson);

        var csharpGeneratorSettings = new CSharpClientGeneratorSettings()
        {
            ClassName = "UsersController",
            CSharpGeneratorSettings = 
            {
                Namespace = "UserManagementApi"
            }
        };

        var csharpGenerator = new CSharpClientGenerator(document, csharpGeneratorSettings);
        var generatedCode = csharpGenerator.GenerateFile();

        await File.WriteAllTextAsync("proxies.cs", generatedCode);
    }

    public async Task GenerateTypescript()
    {
        var httpClient = new HttpClient();

        var swaggerJson = await httpClient.GetStringAsync("http://localhost:5092/swagger/v1/swagger.json");

        var documentJson = JsonSerializer.Deserialize<JsonSchema>(swaggerJson);
        var tsGeneratorSettings = new TypeScriptGeneratorSettings()
        {
            Namespace = "UserManagementApi",
            GenerateConstructorInterface = true,
            DateTimeType = TypeScriptDateTimeType.String,
        };

        var tsGenerator = new TypeScriptGenerator(documentJson, tsGeneratorSettings);
        var generatedCode = tsGenerator.GenerateFile();

        await File.WriteAllTextAsync("proxies.ts", generatedCode);
    }
}