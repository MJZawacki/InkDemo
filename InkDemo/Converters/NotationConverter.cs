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
using Windows.UI.Xaml.Data;

namespace InkDemo
{
    public class NotationConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //KiwiItemTemplateSelector kiwiSelc = parameter as KiwiItemTemplateSelector;
            //bool isSelected = (bool)value;
            //return kiwiSelc.SelectTemplate(isSelected, null);
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }

}
