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
    public class CornerCheckBox : CheckBox
    {
        static CornerCheckBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerCheckBox), new FrameworkPropertyMetadata(typeof(CornerCheckBox)));
            //EventManager.RegisterClassHandler(typeof(CornerCheckBox), CheckBox.CheckedEvent, new RoutedEventHandler(OnCheckedEventHandler));
            //EventManager.RegisterClassHandler(typeof(CornerCheckBox), CheckBox.UncheckedEvent, new RoutedEventHandler(OnUnCheckedEventHandler));
        }

        private static void OnCheckedEventHandler(object sender, RoutedEventArgs args)
        {
            var checkbox = sender as CornerCheckBox;
            if (checkbox != null)
            {
                if(checkbox.CheckBoxType == CheckBoxType.Switch)
                {
                    if (checkbox.Template != null)
                    {
                        Storyboard storyboard = new Storyboard();
                        {
                            Border SwitchBorder = checkbox.Template.FindName("PART_Border", checkbox) as Border;
                            if (SwitchBorder != null)
                            {
                                ColorAnimationUsingKeyFrames doubleAnimation = new ColorAnimationUsingKeyFrames
                                {
                                    KeyFrames =
                                    {
                                        new EasingColorKeyFrame { KeyTime = TimeSpan.Zero, Value = Colors.White },
                                        new EasingColorKeyFrame { KeyTime = TimeSpan.FromSeconds(0.5), Value = ((SolidColorBrush)checkbox.Background).Color }
                                    }
                                };
                                Storyboard.SetTarget(doubleAnimation, SwitchBorder);
                                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
                                storyboard.Children.Add(doubleAnimation);
                            }
                        }
                        storyboard.Begin();
                    }
                }
            }
        }

        private static void OnUnCheckedEventHandler(object sender, RoutedEventArgs args)
        {
            var checkbox = sender as CornerCheckBox;
            if (checkbox != null)
            {
                if (checkbox.CheckBoxType == CheckBoxType.Switch)
                {
                    if (checkbox.Template != null)
                    {
                        Storyboard storyboard = new Storyboard();
                        {
                            Border SwitchBorder = checkbox.Template.FindName("PART_Border", checkbox) as Border;
                            if (SwitchBorder != null)
                            {
                                ColorAnimationUsingKeyFrames doubleAnimation = new ColorAnimationUsingKeyFrames
                                {
                                    KeyFrames =
                                    {
                                        new EasingColorKeyFrame { KeyTime = TimeSpan.Zero, Value = ((SolidColorBrush)checkbox.Background).Color },
                                        new EasingColorKeyFrame { KeyTime = TimeSpan.FromSeconds(0.5), Value = Colors.White }
                                    }
                                };
                                Storyboard.SetTarget(doubleAnimation, SwitchBorder);
                                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));
                                storyboard.Children.Add(doubleAnimation);
                            }
                        }
                        storyboard.Begin();
                    }
                }
            }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerCheckBox));


        public CheckBoxType CheckBoxType
        {
            get { return (CheckBoxType)GetValue(CheckBoxTypeProperty); }
            set { SetValue(CheckBoxTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CheckBoxType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CheckBoxTypeProperty =
            DependencyProperty.Register("CheckBoxType", typeof(CheckBoxType), typeof(CornerCheckBox), new PropertyMetadata(CheckBoxType.Normal));
    }
}
