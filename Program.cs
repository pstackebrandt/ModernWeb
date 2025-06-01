using Microsoft.SemanticKernel; // to use Kernel

Settings? settings = GetSettings();

if (settings is null)
{
    WriteLine("Settings not found or not valid");
    return; // exit the app
}

Kernel kernel = GetKernel(settings);

ConsoleKey key = ConsoleKey.A;

while (key is not ConsoleKey.X)
{
    WriteLine("Enter your question: ");
    string? question = ReadLine();

    if (string.IsNullOrWhiteSpace(question))
    {
        WriteLine("No question provided. Please try again.");
        continue;
    }

    WriteLine(await kernel.InvokePromptAsync(question));

    WriteLine();

    WriteLine("Press 'X' to exit or any other key to ask another question.");

    key = ReadKey(intercept: true).Key;
}