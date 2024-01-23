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
using WPFTemplate.Enums;

namespace WPFTemplate
{
    public class CornerSlider : Slider
    {
        static CornerSlider()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerSlider), new FrameworkPropertyMetadata(typeof(CornerSlider)));
        }

        public double TrackThickness
        {
            get { return (double)GetValue(TrackThicknessProperty); }
            set { SetValue(TrackThicknessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TrackThickness.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TrackThicknessProperty =
            DependencyProperty.Register("TrackThickness", typeof(double), typeof(CornerSlider), new PropertyMetadata(double.Parse("2")));


        public SliderType SliderType
        {
            get { return (SliderType)GetValue(SliderTypeProperty); }
            set { SetValue(SliderTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SliderType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SliderTypeProperty =
            DependencyProperty.Register("SliderType", typeof(SliderType), typeof(CornerSlider));


        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerSlider));
    }
}
