using System;
using System.Collections.Generic;
using System.Linq;
using PhoneCore.Framework.IoC.Interception.Proxies;

namespace PhoneCore.Framework.IoC.Interception
{
    /// <summary>
    /// Represents interception context which provide the way create proxy objects and interact with interceptors
    /// </summary>
    internal static class InterceptionContext
    {
        static readonly List<IInterceptor> __interceptors = new List<IInterceptor>();
        static InterceptionContext()
        {
            __interceptors.Add(new InterfaceInterceptor());
            __interceptors.Add(new ConfigInterfaceInterceptor());
        }

        /// <summary>
        /// Creates proxy from interface type and instance
        /// </summary>
        /// <param name="type">interface type</param>
        /// <param name="instance">instance of implementation type</param>
        /// <returns></returns>
        public static IProxy CreateProxy(Type type, object instance)
        {
            var interceptor = GetInterceptor(type);
            return interceptor != null ? interceptor.CreateProxy(type, instance) : null;
        }

        /// <summary>
        /// Gets first interceptor which can intercept type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IInterceptor GetInterceptor(Type type)
        {
            return __interceptors.SingleOrDefault(i => i.CanIntercept(type));
        }

        /// <summary>
        /// Get component for type from interceptor
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Component GetComponent(Type type)
        {
            var interceptor = GetInterceptor(type);
            return interceptor != null ? interceptor.Resolve(type) : null;
        }

        /// <summary>
        /// Get default interceptor
        /// </summary>
        /// <returns></returns>
        public static IInterceptor GetInterceptor()
        {
            return __interceptors.First();
        }
    }
}
