
using System.Collections.ObjectModel;
using System.Linq;

namespace Learning_TreeView_M_V_VM
{
    /// <summary>
    /// The View Model for a Directory View
    /// </summary>
    public class DirectoryStructureViewModel
    {
        #region Publci Properties
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public DirectoryStructureViewModel()
        {
            // Get The logical Drives
            var children = DirectoryStructure.GetLogicalDrives();
            
            // Create The View Model From The Data
            this.Items = new ObservableCollection<DirectoryItemViewModel>(children.Select(child => new DirectoryItemViewModel(child.FullPath, child.Type) ));
        
        }
        #endregion


    }
}
