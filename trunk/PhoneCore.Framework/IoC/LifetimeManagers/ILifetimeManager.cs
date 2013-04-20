using System;
using System.Reflection;

namespace PhoneCore.Framework.IoC.LifetimeManagers
{
    /// <summary>
    /// Manages lifetime of object creation
    /// </summary>
    public interface ILifetimeManager: IDisposable
    {
        /// <summary>
        /// Interface type
        /// </summary>
        Type InterfaceType { get; set; }

        /// <summary>
        /// target type
        /// </summary>
        Type TargetType { get; set; }

        /// <summary>
        /// constructor signature
        /// </summary>
        ConstructorInfo Constructor { get; set; }

        /// <summary>
        /// constructor's parameters
        /// </summary>
        object[] CstorArgs { get; set; }

        /// <summary>
        /// returns instance of the target type
        /// </summary>
        /// <returns></returns>
        object GetInstance();

        /// <summary>
        /// returns instance of the target type using name provided
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object GetInstance(string name);
    }
}
