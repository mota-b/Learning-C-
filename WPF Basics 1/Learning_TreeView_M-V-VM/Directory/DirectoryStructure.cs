
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Learning_TreeView_M_V_VM
{
    /// <summary>
    /// A Helper Class to query information about Directories
    /// </summary>
    public static class DirectoryStructure
    {
        /// <summary>
        /// Get all logical drives on the computer
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            // Get every logical Drive on the machine
            return System.IO.Directory.GetLogicalDrives().Select((drive)=> new DirectoryItem() { FullPath = drive, Type = DirectoryItemType.Drive }).ToList(); // Instead of list of Strings ==> get list of Directory Items (Drives)
        }

        /// <summary>
        /// Get the directories top level Content
        /// </summary>
        /// <param name="fullPath"> The fullpath to the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContent( string fullPath)
        {
            // Create the empty list
            var items = new List<DirectoryItem>();

            #region Get Directories
            // Try to get directories from the curent folder
            // ignore any issue coming from it
            try
            {
                var dirs = System.IO.Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(dir => new DirectoryItem() { FullPath = dir, Type = DirectoryItemType.Folder }) ); // Instead of range of Strings ==> add range of Directory Items (Folders)
            }
            catch { }
            #endregion

            #region Get Files
            // Try to get files from the curent folder
            // ignore any issue coming from it
            try
            {
                var fls = System.IO.Directory.GetFiles(fullPath);
                if (fls.Length > 0)
                    items.AddRange(fls.Select(file => new DirectoryItem() { FullPath = file, Type = DirectoryItemType.File })); // Instead of range of Strings ==> add range of Directory Items (Files)
            }
            catch { }

            #endregion

            return items;
        }

        #region Helpers
        /// <summary>
        /// Get file or folder name from a full path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>        
        public static string GetFileFolderName(string path)
        {
            // if we have no path, return empty
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            // get last separator path index
            var lastSeparator_index = path.LastIndexOf(Path.DirectorySeparatorChar);

            // if no separator return the path
            if (lastSeparator_index < 0)
                return path;

            // return the folderName
            return path.Substring(lastSeparator_index + 1);
        }
        #endregion
    }
}
