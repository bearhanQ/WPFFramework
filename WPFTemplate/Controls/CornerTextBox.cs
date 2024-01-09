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
