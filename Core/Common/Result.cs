using System.Diagnostics.CodeAnalysis;

namespace Core.Common;

public record Result
{
    public required bool IsFailure { get; init; }

    public required string ErrorMessage { get; init; }

    public static Result Fail(string errorMessage)
    {
        return new Result { IsFailure = true, ErrorMessage = errorMessage };
    }

    public static Result Successful { get; } = new Result { IsFailure = false, ErrorMessage = "" };

    public static Result<T> Fail<T>(string errorMessage)
    {
        return new Result<T> { IsFailure = true, ErrorMessage = errorMessage, Value = default };
    }

    public static Result<T> Success<T>(T value)
    {
        return new Result<T> { IsFailure = false, ErrorMessage = "", Value = value };
    }
}

public record Result<T> : Result
{
    public required T? Value { get; init; }

    public bool TryGetValue([NotNullWhen(true)] out T? value)
    {
        value = !IsFailure ? Value : default;
        return !IsFailure;
    }
}