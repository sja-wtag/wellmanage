namespace wellmanage.api.Models;

public class BaseResponse
{
    public BaseResponse()
    {
        HasError= false;
        ErrorList = new List<string>();
    }

    public bool NotifyUser { get; set; }
    public bool HasError { get; set; }
    public List<string> ErrorList { get; set; }
}
public class ServiceResponse<T> : BaseResponse
{
    public T? ResponseData { get; set; }

    public List<T>? ListResponseData { get; set; }
}
public class ApiResponse : BaseResponse
{
    public string? Status { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}
public class ApiResponse<T> : ServiceResponse<T>
{
    public string? Status { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}

public class PaginatedResponse<T> : BaseResponse
{
    public List<T>? ListResponseData { get; set; }
    public long ResultCount {  get; set; }
    public bool? HasNext { get; set; }
    public string? Status { get; set; }
    public int StatusCode { get; set; }
    public string? Message { get; set; }
}