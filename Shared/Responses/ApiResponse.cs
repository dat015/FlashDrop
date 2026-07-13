using System.Net;

namespace Shared.Responses;

public class ApiResponse<T>
{
    public bool Success { get; init; }

    public string Message { get; init; } = string.Empty;

    public T? Data { get; init; }

    public IEnumerable<string>? Errors { get; init; }

    private ApiResponse(
        bool success,
        string message,
        T? data = default,
        IEnumerable<string>? errors = null)
    {
        Success = success;
        Message = message;
        Data = data;
        Errors = errors;
    }

    public static ApiResponse<T> SuccessResponse(
        T data,
        string message = "Success")
    {
        return new(true, message, data);
    }

    public static ApiResponse<T> FailResponse(
        string message,
        IEnumerable<string>? errors = null)
    {
        return new(false, message, default, errors);
    }
}