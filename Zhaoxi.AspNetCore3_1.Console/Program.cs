using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Zhaoxi.AspNetCore3_1.Interface;
using Zhaoxi.AspNetCore3_1.Service;

namespace Zhaoxi.AspNetCore3_1.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IServiceCollection services = new ServiceCollection();
                services.AddTransient<ITestServiceA, TestServiceA>();//瞬时生命周期
                services.AddSingleton<ITestServiceB, TestServiceB>();//单例--进程唯一实例
                services.AddScoped<ITestServiceC, TestServiceC>();//作用域单例--一个请求一个实例
                services.AddTransient<ITestServiceE, TestServiceE>();
                services.AddTransient<ITestServiceD, TestServiceD>();
                var container = services.BuildServiceProvider();
                {
                    System.Console.WriteLine("****************1************");
                    var A1 = container.GetService<ITestServiceA>();
                    var A2 = container.GetService<ITestServiceA>();
                    System.Console.WriteLine(A1.Equals(A2));
                }
                {
                    System.Console.WriteLine("****************2************");
                    var B1 = container.GetService<ITestServiceB>();
                    var B2 = container.GetService<ITestServiceB>();
                    System.Console.WriteLine(B1.Equals(B2));
                }
                {
                    System.Console.WriteLine("****************3************");
                    var C1 = container.GetService<ITestServiceC>();
                    var C2 = container.GetService<ITestServiceC>();
                    System.Console.WriteLine(C1.Equals(C2));
                }
                {
                    System.Console.WriteLine("****************3************");
                    var C1 = container.GetService<ITestServiceC>();
                    var C2 = container.GetService<ITestServiceC>();
                    var C3 = container.CreateScope().ServiceProvider.GetService<ITestServiceC>();
                    var C4 = container.CreateScope().ServiceProvider.GetService<ITestServiceC>();
                    //所谓作用域，就是CreateScope，只要是以一个Scope 就是相同的

                    ITestServiceC C5 = null;
                    ITestServiceC C6 = null;
                    ITestServiceC C7 = null;
                    Task.Run(() =>
                    { C5 = container.GetService<ITestServiceC>(); });//线程--线程没有影响！
                    Task.Run(() =>
                    { C6 = container.GetService<ITestServiceC>(); }).ContinueWith(t =>
                    {
                        C7 = container.GetService<ITestServiceC>();
                    });//c5-c6-c7线程 其实没关系
                    System.Console.WriteLine(C1.Equals(C2)); //T 
                    System.Console.WriteLine(C1.Equals(C3)); //F
                    System.Console.WriteLine(C1.Equals(C4)); //F
                    System.Console.WriteLine(C3.Equals(C4)); //F
                    System.Console.WriteLine(C3.Equals(C5)); //F
                    System.Console.WriteLine(C3.Equals(C6)); //F
                    System.Console.WriteLine(C3.Equals(C7)); //F
                    System.Console.WriteLine(C5.Equals(C6)); //T
                    System.Console.WriteLine(C5.Equals(C7)); //T
                    System.Console.WriteLine(C6.Equals(C7)); //T
                    //10个  T /F
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            //Console.WriteLine("Hello World!");
        }
    }
}
