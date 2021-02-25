using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Zhaoxi.AspNetCore3_1.Interface;
using Zhaoxi.AspNetCore3_1.Service;

namespace Zhaoxi.AspNetCore31.PracticalDemo.Controllers
{
    /// <summary>
    /// How
    /// Asp.NetCore内置的IOC：
    /// 控制反转--就是把对象的依赖换成对抽象的依赖,
    /// 只依赖于ILogger，程序运行时肯定是个细节具体实例，细节交给第三方决定
    /// 
    /// Why 用IOC---全程IOC
    /// 1 可以去掉对细节的依赖，方便扩展---减小影响范围，甚至转移到配置文件依赖，只需要改配置文件
    /// 假如没有IOC--需要E--先构造C--先构造B--先构造A--上端需要知道全部的细节
    /// 2 可以做到屏蔽细节，对象依赖注入(DI)
    /// 
    /// Asp.NetCore咋弄的IOC
    /// ServiceCollection；内置在Asp.NetCore的一个全新的容器
    /// 
    /// 1 ServiceCollection功能有一定的局限
    /// 2 是新的技术栈
    /// 50%以上会替换上第三方容器
    /// (Eleven准备手写IOC容器专题--最后会把容器替换到asp.net Core)
    /// 
    /// how    autofac
    /// </summary>
    public class ThirdController : Controller
    {
        private readonly ILogger<ThirdController> _logger;
        private readonly ITestServiceA _iTestServiceA;
        private readonly ITestServiceB _iTestServiceB;
        private readonly ITestServiceC _iTestServiceC;
        private readonly ITestServiceD _iTestServiceD;
        private readonly ITestServiceE _iTestServiceE;
        private readonly IA _ia;
        public ThirdController(ILogger<ThirdController> logger
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
        public IActionResult Index()
        {
            //ITestServiceA testServiceA = new TestServiceAV2();
            //别的地方用的都得改
            this._iTestServiceA.Show();
            this._iTestServiceB.Show();
            this._iTestServiceC.Show();
            this._iTestServiceD.Show();
            this._iTestServiceE.Show();

            this._ia.Show(123,"JC");

            //假如没有IOC


            this._logger.LogWarning("This is ThirdController Index");
            return View();
        }
    }
}