using System;
using System.Collections.Generic;
using System.Windows.Navigation;

namespace PhoneCore.Framework.Navigation
{
    /// <summary>
    /// Represents navigation service
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navigating event
        /// </summary>
        event NavigatingCancelEventHandler Navigating;
        
        /// <summary>
        /// Navigates to uri
        /// </summary>
        /// <param name="pageUri"></param>
        void NavigateTo(Uri pageUri);

        /// <summary>
        /// Navigates to uri and passes parameters
        /// </summary>
        /// <param name="pageUri"></param>
        /// <param name="parameters"></param>
        void NavigateTo(Uri pageUri, Dictionary<string, object> parameters);

        /// <summary>
        /// Navigates to page specified
        /// </summary>
        /// <param name="name"></param>
        void NavigateTo(string name);

        /// <summary>
        /// Navigates to page with parameters
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        void NavigateTo(string name, Dictionary<string, object> parameters);

        /// <summary>
        /// Goes back
        /// </summary>
        void GoBack();
    }
}
