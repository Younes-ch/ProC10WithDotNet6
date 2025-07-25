﻿namespace AutoLot.Api.Filters;

public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly IWebHostEnvironment _hostEnvironment;

    public CustomExceptionFilterAttribute(IWebHostEnvironment hostEnvironment)
    {
        _hostEnvironment = hostEnvironment;
    }

    public override void OnException(ExceptionContext context)
    {
        var ex = context.Exception;
        var stackTrace = _hostEnvironment.IsDevelopment() ? context.Exception.StackTrace : string.Empty;
        var message = ex.Message;
        string error;
        IActionResult actionResult;
        switch (ex)
        {
            case DbUpdateConcurrencyException ce:
                //Returns a 400
                error = "Concurrency Issue.";
                actionResult = new BadRequestObjectResult(
                    new { Error = error, Message = message, StackTrace = stackTrace });
                break;
            default:
                error = "General Error.";
                actionResult = new ObjectResult(
                    new { Error = error, Message = message, StackTrace = stackTrace })
                {
                    StatusCode = 500
                };
                break;
        }
        context.Result = actionResult;
    }
}
