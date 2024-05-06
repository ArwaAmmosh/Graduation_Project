namespace Graduation_Project.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> _roleManager)
        {
            var rolesCount = await _roleManager.Roles.CountAsync();
            if (rolesCount <= 0)
            {
                await _roleManager.CreateAsync(new Role()
                {
                    Name = "SuperAdmin"
                });
                await _roleManager.CreateAsync(new Role()
                {
                    Name = "Admin"
                });
                await _roleManager.CreateAsync(new Role()
                {
                    Name = "AdmittedUser"
                });
                await _roleManager.CreateAsync(new Role()
                {
                    Name = "ViewUser"
                });
                await _roleManager.CreateAsync(new Role()
                {
                    Name = "User"
                });
            }

        }
    }
    }

