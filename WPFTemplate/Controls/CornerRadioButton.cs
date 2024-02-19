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
        public static readonly DependencyProperty RadioButtonTypeProperty;

        public static readonly DependencyProperty CornerRadiusProperty;

        public RadioButtonType RadioButtonType
        {
            get { return (RadioButtonType)GetValue(RadioButtonTypeProperty); }
            set { SetValue(RadioButtonTypeProperty, value); }
        }
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        
        static CornerRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerRadioButton), new FrameworkPropertyMetadata(typeof(CornerRadioButton)));
            RadioButtonTypeProperty = DependencyProperty.Register("RadioButtonType", typeof(RadioButtonType), typeof(CornerRadioButton), new PropertyMetadata(RadioButtonType.Normal));
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerRadioButton));
        }
    }
}
