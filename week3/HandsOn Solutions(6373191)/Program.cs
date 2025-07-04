using entity_core.Data;
using entity_core.Models;
using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main(string[] args)
    {
        using var context = new AppDbContext();


        var singleProduct = await context.Products
            .Include(p => p.Category)
            .SingleOrDefaultAsync(p => p.Id == 6);

        if (singleProduct != null)
        {
            Console.WriteLine($"\nSingle Product (Id = 1):");
            Console.WriteLine($"Product: {singleProduct.Name}, Price: ₹{singleProduct.Price}, Category: {singleProduct.Category.Name}");
        }
        else
        {
            Console.WriteLine("\nNo product found with Id = 1.");
        }

 
        var expensive = await context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Price > 50000);

        Console.WriteLine("\nExpensive Product:");
        Console.WriteLine($"Product: {expensive?.Name}, Price: ₹{expensive?.Price}, Category: {expensive?.Category.Name}");
    }
}
