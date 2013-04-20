using System;

namespace PhoneCore.Framework.Bootstrap
{
    /// <summary>
    /// Represents a startup plugin
    /// </summary>
    public interface IBootstrapperPlugin
    {
        /// <summary>
        /// The name of plugin
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Run plugin
        /// </summary>
        /// <returns></returns>
        bool Run();

        /// <summary>
        /// Update plugin
        /// </summary>
        /// <returns></returns>
        bool Update();
    }
}
