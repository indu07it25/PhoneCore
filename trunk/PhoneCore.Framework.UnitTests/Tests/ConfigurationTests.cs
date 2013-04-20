using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.UnitTests.Stubs;

namespace PhoneCore.Framework.UnitTests.Tests
{
    [TestClass]
    public class ConfigurationTests
    {

        [TestMethod]
        public void CanCreateCofig()
        {
            var config = ConfigSettings.GetMergedElement(ConfigSettings.AppConfigFileName, "");
            Assert.IsNotNull(config);
        }

        [TestMethod]
        public void CanMergeNodes()
        {
            var mergeConfig = ConfigSettings.Instance.GetSection("tests/configuration/merge");
            Assert.AreEqual("1", mergeConfig.GetString("mergeNode/@attr1"));
            Assert.AreEqual("2", mergeConfig.GetString("mergeNode/@attr2"));
            Assert.AreEqual("MergeNodeValueHere", mergeConfig.GetString("mergeNode"));
        }

        [TestMethod]
        public void CanReadSection()
        {
            Assert.IsNotNull(ConfigSettings.Instance.GetSection("tests/configuration/sections"));
        }

        [TestMethod]
        public void CanReadSections()
        {
            var sections = ConfigSettings.Instance.GetSections("tests/configuration/sections/section");
            Assert.IsNotNull(sections);
            Assert.AreEqual(2, sections.Count());
        }

        [TestMethod]
        public void CanReadString()
        {
            //act&assert
            Assert.AreEqual("text", ConfigSettings.Instance
                .GetSection("tests/configuration")
                .GetString("string"));
            Assert.AreEqual("attribute", ConfigSettings.Instance
                .GetSection("tests/configuration/string")
                .GetString("@attr"));
        }

        [TestMethod]
        public void CanReadStringAttribute()
        {
            //act&assert
            Assert.AreEqual("text", ConfigSettings.Instance.GetSection("tests").GetString("configuration/string"));
            Assert.AreEqual("attribute", ConfigSettings.Instance.GetSection("tests").GetString("configuration/string/@attr"));
        }

        [TestMethod]
        public void CanReadInt()
        {
            //act&assert
            Assert.AreEqual(1, ConfigSettings.Instance
                .GetSection("tests/configuration")
                .GetInt("int"));
            Assert.AreEqual(2, ConfigSettings.Instance
                .GetSection("tests/configuration/int")
                .GetInt("@attr"));
        }

        [TestMethod]
        public void CanReadType()
        {
               //act&assert
            Assert.AreEqual(typeof(TestType), ConfigSettings.Instance
                .GetSection("tests/configuration")
                .GetType("type"));
            Assert.AreEqual(typeof(TestType), ConfigSettings.Instance
                .GetSection("tests/configuration/type")
                .GetType("@attr"));
        
        }

        [TestMethod]
        public void CanReadInstance()
        {
            //act&assert
            Assert.IsNotNull(ConfigSettings.Instance.GetSection("tests/configuration")
                .GetInstance<TestType>("type"));
            Assert.IsNotNull(ConfigSettings.Instance.GetSection("tests/configuration/type")
                .GetInstance<TestType>("@attr"));
        }
    }
}
