using System;
using System.Collections.Generic;

namespace PhoneCore.Framework.Diagnostic.Tracing
{
    /// <summary>
    /// Default implementation of ITraceCategory
    /// </summary>
    public class TraceCategory : ITraceCategory
    {
        public string Name { get; set; }
        public TraceCategory(string name)
        {
            Name = name;
        }
    }
}
