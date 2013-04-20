using System;

namespace PhoneCore.Framework.Configuration
{
    public interface IConfigurable
    {
        void Configure(IConfigSection config);
    }
}
