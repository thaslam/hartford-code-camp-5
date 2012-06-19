using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Diagnostics;

namespace FitnessLog.WebApi.ActionFilters
{
    public class TracingFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            HttpContext.Current.Trace.Write("WEB API", actionExecutedContext.ActionContext.ActionArguments.ToString());
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}