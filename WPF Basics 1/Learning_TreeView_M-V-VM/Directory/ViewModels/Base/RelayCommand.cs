
using System;
using System.Windows.Input;

namespace Learning_TreeView_M_V_VM
{
    /// <summary>
    /// A basic Command that run an Action
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private Members
        /// <summary>
        /// The Action to run
        /// </summary>
        private Action action;
        #endregion

        #region Public Event
        /// <summary>
        /// The event that fired when the <see cref="CanExecute(object)"/> value has Changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public RelayCommand(Action action)
        {
            this.action = action;  
        }
        #endregion

        #region Command Methodes
        /// <summary>
        /// A rellay Command Can always Execute
        /// Like a Clickable for The UI Buttons
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Execute the Command Action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            this.action();
        }
        #endregion
    } 
}
