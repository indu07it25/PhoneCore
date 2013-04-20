using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.IoC;
using PhoneCore.Framework.Navigation;
using PhoneCore.Framework.UnitTests.Stubs;
using PhoneCore.Framework.Views;

namespace PhoneCore.Framework.UnitTests.Tests
{
    [TestClass]
    public class PageMappingTest
    {

        [TestMethod]
        public void CanGetPage()
        {
            using (IContainer container = new Container())
            {
                var config = ConfigSettings.Instance.GetSection("system/bootstrapping/bootstrappers/bootstrapper/services/pageMapping");
                IPageMapping mapping = new PageMapping(config);
                (mapping as PageMapping).Container = container;
                var uri = mapping.GetUri("TestPage");
                Assert.AreEqual(new Uri("/TestUri.xaml", UriKind.Relative), uri);
            }
        }

        
        [TestMethod]
        public void CanGetViewModel()
        {
            using (IContainer container = new Container())
            {
                var config = ConfigSettings.Instance.GetSection("system/bootstrapping/bootstrappers/bootstrapper/services/pageMapping");
                IPageMapping mapping = new PageMapping(config);
                (mapping as PageMapping).Container = container;
                var viewModel = mapping.GetViewModel("TestPage");
                Assert.IsNotNull(viewModel);
                Assert.IsInstanceOfType(viewModel, typeof(TestViewModel));
            }
        }
    }
}
