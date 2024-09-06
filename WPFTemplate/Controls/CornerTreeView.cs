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
        public static readonly DependencyProperty TreeViewTypeProperty;

        public static readonly DependencyProperty IconDisplayMemberPathProperty;

        public static readonly DependencyProperty SelectedBackgroundProperty;

        public Brush SelectedBackground
        {
            get { return (Brush)GetValue(SelectedBackgroundProperty); }
            set { SetValue(SelectedBackgroundProperty, value); }
        }

        public TreeViewType TreeViewType
        {
            get { return (TreeViewType)GetValue(TreeViewTypeProperty); }
            set { SetValue(TreeViewTypeProperty, value); }
        }

        public string IconDisplayMemberPath
        {
            get { return (string)GetValue(IconDisplayMemberPathProperty); }
            set { SetValue(IconDisplayMemberPathProperty, value); }
        }

        static CornerTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerTreeView), new FrameworkPropertyMetadata(typeof(CornerTreeView)));

            TreeViewTypeProperty = DependencyProperty.Register("TreeViewType", typeof(TreeViewType), typeof(CornerTreeView), new PropertyMetadata(TreeViewType.Original));
            IconDisplayMemberPathProperty = DependencyProperty.Register("IconDisplayMemberPath", typeof(string), typeof(CornerTreeView), 
                new FrameworkPropertyMetadata(string.Empty,FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,new PropertyChangedCallback(IconDisplayMemberPathPropertyChanded)));
            SelectedBackgroundProperty = DependencyProperty.Register("SelectedBackground", typeof(Brush), typeof(CornerTreeView), new PropertyMetadata(Brushes.Blue));

            EventManager.RegisterClassHandler(typeof(CornerTreeView), CheckBox.CheckedEvent, new RoutedEventHandler(ItemCheckBoxChecked));
            EventManager.RegisterClassHandler(typeof(CornerTreeView), CheckBox.UncheckedEvent, new RoutedEventHandler(ItemCheckBoxUnchecked));
            EventManager.RegisterClassHandler(typeof(CornerTreeView), CornerTreeView.PreviewMouseDownEvent, new RoutedEventHandler(CornerTreeViewSelectedItemChanged));
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
                var treeViewItem = item as TreeViewItem;
                if (treeViewItem != null)
                {
                    treeViewItem.Style = ItemContainerStyle;
                    ApplyItemContainerStyle(treeViewItem);
                }
                else
                {
                    var obj = ItemContainerGenerator.ContainerFromItem(item);
                    treeViewItem = obj as TreeViewItem;
                    if (treeViewItem != null)
                    {
                        if (IconDisplayMemberPath != string.Empty)
                        {
                            var icon = item.GetType().GetProperty(IconDisplayMemberPath)?.GetValue(item);
                            treeViewItem.SetValue(TreeViewItemHelper.IconProperty, icon);
                        }
                    }
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
                    var treeviewitem = LocalVisualTreeHelper.GetParent(element, typeof(TreeViewItem)) as TreeViewItem;
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
                    var treeviewitem = LocalVisualTreeHelper.GetParent(element, typeof(TreeViewItem)) as TreeViewItem;
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
        private static void CornerTreeViewSelectedItemChanged(object sender, RoutedEventArgs e)
        {
            var treeview = sender as CornerTreeView;
            if (treeview != null && treeview.TreeViewType == TreeViewType.Normal)
            {
                var element = e.OriginalSource as UIElement;
                var SelectedItem = LocalVisualTreeHelper.GetParent(element, typeof(TreeViewItem)) as TreeViewItem;
                if (SelectedItem != null)
                {
                    if (SelectedItem.HasItems)
                    {
                        SelectedItem.IsExpanded = !SelectedItem.IsExpanded;
                    }
                }
            }
        }
        private static void IconDisplayMemberPathPropertyChanded(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var treeview = d as CornerTreeView;
            if (treeview != null)
            {
                treeview.ApplyItemContainerStyle(treeview);
            }
        }
    }
}
