namespace Graduation_Project.Features.Authorization.Queries.Results
{
    public class MangeUserRolesResult
    {
        public int UserId { get; set; }
        public List<UserRoles> userRoles { get; set; }
    }
    public class UserRoles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasRole { get; set; }
    }
}

