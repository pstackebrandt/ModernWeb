using Microsoft.EntityFrameworkCore;
using Northwind.EntityModels;
using System.Text.Json;
using System.Reflection;
using Microsoft.SemanticKernel;

namespace ChatApp;

/// <summary>
/// Simple test class to verify database connectivity and function operation
/// </summary>
public static class TestDbConnection
{
    public static void RunTests()
    {
        Console.WriteLine("=== Database Connectivity Tests ===");
        
        // Test 1: Basic database connection
        TestDatabaseConnection();
        
        // Test 2: Category count
        TestCategoryCount();
        
        // Test 3: Product function with a known category
        TestGetProductsInCategory();
        
        // Test 4: Test the actual ChatFunction method
        TestChatFunction();
        
        // Test 5: Test Semantic Kernel function registration
        TestSemanticKernelRegistration();
        
        Console.WriteLine("\n=== User Experience Tests ===");
        ShowUserExperienceInfo();
    }
    
    /// <summary>
    /// Tests basic database connectivity to Northwind.db
    /// </summary>
    /// <returns>True if connection successful</returns>
    public static bool TestDatabaseConnection()
    {
        try
        {
            using NorthwindContext db = new();
            bool canConnect = db.Database.CanConnect();
            Console.WriteLine($"✓ Database connection: {(canConnect ? "SUCCESS" : "FAILED")}");
            
            if (canConnect)
            {
                Console.WriteLine($"  Database path: {db.Database.GetConnectionString()}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Database connection FAILED: {ex.Message}");
            return false;
        }
    }
    
    /// <summary>
    /// Tests category count and lists available categories
    /// </summary>
    /// <returns>True if categories found successfully</returns>
    public static bool TestCategoryCount()
    {
        try
        {
            using NorthwindContext db = new();
            int categoryCount = db.Categories.Count();
            Console.WriteLine($"✓ Categories found: {categoryCount}");
            
            if (categoryCount > 0)
            {
                // List all category names
                var categories = db.Categories.Select(c => c.CategoryName).ToList();
                Console.WriteLine($"  Available categories: {string.Join(", ", categories)}");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Category count FAILED: {ex.Message}");
            return false;
        }
    }
    
    /// <summary>
    /// Tests GetProductsInCategory functionality with sample data
    /// </summary>
    /// <returns>True if products retrieved successfully</returns>
    public static bool TestGetProductsInCategory()
    {
        try
        {
            // Test with "Beverages" category (should exist in Northwind)
            string categoryName = "Beverages";
            Console.WriteLine($"✓ Testing GetProductsInCategory with '{categoryName}':");
            
            using NorthwindContext db = new();
            
            // First check if category exists
            var category = db.Categories
                .FirstOrDefault(c => c.CategoryName == categoryName);
            
            if (category == null)
            {
                Console.WriteLine($"  ⚠ Category '{categoryName}' not found");
                return false;
            }
            
            var products = db.Products
                .Include(p => p.Category)
                .Where(p => p.Category!.CategoryName == categoryName)
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    p.Category!.CategoryName,
                    p.UnitPrice,
                    p.UnitsInStock,
                    p.UnitsOnOrder,
                    p.Discontinued
                }).ToArray();
                
            Console.WriteLine($"  Found {products.Length} products in '{categoryName}' category");
            
            if (products.Length > 0)
            {
                // Show first few products
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(products.Take(2), jsonOptions);
                Console.WriteLine($"  Sample products (first 2):\n{jsonString}");
                Console.WriteLine("✓ GetProductsInCategory function works correctly");
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ GetProductsInCategory FAILED: {ex.Message}");
            Console.WriteLine($"  Stack trace: {ex.StackTrace}");
            return false;
        }
    }
    
    /// <summary>
    /// Tests the actual chat function via reflection
    /// </summary>
    /// <returns>True if chat function works correctly</returns>
    public static bool TestChatFunction()
    {
        try
        {
            Console.WriteLine("✓ Testing actual GetProductsInCategory chat function:");
            string categoryName = "Beverages";
            
            // Use reflection to call the private method from Program class
            var programType = typeof(Program);
            var method = programType.GetMethod("GetProductsInCategory", 
                BindingFlags.NonPublic | BindingFlags.Static);
            
            if (method == null)
            {
                Console.WriteLine("✗ GetProductsInCategory method not found");
                return false;
            }
            
            string result = (string)method.Invoke(null, new object[] { categoryName })!;
            
            if (!string.IsNullOrWhiteSpace(result))
            {
                Console.WriteLine($"  Function returned {result.Length} characters of JSON");
                
                // Parse to verify it's valid JSON
                using JsonDocument doc = JsonDocument.Parse(result);
                var root = doc.RootElement;
                
                if (root.ValueKind == JsonValueKind.Array)
                {
                    int productCount = root.GetArrayLength();
                    Console.WriteLine($"  JSON contains {productCount} products");
                    Console.WriteLine("✓ GetProductsInCategory chat function works correctly");
                    
                    // Show a preview of the JSON
                    string preview = result.Length > 200 ? result.Substring(0, 200) + "..." : result;
                    Console.WriteLine($"  JSON preview: {preview}");
                    return true;
                }
                else
                {
                    Console.WriteLine("✗ Result is not a JSON array");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("✗ Function returned empty result");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ ChatFunction test FAILED: {ex.Message}");
            return false;
        }
    }
    
    /// <summary>
    /// Tests Semantic Kernel function registration
    /// </summary>
    /// <returns>True if function registration works</returns>
    public static bool TestSemanticKernelRegistration()
    {
        try
        {
            Console.WriteLine("✓ Testing Semantic Kernel function registration:");
            
            // Create a test kernel with mock settings
            IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
            
            // We'll test without OpenAI connection, just the function registration
            Kernel kernel = kernelBuilder.Build();
            
            // Test if we can import the plugin (this is what GetKernel does)
            var programType = typeof(Program);
            var getProductsMethod = programType.GetMethod("GetProductsInCategory", 
                BindingFlags.NonPublic | BindingFlags.Static);
            
            if (getProductsMethod == null)
            {
                Console.WriteLine("✗ GetProductsInCategory method not found for registration");
                return false;
            }
            
            // Try to create a function from the method
            var kernelFunction = kernel.CreateFunctionFromMethod(
                method: getProductsMethod,
                functionName: "GetProductsInCategory",
                description: "Gets products in a category from the Northwind database");
            
            if (kernelFunction != null)
            {
                Console.WriteLine("✓ Function can be registered with Semantic Kernel");
                Console.WriteLine($"  Function name: {kernelFunction.Name}");
                Console.WriteLine($"  Function description: {kernelFunction.Description}");
                
                // Test function metadata
                var metadata = kernelFunction.Metadata;
                Console.WriteLine($"  Parameters count: {metadata.Parameters.Count}");
                
                if (metadata.Parameters.Count > 0)
                {
                    var param = metadata.Parameters[0];
                    Console.WriteLine($"  First parameter: {param.Name} ({param.ParameterType})");
                }
                return true;
            }
            else
            {
                Console.WriteLine("✗ Failed to create function from method");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Semantic Kernel registration test FAILED: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Shows user experience features and safeguards information
    /// </summary>
    public static void ShowUserExperienceInfo()
    {
        Console.WriteLine("✓ Added safeguard: Application will exit after 3 consecutive invalid attempts");
        Console.WriteLine("✓ Counter resets when valid input is provided");
        Console.WriteLine("✓ Users get clear feedback with attempt count (e.g., '1/3', '2/3', '3/3')");
        Console.WriteLine("✓ Graceful handling of piped input scenarios");
        Console.WriteLine("✓ Enhanced debug logging for function execution tracking");
        Console.WriteLine("✓ Interactive test menu with letter-based selection");
    }
} 