using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class CornerTreeView : TreeView
    {
        static CornerTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerTreeView), new FrameworkPropertyMetadata(typeof(CornerTreeView)));
            EventManager.RegisterClassHandler(typeof(CornerTreeView), CheckBox.CheckedEvent, new RoutedEventHandler(ItemCheckBoxChecked));
            EventManager.RegisterClassHandler(typeof(CornerTreeView), CheckBox.UncheckedEvent, new RoutedEventHandler(ItemCheckBoxUnchecked));
        }

        public CornerTreeView()
        {
            this.Loaded += CornerTreeView_Loaded;
        }

        private void CornerTreeView_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyItemContainerStyle(this);
        }

        private void ApplyItemContainerStyle(ItemsControl itemsControl)
        {
            foreach (var item in itemsControl.Items)
            {
                if (item is TreeViewItem)
                {
                    var treeViewItem = (TreeViewItem)item;
                    treeViewItem.Style = ItemContainerStyle;
                    ApplyItemContainerStyle(treeViewItem);
                }
            }
        }

        private static void ItemCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            var element = e.OriginalSource as CheckBox;

            if (element != null)
            {
                var expression = BindingOperations.GetBindingExpression(element, CheckBox.IsCheckedProperty);
                if (expression != null)
                {
                    var propertyName = expression.ResolvedSourcePropertyName;
                    var treeviewitem = MyVisualTreeHelper.GetParent(element, typeof(TreeViewItem)) as TreeViewItem;
                    var nodes = treeviewitem.Items;
                    foreach (var node in nodes)
                    {
                        var t = node.GetType();
                        PropertyInfo propertyInfo = t.GetProperty(propertyName);
                        propertyInfo.SetValue(node, true);
                    }
                }
            }
        }

        private static void ItemCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            var element = e.OriginalSource as CheckBox;

            if (element != null)
            {
                var expression = BindingOperations.GetBindingExpression(element, CheckBox.IsCheckedProperty);
                if (expression != null)
                {
                    var propertyName = expression.ResolvedSourcePropertyName;
                    var treeviewitem = MyVisualTreeHelper.GetParent(element, typeof(TreeViewItem)) as TreeViewItem;
                    var nodes = treeviewitem.Items;
                    foreach (var node in nodes)
                    {
                        var t = node.GetType();
                        PropertyInfo propertyInfo = t.GetProperty(propertyName);
                        propertyInfo.SetValue(node, false);
                    }
                }
            }
        }

        public TreeViewType TreeViewType
        {
            get { return (TreeViewType)GetValue(TreeViewTypeProperty); }
            set { SetValue(TreeViewTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TreeViewType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TreeViewTypeProperty =
            DependencyProperty.Register("TreeViewType", typeof(TreeViewType), typeof(CornerTreeView), new PropertyMetadata(TreeViewType.Original));
    }
}
