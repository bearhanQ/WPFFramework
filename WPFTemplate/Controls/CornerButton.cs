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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class CornerButton : Button
    {
        public static readonly DependencyProperty CornerRadiusProperty;

        public static readonly DependencyProperty ButtonTypeProperty;

        public static readonly DependencyProperty IconProperty;

        public static readonly DependencyProperty MouseOverForegroundProperty;

        public static readonly DependencyProperty LinkShowUnderLineProperty;

        public static readonly DependencyProperty PressShowShadowProperty;

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public ButtonType ButtonType
        {
            get { return (ButtonType)GetValue(ButtonTypeProperty); }
            set { SetValue(ButtonTypeProperty, value); }
        }
        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public Brush MouseOverForeground
        {
            get { return (Brush)GetValue(MouseOverForegroundProperty); }
            set { SetValue(MouseOverForegroundProperty, value); }
        }
        public bool LinkShowUnderLine
        {
            get { return (bool)GetValue(LinkShowUnderLineProperty); }
            set { SetValue(LinkShowUnderLineProperty, value); }
        }
        public bool PressShowShadow
        {
            get { return (bool)GetValue(PressShowShadowProperty); }
            set { SetValue(PressShowShadowProperty, value); }
        }

        static CornerButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerButton), new FrameworkPropertyMetadata(typeof(CornerButton)));
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerButton));
            ButtonTypeProperty = DependencyProperty.Register("ButtonType", typeof(ButtonType), typeof(CornerButton));
            IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(CornerButton), new PropertyMetadata(string.Empty));
            MouseOverForegroundProperty = DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(CornerButton), new PropertyMetadata(Brushes.Black));
            LinkShowUnderLineProperty = DependencyProperty.Register("LinkShowUnderLine", typeof(bool), typeof(CornerButton), new PropertyMetadata(true));
            PressShowShadowProperty = DependencyProperty.Register("PressShowShadow", typeof(bool), typeof(CornerButton), new PropertyMetadata(true));
        }
    }
}
