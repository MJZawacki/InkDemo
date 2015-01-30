//*********************************************************
//
// Copyright (c) Microsoft. All rights reserved.
//
//*********************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace InkDemo
{
    /// <summary>
    /// An <see cref="IValueConverter"/> that converts between <see cref="Color"/> and <see cref="SolidColorBrush"/>.
    /// </summary>
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if ((bool) value)
                return 100;
            else
                return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if ((bool)value)
                return 100;
            else
                return 0;
        }
    }
}
