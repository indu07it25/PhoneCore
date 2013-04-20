using System;
using System.Collections.Generic;
using System.Linq;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.IoC;
using PhoneCore.Framework.IoC.Interception;
using PhoneCore.Framework.IoC.Interception.Proxies;
using PhoneCore.Framework.IoC.LifetimeManagers;
using PhoneCore.Framework.Views;

namespace PhoneCore.Framework.Navigation
{
    /// <summary>
    /// Provides the way to intercept view models
    /// </summary>
    public class PageMapping : IPageMapping
    {
        [Dependency]
        public IContainer Container { get; set; }

        private readonly Dictionary<string, Uri> _pageMapping = new Dictionary<string, Uri>();
        private readonly Dictionary<string, IProxy> _pageProxyMapping = new Dictionary<string, IProxy>();
        private readonly object _lockObj = new object();
        private readonly IConfigSection _config;

        #region constructors

        public PageMapping(IConfigSection config)
        {
            _config = config;
        }

        #endregion

        private bool _isInitialized;

        /// <summary>
        /// Initialized page mapping
        /// </summary>
        private void Initialize()
        {
            var pages = _config.GetSections("pages/page");
            foreach (var configPage in pages)
            {
                var name = configPage.GetString("@name");
                //var page = resolver.Resolve(name);
                //TODO support other types of uri
                var uri = new Uri(configPage.GetString("uri/@address"), UriKind.Relative);
                var vmSection = configPage.GetSection("viewModel");
                lock (_lockObj)
                {
                    Container.RegisterType(typeof (IViewModel), vmSection.GetType("@type"), name,
                                           new SingletonLifetimeManager());
                    _pageMapping.Add(name, uri);
                }
            }
            _isInitialized = true;
        }

        #region ILifetimeManager

        public Type InterfaceType { get; set; }
        public Type TargetType { get; set; }
        public object[] CstorArgs { get; set; }

        /// <summary>
        /// Returns view model by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IViewModel GetViewModel(string name)
        {
            if (!_isInitialized)
                Initialize();
            return Container.Resolve<IViewModel>(name);
        }

        public void Dispose()
        {
            _pageProxyMapping.Clear();
        }

        #endregion

        /// <summary>
        /// Returns uri by page
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Uri GetUri(string name)
        {
            if (!_isInitialized)
                Initialize();
            return _pageMapping[name];
        }

        /// <summary>
        /// Returns page by uri
        /// </summary>
        /// <param name="pageUri"></param>
        /// <returns></returns>
        public string GetPage(Uri pageUri)
        {
            return _pageMapping.Single(p => p.Value == pageUri).Key;
        }
    }
}
