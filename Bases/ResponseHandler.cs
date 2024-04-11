using Azure;
using Graduation_Project.Resource;

namespace Graduation_Project.Bases
{
    public class ResponseHandler
    {
        public ResponseHandler() { }
        public Response<T> Deleted<T>()
        {
            return new Response<T>()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded=true,
                Message= "Deleted Successfully"

        };
        }
        public Response<T> Success<T>(T entity ,object Meta = null) 
        {
            return new Response<T>
            {
                Data = entity,
                StatusCode = System.Net.HttpStatusCode.OK,
                Succeeded = true,
                Message = "Added Successfully",
                Meta = Meta
            };
        }
        public Response<T> Unauthorized<T>(string Message = null)
         {
            return new Response<T>
            {
                StatusCode = System.Net.HttpStatusCode.Unauthorized,
                Succeeded = false,
                Message = "UnAuthorized"

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
        public Response<T> NotFound<T>(string Message = null)
        {
            return new Response<T>
            {
                StatusCode = System.Net.HttpStatusCode.NotFound,
                Succeeded = false,
                Message = Message == null ? "Not Found" : Message

            };
        }
        public Response<T> Created<T>(T entity, object Meta)
        {
            return new Response<T>
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Succeeded = false,
                Message = "Created",
                Meta=Meta,
                Data= entity

            };
        }

    }
}

