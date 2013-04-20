using System;
using PhoneCore.Framework.IoC.LifetimeManagers;

namespace PhoneCore.Framework.IoC
{
    /// <summary>
    /// Defines the extension methods for Component class
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// Defines singleton lifetime manager for component
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static Component Singleton(this Component component)
        {
            component.LifetimeManager = Activator.CreateInstance(typeof(SingletonLifetimeManager)) as ILifetimeManager;
            return component;
        }

        /// <summary>
        /// Defines Transient lifetime manager for component
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static Component Transient(this Component component)
        {
            component.LifetimeManager = Activator.CreateInstance(typeof(TransientLifetimeManager)) as ILifetimeManager;
            return component;
        }

        /// <summary>
        /// Uses custom lifetimeManager
        /// </summary>
        /// <param name="component"></param>
        /// <param name="lifetimeManager"></param>
        /// <returns></returns>
        public static Component CustomLifetime(this Component component, ILifetimeManager lifetimeManager)
        {
            component.LifetimeManager = lifetimeManager;
            return component;
        }

        /// <summary>
        /// Defines singleton lifetime manager for component
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static Component External(this Component component)
        {
            component.LifetimeManager = Activator.CreateInstance(typeof(ExternalLifetimeManager)) as ILifetimeManager;
            return component;
        }
    }
}