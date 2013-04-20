using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.IoC;
using PhoneCore.Framework.IoC.Interception;
using PhoneCore.Framework.IoC.Interception.Behaviors;
using PhoneCore.Framework.Navigation;
using PhoneCore.Framework.UnitTests.Stubs.Container;

namespace PhoneCore.Framework.UnitTests.Tests
{
    [TestClass]
    public class InterceptionTests
    {
        [TestMethod]
        public void CanComponentWithSingleton()
        {
            using(IContainer container = new Container())
            {
                container.Register(Component.For<IClassC>()
                                                .Use<ClassC>()
                                                .Proxy<ClassCProxy>()
                                                .AddBehavior(new ExecuteBehavior())
                                                .Singleton());

                var classC = container.Resolve<IClassC>();
                Assert.IsInstanceOfType(classC, typeof(ClassCProxy));

                var result = classC.GenerateResult("1");
                Assert.AreEqual("result1", result);

                var classC2 = container.Resolve<IClassC>();
                Assert.AreSame(classC, classC2);
            }
        }

        [TestMethod]
        public void CanInterceptComponentWithTransient()
        {
            using (IContainer container = new Container())
            {
                container.Register(Component.For<IClassC>()
                                                .Use<ClassC>()
                                                .Proxy<ClassCProxy>()
                                                .AddBehavior(new ExecuteBehavior())
                                                .Transient());

                var classC = container.Resolve<IClassC>();
                Assert.IsInstanceOfType(classC, typeof(ClassCProxy));
                var result = classC.GenerateResult("1");
                Assert.AreEqual("result1", result);
                var classC2 = container.Resolve<IClassC>();
                Assert.AreNotSame(classC, classC2);
            }
        }

        [TestMethod]
        public void CanInterceptWithConfiguration()
        {
            using (IContainer container = TestHelper.GetContainer())
            {
                container.RegisterType<IClassC, ClassC>();
                
                var classC = container.Resolve<IClassC>();
                Assert.IsInstanceOfType(classC, typeof(ClassCProxy));
                var result = classC.GenerateResult("1");
                Assert.AreEqual("result1", result);
            }
        }
    }
}
