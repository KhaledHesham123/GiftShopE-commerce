namespace CartService.Shared.Respones
{
    public  class RequestRespones<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public int StatusCode { get; set; }

        public static RequestRespones<T> Result(bool isSuccess)
        {
            return new RequestRespones<T>
            {
                Success = isSuccess,
                
            };
        }

        public static RequestRespones<T> success(T data,string Message="", int statuscode=200) 
        {
            return new RequestRespones<T>
            {
                Data=data,
                Message=Message,
                Success=true,
                StatusCode=statuscode
            };
        }

        public static RequestRespones<T> Fail( string Message = "", int statuscode =400)
        {
            return new RequestRespones<T>
            {
                Message = Message,
                Success = false,
                StatusCode = statuscode
            };
        }
    }
}
