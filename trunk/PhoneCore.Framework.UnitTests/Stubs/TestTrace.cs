using System;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.Diagnostic.Tracing;

namespace PhoneCore.Framework.UnitTests.Stubs
{
    public class TestTrace: ITrace
    {
        public TestTrace(ConfigSection config)
        {
        }

        public int Level
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(ITraceCategory category, string message)
        {
            throw new NotImplementedException();
        }

        public void Info(TraceRecord record)
        {
            throw new NotImplementedException();
        }

        public void Warn(string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(ITraceCategory category, string message)
        {
            throw new NotImplementedException();
        }

        public void Warn(TraceRecord record)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Error(ITraceCategory category, string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Error(TraceRecord record)
        {
            throw new NotImplementedException();
        }

        public void Fatal(string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Fatal(ITraceCategory category, string message, Exception exception)
        {
            throw new NotImplementedException();
        }

        public void Fatal(TraceRecord record)
        {
            throw new NotImplementedException();
        }

        public object GetUnderlyingStorage()
        {
            throw new NotImplementedException();
        }

        public bool IsInitialized
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
