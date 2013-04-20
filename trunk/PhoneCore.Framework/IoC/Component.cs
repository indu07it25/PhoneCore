﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.IoC.Interception;
using PhoneCore.Framework.IoC.Interception.Behaviors;
using PhoneCore.Framework.IoC.Interception.Proxies;
using PhoneCore.Framework.IoC.LifetimeManagers;

namespace PhoneCore.Framework.IoC
{
    /// <summary>
    /// Provides the way to 
    /// </summary>
    public sealed class Component
    {
        #region Internal properties used by Container
        internal Type InterfaceType { get; private set; }
        internal Type TargetType { get; private set; }
        internal ILifetimeManager LifetimeManager { get; set; }
        internal object[] Args { get; private set; }
        internal ConstructorInfo Constructor { get; private set; }
        internal string Name { get; private set; }
        internal List<IBehavior> Behaviors
        {
            get { return _behaviors; }
        }
        #endregion

        #region Private fields
        private Type _proxyType;
        private readonly IConfigSection _config;
        private readonly List<IBehavior> _behaviors = new List<IBehavior>();
        #endregion

        internal Component(Type interfaceType, Type proxyType, IConfigSection config)
        {
            InterfaceType = interfaceType;
            _proxyType = proxyType;
            _config = config;
        }

        /// <summary>
        /// true, if we proxy type is defined
        /// </summary>
        internal bool CanCreateProxy
        {
            get { return _proxyType != null; }
        }

        /// <summary>
        /// Creates proxy using component settings and default behaviors provided
        /// </summary>
        /// <param name="instance">wrapped instance</param>
        /// <param name="behaviors">default behaviors</param>
        /// <returns></returns>
        internal IProxy CreateProxy(object instance, IList<IBehavior> behaviors)
        {
            var proxy = Activator.CreateInstance(_proxyType) as IProxy;
            proxy.Instance = instance;
           
            //TODO distinc the same behaviors
            if (_config != null)
            {
                if (!_config.GetSection("behaviors/clear").IsEmpty)
                    proxy.ClearBehaviors();
                else
                {
                    //set default behaviors
                    foreach (var behavior in behaviors)
                        proxy.AddBehavior(behavior);
                }

                //specified in configuration for this type
                foreach (var behaviorConfig in _config.GetSections("behaviors/behavior"))
                {
                    var behavior = behaviorConfig.GetInstance<IBehavior>("@type");
                    behavior.Name = behaviorConfig.GetString("@name");
                    behavior.Configure(behaviorConfig);
                    proxy.AddBehavior(behavior);
                }
            }
            _behaviors.ForEach(proxy.AddBehavior);
            return proxy;
        }

        #region Public instance methods in fluent and regular style

        /// <summary>
        /// Adds behavior
        /// </summary>
        /// <param name="behavior"></param>
        /// <returns></returns>
        public Component AddBehavior(IBehavior behavior)
        {
            if(!Behaviors.Any(b => b.Name == behavior.Name))
                Behaviors.Add(behavior);
            return this;
        }

        /// <summary>
        /// Links component to usage of implementation by T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        public Component Use<T>(params object[] args)
        {

            return Use(typeof (T), args);
        }

        /// <summary>
        /// Links component to usage of implementation by t
        /// </summary>
        /// <param name="t"></param>
        /// <param name="args">constructor args</param>
        /// <returns></returns>
        public Component Use(Type t, params object[] args)
        {
            Guard.IsAssignableFrom(InterfaceType, t);
            Guard.IsNull(Constructor, "Constructor", "Multiply Use call forbidden");
            TargetType = t;
            Args = args;
            return this;
        }

        /// <summary>
        /// Links component to usage of implementation by t
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args">types of constructor args to resolve</param>
        /// <returns></returns>
        public Component Use<T>(params Type[] args)
        {
            return Use(typeof(T), args);
        }

        /// <summary>
        /// Links component to usage of implementation by t
        /// </summary>
        /// <param name="t"></param>
        /// <param name="args">types of constructor args to resolve</param>
        /// <returns></returns>
        public Component Use(Type t, params  Type[] args)
        {
            Guard.IsAssignableFrom(InterfaceType, t);
            Guard.IsNull(Args, "Args", "Multiply Use call forbidden");
            TargetType = t;
            Constructor = TypeHelper.GetConstructor(t, args);
            return this;
        }

        /// <summary>
        /// Empty args registration
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Component Use<T>()
        {
            return Use(typeof(T), new object[] { });
        }

        /// <summary>
        /// Stores component using name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Component Named(string name)
        {
            Name = name;
            return this;
        }

        /// <summary>
        /// Links component to usage of T proxy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Component Proxy<T>() where T:IProxy
        {
            return Proxy(typeof (T));
        }

        /// <summary>
        /// Links component to usage of T proxy
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Component Proxy(Type t)
        {
            //checking of type t: it should implement IProxy
            Guard.IsAssignableFrom(typeof (IProxy), t);
            _proxyType = t;
            //if proxy called and component isn't registered for interception, try to register it in default interceptor
            if (InterceptionContext.GetInterceptor(InterfaceType) == null)
                InterceptionContext.GetInterceptor().Register(InterfaceType, this);
            return this;
        }

        #endregion

        /// <summary>
        /// Entry point for component definition
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Component For<T>()
        {
            return For(typeof (T));
        }

        /// <summary>
        /// Entry point for component definition
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Component For(Type t)
        {
            //should be initialized manually and afterwards being validated 
            var component = InterceptionContext.GetComponent(t);
            if (component != null)
                return component;
            component = new Component(t, null, null);
            return component;
        }
    }
}
