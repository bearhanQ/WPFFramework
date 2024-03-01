using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFTemplate
{
    public class TreeViewItemHelper
    {
        public static readonly DependencyProperty HeaderHeightProperty;

        public static readonly DependencyProperty IconProperty;

        static TreeViewItemHelper()
        {
            HeaderHeightProperty = DependencyProperty.RegisterAttached("HeaderHeight", typeof(double), typeof(TreeViewItemHelper), new PropertyMetadata((double)20));
            IconProperty = DependencyProperty.RegisterAttached("Icon", typeof(string), typeof(TreeViewItemHelper));
        }

        public static string GetIcon(DependencyObject obj)
        {
            return (string)obj.GetValue(IconProperty);
        }
        public static void SetIcon(DependencyObject obj, string value)
        {
            obj.SetValue(IconProperty, value);
        }

        public static double GetHeaderHeight(DependencyObject obj)
        {
            return (double)obj.GetValue(HeaderHeightProperty);
        }
        public static void SetHeaderHeight(DependencyObject obj, double value)
        {
            obj.SetValue(HeaderHeightProperty, value);
        }
    }
}
