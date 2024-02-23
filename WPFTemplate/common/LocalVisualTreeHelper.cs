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
    public class LocalVisualTreeHelper
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

        public static int GetTreeViewItemLevel(TreeViewItem treeViewItem)
        {
            int level = 0;

            DependencyObject parent = VisualTreeHelper.GetParent(treeViewItem);
            while (parent != null && !(parent is TreeView))
            {
                if (parent is TreeViewItem)
                {
                    level++;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }

            return level;
        }
    }
}
