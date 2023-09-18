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
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFTemplate.Controls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFTemplate.Controls;assembly=WPFTemplate.Controls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CornerTreeView/>
    ///
    /// </summary>
    public class CornerTreeView : TreeView
    {
        static CornerTreeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerTreeView), new FrameworkPropertyMetadata(typeof(CornerTreeView)));
            EventManager.RegisterClassHandler(typeof(CornerTreeView), CheckBox.CheckedEvent, new RoutedEventHandler(ItemCheckBoxChecked));
            EventManager.RegisterClassHandler(typeof(CornerTreeView), CheckBox.UncheckedEvent, new RoutedEventHandler(ItemCheckBoxUnchecked));
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
