using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneCore.Framework.Diagnostic.Tracing;
using PhoneCore.Framework.IoC;
using PhoneCore.Framework.IoC.LifetimeManagers;
using PhoneCore.Framework.UnitTests.Stubs.Container;

namespace PhoneCore.Framework.UnitTests.Tests
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void CanUseMapping()
        {
            using (IContainer container = TestHelper.GetContainer())
            {
                var classA = container.Resolve<IClassA>();
                Assert.IsNotNull(classA);
                Assert.AreEqual("Hello, Ilya", classA.SayHello("Ilya"));
                Assert.AreEqual(5, classA.Add(2, 3));
            }
        }

        [TestMethod]
        public void CanUseRegisterInstance()
        {
            //arrange
            using (IContainer container = new Container())
            {
                IClassA a = new ClassA();
                //act
                container.RegisterInstance<IClassA>(a);
                IClassA aFromContainer = container.Resolve<IClassA>();

                //assert
                Assert.AreSame(a, aFromContainer);
            }
        }

        [TestMethod]
        public void CanRegisterSingleton()
        {
            using (IContainer container = new Container())
            {
                container.RegisterType<IClassA, ClassA>(new SingletonLifetimeManager());
                Assert.AreSame(container.Resolve<IClassA>(), container.Resolve<IClassA>());
            }
        }

        [TestMethod]
        public void CanRegisterTransient()
        {
            using (IContainer container = new Container())
            {
                container.RegisterType<IClassA, ClassA>(new TransientLifetimeManager());
                Assert.AreNotSame(container.Resolve<IClassA>(), container.Resolve<IClassA>());
            }
        }

        [TestMethod]
        public void CanUseRegisterTypeWithName()
        {
            using (IContainer container = new Container())
            {
                container.RegisterType<IClassA, ClassA>("instance1",new TransientLifetimeManager());
                ClassA instance = new ClassA();
                container.RegisterInstance<ClassA>(instance, "instance2");

                Assert.IsNotNull(container.Resolve("instance1"));
                Assert.AreNotSame(container.Resolve("instance1"), container.Resolve("instance2"));
                Assert.AreSame(instance, container.Resolve("instance2"));
            }

            using (IContainer container = new Container())
            {
                //arrange
                var trace = TraceFactory.GetTrace();
                var stub = TraceFactory.GetTrace("stub");
                Assert.AreNotSame(trace, stub);

                //act
                container.RegisterInstance<ITrace>(stub, "stub");
                container.RegisterInstance<ITrace>(trace);

                //assert
                Assert.AreSame(trace, container.Resolve<ITrace>());
                Assert.AreSame(stub, container.Resolve<ITrace>("stub"));
            }
        }

        [TestMethod]
        public void CanRegisterSingletonThroughComponent()
        {
            using(IContainer container = new Container())
            {
                container.Register(Component.For<IClassC>().Use<ClassC>().Singleton());
                var classC = container.Resolve<IClassC>();
                var classC1 = container.Resolve<IClassC>();

                var result = classC.GenerateResult("1");
                Assert.AreEqual("result1", result);
                Assert.AreSame(classC, classC1);
            }
        }

        [TestMethod]
        public void CanRegisterTransientThroughComponent()
        {
            using (IContainer container = new Container())
            {
                container.Register(Component.For<IClassC>().Use<ClassC>().Transient());
                var classC = container.Resolve<IClassC>();
                var classC1 = container.Resolve<IClassC>();

                var result = classC.GenerateResult("1");
                Assert.AreEqual("result1", result);
                Assert.AreNotSame(classC, classC1);
            }
        }

        [TestMethod]
        public void CanUseResolveAll()
        {
            using (IContainer container = new Container())
            {
                container.RegisterInstance<IClassA>(new ClassA(), "inst1");
                container.RegisterInstance<IClassA>(new ClassA(), "inst2");
                Assert.AreEqual(2, container.ResolveAll<IClassA>().Count());
            }
        }

        [TestMethod]
        public void CanUseProgrammaticConstructionDependency()
        {
            using (IContainer container = new Container())
            {
                container.RegisterInstance<IClassA>(new ClassA(), "inst1");
                container.RegisterInstance<IClassB>(new ClassB(), "inst2");
                container.RegisterInstance<IClassC>(new ClassC(), "inst3");
                container.Register(
                    Component.For<IClassD>().Use<ClassD>(typeof (IClassA), typeof (IClassB), typeof (IClassC)));

                var instance = container.Resolve<IClassD>();
                instance.Hello("Ilya");
            }
        }

        [TestMethod]
        public void CanUseDeclarativeConstructionDependency()
        {
            using (IContainer container = new Container())
            {
                container.RegisterInstance<IClassA>(new ClassA(), "inst1");
                container.RegisterInstance<IClassB>(new ClassB(), "inst2");
                container.RegisterInstance<IClassC>(new ClassC(), "inst3");
                container.Register(Component.For<IClassE>().Use<ClassE>());

                var instance = container.Resolve<IClassE>();
                instance.Hello("Ilya");
            }
        }
    }
}
