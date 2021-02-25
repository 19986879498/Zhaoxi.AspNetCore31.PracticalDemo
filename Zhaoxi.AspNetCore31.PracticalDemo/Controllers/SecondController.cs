using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zhaoxi.AspNetCore31.PracticalDemo.Models;

namespace Zhaoxi.AspNetCore31.PracticalDemo.Controllers
{
    public class SecondController : Controller
    {
        private readonly ILogger<SecondController> _logger;
        public SecondController(ILogger<SecondController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            this._logger.LogWarning("12345667789");

            return View();
        }
    }
}