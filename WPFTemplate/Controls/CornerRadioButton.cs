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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class CornerRadioButton : RadioButton
    {
        static CornerRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerRadioButton), new FrameworkPropertyMetadata(typeof(CornerRadioButton)));
        }

        public RadioButtonType RadioButtonType
        {
            get { return (RadioButtonType)GetValue(RadioButtonTypeProperty); }
            set { SetValue(RadioButtonTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RadioButtonType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RadioButtonTypeProperty =
            DependencyProperty.Register("RadioButtonType", typeof(RadioButtonType), typeof(CornerRadioButton), new PropertyMetadata(RadioButtonType.Normal));


        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerRadioButton));
    }
}
