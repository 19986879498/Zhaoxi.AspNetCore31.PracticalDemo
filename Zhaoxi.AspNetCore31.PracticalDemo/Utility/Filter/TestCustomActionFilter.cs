using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhaoxi.AspNetCore31.PracticalDemo.Utility.Filter
{
    /// <summary>
    /// 看方法名称后缀大概就明白一个是Action执行之前，一个是Action方法执行之后调用的
    /// </summary>
    public class TestCustomActionFilter : Attribute, IActionFilter,IFilterMetadata
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"我是CustomActionFilter的OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"我是CustomActionFilter的OnActionExecuting");
        }
    }
}
