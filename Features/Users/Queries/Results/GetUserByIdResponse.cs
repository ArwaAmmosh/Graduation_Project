namespace Graduation_Project.Features.Users.Queries.Results
{
    public class GetUserByIdResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string University { get; set; }
        public string Government { get; set; }
        public string AcademicYear { get; set; }
        public string? College { get; set; }
        public string? PersonalImage { get; set; }

    }
}
