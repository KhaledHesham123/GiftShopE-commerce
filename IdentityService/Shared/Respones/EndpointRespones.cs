using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Respones
{
    public class EndpointRespones<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();
        public int StatusCode { get; set; }


        public static EndpointRespones<T> Result(bool IsSuccess)
        {
            return new EndpointRespones<T> { IsSuccess = IsSuccess };
        }

        public static EndpointRespones<T> Success(T data, string? message = null, int statusCode = 200)
        {
            return new EndpointRespones<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                StatusCode = statusCode
            };
        }
        public static EndpointRespones<T> Fail(string message = "", List<string>? Errors = null, int statuscode = 400)
        {
            return new EndpointRespones<T>
            {
                Errors = Errors,
                IsSuccess = false,
                StatusCode = statuscode,
                Message = message
            };
        }

    }
}
