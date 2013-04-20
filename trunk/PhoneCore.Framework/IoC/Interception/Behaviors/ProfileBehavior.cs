using System;
using System.Diagnostics;
using PhoneCore.Framework.Diagnostic.Tracing;

namespace PhoneCore.Framework.IoC.Interception.Behaviors
{
    /// <summary>
    /// Executes and measures execution time
    /// </summary>
    public class ProfileBehavior: ExecuteBehavior
    {
        private static readonly ITrace __trace = TraceFactory.GetTrace();
        private static readonly ITraceCategory __category = TraceFactory.GetTraceCategory("behavior.profile");

        public ProfileBehavior()
        {
            Name = "profile";
        }

        public override IMethodReturn Invoke(MethodInvocation methodInvocation)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var result = base.Invoke(methodInvocation);
            watch.Stop();
            __trace.Info(__category, string.Format("{0}.{1} execution time: {2} ms", 
                methodInvocation.Target.GetType(), methodInvocation.MethodBase.Name, watch.ElapsedMilliseconds));
            return result;
        }
    }
}
