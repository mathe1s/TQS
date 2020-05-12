using System.ComponentModel;

namespace TQS.ViewModel
{
    public class VMObject : INotifyPropertyChanged
    {
        #region MVVM PropertyChangedEventHandler
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
