using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;

namespace PhoneCore.Framework.Views
{
    /// <summary>
    /// PhoneApplicationPage with supporting of tombstoning
    /// </summary>
    public class ViewPage : PhoneApplicationPage
    {
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            (DataContext as IViewModel).SaveStateTo(State);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            (DataContext as IViewModel).LoadStateFrom(State);
        }
    }
}
