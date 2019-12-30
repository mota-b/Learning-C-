
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Learning_TreeView_M_V_VM
{
    /// <summary>
    /// A View Model for each Directory item
    /// NT: This looks Like The Data/DirectoryItem but Contains ONLY => Information tha t the Ui Needs
    /// </summary>
    public class DirectoryItemViewModel : BaseViewModel
    {
        #region Public Properties
        /// <summary>
        /// The Directory Item Type
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The absolute path of the Directory Item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// The name of this Directory Item
        /// </summary>
        public string Name
        {
            get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); }
        }

        /// <summary>
        /// A list of all Children in Contained inside the Item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Children { get; set; }

        /// <summary>
        /// Indicate if this Item Can Be Expanded
        /// </summary>
        public bool CanExpand { get { return this.Type != DirectoryItemType.File; } }

        /// <summary>
        /// Indicate if the curent Item is Expended Or Not
        /// </summary>
        public bool IsExpanded { 
            get 
            {
                // "?" ==> Means if there are Children ==> (Continue)
                return this.Children?.Count( (child) => child != null ) > 0;
            } 
            set 
            {
                // If the UI tell us To Expand
                if (value == true)
                    Expand();
                // If the UI tell us To Collapse
                else
                    ClearChildrenAndCollapse();
            } 
        }

        #endregion

        #region Public Commands

        /// <summary>
        /// The Command to expand the Item
        /// </summary>
        public ICommand ExpandCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// This Time we initialise inside the constructor because 
        /// we (fear that the Expand is Before the Initialisation of the Type wich We use To NOT add A "dummy Item" to a "File" )
        /// <param name="fulName"> The full path of this Item</param>
        /// <param name="type"> The type of this item</param>
        public DirectoryItemViewModel(string fulName, DirectoryItemType type)
        {
            // Create Command
            this.ExpandCommand = new RelayCommand(Expand); // Bind the Expand Function to the U.I

            // Set Path and Type
            this.FullPath = fulName;
            this.Type = type;

            // Set Children as Needed
            this.ClearChildrenAndCollapse();
        }
        #endregion

        #region Helper Methods
        /// <summary>
        /// Remove all children from the list 
        /// And add a dummy item to Show the expandable Icon
        /// </summary>
        private void ClearChildrenAndCollapse()
        {
            // Clear Items
            this.Children = new ObservableCollection<DirectoryItemViewModel>();

            // Show the expand arrow if not a File
            if(this.Type != DirectoryItemType.File)
                this.Children.Add(null);

            
        }

        /// <summary>
        /// Expand this Directory Item and find All the children
        /// </summary>
        private void Expand()
        {
            // We Can not expand a file 
            if (this.Type == DirectoryItemType.File)
                return;

            // Find All Childrens
            var children = DirectoryStructure.GetDirectoryContent(this.FullPath);
            this.Children = new ObservableCollection<DirectoryItemViewModel>(children.Select(content => new DirectoryItemViewModel(content.FullPath, content.Type) ));

        }
        #endregion
    }
}


