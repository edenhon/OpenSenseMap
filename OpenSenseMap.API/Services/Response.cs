namespace OpenSenseMap.API.Services
{
    public class Response<T>
    {
        public required string Code { get; set; }
        public required string Message { get; set; }
        public required string Token { get; set; }
        public required string RefreshToken { get; set; }
        public T ?Data { get; set; }
    }
}
