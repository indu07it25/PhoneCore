using System;
using PhoneCore.Framework.Navigation;

namespace PhoneCore.Framework.Diagnostic.Tracing
{
    /// <summary>
    /// Default implementation of DefaultTraceRecord
    /// </summary>
    public sealed class TraceRecord
    {
        public ITraceCategory Category { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Page { get; set; }
        public Exception Exception { get; set; }
        public Type SourceType { get; set; }

        public TraceRecord()
        {
            Date = DateTime.MinValue;
        }

    }

}
