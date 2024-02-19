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
    public class CornerMenuItem : MenuItem
    {
        public static new readonly DependencyProperty IconProperty;

        public static readonly DependencyProperty CornerRadiusProperty;

        public new string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        static CornerMenuItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerMenuItem), new FrameworkPropertyMetadata(typeof(CornerMenuItem)));
            IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(CornerMenuItem), new PropertyMetadata(null));
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerMenuItem), new PropertyMetadata(new CornerRadius(0)));
        }
    }
}
