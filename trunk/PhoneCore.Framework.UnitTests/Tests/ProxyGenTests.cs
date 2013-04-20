using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.IoC;
using PhoneCore.Framework.Storage;

namespace PhoneCore.Framework.UnitTests.Tests
{
    [TestClass]
    public class ProxyGenTests
    {
        [TestMethod]
        public void CanUseGenericMethods()
        {
            using(IContainer container = TestHelper.GetContainer())
            {
                IConfigSection config = new ConfigSection(null);
                container.RegisterType<ISettingService, IsolatedStorageSettingService>(config);
                var settingService  = container.Resolve<ISettingService>();
                string key = "my key";
                string value = "my value";
                settingService.Save(key, value);
                Assert.AreSame(value, settingService.Load<string>(key));
            }
        }
    }
}
