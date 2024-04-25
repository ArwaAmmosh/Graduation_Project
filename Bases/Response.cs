﻿using System.Net;

namespace Graduation_Project.Bases
{
    public class Response<T>
    {
        public Response() { }
        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;

        }
        public Response(string message)
        {
            Succeeded = true;
            Message = message;
        }
        public Response(string message, bool succeeded)
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
