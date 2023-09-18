using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFTemplate
{
    internal class MyVisualTreeHelper
    {
        public static DependencyObject GetParent(DependencyObject dependency, Type type)
        {
            if (dependency.GetType() == typeof(Window))
            {
                return dependency;
            }

            var parent = VisualTreeHelper.GetParent(dependency);
            if (parent == null)
            {
                return dependency;
            }

            if (parent.GetType() == type)
            {
                return parent;
            }

            return GetParent(parent, type);
        }

        public static void SetTemplateItemVisibility(Control control, string elementname, Visibility visibility)
        {
            var item = control.Template.FindName(elementname, control) as UIElement;
            if (item != null)
            {
                item.Visibility = visibility;
            }
        }
    }
}
