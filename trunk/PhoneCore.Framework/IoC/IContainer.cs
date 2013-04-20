using System;
using System.Collections.Generic;
using PhoneCore.Framework.IoC.Interception;
using PhoneCore.Framework.IoC.LifetimeManagers;

namespace PhoneCore.Framework.IoC
{
    /// <summary>
    ///  Represents dependency injection container behavior
    ///  </summary>
    public interface IContainer: IDisposable
    {
        /// <summary>
        /// Resolves instance of type stored in container
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        object Resolve(Type type);

        /// <summary>
        /// Resolves instance of type stored in container
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        object Resolve(Type type, string name);

        /// <summary>
        /// Resolves instance of type stored in container
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object Resolve(string name);

        /// <summary>
        /// Resolves instance of type stored in container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();

        /// <summary>
        /// Resolves instance of type stored in container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        T Resolve<T>(string name);

        /// <summary>
        /// Returns all registered instances of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IEnumerable<T> ResolveAll<T>();

        /// <summary>
        /// Returns all registered instances of the type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IEnumerable<object> ResolveAll(Type type);

        /// <summary>
        /// Registers component
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        IContainer Register(Component component);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="t"></param>
        /// <param name="c"></param>
        IContainer RegisterType(Type t, Type c);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="t"></param>
        /// <param name="c"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IContainer RegisterType(Type t, Type c, string name);

        /// <summary>
        /// Registers type using type manager provided
        /// </summary>
        /// <param name="t"></param>
        /// <param name="c"></param>
        /// <param name="lifetimeManager"></param>
        /// <returns></returns>
        IContainer RegisterType(Type t, Type c, ILifetimeManager lifetimeManager);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="t"></param>
        /// <param name="c"></param>
        /// <param name="name"></param>
        /// <param name="lifetimeManager"></param>
        /// <returns></returns>
        IContainer RegisterType(Type t, Type c, string name, ILifetimeManager lifetimeManager);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="t"></param>
        /// <param name="c"></param>
        /// <param name="args"></param>
        IContainer RegisterType(Type t, Type c, params object[] args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="t"></param>
        /// <param name="c"></param>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        IContainer RegisterType(Type t, Type c, string  name, params object[] args);

        /// <summary>
        /// Registers type using type manager provided
        /// </summary>
        /// <param name="t"></param>
        /// <param name="c"></param>
        /// <param name="lifetimeManager"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        IContainer RegisterType(Type t, Type c, ILifetimeManager lifetimeManager, params object[] args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="t"></param>
        /// <param name="c"></param>
        /// <param name="name"></param>
        /// <param name="lifetimeManager"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        IContainer RegisterType(Type t, Type c, string name, ILifetimeManager lifetimeManager, params object[] args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        IContainer RegisterType<T, C>() where C : class, T;

        /// <summary>
        /// Registers type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        IContainer RegisterType<T, C>(string name) where C : class, T;

        /// <summary>
        /// Registers type using type manager provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="lifetimeManager"></param>
        /// <returns></returns>
        IContainer RegisterType<T, C>(ILifetimeManager lifetimeManager) where C : class, T;

        /// <summary>
        /// Registers type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="name"></param>
        /// <param name="lifetimeManager"></param>
        /// <returns></returns>
        IContainer RegisterType<T, C>(string name, ILifetimeManager lifetimeManager) where C : class, T;

        /// <summary>
        /// Registers type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="args"></param>
        IContainer RegisterType<T, C>(params object[] args) where C : class, T;

        /// <summary>
        /// Registers type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        IContainer RegisterType<T, C>(string name, params object[] args) where C : class, T;

        /// <summary>
        /// Registers type using type manager provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="lifetimeManager"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        IContainer RegisterType<T, C>(ILifetimeManager lifetimeManager, params object[] args) where C : class, T;

        /// <summary>
        /// Registers type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="name"></param>
        /// <param name="lifetimeManager"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        IContainer RegisterType<T, C>(string name, ILifetimeManager lifetimeManager, params object[] args) where C : class, T;

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="t"></param>
        IContainer RegisterType(Type t);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="t"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IContainer RegisterType(Type t, string name);

        /// <summary>
        /// Registers type using type manager provided
        /// </summary>
        /// <param name="t"></param>
        /// <param name="lifetimeManager"></param>
        /// <returns></returns>
        IContainer RegisterType(Type t, ILifetimeManager lifetimeManager);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="t"></param>
        /// <param name="name"></param>
        /// <param name="lifetimeManager"></param>
        /// <returns></returns>
        IContainer RegisterType(Type t, string name, ILifetimeManager lifetimeManager);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="t"></param>
        /// <param name="args"></param>
        IContainer RegisterType(Type t, params object[] args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="t"></param>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        IContainer RegisterType(Type t, string name, params object[] args);

        /// <summary>
        /// Registers type using type manager provided
        /// </summary>
        /// <param name="t"></param>
        /// <param name="lifetimeManager"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        IContainer RegisterType(Type t, ILifetimeManager lifetimeManager, params object[] args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="t"></param>
        /// <param name="name"></param>
        /// <param name="lifetimeManager"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        IContainer RegisterType(Type t, string name, ILifetimeManager lifetimeManager, params object[] args);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        IContainer RegisterType<T>() where T : class;

        /// <summary>
        /// Registers type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        IContainer RegisterType<T>(string name) where T : class;

        /// <summary>
        /// Registers type using type manager provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lifetimeManager"></param>
        /// <returns></returns>
        IContainer RegisterType<T>(ILifetimeManager lifetimeManager) where T : class;

        /// <summary>
        /// Registers type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="lifetimeManager"></param>
        /// <returns></returns>
        IContainer RegisterType<T>(string name, ILifetimeManager lifetimeManager) where T : class;

        /// <summary>
        /// Registers type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="args"></param>
        IContainer RegisterType<T>(params object[] args) where T : class;

        /// <summary>
        /// Registers type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        IContainer RegisterType<T>(string name, params object[] args) where T : class;

        /// <summary>
        /// Registers type using type manager provided
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lifetimeManager"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        IContainer RegisterType<T>(ILifetimeManager lifetimeManager, params object[] args) where T : class;

        /// <summary>
        /// Registers type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="lifetimeManager"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        IContainer RegisterType<T>(string name, ILifetimeManager lifetimeManager, params object[] args) where T : class;

        /// <summary>
        /// Registers existing instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <returns></returns>
        IContainer RegisterInstance<T>(T instance);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IContainer RegisterInstance<T>(T instance, string name);

        /// <summary>
        /// Registers existing instance
        /// </summary>
        /// <param name="t"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        IContainer RegisterInstance(Type t, object instance);

        /// <summary>
        /// Registers type
        /// </summary>
        /// <param name="t"></param>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        IContainer RegisterInstance(Type t, object instance, string name);

    }
}
