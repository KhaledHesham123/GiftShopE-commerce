using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfileService.Respones
{
    public class RequestRespones<T>
    {

        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();
        public int StatusCode { get; set; }


        public static RequestRespones<T> Result(bool IsSuccess,string message="") 
        {
            return new RequestRespones<T> { IsSuccess = IsSuccess };
        }

        public static RequestRespones<T> Success(T data, string? message = null, int statusCode = 200)
        {
            return new RequestRespones<T>
            {
                IsSuccess = true,
                Data = data,
                Message = message,
                StatusCode = statusCode
            };
        }
        public static RequestRespones<T> Fail(string message="", int statuscode = 400, List<string>? Errors=null)
        {
            return new RequestRespones<T> { 
                Errors=Errors,
                IsSuccess=false,
                StatusCode= statuscode,
                Message=message
            };
        }

    }
}
