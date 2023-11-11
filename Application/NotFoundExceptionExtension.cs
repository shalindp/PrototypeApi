using LanguageExt.Common;

namespace Application;

public static class NotFoundExceptionExtension
{
    public static Result<T> ToResult<T>(this KeyNotFoundException exception)
    {
        return new Result<T>(exception);
    }
}