using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhaoxi.AspNetCore31.PracticalDemo.Utility.Filter
{
    /// <summary>
    /// AuthorizeFilter或者ActionFilterAttribute来实现登录权限验证和授权
    /// </summary>
    public class TestCustomAuthorizeFilter : AuthorizeFilter, IAllowAnonymousFilter
    {
        public override Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //解析url
            // {/ Home / Index}
            var url = context.HttpContext.Request.Path.Value;
            Console.WriteLine($"解析的url地址是{url}");
            return base.OnAuthorizationAsync(context);
        }
    }
}
