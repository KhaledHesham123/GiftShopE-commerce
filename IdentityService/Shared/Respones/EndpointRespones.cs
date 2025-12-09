using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Respones
{
    public class EndpointRespones<T>
    {
        public T Data { get; set; } = default!;

        public bool IsSuccess { get; set; } = false;

        public int StatusCode { get; set; } = 0;

        public string Message { get; set; } = string.Empty;




        public EndpointRespones(T data, string message = "", bool isSuccess = true)
        {
            Data = data;
            Message = message;
            IsSuccess = isSuccess;
        }
        public EndpointRespones(string message = "", bool isSuccess = true)
        {

            Message = message;
            IsSuccess = isSuccess;
        }

        public static EndpointRespones<T> Success(T data, int statusCode = 200, string Message = "")
        {
            return new EndpointRespones<T>
            {
                Data = data,
                IsSuccess = true,
                StatusCode = statusCode,
                Message = Message
            };
        }
        public static EndpointRespones<T> Fail(string message, int statusCode)
        {
            return new EndpointRespones<T>(message, false)
            {
                StatusCode = statusCode
            };
        }

    }
}
