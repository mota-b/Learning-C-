using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Learning_TreeView_M_V_VM.Converters
{
    /// <summary>
    /// Convert full path to specific Image (acording to type of drive, folder, or file)
    /// </summary>
    [ValueConversion( typeof(DirectoryItemType), typeof(BitmapImage) )]
    class HeaderToImageConverter : IValueConverter
    {
        public static HeaderToImageConverter Instance = new HeaderToImageConverter();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
          
            // By default image
            var image = "Media/Icons/file.png";

            switch ((DirectoryItemType)value)
            {
                case DirectoryItemType.Drive:
                    image = "Media/Icons/disc-storage.png";
                    break;
                case DirectoryItemType.Folder:
                    image = "Media/Icons/folder.png";
                    break;
            }
           

            return new BitmapImage(new Uri($@"pack://application:,,,/{image}"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
