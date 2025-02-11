using System.Text.Json;
using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any(x => x.UserName == "admin@test.com"))
            {
                var roles = new List<IdentityRole>
                {
                    new() {Name = "Admin"},
                    new() {Name = "Customer"}
                };

                foreach(var role in roles)
                {
                    await roleManager.CreateAsync(role);
                }

                var admin = new AppUser
                {
                    FirstName = "SkiNet",
                    LastName = "Admin",
                    UserName = "admin@test.com",
                    Email = "admin@test.com"
                };

                await userManager.CreateAsync(admin, "Kg@m@Dr1v3");
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            if(!context.Products.Any())
            {
                var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/Seed Data/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if(products == null) return;

                context.Products.AddRange(products);

                await context.SaveChangesAsync();
            }

            if(!context.DeliveryMethods.Any())
            {
                var deliveryMethodsData = await File.ReadAllTextAsync("../Infrastructure/Data/Seed Data/delivery.json");

                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);
                if(methods == null) return;

                context.DeliveryMethods.AddRange(methods);

                await context.SaveChangesAsync();
            }
        }
    }
}