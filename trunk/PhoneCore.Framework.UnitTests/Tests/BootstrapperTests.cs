using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneCore.Framework.Bootstrap;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.Diagnostic.Tracing;
using PhoneCore.Framework.IoC;
using PhoneCore.Framework.Navigation;
using PhoneCore.Framework.Storage;

namespace PhoneCore.Framework.UnitTests.Tests
{
    [TestClass]
    public class BootstrapperTests
    {
        [TestMethod]
        public void CanCreateApplicationBootstrapper()
        {
            //expects no exceptions
            ApplicationBootstrapper  bootstrapper = new ApplicationBootstrapper();
        }

        [TestMethod]
        public void CanRunCoreBootstrapper()
        {
            var configPlugin = ConfigSettings.Instance.GetSection("system/bootstrapping/bootstrappers/bootstrapper");
            IContainer container = TestHelper.GetContainer();
            CoreBootstrapperPlugin plugin = new CoreBootstrapperPlugin(configPlugin);
            plugin.Container = container;
            plugin.Trace = container.Resolve<ITrace>();
            plugin.Category = container.Resolve<ITraceCategory>("test");
            plugin.Run();
        }

        [TestMethod]
        public void CanRegisterCoreServices()
        {
            var configPlugin = ConfigSettings.Instance.GetSection("system/bootstrapping/bootstrappers/bootstrapper");
            IContainer container = TestHelper.GetContainer();
            CoreBootstrapperPlugin plugin = new CoreBootstrapperPlugin(configPlugin);
            plugin.Container = container;
            plugin.Trace = container.Resolve<ITrace>();
            plugin.Category = container.Resolve<ITraceCategory>("test");
            plugin.Run();

            var settingService = container.Resolve<ISettingService>();
            Assert.IsNotNull(settingService);

            var fileService = container.Resolve<IFileSystemService>();
            Assert.IsNotNull(fileService);

            var navigationService = container.Resolve<INavigationService>();
            Assert.IsNotNull(navigationService);

        }
    }
}
