using Microsoft.SemanticKernel; // to use Kernel
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using ChatApp; // to use TestDbConnection

// Check for command line arguments for testing
if (args.Length > 0 && args[0] == "test")
{
    TestDbConnection.RunTests();
    return; // Exit after running tests
}

// Ask user if they want to run tests first
WriteLine("Do you want to run database connectivity tests first? (y/n): ");
string testChoice = ReadLine()?.Trim().ToLower() ?? "";

if (testChoice == "y" || testChoice == "yes")
{
    TestDbConnection.RunTests();
    WriteLine("\nPress any key to continue to chat or Ctrl+C to exit...");
    try
    {
        ReadKey();
    }
    catch (InvalidOperationException)
    {
        // Handle piped input case
        Read();
    }
    WriteLine();
}

// Get and validate app settings
// ==================================
Settings? settings = GetSettings();

if (settings is null)
{
    WriteLine("Settings not found or not valid");
    return; // exit the app
}

Kernel kernel = GetKernel(settings);


IChatCompletionService completion = kernel.GetRequiredService<IChatCompletionService>();

// Add debug information
WriteLine($"Debug: Kernel has {kernel.Plugins.Count} plugins registered:");
foreach (var plugin in kernel.Plugins)
{
    WriteLine($"  Plugin: {plugin.Name}");
    foreach (var function in plugin)
    {
        WriteLine($"    Function: {function.Name} - {function.Description}");
    }
}
WriteLine();
string shortSystemMessage = @"You are an AI assistant with access to specific functions. You MUST use these functions when appropriate.";


/* string longSystemMessage = @"You are an AI assistant with access to specific functions. You MUST use these functions when appropriate.

AVAILABLE FUNCTIONS:
1. GetAuthorBiography() - Use when asked about the author or book author
2. GetProductsInCategory(categoryName) - Use when asked about products in categories
3. TestFunctionCalling() - Use when asked to test function calling

CRITICAL INSTRUCTIONS:
- When users ask about products in categories (Beverages, Seafood, etc.), you MUST call GetProductsInCategory
- When users ask to test functions or call TestFunctionCalling, you MUST call TestFunctionCalling  
- When users ask about the author, you MUST call GetAuthorBiography
- Always use the actual functions rather than providing generic responses
- If a user specifically asks you to call a function, DO IT

Examples:
- 'What products are in Seafood?' → Call GetProductsInCategory('Seafood')
- 'Call TestFunctionCalling' → Call TestFunctionCalling()
- 'Tell me about the author' → Call GetAuthorBiography()"; */

ChatHistory history = new(systemMessage: shortSystemMessage);

PromptExecutionSettings options = new()
{
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
};

int consecutiveInvalidAttempts = 0;
const int maxInvalidAttempts = 3;

while (true)
{
    // Get user input
    WriteLine("Enter your question (or type 'x' to exit): ");
    string question = ReadLine()!;

    // Validate input
    if (string.IsNullOrWhiteSpace(question))
    {
        consecutiveInvalidAttempts++;
        WriteLine($"No question provided. Please try again. ({consecutiveInvalidAttempts}/{maxInvalidAttempts})");
        
        if (consecutiveInvalidAttempts >= maxInvalidAttempts)
        {
            WriteLine("Too many invalid attempts. Exiting application.");
            break;
        }
        continue;
    }

    // Reset counter on valid input
    consecutiveInvalidAttempts = 0;

    // Exit questioning if required
    if (IsExitCommand(question))
    {
        break;
    }
   
    // Prepare call
    history.AddUserMessage(question);
    
    WriteLine($"[DEBUG] Sending message to AI. History has {history.Count} messages");
    WriteLine($"[DEBUG] FunctionChoiceBehavior: {options.FunctionChoiceBehavior}");

    ChatMessageContent answer = await completion.GetChatMessageContentAsync(history, options, kernel);

    WriteLine($"[DEBUG] Received response. Content length: {answer.Content?.Length ?? 0}");
    WriteLine($"[DEBUG] Function calls in response: {answer.Items.OfType<FunctionCallContent>().Count()}");

    // Use the answer
    history.AddAssistantMessage(answer.Content!);
    WriteLine(answer.Content!);

    WriteLine();
}

// Helper method to check if input is exit command
static bool IsExitCommand(string input) =>
    input.Trim().Equals("x", StringComparison.OrdinalIgnoreCase);