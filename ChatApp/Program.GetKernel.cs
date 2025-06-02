using Microsoft.SemanticKernel;

/*
    This file contains the code that creates a configured Semantic Kernel instance with OpenAI chat capabilities.
    It also adds plugins to the kernel. imports the plugins from the ChatFunctions.cs file.
*/
partial class Program
{
    /// <summary>
    /// Creates a configured Semantic Kernel instance with OpenAI chat capabilities.
    /// </summary>
    /// <param name="settings">Settings with model ID and API key</param>
    /// <returns>Configured Kernel instance</returns>
    private static Kernel GetKernel(Settings settings)
    {
        // Setup the kernel
        // ================================
        IKernelBuilder kernelBuilder = Kernel.CreateBuilder();

        // Configure the OpenAI chat with model and secret key
        kernelBuilder.AddOpenAIChatCompletion(
            settings.ModelId,
            settings.OpenAISecretKey);

        Kernel kernel = kernelBuilder.Build();

        // Import plugins
        // ================================
        kernel.ImportPluginFromFunctions(
            pluginName: "AuthorInformation",
            [
                kernel.CreateFunctionFromMethod(
                    method: GetAuthorBiography,
                    functionName: nameof(GetAuthorBiography),
                    description: "Gets the author's biography")
            ]);

        kernel.ImportPluginFromFunctions(
            "NorthwindProducts",
            [
                kernel.CreateFunctionFromMethod(
                    method: GetProductsInCategory,
                    functionName: nameof(GetProductsInCategory),
                    description: "Gets products in a category from the Northwind database")
            ]);

        kernel.ImportPluginFromFunctions(
            "TestFunctions",
            [
                kernel.CreateFunctionFromMethod(
                    method: TestFunctionCalling,
                    functionName: nameof(TestFunctionCalling),
                    description: "Simple test to verify function calling works - returns a success message with timestamp")
            ]);

        return kernel;
    }
}