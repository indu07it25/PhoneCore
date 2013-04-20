using System;
using System.Diagnostics;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.Diagnostic.Tracing;
using PhoneCore.Framework.IoC;

namespace PhoneCore.Framework.Bootstrap
{
    /// <summary>
    /// Represents an application bootsrtapper. It is Component Root
    /// </summary>
    public class ApplicationBootstrapper
    {
        private readonly ITrace _trace;
        private readonly TraceCategoryManager _categoryManager;
        protected IContainer Container { get; private set; }
        protected IConfigSection Config { get; private set; }

        public ApplicationBootstrapper()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            //initialize tracing subsystem
            _categoryManager = new TraceCategoryManager();
            _trace = TraceFactory.GetTrace();
            var category = TraceFactory.GetTraceCategory("bootstrapper");
            try
            {
                _trace.Warn(category, "start init");
                Config = ConfigSettings.Instance.GetSection("system");
                var containerConfig = Config.GetSection("container");
                Container = Activator.CreateInstance(containerConfig.GetType("@type"), containerConfig) as IContainer;
                watch.Stop();
                long elapsedMsBoot = watch.ElapsedMilliseconds;
                watch.Start();
                _trace.Info(category, "configure");
                Configure();
                _trace.Info(category, "run");
                Run();
                watch.Stop();
                _trace.Warn(category, String.Format("end init. system:{0}, plugins:{1} ms", elapsedMsBoot, watch.ElapsedMilliseconds));
            }
            catch (Exception ex)
            {
                _trace.Fatal(category, "Unable to bootstrap", ex);
                throw;
            }
        }

        public IContainer GetContainer()
        {
            return Container;
        }

        /// <summary>
        /// Configure underlying built-in services
        /// </summary>
        protected void Configure()
        {
            Container
                .RegisterInstance<ITrace>(_trace)
                .RegisterType<ITraceCategory>(_categoryManager)
                .RegisterType(typeof(IBootstrapperService), Config.GetType("bootstrapping/@service"), Config.GetSection("bootstrapping"));
        }

        /// <summary>
        /// Run bootstrapper service
        /// </summary>
        protected void Run()
        {
            Container.Resolve<IBootstrapperService>().Run();
        }
    }
}
