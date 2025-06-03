using Microsoft.SemanticKernel;

namespace ChatApp.Extensions;

public static class KernelExtensions
{
    /// <summary>
    /// Prints information about all registered plugins and their functions to the console.
    /// </summary>
    /// <param name="kernel">The kernel instance to inspect</param>
    public static void PrintRegisteredPluginsInfo(this Kernel kernel)
    {
        // Display header with total plugin count for overview
        Console.WriteLine($"Debug: Kernel has {kernel.Plugins.Count} plugins registered:");
        
        // Iterate through each registered plugin
        foreach (var plugin in kernel.Plugins)
        {
            // Print plugin name with indentation for hierarchy
            Console.WriteLine($"  Plugin: {plugin.Name}");
            
            // List all functions available in this plugin
            foreach (var function in plugin)
            {
                // Display function name and description with double indentation
                Console.WriteLine($"    Function: {function.Name} - {function.Description}");
            }
        }
        
        // Add blank line for better readability in console output
        Console.WriteLine();
    }
} 