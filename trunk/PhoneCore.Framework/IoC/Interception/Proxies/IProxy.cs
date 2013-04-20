using System;
using System.Collections.Generic;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.IoC.Interception.Behaviors;

namespace PhoneCore.Framework.IoC.Interception.Proxies
{
    /// <summary>
    /// Represents a behavior of proxy
    /// </summary>
    public interface IProxy
    {
        object Instance { get; set; }

        /// <summary>
        /// Adds new behavior to wrapped instance
        /// </summary>
        /// <param name="behavior"></param>
        void AddBehavior(IBehavior behavior);

        /// <summary>
        /// Clear list of behaviors
        /// </summary>
        void ClearBehaviors();
    }
}
