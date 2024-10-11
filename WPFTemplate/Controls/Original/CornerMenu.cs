using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFTemplate
{
    public class CornerMenu : Menu
    {
        static CornerMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerMenu), new FrameworkPropertyMetadata(typeof(CornerMenu)));
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is CornerMenuItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CornerMenuItem();
        }
    }
}
