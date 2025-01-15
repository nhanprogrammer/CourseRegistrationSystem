using System.Text.Json.Serialization;

public class ApiResponse<T>
{
    public int Status { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string Description { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public T Data { get; set; }

    public ApiResponse(int status, T data)
    {
        Status = status;
        Data = data;
        Description = null;
    }

    public ApiResponse(int status, string description)
    {
        Status = status;
        Description = description;
        Data = default!;
    }
}
