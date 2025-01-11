using System.Text.Json.Serialization;

public class ApiResponse<T>
{
    public int ErrorCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Description { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T Data { get; set; }

    public ApiResponse(int errorCode, T data)
    {
        ErrorCode = errorCode;
        Data = data;
        Description = string.Empty;
    }

    public ApiResponse(int errorCode, string description)
    {
        ErrorCode = errorCode;
        Description = description;
        Data = default!;
    }
}
