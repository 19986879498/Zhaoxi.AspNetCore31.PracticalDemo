using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhaoxi.AspNetCore31.PracticalDemo.Utility.Filter
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<CustomExceptionFilterAttribute> _logger;
        public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
        {
            this._logger = logger;
        }
        
        /// <summary>
        /// 当异常发生时  会进来
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                Console.WriteLine($"{context.HttpContext.Request.Path} {context.Exception.Message}");
                //希望写文本日志 
                this._logger.LogError($"{context.HttpContext.Request.Path} {context.Exception.Message}");
                context.Result = new JsonResult(new
                {
                    Result = false,
                    Msg = "发生异常，请联系管理员"
                });
                context.ExceptionHandled = true;
            }
        }
    }
}
