using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneCore.Framework.Diagnostic.Tracing;
using PhoneCore.Framework.IoC;

namespace PhoneCore.Framework.UnitTests.Tests
{
    [TestClass]
    public class TraceTests
    {
        [TestMethod]
        public void CanGetDefaultTrace()
        {
            var trace = TraceFactory.GetTrace();
            Assert.IsNotNull(trace);
            Assert.AreEqual(typeof(DefaultTrace), trace.GetType());
        }

        [TestMethod]
        public void CanGetTraceCategory()
        {
            TraceCategoryManager manager = new TraceCategoryManager();
            var category = manager.GetInstance("test.category");
            Assert.IsNotNull(category);
            Assert.AreEqual("test.category", category.Name);
        }

        [TestMethod]
        public void CanGenerateStatistics()
        {
            using(IContainer container = TestHelper.GetContainer())
            {
                var trace = container.Resolve<ITrace>();
                var category = container.Resolve<ITraceCategory>("test.category");
                trace.Info(category,"testmessage");
                DefaultTraceController controller = new DefaultTraceController(trace as DefaultTrace, "Diagnostic/DefaultTraceReport.txt");
                var report = controller.GenerateReport();
                Assert.IsNotNull(report.ToString());
            }
        }
    }
}
