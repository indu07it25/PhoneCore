using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PhoneCore.Framework.Configuration;

namespace PhoneCore.Framework.ProxyGen
{
    class Program
    {
        static void Main(string[] args)
        {
            //args[0] - path to application.config
            //args[1] - assemblies path
            //args[2] - output dir path
            //System.Diagnostics.Debugger.Launch();
            Environment.CurrentDirectory = args[0];
            var configElement = ConfigSettings.GetMergedElement(String.Format(@"{0}\application.config",args[0]), "");
            var config = new ConfigSection(configElement);

            foreach (var proxyConfig in config.GetSections("system/container/interception/components/component"))
            {
                var fullName = proxyConfig.GetString("@interface");
                var name = fullName.Split(',')[0];
                var assembly = fullName.Split(',')[1];
                var assemblyPath = String.Format(@"{0}\{1}.dll", args[1], assembly);
                if(!String.IsNullOrEmpty(proxyConfig.GetString("@name")))
                    InterfaceImplementer.Generate(assemblyPath, name, proxyConfig.GetString("@name"), args[2]);
            }
        }
    }
}
