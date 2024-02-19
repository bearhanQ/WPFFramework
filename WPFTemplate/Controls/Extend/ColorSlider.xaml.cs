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
    /// <summary>
    /// ColorSlider.xaml 的交互逻辑
    /// </summary>
    public partial class ColorSlider : UserControl
    {
        public static readonly DependencyProperty SelectedColorProperty;

        public Brush SelectedColor
        {
            get { return (Brush)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        public ColorSlider()
        {
            InitializeComponent();
        }

        static ColorSlider()
        {
            SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Brush), typeof(ColorSlider)
                ,new PropertyMetadata(new SolidColorBrush(Color.FromRgb(255, 0, 0))));
        }

        private void CornerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var sd = sender as CornerSlider;
            var value = Math.Round(sd.Value, 2);
            var factor = value - Math.Floor(value);
            //when index<=2        R=255;    G=up;    B=0;
            //when 2<index<=3      R=down;   G=255;   B=0;
            //when 3<index<=4      R=0;      G=255;   B=up;
            //when 4<index<=5      R=0;      G=down;  B=255;
            //when 5<index<=6      R=up;     G=0;     B=255;
            //when 6<index<=7      R=255;    G=up;    B=255;
            //when 7<index<=8      R=down;   G=down;  B=down;
            if (value <= 2)
            {
                var g = (byte)(255 * (value / 2));
                SelectedColor = new SolidColorBrush(Color.FromRgb(255, g, 0));
            }
            else if (value > 2 && value <= 3)
            {
                var r = (byte)(255 - 255 * factor);
                SelectedColor = new SolidColorBrush(Color.FromRgb(r, 255, 0));
            }
            else if (value > 3 && value <= 4)
            {
                var b = (byte)(255 * factor);
                SelectedColor = new SolidColorBrush(Color.FromRgb(0, 255, b));
            }
            else if (value > 4 && value <= 5)
            {
                var g = (byte)(255 - 255 * factor);
                SelectedColor = new SolidColorBrush(Color.FromRgb(0, g, 255));
            }
            else if (value > 5 && value <= 6)
            {
                var r = (byte)(255 * factor);
                SelectedColor = new SolidColorBrush(Color.FromRgb(r, 0, 255));
            }
            else if (value > 6 && value <= 7)
            {
                var g = (byte)(255 * factor);
                SelectedColor = new SolidColorBrush(Color.FromRgb(255, g, 255));
            }
            else
            {
                var c = (byte)(255 - 255 * factor);
                SelectedColor = new SolidColorBrush(Color.FromRgb(c, c, c));
            }
        }
    }
}
