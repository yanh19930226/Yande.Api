using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;

namespace SignApi.Filters
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var validationErrors = context.ModelState
                .Keys
                .SelectMany(k => context.ModelState[k].Errors)
                .Select(e => e.ErrorMessage)
                .ToArray();

            context.Result = new OkObjectResult(string.Join(",", validationErrors));
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
        }
    }
}
