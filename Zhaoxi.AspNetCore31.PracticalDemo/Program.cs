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
            host.Run();//׼��һ��web������Ȼ����������
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return
                Host.CreateDefaultBuilder(args)//����Ĭ��builder-����ɸ�������
                 .ConfigureLogging((context, loggingBuilder) =>
                 {
                     loggingBuilder.AddFilter("System", LogLevel.Warning);//���˵������ռ�
                     loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);
                     loggingBuilder.AddLog4Net();//ʹ��log4net
                 })//��չ��־
                 .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                 .ConfigureWebHostDefaults(//ָ��һ��web������--Kestrel
                    webBuilder =>
                    {
                        //   webBuilder.UseKestrel(o =>
                        //   {
                        //       o.Listen(IPAddress.Loopback, 12344);
                        //   })
                        //.Configure(app => app.Run(async context => await context.Response.WriteAsync("Hello   World0205")))
                        //.UseIIS()//iis����
                        //.UseIISIntegration();

                        webBuilder.UseStartup<Startup>();//���Ǹ�MVC���̴�����
                    });
        }
    }
}
