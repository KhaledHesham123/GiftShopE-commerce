using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain_Layer.Respones
{
    public class RequestRespones<T>
    {
        public T Data { get; set; } = default!;

        public bool IsSuccess { get; set; } = false;

        public int StatusCode { get; set; } = 0;    

        public string Message { get; set; } = string.Empty;

      


        public RequestRespones(T data, string message = "", bool isSuccess = true)
        {
            Data = data;
            Message = message;
            IsSuccess = isSuccess;
        }
        public RequestRespones(string message = "", bool isSuccess = true)
        {

            Message = message;
            IsSuccess = isSuccess;
        }

        public static RequestRespones<T>Success(T data, int statusCode=200, string Message="")
        {
            return new RequestRespones<T>
            {
                Data = data,
                IsSuccess = true,
                StatusCode = statusCode,
                Message= Message
            };
        }
        public static RequestRespones<T> Fail(string message, int statusCode)
        {
            return new RequestRespones<T>(message, false) 
            {
                StatusCode= statusCode
            };
        }
    }
}
