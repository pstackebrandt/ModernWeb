using Microsoft.SemanticKernel;

partial class Program
{
    /// <summary>
    /// Creates a configured Semantic Kernel instance with OpenAI chat capabilities.
    /// </summary>
    /// <param name="settings">Settings with model ID and API key</param>
    /// <returns>Configured Kernel instance</returns>
    private static Kernel GetKernel(Settings settings)
    {
        IKernelBuilder kernelBuilder = Kernel.CreateBuilder();

        // Configure the OpenAI chat with model and secret key
        kernelBuilder.AddOpenAIChatCompletion(
            settings.ModelId,
            settings.OpenAISecretKey);

        Kernel kernel = kernelBuilder.Build();

        return kernel;
    }
}