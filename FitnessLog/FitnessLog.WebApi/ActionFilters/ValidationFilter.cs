using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Json;
using System.Net;

namespace FitnessLog.WebApi.ActionFilters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var state = actionContext.ModelState;

            if (!state.IsValid)
            {
                dynamic errors = new JsonObject();
                foreach (var key in state.Keys)
                {
                    var errs = state[key].Errors;
                    if (errs.Any())
                    {
                        errors[key] = errs.First().ErrorMessage;
                    }
                }

                actionContext.Response =
                    new System.Net.Http.HttpResponseMessage<JsonValue>(errors, HttpStatusCode.BadRequest);
            }
        }
    }
}