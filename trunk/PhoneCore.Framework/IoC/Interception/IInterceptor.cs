using System;
using PhoneCore.Framework.IoC.Interception.Proxies;

namespace PhoneCore.Framework.IoC.Interception
{
    /// <summary>
    /// Defines behavior of method interception
    /// </summary>
    public interface IInterceptor
    {
        /// <summary>
        /// True, if type can be intercepted
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool CanIntercept(Type type);

        /// <summary>
        /// Return proxy for the type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IProxy CreateProxy(Type type, object instance);

        /// <summary>
        /// Registers component
        /// </summary>
        /// <param name="type"></param>
        /// <param name="component"></param>
        void Register(Type type, Component component);

        /// <summary>
        /// Resolves component
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Component Resolve(Type type);

    }
}
