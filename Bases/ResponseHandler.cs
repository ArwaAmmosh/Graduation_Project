namespace Graduation_Project.Bases
{
    public class ResponseHandler
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

#pragma warning disable CS8618 // Non-nullable field '_stringLocalizer' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.
        public ResponseHandler() { }
#pragma warning restore CS8618 // Non-nullable field '_stringLocalizer' must contain a non-null value when exiting constructor. Consider declaring the field as nullable.

        public ResponseHandler(IStringLocalizer<SharedResource> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }

        public Response<T> Deleted<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = "Deleted Successfully"

            };
        }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public Response<T> Success<T>(T entity, object Meta = null,string Message=null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            return new Response<T>
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = Message == null ? "Added Successfully" : Message,
                Meta = Meta
            };
        }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public Response<T> Unauthorized<T>(string Message = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            return new Response<T>
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = false,
                Message = "UnAuthorized"

            };
        }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public Response<T> BadRequest<T>(string Message = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            return new Response<T>
            {
                StatusCode = System.Net.HttpStatusCode.BadRequest,
                Succeeded = false,
                Message = Message == null ? "Bad Reqest" : Message

            };
        }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public Response<T> UnprocessableEntity<T>(string Message = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            return new Response<T>
            {
                StatusCode = System.Net.HttpStatusCode.UnprocessableEntity,
                Succeeded = false,
                Message = Message == null ? "Unprocessable Entity" : Message

            };
        }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public Response<T> NotFound<T>(string Message = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            return new Response<T>
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = Message == null ? "Not Found" : Message

            };
        }
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public Response<T> Created<T>(T entity, object Meta = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        {
            return new Response<T>
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = false,
                Message = "Created",
                Meta = Meta,
                Data = entity

            };
        }

    }
}

