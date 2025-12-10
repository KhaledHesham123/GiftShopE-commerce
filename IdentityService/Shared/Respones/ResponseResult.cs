namespace IdentityService.Shared.Respones
{
    public class ResponseResult<TData>
    {

        public bool Success { get; set; }
        public string? Message { get; set; }
        public TData? Data { get; set; }
        public List<string> Errors { get; set; } = new();
        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ResponseResult<TData> Response(bool success)
        {
            return new ResponseResult<TData> { Success = success };
        }
        public static ResponseResult<TData> SuccessResponse(TData data, string? message = null, int statusCode = 200)
        {
            return new ResponseResult<TData>
            {
                Success = true,
                Data = data,
                Message = message,
                StatusCode = statusCode
            };
        }
        public static ResponseResult<TData> FailResponse(string message, List<string>? errors = null, int statusCode = 400)
        {
            return new ResponseResult<TData>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>(),
                StatusCode = statusCode
            };
        }
    }
    
}
