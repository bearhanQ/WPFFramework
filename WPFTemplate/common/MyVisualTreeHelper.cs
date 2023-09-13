using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPFTemplate
{
    internal class MyVisualTreeHelper
    {
        public static DependencyObject GetParent(DependencyObject dependency, Type type)
        {
            var parent = VisualTreeHelper.GetParent(dependency);
            if (parent.GetType() == type)
            {
                return parent;
            }
            return GetParent(parent, type);
        }
    }
}
