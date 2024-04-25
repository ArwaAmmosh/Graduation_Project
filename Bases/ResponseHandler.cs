using Microsoft.Extensions.Localization;
using SharpDX.DXGI;

namespace Graduation_Project.Bases
{
    public class ResponseHandler
    {
        private readonly IStringLocalizer<SharedResource> _stringLocalizer;

        public ResponseHandler() { }

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
        public Response<T> Success<T>(T entity, object Meta = null)
        {
            return new Response<T>
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = "operation is done",
                Meta = Meta
            };
        }
        public Response<T> Unauthorized<T>(string Message = null)
        {
            return new Response<T>
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = false,
                Message = Message == null ? "UnAuthorized":Message

            };
        }
        public Response<T> BadRequest<T>(string Message = null)
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

