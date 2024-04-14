using Microsoft.AspNetCore.Identity;

namespace Graduation_Project.Entities.Identity
{
    public class User : IdentityUser<int>
    { 
#pragma warning disable CS8618 // Non-nullable property 'FullName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning restore CS8618 // Non-nullable property 'FullName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Univserity' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string Univserity { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Univserity' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'FirstName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string FirstName { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'FirstName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'LastName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string LastName { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'LastName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Government' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string? Government { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Government' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'AcadmicYear' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string? AcadmicYear { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'AcadmicYear' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'College' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string? College { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'College' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Phone' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning restore CS8618 // Non-nullable property 'Phone' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'FrontIdImage' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public byte[]? FrontIdImage { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'FrontIdImage' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'BackIdImage' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public byte[]? BackIdImage { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'BackIdImage' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'CollegeCardFrontImage' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public byte[]? CollegeCardFrontImage { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'CollegeCardFrontImage' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'CollegeCardBackImage' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public byte[]? CollegeCardBackImage { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'CollegeCardBackImage' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'PersonalImage' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public byte[]? PersonalImage { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'PersonalImage' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'NationalId' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string? NationalId { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'NationalId' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Tool' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public List<Tool> Tool { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Tool' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'FavoriteTool' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public List<FavoriteTool> FavoriteTool { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'FavoriteTool' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    }
}
