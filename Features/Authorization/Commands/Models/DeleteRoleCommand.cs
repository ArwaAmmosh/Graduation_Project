namespace Graduation_Project.Features.Authorization.Commands.Models
{
    public class DeleteRoleCommand:IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteRoleCommand(int id) 
        { 
            Id=id;
        }
    }
}
