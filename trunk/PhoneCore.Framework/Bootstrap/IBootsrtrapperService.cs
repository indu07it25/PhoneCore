using System;

namespace PhoneCore.Framework.Bootstrap
{
    /// <summary>
    /// Provides the functionality to manage startup plugins
    /// </summary>
    public interface IBootstrapperService
    {
        /// <summary>
        /// Runs startup plugins
        /// </summary>
        /// <returns></returns>
        bool Run();

        /// <summary>
        /// Updates startup plugins
        /// </summary>
        /// <returns></returns>
        bool Update();

        /// <summary>
        /// Gets plugin by its type
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        IBootstrapperPlugin GetPlugin(Type t);

        /// <summary>
        /// Gets plugin by its type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IBootstrapperPlugin GetPlugin<T>();
    }
}
