using Zhaoxi.AspNetCore3_1.Interface;
using System;

namespace Zhaoxi.AspNetCore3_1.Service
{
    public class TestServiceE : ITestServiceE
    {
        public TestServiceE(ITestServiceC serviceC)
        {
            Console.WriteLine($"{this.GetType().Name}被构造。。。");
        }

        public void Show()
        {
            Console.WriteLine("E123456");
        }
    }
}
