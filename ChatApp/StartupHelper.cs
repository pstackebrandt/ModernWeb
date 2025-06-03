using Microsoft.SemanticKernel;

namespace ChatApp;

/// <summary>
/// Handles startup logic including command-line argument parsing and interactive test menu.
/// </summary>
public static class StartupHelper
{
    /// <summary>
    /// Handles startup logic based on command-line arguments.
    /// </summary>
    /// <param name="args">Command-line arguments</param>
    /// <param name="kernel">Initialized Semantic Kernel instance</param>
    public static void HandleStartupChecks(string[] args, Kernel kernel)
    {
        // Check kernel
        if (kernel == null)
        {
            Console.WriteLine("[WARNING] Kernel is null - skipping startup operations");
            return;
        }

        // Check for test arguments
        if (ShouldRunTests(args))
        {
            ShowInteractiveTestMenu();
        }
    }

    /// <summary>
    /// Determines if tests should be run based on command-line arguments.
    /// </summary>
    /// <param name="args">Command-line arguments</param>
    /// <returns>True if tests should be run</returns>
    private static bool ShouldRunTests(string[] args)
    {
        if (args == null || args.Length == 0)
        {
            return false;
        }

        // Check for test arguments (case-insensitive)
        return args.Any(arg => 
            string.Equals(arg, "test", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(arg, "--test", StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Shows interactive test menu and handles user selections.
    /// </summary>
    private static void ShowInteractiveTestMenu()
    {
        int invalidAttempts = 0;
        const int maxInvalidAttempts = 3;

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== ChatApp Test Menu ===");
            Console.WriteLine("Select test to run:");
            Console.WriteLine();
            Console.WriteLine("  d) Database Connection    - Verify Northwind.db connectivity");
            Console.WriteLine("  c) Categories            - Check category data and count");
            Console.WriteLine("  p) Products Query        - Test product retrieval functionality");
            Console.WriteLine("  f) Function Integration  - Verify chat function works via reflection");
            Console.WriteLine("  k) Kernel Registration   - Test Semantic Kernel function setup");
            Console.WriteLine("  u) User Experience      - Display UX features and safeguards");
            Console.WriteLine();
            Console.WriteLine("  a) All Tests            - Run complete test suite");
            Console.WriteLine("  x) Exit to Chat         - Continue to main application");
            Console.WriteLine();
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine()?.Trim().ToLower() ?? "";

            Console.WriteLine();

            // Handle empty input
            if (string.IsNullOrWhiteSpace(choice))
            {
                invalidAttempts++;
                Console.WriteLine($"⚠️ No choice entered. Please select a letter from the menu. ({invalidAttempts}/{maxInvalidAttempts})");
                
                if (invalidAttempts >= maxInvalidAttempts)
                {
                    Console.WriteLine("Too many invalid attempts. Exiting test menu to continue to chat...");
                    return;
                }
                continue;
            }

            switch (choice)
            {
                case "d":
                    Console.WriteLine("=== Database Connection Test ===");
                    var dbResult = TestDbConnection.TestDatabaseConnection();
                    ShowTestResult("Database Connection", dbResult);
                    invalidAttempts = 0; // Reset counter on valid choice
                    break;

                case "c":
                    Console.WriteLine("=== Categories Test ===");
                    var catResult = TestDbConnection.TestCategoryCount();
                    ShowTestResult("Categories", catResult);
                    invalidAttempts = 0; // Reset counter on valid choice
                    break;

                case "p":
                    Console.WriteLine("=== Products Query Test ===");
                    var prodResult = TestDbConnection.TestGetProductsInCategory();
                    ShowTestResult("Products Query", prodResult);
                    invalidAttempts = 0; // Reset counter on valid choice
                    break;

                case "f":
                    Console.WriteLine("=== Function Integration Test ===");
                    var funcResult = TestDbConnection.TestChatFunction();
                    ShowTestResult("Function Integration", funcResult);
                    invalidAttempts = 0; // Reset counter on valid choice
                    break;

                case "k":
                    Console.WriteLine("=== Kernel Registration Test ===");
                    var kernelResult = TestDbConnection.TestSemanticKernelRegistration();
                    ShowTestResult("Kernel Registration", kernelResult);
                    invalidAttempts = 0; // Reset counter on valid choice
                    break;

                case "u":
                    Console.WriteLine("=== User Experience Features ===");
                    TestDbConnection.ShowUserExperienceInfo();
                    ShowTestResult("User Experience", true); // Always passes
                    invalidAttempts = 0; // Reset counter on valid choice
                    break;

                case "a":
                    Console.WriteLine("=== Running All Tests ===");
                    TestDbConnection.RunTests();
                    Console.WriteLine("\n✓ All tests completed");
                    invalidAttempts = 0; // Reset counter on valid choice
                    break;

                case "x":
                    Console.WriteLine("Exiting test menu, continuing to chat...");
                    return;

                default:
                    invalidAttempts++;
                    Console.WriteLine($"⚠️ Invalid choice '{choice}'. Please select a valid option (d, c, p, f, k, u, a, x). ({invalidAttempts}/{maxInvalidAttempts})");
                    
                    if (invalidAttempts >= maxInvalidAttempts)
                    {
                        Console.WriteLine("Too many invalid attempts. Exiting test menu to continue to chat...");
                        return;
                    }
                    continue;
            }

            if (choice != "x")
            {
                Console.WriteLine("\nPress any key to return to test menu...");
                try
                {
                    Console.ReadKey();
                }
                catch (InvalidOperationException)
                {
                    // Handle piped input case
                    Console.Read();
                }
            }
        }
    }

    /// <summary>
    /// Shows the result of a test with consistent formatting.
    /// </summary>
    /// <param name="testName">Name of the test</param>
    /// <param name="success">Whether the test passed</param>
    private static void ShowTestResult(string testName, bool success)
    {
        string status = success ? "✅ PASSED" : "❌ FAILED";
        Console.WriteLine($"\n{status}: {testName}");
    }
} 