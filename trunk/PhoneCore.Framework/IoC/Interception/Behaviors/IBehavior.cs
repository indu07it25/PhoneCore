using System;
using PhoneCore.Framework.Configuration;

namespace PhoneCore.Framework.IoC.Interception.Behaviors
{
    /// <summary>
    /// Represents an additional behavior of method invocation
    /// </summary>
    public interface IBehavior: IConfigurable
    {
        /// <summary>
        /// The name of behavior
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Provides the way to attach additional behavior to method
        /// </summary>
        /// <param name="methodInvocation"></param>
        /// <returns></returns>
        IMethodReturn Invoke(MethodInvocation methodInvocation);
    }
}
