using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Stef.Validation;

namespace FluentValidationExample.WebApi6.Filters;

public class GlobalExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger _logger;

    public GlobalExceptionFilter(ILoggerFactory loggerFactory)
    {
        Guard.NotNull(loggerFactory);

        _logger = loggerFactory.CreateLogger(nameof(GlobalExceptionFilter));
    }

    public override void OnException(ExceptionContext context)
    {
        Guard.NotNull(context);

        if (context.Exception is ValidationException validationException)
        {
            _logger.LogError(validationException, "ValidationException");

            var modelState = new ModelStateDictionary();
            foreach (var error in validationException.Errors.OrderBy(e => e.PropertyName))
            {
                modelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            context.Result = new BadRequestObjectResult(modelState);
        }

        base.OnException(context);
    }
}