using System;

namespace PhoneCore.Framework.IoC
{
    /// <summary>
    /// Allows automatically resolving property dependency
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Constructor)]
    public class DependencyAttribute: Attribute
    {
        /// <summary>
        /// Named type/instance in container
        /// </summary>
        public string Name { get; set; }

        public DependencyAttribute()
        {
            
        }

        /// <summary>
        /// Allows definition of name of registered type. Used only for property injection
        /// </summary>
        /// <param name="name"></param>
        public DependencyAttribute(string name)
        {
            Name = name;
        }
    }
}
