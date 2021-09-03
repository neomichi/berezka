using System;
using Berezka.Data.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Berezka.WebApp.Code
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomCheckingTokenAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {       





        public CustomCheckingTokenAuthorizeAttribute()
        {
          
        }         

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorized = true;

            var headers = context.HttpContext.Request.Headers;
                             
            

            if (headers.ContainsKey("Authorization") &&
                !string.IsNullOrWhiteSpace(headers["Authorization"])
                && headers["Authorization"].ToString().Trim().StartsWith("Bearer "))
            {
                var token = headers["Authorization"].ToString().Substring("Bearer ".Length);
                
                var _tokenService = context.HttpContext.RequestServices.GetService<ITokenService>();
               
                var result = _tokenService.CheckingRefreshToken(token);

                
                if (result.ReturnType == Data.EnumType.TokenState.Refresh) isAuthorized = false;
                //WTF это не работает <-3
                //{  
                //    var text = _tokenService.GetToken(token.ToGuid());
                //    context.HttpContext.Response.Headers["newtoken"] = text.access_token;

                //    context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.Redirect);                    
                //}

                headers["TokenGuid"] = result.Value.ToString();                
            }

         

            if (!isAuthorized)
            {                
                context.Result = new StatusCodeResult((int)System.Net.HttpStatusCode.BadRequest);
                return;
            }
           
        }
    }

   
}