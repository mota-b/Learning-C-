using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Learning_TreeView.Convs
{
    /// <summary>
    /// Convert full path to specific Image (acording to type of drive, folder, or file)
    /// </summary>
    [ValueConversion(typeof(string), typeof(BitmapImage))]
    class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Get the full path
            var path = (string)value;

            // check if the full path is null
            if (path == null)
                return null;

            // get the name of the file/folder
            var name = MainWindow.GetFileFolderName(path);

            // By default image
            var image = "Media/Icons/file.png";

            // if the name is black we assume that it's a drive as we cant have a blank file or folder
            if (string.IsNullOrEmpty(name))
                image = "Media/Icons/disc-storage.png";
            else if (new FileInfo(path).Attributes.HasFlag(FileAttributes.Directory))
                image = "Media/Icons/folder.png";

            return new BitmapImage(new Uri($@"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
