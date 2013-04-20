using System;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.IoC.Interception.Behaviors;

namespace PhoneCore.Framework.IoC.Interception
{
    /// <summary>
    /// Provides functionality to intercept methods using custom implementation of type
    /// </summary>
    public class ConfigInterfaceInterceptor : InterfaceInterceptor
    {

        public ConfigInterfaceInterceptor()
        {
            //read default behaviors
            foreach (var behaviorConfig in ConfigSettings.Instance.GetSections("system/container/interception/behaviors/behavior"))
            {
                var behavior = behaviorConfig.GetInstance<IBehavior>("@type");
                behavior.Name = behaviorConfig.GetString("@name");
                behavior.Configure(behaviorConfig);
                Behaviors.Add(behavior);
            }

            //read proxies
            foreach (var proxyConfig in ConfigSettings.Instance.GetSections("system/container/interception/components/component"))
            {
                //TODO improve approach!
                var type = proxyConfig.GetType("@interface");
                var proxy = proxyConfig.GetType("@proxy");
                ProxyComponentMapping.Add(type, new Component(type, proxy, proxyConfig));
            }
        }

    }
}
