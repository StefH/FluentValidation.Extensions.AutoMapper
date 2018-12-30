using FluentValidation;
using FluentValidationExample.Common.Validation;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace FluentValidationExample.Web.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public GlobalExceptionFilter([NotNull] ILoggerFactory loggerFactory)
        {
            Guard.NotNull(loggerFactory, nameof(loggerFactory));

            _logger = loggerFactory.CreateLogger(nameof(GlobalExceptionFilter));
        }

        public override void OnException([NotNull] ExceptionContext context)
        {
            Guard.NotNull(context, nameof(context));

            if (context.Exception is ValidationException validationException)
            {
                _logger.LogError(validationException, "ValidationException");

                var modelState = new ModelStateDictionary();
                foreach (var error in validationException.Errors)
                {
                    modelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                context.Result = new BadRequestObjectResult(modelState);
            }

            base.OnException(context);
        }
    }
}
