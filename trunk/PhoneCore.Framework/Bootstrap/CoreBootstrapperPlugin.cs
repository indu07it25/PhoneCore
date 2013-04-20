using System;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.Diagnostic.Tracing;
using PhoneCore.Framework.IoC;
using PhoneCore.Framework.Navigation;
using PhoneCore.Framework.Storage;

namespace PhoneCore.Framework.Bootstrap
{
    /// <summary>
    /// Registers core services, should be executed first
    /// </summary>
    public class CoreBootstrapperPlugin: BootstrapperPlugin
    {
        public CoreBootstrapperPlugin(IConfigSection config) : base(config) { }

        /// <summary>
        /// Registers core services
        /// </summary>
        /// <returns></returns>
        public override bool Run()
        {
            try
            {
                Trace.Info(Category, "get core services configuration");
                var pageMappingSection = Config.GetSection("services/pageMapping");
                var navigationSection = Config.GetSection("services/navigation");
                var fileSection = Config.GetSection("services/fileSystem");
                var settingSection = Config.GetSection("services/settings");

                Trace.Info(Category, "register core services");
                Container
                    .RegisterType(typeof(IPageMapping), pageMappingSection.GetType("@type"), pageMappingSection)
                    .RegisterType(typeof(INavigationService), navigationSection.GetType("@type"), navigationSection)
                    .RegisterType(typeof(IFileSystemService), fileSection.GetType("@type"), fileSection)
                    .RegisterType(typeof(ISettingService), settingSection.GetType("@type"), settingSection);
                
                return true;
            }
            catch (Exception ex)
            {
                Trace.Fatal(Category, "registration failed", ex);
                throw;
            }
        }

        /// <summary>
        /// Updates
        /// </summary>
        /// <returns></returns>
        public override bool Update()
        {
            //not updatable
            Trace.Info(Category, "update");
            return false;
        }
    }
}
