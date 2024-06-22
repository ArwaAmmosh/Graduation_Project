namespace Graduation_Project.Helpers;

public class LoginResponseDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? University { get; set; }
    public string? Government { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? AcademicYear { get; set; }
    public string? College { get; set; }
    public string? Department { get; set; }
    public string? FrontIdImage { get; set; }
    public string? BackIdImage { get; set; }
    public string? CollegeCardFrontImage { get; set; }
    public string? CollegeCardBackImage { get; set; }
    public string? PersonalImage { get; set; }
    public string? NationalId { get; set; }
    public bool IsComplete { get; set; }
    public string? AccessToken { get; set; }
}