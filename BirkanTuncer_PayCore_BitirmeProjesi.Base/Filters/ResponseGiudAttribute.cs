using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BirkanTuncer_PayCore_BitirmeProjesi.Base
{
    public class ResponseGiudAttribute : ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("Response-Hash", Guid.NewGuid().ToString());
        }
    }
}
