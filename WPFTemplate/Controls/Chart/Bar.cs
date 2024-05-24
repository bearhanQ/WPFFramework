using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class Bar : Chart
    {
        static Bar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Bar), new FrameworkPropertyMetadata(typeof(Bar)));
        }
        protected override DependencyObject GetContainerForItemOverride()
        {
            return new BarItem();
        }
        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is BarItem;
        }
    }
}
