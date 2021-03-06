﻿using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;

namespace Core.Utilities.Interceptors
{
    partial class Class1
    {
        public class AspectInterceptorSelector : IInterceptorSelector
        {
            public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
            {
                var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>
                    (true).ToList();
                var methodAttributes = type.GetMethod(method.Name)
                    .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
                classAttributes.AddRange(methodAttributes);
                //classAttributes.Add(new ExceptionLogAspect(typeof(FileLogger)));
                //59--> Bütün methodları loglar.Tek tek loglamaya gerek kalmaz.

                return classAttributes.OrderBy(x => x.Priority).ToArray();
            }
        }
    }
}
