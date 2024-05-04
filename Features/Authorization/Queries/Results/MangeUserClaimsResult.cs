namespace Graduation_Project.Features.Authorization.Queries.Results
{
    public class MangeUserClaimsResult
    {
        public int UserId { get; set; }
        public List<UserClaims> userClaims { get; set; }
    }
    public class UserClaims
    {
        public string Type { get; set; }
        public bool Value { get; set; }
    }
}

