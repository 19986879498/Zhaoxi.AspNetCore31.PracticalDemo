﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zhaoxi.AspNetCore31.PracticalDemo.Models;
using Zhaoxi.AspNetCore31.PracticalDemo.Utility.Filter;

namespace Zhaoxi.AspNetCore31.PracticalDemo.Controllers
{
    [ServiceFilter(typeof(TestCustomExceptionFilter))]//当出现异常时处理的aop
    //[TypeFilter(typeof(CustomExceptionFilterAttribute))]
    [CustomFilterFactory(typeof(TestCustomExceptionFilter))] 
    [TestCustomActionFilter]
    [TestCustomIResourceFilter]
    [TestCustomResultFilter]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            throw new Exception("1234566");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
