using IdentityService.Shared.Entites;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data.DataSeed
{
    public class DataSeeder
    {
        // Define GUIDs as constants for consistent seeding
        private static readonly Guid AdminRoleId = Guid.Parse("a1111111-1111-1111-1111-111111111111");
        private static readonly Guid UserRoleId = Guid.Parse("a2222222-2222-2222-2222-222222222222");

        private static readonly Guid CreateUserPermId = Guid.Parse("b1111111-1111-1111-1111-111111111111");
        private static readonly Guid DeleteUserPermId = Guid.Parse("b2222222-2222-2222-2222-222222222222");
        private static readonly Guid UpdateUserPermId = Guid.Parse("b3333333-3333-3333-3333-333333333333");
        private static readonly Guid ViewUserPermId = Guid.Parse("b4444444-4444-4444-4444-444444444444");
        private static readonly Guid ManageRolesPermId = Guid.Parse("b5555555-5555-5555-5555-555555555555");

        public static async Task SeedAsync(ApplicationDbcontext context)
        {
            // Ensure database is created
            await context.Database.MigrateAsync();

            // Seed Roles
            if (!await context.Roles.AnyAsync())
            {
                await SeedRolesAsync(context);
            }

            // Seed Permissions
            if (!await context.Permissions.AnyAsync())
            {
                await SeedPermissionsAsync(context);
            }

            // Seed RolePermissions
            if (!await context.RolePermissions.AnyAsync())
            {
                await SeedRolePermissionsAsync(context);
            }
        }

        private static async Task SeedRolesAsync(ApplicationDbcontext context)
        {
            var roles = new List<Role>
        {
            new Role { Id = AdminRoleId, Name = "Admin" },
            new Role { Id = UserRoleId, Name = "Customer" },
        };

            await context.Roles.AddRangeAsync(roles);
            await context.SaveChangesAsync();
        }

        private static async Task SeedPermissionsAsync(ApplicationDbcontext context)
        {
            var permissions = new List<Permission>
        {
            new Permission { Id = CreateUserPermId, Name = "CreateUser" },
            new Permission { Id = DeleteUserPermId, Name = "DeleteUser" },
            new Permission { Id = UpdateUserPermId, Name = "UpdateUser" },
            new Permission { Id = ViewUserPermId, Name = "ViewUser" },
            new Permission { Id = ManageRolesPermId, Name = "ManageRoles" }
        };

            await context.Permissions.AddRangeAsync(permissions);
            await context.SaveChangesAsync();
        }

        private static async Task SeedRolePermissionsAsync(ApplicationDbcontext context)
        {
            var rolePermissions = new List<RolePermission>
        {
            // Admin has all permissions
            new RolePermission { RoleId = AdminRoleId, PermissionId = CreateUserPermId },
            new RolePermission { RoleId = AdminRoleId, PermissionId = DeleteUserPermId },
            new RolePermission { RoleId = AdminRoleId, PermissionId = UpdateUserPermId },
            new RolePermission { RoleId = AdminRoleId, PermissionId = ViewUserPermId },
            new RolePermission { RoleId = AdminRoleId, PermissionId = ManageRolesPermId },

            // User has limited permissions
            new RolePermission { RoleId = UserRoleId, PermissionId = ViewUserPermId },
        };

            await context.RolePermissions.AddRangeAsync(rolePermissions);
            await context.SaveChangesAsync();
        }
    }
}
