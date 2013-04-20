using System;

namespace PhoneCore.Framework.Diagnostic.Tracing
{
    /// <summary>
    /// Represents a trace category which helps to group trace records
    /// </summary>
    public interface ITraceCategory
    {
        /// <summary>
        /// Name of trace category
        /// </summary>
        string Name { get; }

    }
}
