using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PhoneCore.Framework.Views
{
    /// <summary>
    /// View model base
    /// </summary>
    public class ViewModelBase: IViewModel, INotifyPropertyChanged
    {

        #region IViewModel implementation

        private Dictionary<string, object> _navigationParameters = null;
        public Dictionary<string, object> NavigationParameters
        {
            get
            {
                return _navigationParameters;
            }
            set
            {
                _navigationParameters = value;
                ReadNavigationParameters();
            }
        }

        protected virtual void ReadNavigationParameters()
        {
        }

        /// <summary>
        /// Saves view model state into dictionary
        /// </summary>
        /// <param name="state"></param>
        public virtual void SaveStateTo(IDictionary<string, object> state)
        {

        }

        /// <summary>
        /// Restores view model state from dictionary
        /// </summary>
        /// <param name="state"></param>
        public virtual void LoadStateFrom(IDictionary<string, object> state)
        {

        }

        #endregion

        #region INotiyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(name));
        }

        #endregion
    }
}
