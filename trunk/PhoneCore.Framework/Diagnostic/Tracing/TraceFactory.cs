using System;
using System.Collections.Generic;
using System.Windows;
using PhoneCore.Framework.Configuration;

namespace PhoneCore.Framework.Diagnostic.Tracing
{
    /// <summary>
    /// Tracer factory
    /// </summary>
    public static class TraceFactory
    {
        public const string Default = "default";
        private static readonly Dictionary<string, ITrace> _traces = new Dictionary<string, ITrace>();
        private static readonly object _lockInstance = new object();
        private static readonly TraceCategoryManager _categoryFactory = new TraceCategoryManager();
        public static bool IsInitialized { get; private set; }

        static void Initialize()
        {
            try
            {
                //get traces
                var traceConfigs = ConfigSettings.Instance.GetSections("system/diagnostic/traces/trace");
                foreach (var traceConfig in traceConfigs)
                {
                    string name = traceConfig.GetString("@name");
                    ITrace trace = traceConfig.GetInstance<ITrace>("@type", new object[]{traceConfig});
                    _traces.Add(name, trace);
                }
                IsInitialized = true;
            }
            catch(Exception ex)
            {
                throw new InvalidOperationException("Fatal error: unable to register trace subsystem", ex);
            }
        }

        static void Ensure()
        {
            if (!IsInitialized)
                lock (_lockInstance)
                {
                    if (!IsInitialized)
                        Initialize();
                }
        }

        /// <summary>
        /// Gets default tracer
        /// </summary>
        /// <returns></returns>
        public static ITrace GetTrace()
        {
            Ensure();
            return GetTrace(Default);
        }

        /// <summary>
        /// Gets tracer associated with the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ITrace GetTrace(string name)
        {
            Ensure();
            return _traces[name];
        }

        public static ITraceCategory GetTraceCategory(string name)
        {
            return _categoryFactory.GetInstance(name);
        }
    }
}
