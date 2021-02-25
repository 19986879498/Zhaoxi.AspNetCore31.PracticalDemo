using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Zhaoxi.AspNetCore3_1.Interface;
using Zhaoxi.AspNetCore3_1.Service;
using Zhaoxi.AspNetCore31.PracticalDemo.Utility.Filter;
using Zhaoxi.AspNetCore31.PracticalDemo.Utility.Middleware;

namespace Zhaoxi.AspNetCore31.PracticalDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();//该怎么用session
            services.AddControllersWithViews(
                options =>
                {
                    //options.Filters.Add(typeof(CustomExceptionFilterAttribute));//全局注册 全局生效
                    options.Filters.Add(typeof(CustomGlobalFilterAttribute));
                });

            services.AddTransient<CustomExceptionFilterAttribute>();

            services.AddTransient<ITestServiceA, TestServiceA>();
            services.AddTransient<ITestServiceB, TestServiceB>();
            services.AddTransient<ITestServiceC, TestServiceC>();
            //services.AddTransient<ITestServiceD, TestServiceD>();
            ////services.AddTransient<ITestServiceE, TestServiceE>();
            //services.AddScoped<ITestServiceE, TestServiceE>();
            //单例很好--不建议太多单例---仅适合于那些需要单例(配置文件/链接池)--而且摒弃传统单例
            //大部分其实都是瞬时的生命周期
            //请求单例--一次Http请求就是同一个实例---这说明一次请求会创建一个容器实例---可以发挥想象力---比如事务-数据库连接---主要适用于一次请求只需要一个对象不同请求需要不同对象
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterModule<CustomAutofacModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //一个步骤或者多个步骤
            //RequestDelegate

            #region middleware
            //app.Use(next =>
            //{
            //    Console.WriteLine("Middleware 1 out");
            //    return new RequestDelegate(
            //       async context =>
            //        {
            //            await context.Response.WriteAsync("This is Middleware 1 start");
            //            await next.Invoke(context);
            //            await context.Response.WriteAsync("This is Middleware 1 end");
            //        });
            //});
            //app.Use(next =>
            //{
            //    Console.WriteLine("Middleware 2 out");
            //    return new RequestDelegate(
            //       async context =>
            //       {
            //           await context.Response.WriteAsync("This is Middleware 2 start");
            //           await next.Invoke(context);
            //           await context.Response.WriteAsync("This is Middleware 2 end");
            //       });
            //});
            //app.Use(
            //    next =>
            //    {
            //        Console.WriteLine("Middleware 3 out");
            //        return new RequestDelegate(
            //           async context =>
            //           {
            //               await context.Response.WriteAsync("This is Middleware 3 start");
            //               //next.Invoke(context);//404
            //               await context.Response.WriteAsync("This is Middleware 3 end");
            //           });
            //    }
            //);
            //1 是一张白纸，没有任何内置的东西，要什么写什么，pay-for-what-you-use
            //2 很容易扩展---任意环节任意顺序都可以扩展

            //第一阶段是初始化，调用Build---得到RequestDelegate-middleware1(包含了next引用)--这就是管道
            //然后等着请求来---Context--一层层调用，然后就俄罗斯套娃(revserse顺序舒服点)

            //写法不清楚加下小助教，有专门讲过await async +委托
            #endregion

            #region 中间件源码
            //public RequestDelegate Build()
            //{
            //    RequestDelegate app = context =>
            //    {
            //        // If we reach the end of the pipeline, but we have an endpoint, then something unexpected has happened.
            //        // This could happen if user code sets an endpoint, but they forgot to add the UseEndpoint middleware.
            //        var endpoint = context.GetEndpoint();
            //        var endpointRequestDelegate = endpoint?.RequestDelegate;
            //        if (endpointRequestDelegate != null)
            //        {
            //            var message =
            //                $"The request reached the end of the pipeline without executing the endpoint: '{endpoint.DisplayName}'. " +
            //                $"Please register the EndpointMiddleware using '{nameof(IApplicationBuilder)}.UseEndpoints(...)' if using " +
            //                $"routing.";
            //            throw new InvalidOperationException(message);
            //        }

            //        context.Response.StatusCode = 404;
            //        return Task.CompletedTask;
            //    };

            //    //_components是一个middleware中间件的集合  1 2 3 4 5
            //    //Reverse倒序  5 4 3 2 1 
            //    //component是个委托 Func<RequestDelegate, RequestDelegate>
            //    foreach (var component in _components.Reverse())
            //    {
            //        app = component.Invoke(app);
            //    }

            //    return app;
            //}
            #endregion

            #region 标准中间件用法
            //app.Use(next =>
            //{
            //    return async c =>
            //    {
            //        await c.Response.WriteAsync("Hello World!");
            //    };
            //});//标准中间件用法
            #endregion

            #region 权限认证
            //app.Use(next =>
            //{
            //    return async c =>
            //    {
            //        if (c.Request.Query.ContainsKey("Name"))
            //        {
            //            await next.Invoke(c);
            //        }
            //        else
            //        {
            //            await c.Response.WriteAsync("No Authorization");
            //        }
            //    };
            //});
            #endregion

            #region 防盗链
            //app.UseMiddleware<RefuseStealingMiddleWare>();
            #endregion

            app.UseSession();//表示请求需要使用session
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot"))
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class CustomAutofacModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder containerBuilder)
        {
            var assembly = this.GetType().GetTypeInfo().Assembly;
            var builder = new ContainerBuilder();
            var manager = new ApplicationPartManager();
            manager.ApplicationParts.Add(new AssemblyPart(assembly));
            manager.FeatureProviders.Add(new ControllerFeatureProvider());
            var feature = new ControllerFeature();
            manager.PopulateFeature(feature);
            builder.RegisterType<ApplicationPartManager>().AsSelf().SingleInstance();
            builder.RegisterTypes(feature.Controllers.Select(ti => ti.AsType()).ToArray()).PropertiesAutowired();
            //containerBuilder.RegisterType<FirstController>().PropertiesAutowired();

            containerBuilder.Register(c => new CustomAutofacAop());//aop注册
            //containerBuilder.RegisterType<TestServiceA>().As<ITestServiceA>().SingleInstance().PropertiesAutowired();
            //containerBuilder.RegisterType<TestServiceC>().As<ITestServiceC>();
            //containerBuilder.RegisterType<TestServiceB>().As<ITestServiceB>();
            containerBuilder.RegisterType<TestServiceD>().As<ITestServiceD>();
            containerBuilder.RegisterType<TestServiceE>().As<ITestServiceE>();

            containerBuilder.RegisterType<A>().As<IA>().EnableInterfaceInterceptors();

            //containerBuilder.Register<FirstController>();

            //containerBuilder.RegisterType<JDDbContext>().As<DbContext>();
            //containerBuilder.RegisterType<CategoryService>().As<ICategoryService>();

            //containerBuilder.RegisterType<UserServiceTest>().As<IUserServiceTest>();
        }
    }

    public class CustomAutofacAop : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"invocation.Methond={invocation.Method}");
            Console.WriteLine($"invocation.Arguments={string.Join(",", invocation.Arguments)}");

            invocation.Proceed(); //继续执行

            Console.WriteLine($"方法{invocation.Method}执行完成了");
        }
    }
    public interface IA
    {
        void Show(int id, string name);
    }
    /// <summary>
    /// autofac是5.0
    /// </summary>
    [Intercept(typeof(CustomAutofacAop))]
    public class A : IA
    {
        public void Show(int id, string name)
        {
            Console.WriteLine($"This is A {id} _{name}");
        }
    }
}
