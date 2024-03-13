using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFTemplate
{
    public class LocalLogicalTreeHelper
    {
        public static DependencyObject GetParent(DependencyObject dependency, Type type)
        {
            if (dependency.GetType() == typeof(Window))
            {
                return dependency;
            }

            var parent = LogicalTreeHelper.GetParent(dependency);
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
    }
}
