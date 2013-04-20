using System;
using System.Collections.Generic;
using System.Linq;
using PhoneCore.Framework.Configuration;

namespace PhoneCore.Framework.IoC.Interception.Behaviors
{
    /// <summary>
    /// Executes method
    /// </summary>
    public class ExecuteBehavior: IBehavior
    {
        public ExecuteBehavior()
        {
            Name = "execute";
        }
        public virtual void Configure(IConfigSection config)
        {

        }

        public string Name { get; set; }

        /// <summary>
        /// Executes method
        /// </summary>
        /// <param name="methodInvocation"></param>
        /// <returns></returns>
        public virtual IMethodReturn Invoke(MethodInvocation methodInvocation)
        {
            var methodBase = TypeHelper.GetMethodBySign(methodInvocation.Target.GetType(), 
                methodInvocation.MethodBase, methodInvocation.GenericTypes.ToArray());
            var result = methodBase.Invoke(methodInvocation.Target, methodInvocation.Parameters.Values.ToArray());
            methodInvocation.IsInvoked = true;
            return methodInvocation.Return = new MethodReturn(result);
        }

    }
}
