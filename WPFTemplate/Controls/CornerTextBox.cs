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

        public static readonly DependencyProperty WaterTextProperty;

        public static readonly DependencyProperty ShowWaterTextProperty;

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
        public string WaterText
        {
            get { return (string)GetValue(WaterTextProperty); }
            set { SetValue(WaterTextProperty, value); }
        }
        public bool ShowWaterText
        {
            get { return (bool)GetValue(ShowWaterTextProperty); }
            set { SetValue(ShowWaterTextProperty, value); }
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
            WaterTextProperty = DependencyProperty.Register("WaterText", typeof(string), typeof(CornerTextBox), new PropertyMetadata("WaterText"));
            ShowWaterTextProperty = DependencyProperty.Register("ShowWaterText", typeof(bool), typeof(CornerTextBox), new PropertyMetadata(true));
            PressShowShadowProperty = DependencyProperty.Register("PressShowShadow", typeof(bool), typeof(CornerTextBox), new PropertyMetadata(true));
        }
    }
}
