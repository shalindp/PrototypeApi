using System.ComponentModel.DataAnnotations;
using LanguageExt.Common;

namespace Application;

public static class ValidationExceptionExtension
{
    public static Result<T> ToResult<T>(this ValidationException exception)
    {
        return new Result<T>(exception);
    }
}