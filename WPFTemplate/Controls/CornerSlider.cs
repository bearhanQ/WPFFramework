using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class CornerSlider : Slider
    {
        public static readonly DependencyProperty TrackThicknessProperty;

        public static readonly DependencyProperty SliderTypeProperty;

        public static readonly DependencyProperty CornerRadiusProperty;

        public static readonly DependencyProperty TrackBackgroundProperty;

        public double TrackThickness
        {
            get { return (double)GetValue(TrackThicknessProperty); }
            set { SetValue(TrackThicknessProperty, value); }
        }
        public SliderType SliderType
        {
            get { return (SliderType)GetValue(SliderTypeProperty); }
            set { SetValue(SliderTypeProperty, value); }
        }
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public Brush TrackBackground
        {
            get { return (Brush)GetValue(TrackBackgroundProperty); }
            set { SetValue(TrackBackgroundProperty, value); }
        }

        static CornerSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerSlider), new FrameworkPropertyMetadata(typeof(CornerSlider)));
            TrackThicknessProperty = DependencyProperty.Register("TrackThickness", typeof(double), typeof(CornerSlider), new PropertyMetadata(double.Parse("2")));
            SliderTypeProperty = DependencyProperty.Register("SliderType", typeof(SliderType), typeof(CornerSlider));
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerSlider));
            TrackBackgroundProperty = DependencyProperty.Register("TrackBackground", typeof(Brush), typeof(CornerSlider), 
                new PropertyMetadata(new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFE7EAEA"))));
        }
    }
}
