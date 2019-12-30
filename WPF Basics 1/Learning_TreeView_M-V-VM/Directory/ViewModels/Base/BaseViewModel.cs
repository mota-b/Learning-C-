using System.ComponentModel;

namespace Learning_TreeView_M_V_VM
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e)=> { };
    }
}