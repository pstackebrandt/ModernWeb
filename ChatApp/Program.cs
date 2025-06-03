using Microsoft.SemanticKernel; // to use Kernel
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using ChatApp; // to use TestDbConnection
using ChatApp.Extensions; // to use Kernel extensions

// Get and validate app settings
// ==================================
Settings? settings = GetSettings();

if (settings is null)
{
    WriteLine("Settings not found or not valid");
    return; // exit the app
}

// Get kernel
// ==================================
Kernel kernel = GetKernel(settings);

// Handle startup operations (including tests if requested)
// ==================================
StartupHelper.HandleStartupChecks(args, kernel);

// Get chat completion service
// ==================================
IChatCompletionService completion = kernel.GetRequiredService<IChatCompletionService>();

// Add debug information
kernel.PrintRegisteredPluginsInfo();

string systemMessage = @"You are an AI assistant with access to specific functions. You MUST use these functions when appropriate.";

ChatHistory history = new(systemMessage: systemMessage);

// Configure how the AI should handle function calls during conversation
PromptExecutionSettings options = new()
{
    // Enable automatic function calling - AI will decide when to use available functions
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