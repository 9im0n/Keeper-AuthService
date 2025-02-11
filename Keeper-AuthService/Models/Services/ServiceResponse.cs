namespace Keeper_AuthService.Models.Services
{
    public class ServiceResponse<T>
    {
        public bool IsSuccess { get; set; }
        public int Status { get; set; }
        public T? Data { get; set; }
        public string Message { get; set; }

        public static ServiceResponse<T?> Success(T? data, int status = 200, string message = "")
        {
            return new ServiceResponse<T?> { IsSuccess = true, Status = status, Data = data, Message = message };
        }

        public static ServiceResponse<T?> Fail(T? data, int status = 400, string message = "")
        {
            return new ServiceResponse<T?> { IsSuccess = false, Status = status, Data = data, Message = message };
        }
    }
}
