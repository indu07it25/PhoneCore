using System;
using PhoneCore.Framework.Views;

namespace PhoneCore.Framework.Navigation
{
    /// <summary>
    /// Defines behavior of page mapping engine
    /// </summary>
    public interface IPageMapping
    {
        /// <summary>
        /// get uri by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Uri GetUri(string name);

        /// <summary>
        /// Get view model by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IViewModel GetViewModel(string name);

        /// <summary>
        /// get page name by uri
        /// </summary>
        /// <param name="pageUri"></param>
        /// <returns></returns>
        string GetPage(Uri pageUri);
    }
}