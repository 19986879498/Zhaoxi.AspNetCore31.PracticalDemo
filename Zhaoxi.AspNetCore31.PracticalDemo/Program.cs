using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Zhaoxi.AspNetCore31.PracticalDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);
            var host = hostBuilder.Build();
            host.Run();//准备一个web服务器然后运行起来
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return
                Host.CreateDefaultBuilder(args)//创建默认builder-会完成各种配置
                 .ConfigureLogging((context, loggingBuilder) =>
                 {
                     loggingBuilder.AddFilter("System", LogLevel.Warning);//过滤掉命名空间
                     loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
                     loggingBuilder.AddLog4Net();//使用log4net
                 })//扩展日志
                 .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                 .ConfigureWebHostDefaults(//指定一个web服务器--Kestrel
                    webBuilder =>
                    {
                        //   webBuilder.UseKestrel(o =>
                        //   {
                        //       o.Listen(IPAddress.Loopback, 12344);
                        //   })
                        //.Configure(app => app.Run(async context => await context.Response.WriteAsync("Hello   World0205")))
                        //.UseIIS()//iis可用
                        //.UseIISIntegration();

                        webBuilder.UseStartup<Startup>();//就是跟MVC流程串起来
                    });
        }
    }
}
