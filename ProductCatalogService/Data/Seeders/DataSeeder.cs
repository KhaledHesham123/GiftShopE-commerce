using UserProfileService.Data.DBContexts;
using UserProfileService.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace UserProfileService.Data.Seeders
{
    public class DataSeeder
    {
        public static List<Category> Categories => new()
        {
            new Category { Name = "Flowers", Status = true },
            new Category { Name = "Gifts", Status = true },
            new Category { Name = "Jewelry", Status = true },
            new Category { Name = "Cards", Status = true}
        };

        public static List<Occasion> Occasions => new()
        {
            new Occasion { Name = "Birthday", Status = true },
            new Occasion { Name = "Wedding", Status = true},
            new Occasion { Name = "Graduation", Status = true },
            new Occasion { Name = "Anniversary", Status = true }
        };
        public static async Task SeedAsync(ProductCatalogDbContext context)
        {
            // Ensure database is created
            await context.Database.MigrateAsync();


            if (!await context.Categories.AnyAsync())
            {
                await context.Categories.AddRangeAsync(Categories);
                await context.SaveChangesAsync();
            }

            // ============ Seed Occasions ============
            if (!await context.Occasions.AnyAsync())
            {
                await context.Occasions.AddRangeAsync(Occasions);
                await context.SaveChangesAsync();
            }
        }


        //private static async Task SeedRolePermissionsAsync(ProductCatalogDbContext context)
        //{


        //    //await context.Categories.AddRangeAsync(rolePermissions);
        //    await context.SaveChangesAsync();
        //}
    }
}
