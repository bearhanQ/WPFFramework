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
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFTemplate"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFTemplate;assembly=WPFTemplate"
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
    ///     <MyNamespace:CornerButton/>
    ///
    /// </summary>
    public class CornerButton : Button
    {
        static CornerButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerButton), new FrameworkPropertyMetadata(typeof(CornerButton)));
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
            {
                EventManager.RegisterClassHandler(typeof(CornerButton), Button.MouseEnterEvent, new RoutedEventHandler(OnButtonMouseEnter));
                EventManager.RegisterClassHandler(typeof(CornerButton), Button.MouseLeaveEvent, new RoutedEventHandler(OnButtonMouseLeave));
            }
        }

        private static void OnButtonMouseEnter(object sender, RoutedEventArgs e)
        {
            var button = sender as CornerButton;
            var border = VisualTreeHelper.GetChild(button, 0);
            var buttonStyle = (ButtonStyle)button.GetValue(ButtonStyleProperty);
            
            if (buttonStyle == ButtonStyle.BorderOnly)
            {
                var background = (SolidColorBrush)button.Background;
                //var foreground = (SolidColorBrush)button.Foreground;
                if (background != null)
                {
                    var storyboard = new Storyboard();
                    
                    //background animation
                    ColorAnimationUsingKeyFrames animation = new ColorAnimationUsingKeyFrames
                    {
                        KeyFrames = {
                            new LinearColorKeyFrame(Colors.Transparent, KeyTime.FromTimeSpan(TimeSpan.Zero)),
                            new LinearColorKeyFrame(background.Color, KeyTime.Paced)
                        },
                        Duration = TimeSpan.FromSeconds(0.5)
                    };
                    Storyboard.SetTarget(animation, border);
                    Storyboard.SetTargetProperty(animation, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));

                    ////foreground animation
                    //ColorAnimationUsingKeyFrames foreanimation = new ColorAnimationUsingKeyFrames
                    //{
                    //    KeyFrames ={
                    //        new LinearColorKeyFrame(foreground.Color, KeyTime.FromTimeSpan(TimeSpan.Zero)),
                    //        new LinearColorKeyFrame(Colors.White, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)))
                    //    },
                    //    Duration = TimeSpan.FromSeconds(1)
                    //};
                    //Storyboard.SetTarget(foreanimation, button);
                    //Storyboard.SetTargetProperty(foreanimation, new PropertyPath("(Button.Foreground).(SolidColorBrush.Color)"));

                    storyboard.Children.Add(animation);
                    //storyboard.Children.Add(foreanimation);
                    storyboard.Begin();
                }
            }

            if (buttonStyle == ButtonStyle.Normal)
            {
                var hovercolor = button.HoverBackground;
                if (hovercolor != null)
                {
                    border.SetValue(Border.BackgroundProperty, hovercolor);
                }
            }
        }

        private static void OnButtonMouseLeave(object sender, RoutedEventArgs e)
        {
            var button = sender as CornerButton;
            var border = VisualTreeHelper.GetChild(button, 0);
            var buttonStyle = (ButtonStyle)button.GetValue(ButtonStyleProperty);
            if (buttonStyle == ButtonStyle.BorderOnly)
            {
                var background = (SolidColorBrush)button.Background;
                //var foreground = (SolidColorBrush)button.Foreground;
                if (background != null)
                {
                    var storyboard = new Storyboard();
                    
                    //background animation
                    ColorAnimationUsingKeyFrames animation = new ColorAnimationUsingKeyFrames
                    {
                        KeyFrames = {
                            new LinearColorKeyFrame(background.Color, KeyTime.FromTimeSpan(TimeSpan.Zero)),
                            new LinearColorKeyFrame(Colors.Transparent, KeyTime.Paced)
                        },
                        Duration = TimeSpan.FromSeconds(0.5)
                    };
                    Storyboard.SetTarget(animation, border);
                    Storyboard.SetTargetProperty(animation, new PropertyPath("(Border.Background).(SolidColorBrush.Color)"));

                    ////foreground animation
                    //ColorAnimationUsingKeyFrames foreanimation = new ColorAnimationUsingKeyFrames
                    //{
                    //    KeyFrames ={
                    //        new LinearColorKeyFrame(Colors.White, KeyTime.FromTimeSpan(TimeSpan.Zero)),
                    //        new LinearColorKeyFrame(foreground.Color, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1)))
                    //    },
                    //    Duration = TimeSpan.FromSeconds(1)
                    //};
                    //Storyboard.SetTarget(foreanimation, button);
                    //Storyboard.SetTargetProperty(foreanimation, new PropertyPath("(Button.Foreground).(SolidColorBrush.Color)"));

                    storyboard.Children.Add(animation);
                    //storyboard.Children.Add(foreanimation);
                    storyboard.Begin();
                }
            }

            if (buttonStyle == ButtonStyle.Normal)
            {
                var background = button.Background;
                if (background != null)
                {
                    border.SetValue(Border.BackgroundProperty, background);
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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerButton));

        public ButtonStyle ButtonStyle
        {
            get { return (ButtonStyle)GetValue(ButtonStyleProperty); }
            set { SetValue(ButtonStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonStyleProperty =
            DependencyProperty.Register("ButtonStyle", typeof(ButtonStyle), typeof(CornerButton), new PropertyMetadata(ButtonStyle.Normal));


        public ButtonType ButtonType
        {
            get { return (ButtonType)GetValue(ButtonTypeProperty); }
            set { SetValue(ButtonTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ButtonType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ButtonTypeProperty =
            DependencyProperty.Register("ButtonType", typeof(ButtonType), typeof(CornerButton), new PropertyMetadata(ButtonType.None));

        public Brush HoverBackground
        {
            get { return (Brush)GetValue(HoverBackgroundProperty); }
            set { SetValue(HoverBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoverBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverBackgroundProperty =
            DependencyProperty.Register("HoverBackground", typeof(Brush), typeof(CornerButton));

    }
}
