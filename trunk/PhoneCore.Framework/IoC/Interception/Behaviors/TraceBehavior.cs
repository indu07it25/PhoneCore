using System;
using System.Linq;
using System.Text;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.Diagnostic.Tracing;

namespace PhoneCore.Framework.IoC.Interception.Behaviors
{
    /// <summary>
    /// Writes method invocation info into trace
    /// </summary>
    public class TraceBehavior: IBehavior
    {
        private static readonly ITrace __trace = TraceFactory.GetTrace();
        private static readonly ITraceCategory __category = TraceFactory.GetTraceCategory("behavior.trace");

        private bool _showParam = true;
        private bool _showReturn = true;

        public TraceBehavior()
        {
            Name = "trace";
        }

        public virtual void Configure(IConfigSection config)
        {
            _showParam = config.GetBool("@showParam", true);
            _showReturn = config.GetBool("@showReturn", true);
        }

        public string Name { get; set; }

        public virtual IMethodReturn Invoke(MethodInvocation methodInvocation)
        {
            __trace.Info(__category, String.Format("{0}.{1}", methodInvocation.Target.GetType(), methodInvocation.MethodBase.Name));
            if(_showParam)
            {
                StringBuilder builder = new StringBuilder();
                methodInvocation.Parameters.Keys.ToList().ForEach(
                    key => builder.AppendFormat("{0}={1} ", key, methodInvocation.Parameters[key]));
                if(builder.Length > 0)
                    __trace.Info(__category, builder.ToString());
            }
            if (methodInvocation.IsInvoked && (_showReturn && methodInvocation.Return != null))
                 __trace.Info(__category, String.Format("return={0}", methodInvocation.Return));
          
            return methodInvocation.Return;
        }


    }
}
