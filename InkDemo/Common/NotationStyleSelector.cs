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
    public class NotationStyleSelector : StyleSelector
    {
        public Style NoteTemplate { get; set; }
        public Style RestTemplate { get; set; }

        public Style StaffTemplate { get; set; }
        public Style GClefTemplate { get; set; }

        public Style TextTemplate { get; set; }

        protected override Style SelectStyleCore(object type, DependencyObject container)
        {
            var thisType = type.GetType();
            if (thisType == typeof(Tuple<int, int, int, int>))
                return StaffTemplate;
            else if (thisType == typeof (Clef))
                return GClefTemplate;
            else if (thisType == typeof (TextResult))
                return TextTemplate;

            return TextTemplate;
        }


    }
}
