using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;

public partial class NorthwindContext : DbContext
{
    public NorthwindContext()
    {
    }

    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string database = "Northwind.db";
            string dir = Environment.CurrentDirectory;
            string path = string.Empty;

            // Determine the path to the database file.
            if (dir.EndsWith("net9.0"))
            {
                // In the <project>\bin\<Debug|Release>\net9.0 directory. (Visual Studio)
                path = Path.Combine("..", "..", "..", "..", database);
            }
            else
            {
                // In the <project> directory. (CLI, VSC)
                path = Path.Combine("..", database);
            }

            path = Path.GetFullPath(path); // Convert to absolute path.
            try
            {
                NorthwindContextLogger.WriteLine($"Database path: {path}");
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }

            if (!File.Exists(path))
            {
                // Suppresss automatic creation of empty database if expecteddb not found.
                throw new FileNotFoundException(
                  message: $"{path} not found.", fileName: path);
            }

            optionsBuilder.UseSqlite($"Data Source={path}");

            optionsBuilder.LogTo(NorthwindContextLogger.WriteLine,
              new[] { Microsoft.EntityFrameworkCore
        .Diagnostics.RelationalEventId.CommandExecuting });
        }
    }

    /// <summary>
    /// Configures the database model and its relationships when the model is being created.
    /// Sets up entity configurations, default values, and relationship behaviors.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Removed a couple of ValueGeneratedNever() calls, p. 639
        // To allow to set keys manually, p. 639

        modelBuilder.Entity<Order>(entity =>
        {
            // Freight set to decimal, not a double, p. 639
            entity.Property(e => e.Freight).HasDefaultValue(0.0M);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.Property(e => e.Quantity).HasDefaultValue((short)1);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ReorderLevel).HasDefaultValue((short)0);
            // UnitPrice set to decimal, not a double, p. 639
            entity.Property(e => e.UnitPrice).HasDefaultValue(0.0M);
            // Conversion added, p. 639
            entity.Property(e => e.UnitPrice).HasConversion<double>();
            entity.Property(e => e.UnitsInStock).HasDefaultValue((short)0);
            entity.Property(e => e.UnitsOnOrder).HasDefaultValue((short)0);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
