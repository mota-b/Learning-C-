using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;


namespace Learning_TreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
         * Window
         */
        #region Constructor
        public MainWindow()
        {
            
            InitializeComponent();

        }
        #endregion

        #region On Loeaded
        
        /// <summary>
        /// When the application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            // Get logical Drives
            foreach(var drive in Directory.GetLogicalDrives())
            {
                /*
                 * Create and format the Item
                 */

                // Creat the item
                var item = new TreeViewItem()
                {
                    // Setting the properties of an object in c#


                    // Give the item a Header text
                    Header = drive,

                    // Give the item some data ==> the full paths
                    Tag = drive

                    
                };

                

                // Initialise the branch of the itam
                item.Items.Add(null);

                // Listen out for item being expended
                item.Expanded += Folder_expanded;

                item.Collapsed += Folder_collapsed;

                // Add the item to the Three view list iytem
                myDiscsView.Items.Add(item);




               
            }
        }
        #endregion


        /*
         * Events
         */
        #region Folder Expanded

        /// <summary>
        /// When a folder is expended find the Files and folders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Folder_expanded(object sender, RoutedEventArgs e) // The sender is the TreeView Item here
        {
            #region Initial Checks
            var item = (TreeViewItem) sender;

            // If the item contain only dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            // Clear if contains only dummy data
            item.Items.Clear();

            // get full path
            var fullPath = (string) item.Tag;
            #endregion

            #region Get Directories
            // Create blank list of directories
            var directories = new List<string>();

            // Try to get directories from the curent folder
            // ignore any issue coming from it
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }

            // for each directory
            directories.ForEach((directoryPath) => {

                // Crete directory item
                var subItem = new TreeViewItem()
                {
                    // folderName
                    Header = GetFileFolderName(directoryPath),
                    
                    // folderpath
                    Tag = directoryPath
                };

                // add dummy item so can expand
                subItem.Items.Add(null);

                // Handle expend event
                subItem.Expanded += Folder_expanded;

                // add this subItem to the parent
                item.Items.Add(subItem);
            });
            #endregion

            #region Get Files
            // Create blank list of files
            var files = new List<string>();

            // Try to get files from the curent folder
            // ignore any issue coming from it
            try
            {
                var fls = Directory.GetFiles(fullPath);
                if (fls.Length > 0)
                    files.AddRange(fls);
            }
            catch { }

            // for each file
            files.ForEach((filePath) => {

                // Crete file item
                var subItem = new TreeViewItem()
                {
                    // FileName
                    Header = GetFileFolderName(filePath),

                    // filepath
                    Tag = filePath
                };

                // add this subItem to the parent
                item.Items.Add(subItem);
            });
            #endregion  
        }
        #endregion

        #region Folder Collapsed

        /// <summary>
        /// When a folder is collapsed Clear the items list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Folder_collapsed(object sender, RoutedEventArgs e) // The sender is the TreeView Item here
        {
            #region Initial Checks
            var item = (TreeViewItem)sender;


            // If the item contain only dummy data
            if (item.Items.Count == 1 && item.Items[0] == null)
                return;

            // Clear if contains More than only dummy data
            item.Items.Clear();

            // Initialise the branch of the itam with only dummy data
            item.Items.Add(null);

            // get full path
            var fullPath = (string)item.Tag;
            #endregion
        }
        #endregion


        /*
         * Methodes
         */
        #region HelpFul Methodes
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
