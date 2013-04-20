using System;
using System.Collections.Generic;
using System.Linq;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.Diagnostic.Tracing;
using PhoneCore.Framework.IoC;
using PhoneCore.Framework.IoC.LifetimeManagers;

namespace PhoneCore.Framework.Bootstrap
{
    /// <summary>
    /// Provides default functionality to execute startup plugins
    /// </summary>
    public class BootstrapperService: IBootstrapperService
    {
        private readonly IConfigSection _config;
        
        [Dependency]
        public IContainer Container { get; set; }
        
        [Dependency]
        public ITrace Trace { get; set; }

        [Dependency("Bootstrapping.Service")]
        public ITraceCategory Category { get; set; }


        public BootstrapperService(IConfigSection config)
        {
            _config = config;
        }

        public BootstrapperService(IBootstrapperPlugin[] plugins)
        {
            Trace.Info(Category, String.Format("register plugins: {0}", plugins.Count()));
            //_plugins = plugins;
        }

        public bool IsInitialized { get; private set; }

        private void Initialze()
        {
            Trace.Info(Category, "get bootstrappers from configuration");
            var bootstrappers = _config.GetSections("bootstrappers/bootstrapper");
            int pluginCount = 0;
            foreach (var bootsrapperSection in bootstrappers)
            {
                var name = bootsrapperSection.GetString("@name");
                var type = bootsrapperSection.GetType("@type");
                Trace.Info(Category, String.Format("register '{0}' of type '{1}'", name, type));
                Container.RegisterType(typeof(IBootstrapperPlugin), type, name, 
                    new SingletonLifetimeManager(), bootsrapperSection);
                pluginCount++;
            }
            IsInitialized = true;
            Trace.Info(Category, String.Format("register plugins: {0}", pluginCount));
        }

        #region IBootstrapperService members

        /// <summary>
        /// Run all registred bootstrappers
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            Trace.Info(Category, "run bootstrapper");
            if(!IsInitialized)
                Initialze();
            return Container.ResolveAll<IBootstrapperPlugin>()
                .ToList().Aggregate(true, (current, task) => current & task.Run());
        }

        /// <summary>
        /// Updates all registred bootstrappers
        /// </summary>
        /// <returns></returns>
        public bool Update()
        {
            return Container.ResolveAll<IBootstrapperPlugin>()
                .ToList().Aggregate(true, (current, task) => current & task.Update());
        }

        /// <summary>
        /// Returns the plugin registered in bootstrapper by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBootstrapperPlugin GetPlugin(string name)
        {
            return Container.Resolve<IBootstrapperPlugin>(name);
        }

        /// <summary>
        /// Returns the plugin registered in bootstrapper by type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public IBootstrapperPlugin GetPlugin(Type t)
        {
            return Container.ResolveAll<IBootstrapperPlugin>().Single(plugin => plugin.GetType() == t);
        }

        /// <summary>
        ///  Returns the plugin registered in bootstrapper by type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IBootstrapperPlugin GetPlugin<T>()
        {
            return GetPlugin(typeof(T));
        }

        #endregion
    }
}
