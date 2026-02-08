using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Attributes
{
    public class CacheAttribute(int DurationInSec=90): ActionFilterAttribute  
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //1.Create Key 
            string CacheKey=CreateCacheKey(context.HttpContext.Request);
            //2.check in cache if found  [Y3ni m3mlo cache ll data de 2bl kda wala la]
            ICacheService cacheService= context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cacheValue = await cacheService.GetAsync(CacheKey);
            //3.Return value if is not null[return data from cache mn el redis y3ni]
            if (cacheValue is not null)
            {
                context.Result = new ContentResult()
                {
                    Content = cacheValue,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                // ana 3iza aw2fo l7d hena law 5las el data m3molha cache w mkmlsh el action wala ad5l 3la el endpoint
                return;
            }
            //4.invoke .Next [invoke the action]
            var executedContext = await next.Invoke(); // hena b3ml invoke ll action bta3y
            //6.Set value with cache key [ba3d ma ageeb el data mn el database b3mlha cache fel redis]
            if (executedContext.Result is OkObjectResult result) 
            {
                await cacheService.SetAsync(CacheKey, result.Value, TimeSpan.FromSeconds(DurationInSec)); // hena b3ml set ll data ely agebtaha mn el database fel redis w ba3mlha cache 
            }
        }

        private string CreateCacheKey(HttpRequest request)
        {
            //.......{{BaseUrl}}/api/Products?BrandId=3 msln de el data ely 3iza a3mlha cache key
           StringBuilder key = new StringBuilder();
            // awal haga hmsk el path bta3 el request[/api/Products?] 3lshan b3deh ashof el query params ely b3deh ely b3mlha cache 3la 7sbha
            key.Append(request.Path + '?');
            foreach (var Item in request.Query.OrderBy(Q=> Q.Key)) // hena b3ml order 3la el query params 3lshan lw el user b3at el params b tartb mo5talef msh y3ml cache key mo5talef
            {
                key.Append($"{Item.Key}={Item.Key}&"); // hena b3ml append ll key w el value bta3t l kol param
            }
            return key.ToString();
        }
    }
}
