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
using Windows.Media.Capture;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using InkDemo.Model;

namespace InkDemo.Common
{
    public class NotationSelector : DataTemplateSelector
    {
        public DataTemplate NoteTemplate { get; set; }
        public DataTemplate RestTemplate { get; set; }

        public DataTemplate StaffTemplate { get; set; }
        public DataTemplate GClefTemplate { get; set; }

        public DataTemplate TextTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object type, DependencyObject container)
        {
            var thisType = type.GetType();
            if (thisType == typeof(Tuple<int, int, int, int>))
                return StaffTemplate;
            else if (thisType == typeof (Clef))
                return GClefTemplate;
            else if (thisType == typeof(TextResult))
                return TextTemplate;

            return TextTemplate;
        }
    }
}
