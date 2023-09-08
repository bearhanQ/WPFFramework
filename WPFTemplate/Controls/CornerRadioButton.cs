﻿using System;
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
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFTemplate.Controls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFTemplate.Controls;assembly=WPFTemplate.Controls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CornerRadioButton/>
    ///
    /// </summary>
    public class CornerRadioButton : RadioButton
    {
        static CornerRadioButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerRadioButton), new FrameworkPropertyMetadata(typeof(CornerRadioButton)));

            EventManager.RegisterClassHandler(typeof(CornerRadioButton), RadioButton.CheckedEvent, new RoutedEventHandler(OnCheckedEventHandler));
            EventManager.RegisterClassHandler(typeof(CornerRadioButton), RadioButton.UncheckedEvent, new RoutedEventHandler(OnUnCheckedEventHandler));
        }

        public CornerRadioButton()
        {
            Loaded += CornerRadioButton_Loaded;
        }

        private void CornerRadioButton_Loaded(object sender, RoutedEventArgs e)
        {
            if (IsChecked ?? true)
            {
                OnCheckedEventHandler(this, new RoutedEventArgs());
            }
            else
            {
                OnUnCheckedEventHandler(this, new RoutedEventArgs());
            }
        }

        private static void OnCheckedEventHandler(object sender, RoutedEventArgs args)
        {
            var checkbox = sender as CornerRadioButton;
            if (checkbox != null)
            {
                if (checkbox.RadioButtonType == RadioButtonType.Normal)
                {
                    if (checkbox.Template != null)
                    {
                        Storyboard storyboard = new Storyboard();

                        var rectangle = checkbox.Template.FindName("indeterminateMark", checkbox) as Rectangle;
                        DoubleAnimationUsingKeyFrames doubleAnimation = new DoubleAnimationUsingKeyFrames()
                        {
                            KeyFrames =
                            {
                                new EasingDoubleKeyFrame{KeyTime=TimeSpan.Zero,Value=1},
                                new EasingDoubleKeyFrame{KeyTime=TimeSpan.FromSeconds(0.3),Value=0}
                            }
                        };

                        DoubleAnimationUsingKeyFrames doubleAnimation2 = new DoubleAnimationUsingKeyFrames()
                        {
                            KeyFrames =
                            {
                                new EasingDoubleKeyFrame{KeyTime=TimeSpan.Zero,Value=0},
                                new EasingDoubleKeyFrame{KeyTime=TimeSpan.FromSeconds(0.3),Value=5}
                            }
                        };

                        Storyboard.SetTarget(doubleAnimation, rectangle);
                        Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
                        storyboard.Children.Add(doubleAnimation);

                        Storyboard.SetTarget(doubleAnimation2, rectangle);
                        Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.X)"));
                        storyboard.Children.Add(doubleAnimation2);

                        storyboard.Begin();
                    }

                }

                if (checkbox.RadioButtonType == RadioButtonType.Switch)
                {
                    if (checkbox.Template != null)
                    {
                        Storyboard storyboard = new Storyboard();

                        {
                            var switchSquare = checkbox.Template.FindName("switchSquare", checkbox) as Border;
                            DoubleAnimationUsingKeyFrames doubleAnimation = new DoubleAnimationUsingKeyFrames
                            {
                                KeyFrames =
                            {
                                new EasingDoubleKeyFrame { KeyTime = TimeSpan.Zero, Value = 0 },
                                new EasingDoubleKeyFrame { KeyTime = TimeSpan.FromSeconds(0.2), Value = 10 }
                            }
                            };
                            Storyboard.SetTarget(doubleAnimation, switchSquare);
                            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                            storyboard.Children.Add(doubleAnimation);
                        }


                        {
                            var assistBorder = checkbox.Template.FindName("assistBorder", checkbox) as Border;
                            DoubleAnimationUsingKeyFrames doubleAnimation = new DoubleAnimationUsingKeyFrames
                            {
                                KeyFrames =
                            {
                                new EasingDoubleKeyFrame { KeyTime = TimeSpan.Zero, Value = 1 },
                                new EasingDoubleKeyFrame { KeyTime = TimeSpan.FromSeconds(0.2), Value = 0 }
                            }
                            };
                            Storyboard.SetTarget(doubleAnimation, assistBorder);
                            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(UIElement.Opacity)"));
                            storyboard.Children.Add(doubleAnimation);
                        }

                        storyboard.Begin();
                    }
                }
            }
        }

        private static void OnUnCheckedEventHandler(object sender, RoutedEventArgs args)
        {
            var checkbox = sender as CornerRadioButton;
            if (checkbox != null)
            {
                if (checkbox.RadioButtonType == RadioButtonType.Normal)
                {
                    if (checkbox.Template != null)
                    {
                        Storyboard storyboard = new Storyboard();

                        var rectangle = checkbox.Template.FindName("indeterminateMark", checkbox) as Rectangle;
                        DoubleAnimationUsingKeyFrames doubleAnimation = new DoubleAnimationUsingKeyFrames()
                        {
                            KeyFrames =
                            {
                                new EasingDoubleKeyFrame{KeyTime=TimeSpan.Zero,Value=1}
                            }
                        };

                        DoubleAnimationUsingKeyFrames doubleAnimation2 = new DoubleAnimationUsingKeyFrames()
                        {
                            KeyFrames =
                            {
                                new EasingDoubleKeyFrame{KeyTime=TimeSpan.Zero,Value=0}
                            }
                        };

                        Storyboard.SetTarget(doubleAnimation, rectangle);
                        Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
                        storyboard.Children.Add(doubleAnimation);

                        Storyboard.SetTarget(doubleAnimation2, rectangle);
                        Storyboard.SetTargetProperty(doubleAnimation2, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.X)"));
                        storyboard.Children.Add(doubleAnimation2);

                        storyboard.Begin();
                    }
                }

                if (checkbox.RadioButtonType == RadioButtonType.Switch)
                {
                    if (checkbox.Template != null)
                    {
                        Storyboard storyboard = new Storyboard();

                        {
                            var switchSquare = checkbox.Template.FindName("switchSquare", checkbox) as Border;
                            DoubleAnimationUsingKeyFrames doubleAnimation = new DoubleAnimationUsingKeyFrames
                            {
                                KeyFrames =
                            {
                                new EasingDoubleKeyFrame { KeyTime = TimeSpan.Zero, Value = 10 },
                                new EasingDoubleKeyFrame { KeyTime = TimeSpan.FromSeconds(0.2), Value = 0 }
                            }
                            };
                            Storyboard.SetTarget(doubleAnimation, switchSquare);
                            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                            storyboard.Children.Add(doubleAnimation);
                        }

                        {
                            var assistBorder = checkbox.Template.FindName("assistBorder", checkbox) as Border;
                            DoubleAnimationUsingKeyFrames doubleAnimation = new DoubleAnimationUsingKeyFrames
                            {
                                KeyFrames =
                            {
                                new EasingDoubleKeyFrame { KeyTime = TimeSpan.Zero, Value = 0 },
                                new EasingDoubleKeyFrame { KeyTime = TimeSpan.FromSeconds(0.2), Value = 1 }
                            }
                            };
                            Storyboard.SetTarget(doubleAnimation, assistBorder);
                            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(UIElement.Opacity)"));
                            storyboard.Children.Add(doubleAnimation);
                        }

                        storyboard.Begin();
                    }
                }
            }
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
