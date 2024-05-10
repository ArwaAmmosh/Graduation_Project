namespace Graduation_Project.Features.Authentication.Queries.Models
{
    public class ConfirmEmailQuery: IRequest<Response<string>>
    {
        public string Code { get; set; }
    }
}
