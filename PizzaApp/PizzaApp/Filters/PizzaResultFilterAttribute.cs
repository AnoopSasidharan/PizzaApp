using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using PizzaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaApp.Filters
{
    public class PizzaResultFilterAttribute: ResultFilterAttribute
    {
        public async override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;

            if(result?.Value==null ||
                result.StatusCode<200 ||
                result.StatusCode>=300)
            {
                await next();
                return;
            }

            var mapper = context.HttpContext.RequestServices.GetRequiredService<IMapper>();
            result.Value = mapper.Map<PizzaDto>(result.Value);

            // return base.OnResultExecutionAsync(context, next);
            await next();
            
        }
    }
}
