using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zhaoxi.AspNetCore31.PracticalDemo.Utility.Filter
{
    public class CustomFilterFactoryAttribute : Attribute, IFilterFactory
    {
        private Type _FilterType = null;
        public CustomFilterFactoryAttribute(Type type)
        {
            this._FilterType = type;
        }
        public bool IsReusable => true;

        /// <summary>
        /// 容器的实例--构造对象---就可以依赖注入---
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns>为了标识，是一个filter</returns>
        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return (IFilterMetadata)serviceProvider.GetService(this._FilterType);

            //throw new NotImplementedException();
        }
    }
}
