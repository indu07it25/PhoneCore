using System;
using System.IO.IsolatedStorage;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.Diagnostic.Tracing;
using PhoneCore.Framework.IoC;

namespace PhoneCore.Framework.Storage
{
    /// <summary>
    /// Isolated storage setting service
    /// </summary>
    public class IsolatedStorageSettingService : ISettingService
    {
        [Dependency]
        public ITrace Trace { get; set; }
        [Dependency("SettingService")]
        public ITraceCategory Category { get; set; }

        private readonly IsolatedStorageSettings _settings;

        public IsolatedStorageSettingService(IConfigSection config)
        {
            _settings = IsolatedStorageSettings.ApplicationSettings;
        }

        public void Save(string key, object value)
        {
            Trace.Info(Category, String.Format("save '{0}':'{1}'",key, value));
            _settings[key] = value;
            _settings.Save();
        }

        public bool IsExist(string key)
        {
            return _settings.Contains(key);
        }

        public T Load<T>(string key)
        {
            Trace.Info(Category, String.Format("load {0}",key));

            if (!_settings.Contains(key))
                return default(T);

            return (T)_settings[key];
        }
    }
}
