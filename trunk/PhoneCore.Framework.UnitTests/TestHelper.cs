using System;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.Diagnostic.Tracing;
using PhoneCore.Framework.IoC;

namespace PhoneCore.Framework.UnitTests
{
    public static class TestHelper
    {
        public static IContainer GetContainer()
        {
            var configContainer = ConfigSettings.Instance.GetSection("system/container");
            //ITraceFactory traceFactory = new TraceFactory();
            Container container =  new Container(configContainer);
            container
                //.RegisterInstance<ITraceFactory>(traceFactory)
                .RegisterInstance<ITrace>(TraceFactory.GetTrace())
                .RegisterType<ITraceCategory>(new TraceCategoryManager());
            return container;
        }
    }
}
