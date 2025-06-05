using OllamaSharp; // To use OllamaApiClient
using OllamaSharp.Models; // To use models
using Spectre.Console; // To use ToolCallBehavior
using System.Diagnostics; // To use Stopwatch


// Configure and create ollama client
// =============================
// Default port for Ollama is 11434
string port = "11434";
Uri uri = new ($"http://localhost:{port}");

OllamaApiClient ollama = new(uri);

// Get data from ollama
// =============================
IEnumerable<Model> models = await ollama.ListLocalModelsAsync();

// Create table with model data
// =============================
Table table = new();
table.AddColumn("Name");
table.AddColumn("Size");

foreach (Model model in models)
{
    table.AddRow(model.Name, model.Size.ToString());
}

AnsiConsole.Write(table);

// Prepare language model usage
// =============================
string modelName = "llama3.1:latest"; 
// The console command "ollama run llama3.1:latest" leads currently to the use of llama3.1:8b which I installed manually

WriteLine();
WriteLine($"Selected model: {modelName}");
ollama.SelectedModel = modelName;

// Communicate with the model
// =============================

// Prepare user prompt
Write("Please enter your prompt: ");
string? prompt = ReadLine();
if (string.IsNullOrWhiteSpace(prompt))
{
    WriteLine("Prompt is required. Exiting the app.");
    return;
}

Stopwatch timer = Stopwatch.StartNew();

// Run query and stream the response
await foreach (var stream in ollama.GenerateAsync(prompt))
{
    Write(stream?.Response);
}

// Stop timer and display elapsed time
// ===================================
timer.Stop();

WriteLine();
WriteLine();
WriteLine($"Elapsed time: {timer.ElapsedMilliseconds:N0} ms");
