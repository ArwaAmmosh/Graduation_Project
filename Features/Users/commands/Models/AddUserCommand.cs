namespace Graduation_Project.Features.Users.commands.Models
{
    public class AddUserCommand : IRequest<Graduation_Project.Bases.Response<string>>
    {
#pragma warning disable CS8618 // Non-nullable property 'FirstName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string FirstName { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'FirstName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'LastName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string LastName { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'LastName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Email' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string Email { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Email' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Password' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string Password { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'Password' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'ConfirmPassword' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string ConfirmPassword { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'ConfirmPassword' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'University' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string Univserity { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'University' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    }
}
