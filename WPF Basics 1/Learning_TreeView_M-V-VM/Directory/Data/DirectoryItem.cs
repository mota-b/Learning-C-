
namespace Learning_TreeView_M_V_VM
{
    /// <summary>
    /// Information about directory items such as Drive folder or file
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// The Directory Item Type
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// The absolute path of the Directory Item
        /// </summary>
        public string FullPath { get; set;}

        /// <summary>
        /// The name of this Directory Item
        /// </summary>
        public string Name { 
            get { return this.Type == DirectoryItemType.Drive ? this.FullPath : DirectoryStructure.GetFileFolderName(this.FullPath); } 
        }
    }
}
