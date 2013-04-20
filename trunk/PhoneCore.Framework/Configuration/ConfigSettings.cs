using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace PhoneCore.Framework.Configuration
{
    /// <summary>
    /// Provides the access to the configuration subsystem
    /// </summary>
    public class ConfigSettings
    {
        public const string AppConfigFileName = "application.config";
        private ConfigElement _root;

        private ConfigSettings()
        {
            _root = GetMergedElement(AppConfigFileName, ""); 
        }

        /// <summary>
        /// Returns merged element for environment
        /// </summary>
        /// <param name="appConfig"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static ConfigElement GetMergedElement(string appConfig, string environment)
        {
            //TODO cache results
            var mergedConfig = MergeConfigs(appConfig, environment);
            return new ConfigElement(mergedConfig);
        }

        /// <summary>
        /// Merges configs defined in root config
        /// </summary>
        /// <param name="appConfigPath"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static XElement MergeConfigs(string appConfigPath, string environment)
        {
            //load application config
            var appDocument = XDocument.Load(appConfigPath);

            List<XElement> configs = new List<XElement>();
            //interate each config node and store it's root XElement
            foreach (ConfigElement configElement in 
                (new ConfigElement(appDocument.Root)).GetElements("configs/config"))
            {
                var document = GetDocument(configElement, environment);
                if(document != null)
                    configs.Add(document.Root);
            }
            
            //merge XElements
            XElement result = null;
            return configs.Aggregate(result, ConfigMerger.Merge);
        }

        /// <summary>
        /// Gets XDcument from element
        /// </summary>
        /// <param name="configElement"></param>
        /// <param name="environment"></param>
        /// <returns></returns>
        public static XDocument GetDocument(ConfigElement configElement, string environment)
        {
            ConfigSection section = new ConfigSection(configElement);
            var include = section.GetString("@include");
            var location = section.GetString("@location");
            var optional = section.GetBool("@optional");
            try
            {
                //TODO support different location
                if (location == "file")
                {
                    var document = XDocument.Load(include);
                    
                    //don't use environment feature
                    if (String.IsNullOrEmpty(environment))
                        return document;
                    //checks whether config is related to using environment
                    var envAttr = document.Root.Attribute("environment");
                    if ((envAttr != null) && (envAttr.Value == environment))
                        return document;
                }
            }
            catch
            {
                //optional config may not exist
                if(optional)
                    return null;
                throw;
            }
            return null;
        }


        #region singleton
        private static object _syncLock = new object();
        private volatile static ConfigSettings _instance;
        public static ConfigSettings Instance
        {
            get
            {
                if (_instance == null)
                    lock (_syncLock)
                        if (_instance == null)
                            _instance = new ConfigSettings();
                return _instance;
            }
        }
        #endregion

        /// <summary>
        /// Get section
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public IConfigSection GetSection(string xpath)
        {
            return (new ConfigSection(_root)).GetSection(xpath);
        }

        /// <summary>
        /// Get the set of sections
        /// </summary>
        /// <param name="xpath"></param>
        /// <returns></returns>
        public IEnumerable<IConfigSection> GetSections(string xpath)
        {
            return (new ConfigSection(_root)).GetSections(xpath);
        }

    }
}
