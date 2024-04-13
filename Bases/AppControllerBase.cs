using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Graduation_Project.Bases
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppControllerBase : ControllerBase
    {
#pragma warning disable CS8618 // Non-nullable field '_mediatorInstance' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
        private IMediator? _mediatorInstance;
#pragma warning restore CS8618 // Non-nullable field '_mediatorInstance' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8603 // Possible null reference return.
        protected IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>() ;

#pragma warning restore CS8603 // Possible null reference return.
#pragma warning restore CS8601 // Possible null reference assignment.
        #region Actions
        public ObjectResult NewResult<T>(Response<T> response)
        {
            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new OkObjectResult(response);
                case HttpStatusCode.Accepted:
                    return new AcceptedResult(string.Empty, response);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(response);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedObjectResult(response);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(response);
                case HttpStatusCode.UnprocessableEntity:
                    return new UnprocessableEntityObjectResult(response);
                case HttpStatusCode.Created:
                    return new CreatedResult(string.Empty, response);
                default:
                    return new BadRequestObjectResult(response);
            }
        }
        #endregion

    }
}
