using System.Net;

namespace Graduation_Project.Bases
{
    public class Response<T>
    {
#pragma warning disable CS8618 // Non-nullable property 'Meta' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Message' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Errors' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Data' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public Response() { }
#pragma warning restore CS8618 // Non-nullable property 'Data' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning restore CS8618 // Non-nullable property 'Errors' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning restore CS8618 // Non-nullable property 'Message' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning restore CS8618 // Non-nullable property 'Meta' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Meta' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Errors' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        public Response(T data, string message = null)
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8618 // Non-nullable property 'Errors' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning restore CS8618 // Non-nullable property 'Meta' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        {
            Succeeded = true;
            Message = message;
            Data = data;

        }
#pragma warning disable CS8618 // Non-nullable property 'Meta' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Errors' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Data' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public Response(string message)
#pragma warning restore CS8618 // Non-nullable property 'Data' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning restore CS8618 // Non-nullable property 'Errors' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning restore CS8618 // Non-nullable property 'Meta' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        {
            Succeeded = true;
            Message = message;
        }
#pragma warning disable CS8618 // Non-nullable property 'Meta' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Errors' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'Data' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public Response(string message, bool succeeded)
#pragma warning restore CS8618 // Non-nullable property 'Data' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning restore CS8618 // Non-nullable property 'Errors' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning restore CS8618 // Non-nullable property 'Meta' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        {
            Succeeded = succeeded;
            Message = message;
        }
        public HttpStatusCode StatusCode { get; set; }
        public object Meta { get; set; }
        public string Message { get; set; }
        public bool Succeeded { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }


    }
}
