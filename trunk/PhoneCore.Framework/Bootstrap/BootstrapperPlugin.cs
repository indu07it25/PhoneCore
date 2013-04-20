using System;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.Diagnostic.Tracing;
using PhoneCore.Framework.IoC;

namespace PhoneCore.Framework.Bootstrap
{
    /// <summary>
    /// Represents a bootstrapper plugin
    /// </summary>
    public abstract class BootstrapperPlugin: IBootstrapperPlugin
    {
        [Dependency]
        public IContainer Container { get; set; }

        [Dependency]
        public ITrace Trace { get; set; }

        [Dependency("Bootstrapping.Plugin")]
        public ITraceCategory Category { get; set; }

        protected IConfigSection Config { get; private set; }


        public BootstrapperPlugin(IConfigSection config)
        {
            Config = config;
        }

        #region IBootstrapperPlugin members

        /// <summary>
        /// Returns the name of plugin
        /// </summary>
        public string Name
        {
            get { return Config.GetString("@name"); }
        }

        public abstract bool Run();
        public abstract bool Update();
        #endregion
    }
}
