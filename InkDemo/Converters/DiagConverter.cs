using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace InkDemo
{

    
    /// <summary>
    /// This converter does nothing except breaking the
    /// debugger into the convert method
    /// </summary>
    public class DatabindingDebugConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}

