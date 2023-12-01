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
    ///     <MyNamespace:CornerTextBox/>
    ///
    /// </summary>
    public class CornerTextBox : TextBox
    {
        static CornerTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerTextBox), new FrameworkPropertyMetadata(typeof(CornerTextBox)));
            EventManager.RegisterClassHandler(typeof(CornerTextBox), TextBox.GotFocusEvent, new RoutedEventHandler(OnGotFocuseEvent));
            EventManager.RegisterClassHandler(typeof(CornerTextBox), TextBox.LostFocusEvent, new RoutedEventHandler(OnLostFocuseEvent));
        }

        private static void OnGotFocuseEvent(object sender, RoutedEventArgs e)
        {
            var tb = sender as CornerTextBox;
            if (tb.FocusedHighLight)
            {
                var border = tb.Template.FindName("border", tb) as Border;
                if (tb != null)
                {
                    Storyboard storyboard = new Storyboard();

                    ColorAnimationUsingKeyFrames frames = new ColorAnimationUsingKeyFrames
                    {
                        KeyFrames = new ColorKeyFrameCollection
                    {
                        new EasingColorKeyFrame(Colors.Transparent, KeyTime.FromTimeSpan(TimeSpan.Zero)),
                        new EasingColorKeyFrame(((SolidColorBrush)tb.ShadowColor).Color, KeyTime.Paced)
                    },
                        Duration = TimeSpan.FromSeconds(0.5)
                    };
                    Storyboard.SetTarget(frames, border);
                    Storyboard.SetTargetProperty(frames, new PropertyPath("(Border.Effect).(DropShadowEffect.Color)"));
                    storyboard.Children.Add(frames);

                    storyboard.Begin();
                }
            }
        }

        private static void OnLostFocuseEvent(object sender, RoutedEventArgs e)
        {
            var tb = sender as CornerTextBox;
            if (tb.FocusedHighLight)
            {
                var border = tb.Template.FindName("border", tb) as Border;
                if (tb != null)
                {
                    Storyboard storyboard = new Storyboard();

                    ColorAnimationUsingKeyFrames frames = new ColorAnimationUsingKeyFrames
                    {
                        KeyFrames = new ColorKeyFrameCollection
                    {
                        new EasingColorKeyFrame(((SolidColorBrush)tb.ShadowColor).Color, KeyTime.FromTimeSpan(TimeSpan.Zero)),
                        new EasingColorKeyFrame(Colors.Transparent, KeyTime.Paced)
                    },
                        Duration = TimeSpan.FromSeconds(0.5)
                    };

                    Storyboard.SetTarget(frames, border);
                    Storyboard.SetTargetProperty(frames, new PropertyPath("(Border.Effect).(DropShadowEffect.Color)"));
                    storyboard.Children.Add(frames);
                    storyboard.Begin();
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
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerTextBox));


        public Brush ShadowColor
        {
            get { return (Brush)GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShadowColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShadowColorProperty =
            DependencyProperty.Register("ShadowColor", typeof(Brush), typeof(CornerTextBox), new PropertyMetadata(new SolidColorBrush(Colors.Black)));


        public TextBoxIcon Icon
        {
            get { return (TextBoxIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(TextBoxIcon), typeof(CornerTextBox), new PropertyMetadata(TextBoxIcon.None));


        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(CornerTextBox), new PropertyMetadata("Watermark"));

        public bool ShowWatermark
        {
            get { return (bool)GetValue(ShowWatermarkProperty); }
            set { SetValue(ShowWatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowWatermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowWatermarkProperty =
            DependencyProperty.Register("ShowWatermark", typeof(bool), typeof(CornerTextBox), new PropertyMetadata(true));



        public bool FocusedHighLight
        {
            get { return (bool)GetValue(FocusedHighLightProperty); }
            set { SetValue(FocusedHighLightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FocusedHighLight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FocusedHighLightProperty =
            DependencyProperty.Register("FocusedHighLight", typeof(bool), typeof(CornerTextBox), new PropertyMetadata(true));
    }
}
