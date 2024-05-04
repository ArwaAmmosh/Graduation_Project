namespace Graduation_Project.Features.Authorization.Commands.Models
{
    public class AddRoleCommand:IRequest<Response<string>>
    {
        public string RoleName { get; set; }
    }
}
