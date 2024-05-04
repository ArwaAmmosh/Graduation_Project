namespace Graduation_Project.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> _userManager)
        {
            var usersCount=await _userManager.Users.CountAsync();
            if (usersCount <= 0)
            {
                var defaultUser = new User()
                {
                    UserName = "Admin",
                    Email = "arwaahmedamosh@gmail.com",
                    FirstName = "UNTOOL",
                    LastName = "Project",
                    Univserity = "Tanta",
                    Government = "El-Garbia",
                    PhoneNumber = "010250123695",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                await _userManager.CreateAsync(defaultUser, "@ArwaAmmosh123");
                await _userManager.AddToRoleAsync(defaultUser, "Admin");
            }
        }
    }
}
