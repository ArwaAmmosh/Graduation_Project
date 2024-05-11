namespace Graduation_Project.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount = await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultUser = new User()
                {
                    UserName = "UNITOOL",
                    Email = "UNITOOLTeam@gmail.com",
                    FirstName = "UNITOOL",
                    LastName = "Team",
                    University = "Tanta",
                    Government = "El-Garbia",
                    PhoneNumber = "01025019961",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString()

                };
                await _userManager.CreateAsync(defaultUser, "@UNITool123");
                await _userManager.AddToRoleAsync(defaultUser, "SuperAdmin");
            }
        }
    }
}

