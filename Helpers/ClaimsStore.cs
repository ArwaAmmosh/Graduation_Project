using System.Security.Claims;

namespace Graduation_Project.Helpers
{
    public static class ClaimsStore
    {
        public static List<Claim> claims = new()
        {
            new Claim("Registeriation","false"),
            new Claim("AddNewRole","false"),
            new Claim("AddNewTool","false"),
            new Claim("ChangePassword","false"),


        };
    }
}
