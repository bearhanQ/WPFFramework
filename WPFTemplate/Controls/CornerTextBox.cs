using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class CornerTextBox : TextBox
    {
        public static readonly DependencyProperty CornerRadiusProperty;

        public static readonly DependencyProperty IconProperty;

        public static readonly DependencyProperty WatermarkProperty;

        public static readonly DependencyProperty ShowWatermarkProperty;

        public static readonly DependencyProperty PressShowShadowProperty;

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }
        public bool ShowWatermark
        {
            get { return (bool)GetValue(ShowWatermarkProperty); }
            set { SetValue(ShowWatermarkProperty, value); }
        }
        public bool PressShowShadow
        {
            get { return (bool)GetValue(PressShowShadowProperty); }
            set { SetValue(PressShowShadowProperty, value); }
        }

        static CornerTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerTextBox), new FrameworkPropertyMetadata(typeof(CornerTextBox)));
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerTextBox));
            IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(CornerTextBox), new PropertyMetadata(null));
            WatermarkProperty = DependencyProperty.Register("Watermark", typeof(string), typeof(CornerTextBox), new PropertyMetadata("Watermark"));
            ShowWatermarkProperty = DependencyProperty.Register("ShowWatermark", typeof(bool), typeof(CornerTextBox), new PropertyMetadata(true));
            PressShowShadowProperty = DependencyProperty.Register("PressShowShadow", typeof(bool), typeof(CornerTextBox), new PropertyMetadata(true));
        }
    }
}
