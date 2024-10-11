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
    public class CornerContextMenu : ContextMenu
    {
        public static readonly DependencyProperty CornerRadiusProperty;

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        static CornerContextMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerContextMenu), new FrameworkPropertyMetadata(typeof(CornerContextMenu)));
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerContextMenu), new PropertyMetadata(new CornerRadius(0)));
        }
    }
}
