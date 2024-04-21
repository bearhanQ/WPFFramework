using System;
using System.Collections.Generic;
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
    public class DrawerItem : ContentControl
    {

        public static readonly DependencyProperty CornerRadiusProperty;

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        static DrawerItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DrawerItem), new FrameworkPropertyMetadata(typeof(DrawerItem)));
            CornerRadiusProperty =  DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(DrawerItem));
        }
    }
}
