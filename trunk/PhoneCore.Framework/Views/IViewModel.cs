using System;
using System.Collections.Generic;

namespace PhoneCore.Framework.Views
{
    /// <summary>
    /// Defines a behavior of page's ViewModel
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// Navigation parameters mapping
        /// </summary>
        Dictionary<string, object> NavigationParameters { get; set; }

        /// <summary>
        /// Save state to
        /// </summary>
        /// <param name="state"></param>
        void SaveStateTo(IDictionary<string, object> state);

        /// <summary>
        /// Load state from
        /// </summary>
        /// <param name="state"></param>
        void LoadStateFrom(IDictionary<string, object> state);
    }
}
