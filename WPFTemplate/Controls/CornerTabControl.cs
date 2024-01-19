﻿using System;
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
        static CornerTabControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerTabControl), new FrameworkPropertyMetadata(typeof(CornerTabControl)));
            EventManager.RegisterClassHandler(typeof(CornerTabControl), TabItem.PreviewMouseDownEvent, new RoutedEventHandler(DeleteAndDragEvent));
            EventManager.RegisterClassHandler(typeof(CornerTabControl), TabItem.DropEvent, new DragEventHandler(DropItem));
            EventManager.RegisterClassHandler(typeof(CornerTabControl),TabItem.DragEnterEvent, new DragEventHandler(ItemDragEnter));
            EventManager.RegisterClassHandler(typeof(CornerTabControl), TabItem.DragLeaveEvent, new DragEventHandler(ItemDragLeave));
        }

        private static void DeleteAndDragEvent(object sender, RoutedEventArgs e)
        {
            //delete item
            var element = e.OriginalSource as FrameworkElement;
            if (element.Name == "deleteItem")
            {
                var tabItem = (TabItem)MyVisualTreeHelper.GetParent(element, typeof(TabItem));
                var tabControl = (CornerTabControl)MyVisualTreeHelper.GetParent(element, typeof(CornerTabControl));
                tabControl.Items.Remove(tabItem);
                e.Handled = true;
            }

            //dragdrop item
            if (element.Name == "textblockheader" || element.Name == "bordermain" || element.Name == "gridmain")
            {
                var tabItem = (TabItem)MyVisualTreeHelper.GetParent(element, typeof(TabItem));
                DragDrop.DoDragDrop(tabItem, tabItem, DragDropEffects.Move);
            }
        }


        private static void DropItem(object sender, DragEventArgs e)
        {
            TabItem targetTabItem = e.Source as TabItem;
            TabItem draggedTabItem = e.Data.GetData(typeof(TabItem)) as TabItem;
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
                    MyVisualTreeHelper.SetTemplateItemVisibility(targetTabItem, "lefthighlightborder", Visibility.Collapsed);
                    MyVisualTreeHelper.SetTemplateItemVisibility(targetTabItem, "righthighlightborder", Visibility.Collapsed);

                    tabControl.Items.RemoveAt(draggedIndex);
                    tabControl.Items.Insert(targetIndex, draggedTabItem);
                    tabControl.SelectedIndex = targetIndex;
                }
            }
        }

        private static void ItemDragEnter(object sender, DragEventArgs e)
        {
            var targetitem = e.Source as TabItem;
            var draggeditem = e.Data.GetData(typeof(TabItem));

            if (targetitem == draggeditem)
            {
                return;
            }

            if (targetitem != null && draggeditem != null)
            {
                var tabControl = (CornerTabControl)MyVisualTreeHelper.GetParent(targetitem, typeof(CornerTabControl));

                if (tabControl.Items.IndexOf(targetitem) < tabControl.Items.IndexOf(draggeditem))
                {
                    MyVisualTreeHelper.SetTemplateItemVisibility(targetitem, "lefthighlightborder", Visibility.Visible);
                }
                else
                {
                    MyVisualTreeHelper.SetTemplateItemVisibility(targetitem, "righthighlightborder", Visibility.Visible);
                }
            }
        }

        private static void ItemDragLeave(object sender, DragEventArgs e)
        {
            var targetitem = e.Source as TabItem;
            if (targetitem != null)
            {
                MyVisualTreeHelper.SetTemplateItemVisibility(targetitem, "lefthighlightborder", Visibility.Collapsed);
                MyVisualTreeHelper.SetTemplateItemVisibility(targetitem, "righthighlightborder", Visibility.Collapsed);
            }
        }


        public TabControlType TabControlType
        {
            get { return (TabControlType)GetValue(TabControlTypeProperty); }
            set { SetValue(TabControlTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TabControlType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TabControlTypeProperty =
            DependencyProperty.Register("TabControlType", typeof(TabControlType), typeof(CornerTabControl), new PropertyMetadata(TabControlType.Normal));

        public Brush SelectedColor
        {
            get { return (Brush)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Brush), typeof(CornerTabControl)
                , new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF206BC4"))));


        public bool ItemRemovable
        {
            get { return (bool)GetValue(ItemRemovableProperty); }
            set { SetValue(ItemRemovableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemRemovable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemRemovableProperty =
            DependencyProperty.Register("ItemRemovable", typeof(bool), typeof(CornerTabControl), new PropertyMetadata(false));
    }
}
