using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhaoxi.AspNetCore31.PracticalDemo.Utility.Filter
{
    public class CustomActionFilterAttribute : Attribute, IActionFilter, IFilterMetadata, IOrderedFilter
    {
        public int Order { get; set; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"This is {typeof(CustomActionFilterAttribute)} OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This is {typeof(CustomActionFilterAttribute)} OnActionExecuting");
        }
    }

    public class CustomControllerFilterAttribute : Attribute, IActionFilter, IFilterMetadata
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"This is {typeof(CustomControllerFilterAttribute)} OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This is {typeof(CustomControllerFilterAttribute)} OnActionExecuting");
        }
    }

    public class CustomGlobalFilterAttribute : Attribute, IActionFilter, IFilterMetadata
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"This is {typeof(CustomGlobalFilterAttribute)} OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This is {typeof(CustomGlobalFilterAttribute)} OnActionExecuting");
        }
    }
}
