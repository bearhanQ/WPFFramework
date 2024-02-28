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
    public class CornerScrollViewer : ScrollViewer
    {
        public static readonly DependencyProperty CornerRadiusProperty;

        public static readonly DependencyProperty TrackWidthProperty;

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public double TrackWidth
        {
            get { return (double)GetValue(TrackWidthProperty); }
            set { SetValue(TrackWidthProperty, value); }
        }

        static CornerScrollViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerScrollViewer), new FrameworkPropertyMetadata(typeof(CornerScrollViewer)));
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerScrollViewer));
            TrackWidthProperty = DependencyProperty.Register("TrackWidth", typeof(double), typeof(CornerScrollViewer), new PropertyMetadata((double)10));
        }
    }
}
