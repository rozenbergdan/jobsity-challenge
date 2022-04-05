using Challenge.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Challenge.WebApi.ExceptionFilters
{
    public class HttpDomainExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is DomainException)
            {
                var msg = context.Exception.GetBaseException().Message;
                var apiErrorDTO = new { message = msg, statusCode = (int)HttpStatusCode.BadRequest, stackTrace = context.Exception.StackTrace };

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(apiErrorDTO);
            }
        }
    }
}
