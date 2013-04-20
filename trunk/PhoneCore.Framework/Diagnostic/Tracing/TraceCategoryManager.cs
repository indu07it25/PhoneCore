using System;
using System.Collections.Generic;
using PhoneCore.Framework.IoC;
using PhoneCore.Framework.IoC.LifetimeManagers;

namespace PhoneCore.Framework.Diagnostic.Tracing
{
    public class TraceCategoryManager:  ILifetimeManager
    {
        private readonly Dictionary<string, ITraceCategory> _categories = new Dictionary<string, ITraceCategory>();
        private readonly object _lockInstance = new object();

        /// <summary>
        /// Returns the instance of trace category
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ITraceCategory GetInstance(string name)
        {
            if (!_categories.ContainsKey(name))
            {
                lock (_lockInstance)
                {
                    if (!_categories.ContainsKey(name))
                        _categories.Add(name, new TraceCategory(name));
                }
            }
            return _categories[name];
        }

        /// <summary>
        /// Returns registred category names
        /// </summary>
        public  IEnumerable<string> GetRegistredCategoryNames
        {
            get
            {
                lock (_lockInstance)
                    return _categories.Keys;
            }
        }

        #region Implicit implementation of ILifetimeManager
 

        object[] ILifetimeManager.CstorArgs
   
             { get; set; }
    

        object ILifetimeManager.GetInstance()
        {
            throw new NotImplementedException();
        }

        object ILifetimeManager.GetInstance(string name)
        {
            return this.GetInstance(name);
        }

        public System.Reflection.ConstructorInfo Constructor { get; set; }

        public Type InterfaceType { get; set; }

        public Type TargetType { get; set; }

        public void Dispose()
        {
            _categories.Clear();
        }

        #endregion


    }
}
