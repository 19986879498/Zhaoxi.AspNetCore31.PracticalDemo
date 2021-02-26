using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Zhaoxi.AspNetCore3_1.Interface;
using Zhaoxi.AspNetCore31.PracticalDemo.Utility.Filter;

namespace Zhaoxi.AspNetCore31.PracticalDemo.Controllers
{
    /// <summary>
    /// 下面准备不写try-catch  不修改方法  但是也完成异常处理
    /// 你加了特性呀  修改了方法
    /// 
    /// </summary>
    //[CustomExceptionFilterAttribute()]//控制器注册

    [ServiceFilter(typeof(CustomExceptionFilterAttribute))]//当出现异常时处理的aop
    //[TypeFilter(typeof(CustomExceptionFilterAttribute))]
    [CustomFilterFactory(typeof(CustomExceptionFilterAttribute))]
    [CustomControllerFilter]
    public class FourthController : Controller
    {

        #region Identity
        private readonly ILogger<FourthController> _logger;
        private readonly ITestServiceA _iTestServiceA;
        private readonly ITestServiceB _iTestServiceB;
        private readonly ITestServiceC _iTestServiceC;
        private readonly ITestServiceD _iTestServiceD;
        private readonly ITestServiceE _iTestServiceE;
        private readonly IA _ia;
        public FourthController(ILogger<FourthController> logger
            , ITestServiceA testServiceA
             , ITestServiceB testServiceB
            , ITestServiceC testServiceC
            , ITestServiceD testServiceD
            , ITestServiceE testServiceE
            , IA a)
        {
            this._logger = logger;
            this._iTestServiceA = testServiceA;
            this._iTestServiceB = testServiceB;
            this._iTestServiceC = testServiceC;
            this._iTestServiceD = testServiceD;
            this._iTestServiceE = testServiceE;
            this._ia = a;
        }
        #endregion

        //[CustomExceptionFilterAttribute]//action注册
        public IActionResult Index()
        {
            this._logger.LogInformation("12345648658686");
            //try catch
            int i = 1;
            int k = 3;
            int m = i + k;//业务逻辑
            int l = m - m;
            int j = m / l;//其实是要来个异常

            return View();
        }

        public IActionResult Info()
        {
            //try catch
            int i = 1;
            int k = 3;
            int m = i + k;//业务逻辑
            int l = m - m;
            int j = m / l;//其实是要来个异常

            return View();
        }

        [CustomActionFilterAttribute]
        //[CustomActionFilterAttribute(Order =123)]
        public IActionResult Infomation()
        {
            Console.WriteLine("This is Infomation Action");
            this._ia.Show(123, "简简单单");
            return View();
        }
    }
}