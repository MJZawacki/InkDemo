using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace InkDemo.Common
{
    public class SheetMusicItemsControl : ItemsControl
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            FrameworkElement contentitem = element as FrameworkElement;
            Binding leftBinding = new Binding();
            leftBinding.Path = new PropertyPath("X");

            Binding topBinding = new Binding();
            topBinding.Path = new PropertyPath("Y");
            contentitem.SetBinding(Canvas.LeftProperty, leftBinding);
            contentitem.SetBinding(Canvas.TopProperty, topBinding);
            base.PrepareContainerForItemOverride(element, item);
        }
    }
}
