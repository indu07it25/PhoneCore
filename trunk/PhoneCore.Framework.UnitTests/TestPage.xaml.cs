using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Silverlight.Testing;

namespace PhoneCore.Framework.UnitTests
{
    public partial class TestPage : PhoneApplicationPage
    {
        // Constructor
        public TestPage()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //UnitTestSystem.RegisterUnitTestProvider(
            //        new Microsoft.Silverlight.Testing.UnitTesting.Metadata.NUnit.NUnitProvider());
          var testPage = UnitTestSystem.CreateTestPage() as IMobileTestPage;

            BackKeyPress += (x, xe) => xe.Cancel = testPage.NavigateBack();
            (Application.Current.RootVisual as PhoneApplicationFrame).Content = testPage;


        }

    }
}