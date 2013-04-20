using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.Diagnostic.Tracing;
using PhoneCore.Framework.IoC;
using PhoneCore.Framework.Views;

namespace PhoneCore.Framework.Navigation
{
    /// <summary>
    /// Default implementation of navigation service
    /// </summary>
    public class NavigationService : INavigationService
    {
        [Dependency]
        public IContainer Container { get; set; }
        [Dependency]
        public ITrace Trace { get; set; }
        [Dependency("Navigation.Navigate")]
        public ITraceCategory Category { get; set; }
        [Dependency]
        public IPageMapping PageMapping { get; set; }

        private PhoneApplicationFrame _mainFrame;
       
        protected IConfigSection Config { get; private set; }

        public NavigationService(IConfigSection config)
        {
            Config = config;
        }


        public event NavigatingCancelEventHandler Navigating;

        /// <summary>
        /// Navigates to the specific Uri
        /// </summary>
        /// <param name="pageUri"></param>
        public void NavigateTo(Uri pageUri)
        {
            if (EnsureMainFrame())
            {
                Trace.Info(Category, String.Format("Navigate to {0}", pageUri));
                _mainFrame.Navigate(pageUri);
            }
        }

        /// <summary>
        /// Navigates to the specific Uri with parameters
        /// </summary>
        /// <param name="pageUri"></param>
        /// <param name="parameters"></param>
        public void NavigateTo(Uri pageUri, Dictionary<string, object> parameters)
        {
            string page = PageMapping.GetPage(pageUri);
            NavigateTo(page, parameters);
        }

        /// <summary>
        /// Navigates to the specific page
        /// </summary>
        /// <param name="name"></param>
        public void NavigateTo(string name)
        {

            Uri pageUri = PageMapping.GetUri(name);
            NavigateTo(pageUri);
        }

        /// <summary>
        /// Navigates to the specific page with parameters
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        public void NavigateTo(string name, Dictionary<string, object> parameters)
        {
            IViewModel vm = PageMapping.GetViewModel(name);
            vm.NavigationParameters = parameters;
            NavigateTo(name);
        }

        public void GoBack()
        {
            if (EnsureMainFrame() && _mainFrame.CanGoBack)
            {
                Trace.Info(Category, String.Format("Go back from {0}", _mainFrame.CurrentSource));
                _mainFrame.GoBack();
            }
        }

        private bool EnsureMainFrame()
        {
            if (_mainFrame != null)
            {
                return true;
            }

            _mainFrame = Application.Current.RootVisual as PhoneApplicationFrame;

            if (_mainFrame != null)
            {
                // Could be null if the app runs inside a design tool
                _mainFrame.Navigating += (s, e) =>
                {
                    if (Navigating != null)
                    {
                        Navigating(s, e);
                    }
                };

                return true;
            }

            return false;
        }


    }
}
