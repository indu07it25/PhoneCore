using System;

namespace PhoneCore.Framework.IoC.Interception
{
    /// <summary>
    /// Represents a result of method invocation
    /// </summary>
    public interface IMethodReturn
    {
        /// <summary>
        /// Returns return value of method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetReturnValue<T>();

        /// <summary>
        /// Exception which occured during method invocation
        /// </summary>
        Exception Exception { get; }
    }
}
