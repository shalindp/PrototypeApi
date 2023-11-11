
using System.ComponentModel.DataAnnotations;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.Extensions;

public static class ControllerExtensions
{
    public static IActionResult Resolve<TResult, TContract>(this Result<TResult> result,
        Func<TResult, TContract> mapper)
    {
        return result.Match<IActionResult>(success =>
        {
            var response = mapper(success);
            return new OkObjectResult(response);
        }, exception =>
        {
            if (exception is ValidationException validationException)
            {
                return new BadRequestObjectResult(validationException);
            }

            if (exception is KeyNotFoundException)
            {
                return new NotFoundResult();
            }

            return new StatusCodeResult(500);
        });
    }
}