using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class CornerTabControl : TabControl
    {
        public static readonly DependencyProperty TabControlTypeProperty;

        public static readonly DependencyProperty SelectedColorProperty;

        public static readonly DependencyProperty ItemRemovableProperty;

        public TabControlType TabControlType
        {
            get { return (TabControlType)GetValue(TabControlTypeProperty); }
            set { SetValue(TabControlTypeProperty, value); }
        }
        public Brush SelectedColor
        {
            get { return (Brush)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }
        public bool ItemRemovable
        {
            get { return (bool)GetValue(ItemRemovableProperty); }
            set { SetValue(ItemRemovableProperty, value); }
        }

        static CornerTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerTabControl), new FrameworkPropertyMetadata(typeof(CornerTabControl)));

            TabControlTypeProperty = DependencyProperty.Register("TabControlType", typeof(TabControlType), typeof(CornerTabControl), new PropertyMetadata(TabControlType.Normal));
            SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Brush), typeof(CornerTabControl)
                , new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF206BC4"))));
            ItemRemovableProperty = DependencyProperty.Register("ItemRemovable", typeof(bool), typeof(CornerTabControl), new PropertyMetadata(false));

            EventManager.RegisterClassHandler(typeof(CornerTabControl), CornerTabItem.PreviewMouseDownEvent, new RoutedEventHandler(DeleteAndDragEvent));
            EventManager.RegisterClassHandler(typeof(CornerTabControl), CornerTabItem.DropEvent, new DragEventHandler(DropItem));
            EventManager.RegisterClassHandler(typeof(CornerTabControl), CornerTabItem.DragEnterEvent, new DragEventHandler(ItemDragEnter));
            EventManager.RegisterClassHandler(typeof(CornerTabControl), CornerTabItem.DragLeaveEvent, new DragEventHandler(ItemDragLeave));
        }

        private static void DeleteAndDragEvent(object sender, RoutedEventArgs e)
        {
            //delete item
            var element = e.OriginalSource as FrameworkElement;
            if (element.Name == "deleteItem")
            {
                var tabItem = (CornerTabItem)LocalVisualTreeHelper.GetParent(element, typeof(CornerTabItem));
                var tabControl = (CornerTabControl)LocalVisualTreeHelper.GetParent(element, typeof(CornerTabControl));
                tabControl.Items.Remove(tabItem);
                e.Handled = true;
            }

            //dragdrop item
            if (element.Name == "CPHeader" || element.Name == "bordermain" || element.Name == "gridmain")
            {
                var tabItem = (CornerTabItem)LocalVisualTreeHelper.GetParent(element, typeof(CornerTabItem));
                DragDrop.DoDragDrop(tabItem, tabItem, DragDropEffects.Move);
            }
        }
        private static void DropItem(object sender, DragEventArgs e)
        {
            CornerTabItem targetTabItem = e.Source as CornerTabItem;
            CornerTabItem draggedTabItem = e.Data.GetData(typeof(CornerTabItem)) as CornerTabItem;

            TabControl tabControl = (TabControl)sender;

            if (targetTabItem != null && draggedTabItem != null)
            {
                int targetIndex = tabControl.Items.IndexOf(targetTabItem);
                int draggedIndex = tabControl.Items.IndexOf(draggedTabItem);

                if (targetIndex == draggedIndex)
                {
                    return;
                }

                if (targetIndex >= 0 && draggedIndex >= 0 && targetIndex != draggedIndex)
                {
                    LocalVisualTreeHelper.SetTemplateItemVisibility(targetTabItem, "lefthighlightborder", Visibility.Collapsed);
                    LocalVisualTreeHelper.SetTemplateItemVisibility(targetTabItem, "righthighlightborder", Visibility.Collapsed);

                    tabControl.Items.RemoveAt(draggedIndex);
                    tabControl.Items.Insert(targetIndex, draggedTabItem);
                    tabControl.SelectedIndex = targetIndex;
                }
            }
        }
        private static void ItemDragEnter(object sender, DragEventArgs e)
        {
            var targetitem = e.Source as CornerTabItem;
            var draggeditem = e.Data.GetData(typeof(CornerTabItem));

            if (targetitem == draggeditem)
            {
                return;
            }

            if (targetitem != null && draggeditem != null)
            {
                var tabControl = (CornerTabControl)LocalVisualTreeHelper.GetParent(targetitem, typeof(CornerTabControl));

                if (tabControl.Items.IndexOf(targetitem) < tabControl.Items.IndexOf(draggeditem))
                {
                    LocalVisualTreeHelper.SetTemplateItemVisibility(targetitem, "lefthighlightborder", Visibility.Visible);
                }
                else
                {
                    LocalVisualTreeHelper.SetTemplateItemVisibility(targetitem, "righthighlightborder", Visibility.Visible);
                }
            }
        }
        private static void ItemDragLeave(object sender, DragEventArgs e)
        {
            var targetitem = e.Source as TabItem;
            if (targetitem != null)
            {
                LocalVisualTreeHelper.SetTemplateItemVisibility(targetitem, "lefthighlightborder", Visibility.Collapsed);
                LocalVisualTreeHelper.SetTemplateItemVisibility(targetitem, "righthighlightborder", Visibility.Collapsed);
            }
        }
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is CornerTabItem;
        }
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CornerTabItem();
        }
    }
}
