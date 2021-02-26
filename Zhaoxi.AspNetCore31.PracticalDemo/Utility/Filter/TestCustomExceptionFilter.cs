using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhaoxi.AspNetCore31.PracticalDemo.Utility.Filter
{
    public class TestCustomExceptionFilter : ExceptionFilterAttribute, IFilterMetadata
    {
        public override void OnException(ExceptionContext context)
        {
            if (!context.ExceptionHandled)
            {
                Console.WriteLine($"{context.HttpContext.Request.Path} {context.Exception.Message}");
                //希望写文本日志 
               // this._logger.LogError($"{context.HttpContext.Request.Path} {context.Exception.Message}");
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
