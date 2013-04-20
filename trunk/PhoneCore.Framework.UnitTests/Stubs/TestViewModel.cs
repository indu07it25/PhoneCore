using System;
using System.Collections.Generic;
using PhoneCore.Framework.Configuration;
using PhoneCore.Framework.IoC;
using PhoneCore.Framework.Views;

namespace PhoneCore.Framework.UnitTests.Stubs
{
    public class TestViewModel: IViewModel
    {

        public Dictionary<string, object> NavigationParameters { get; set; }

        public void SaveStateTo(IDictionary<string, object> state)
        {

        }

        public void LoadStateFrom(IDictionary<string, object> state)
        {

        }
    }
}
