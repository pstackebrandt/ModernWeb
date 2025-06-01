using Microsoft.SemanticKernel; // to use Kernel

Settings? settings = GetSettings();

if (settings is null)
{
    WriteLine("Settings not found or not valid");
    return; // exit the app
}

Kernel kernel = GetKernel(settings);

while (true)
{
    WriteLine("Enter your question (or type 'x' to exit): ");
    string? question = ReadLine();

    if (string.IsNullOrWhiteSpace(question))
    {
        WriteLine("No question provided. Please try again.");
        continue;
    }

    // Check if user wants to exit by typing 'x' or 'X'
    if (IsExitCommand(question))
    {
        break;
    }

    WriteLine(await kernel.InvokePromptAsync(question));

    WriteLine();
}

// Helper method to check if input is exit command
static bool IsExitCommand(string input) =>
    input.Trim().Equals("x", StringComparison.OrdinalIgnoreCase);