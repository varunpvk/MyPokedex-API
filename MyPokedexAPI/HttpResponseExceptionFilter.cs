namespace MyPokedex.API
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using MyPokedex.Core;
    using System;
    using System.Net;

    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is HttpResponseException exception) {
                context.Result = new ObjectResult(exception.Value) {
                    StatusCode = (int)exception.Status,
                };
            }
            
            if(context.Exception is ArgumentNullException argException) {
                context.Result = new ObjectResult(argException.Message) {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }

            context.ExceptionHandled = true;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //This method is deliberately left empty
        }
    }
}
