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
    /// <summary>
    /// CornerPasswordBox.xaml 的交互逻辑
    /// </summary>
    public partial class CornerPasswordBox : UserControl
    {
        private bool isPasswordChanged = false;
        private PasswordBox BasePasswordBox;
        private Border ShadowBorder;

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerPasswordBox));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(CornerPasswordBox),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnPasswordChanged), null, false, UpdateSourceTrigger.PropertyChanged));

        public Brush ShadowColor
        {
            get { return (Brush)GetValue(ShadowColorProperty); }
            set { SetValue(ShadowColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShadowColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShadowColorProperty =
            DependencyProperty.Register("ShadowColor", typeof(Brush), typeof(CornerPasswordBox), new PropertyMetadata(new SolidColorBrush(Colors.Black)));


        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(CornerPasswordBox), new PropertyMetadata("Password"));

        public CornerPasswordBox()
        {
            InitializeComponent();
            this.GotFocus += CornerPasswordBox_GotFocus;
            this.LostFocus += CornerPasswordBox_LostFocus;
        }

        private void CornerPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.ShadowBorder != null)
            {
                Storyboard storyboard = new Storyboard();
                ColorAnimationUsingKeyFrames frames = new ColorAnimationUsingKeyFrames
                {
                    KeyFrames = new ColorKeyFrameCollection
                    {
                        new EasingColorKeyFrame(((SolidColorBrush)this.ShadowColor).Color, KeyTime.FromTimeSpan(TimeSpan.Zero)),
                        new EasingColorKeyFrame(Colors.Transparent, KeyTime.Paced)
                    },
                    Duration = TimeSpan.FromSeconds(0.5)
                };
                Storyboard.SetTarget(frames, ShadowBorder);
                Storyboard.SetTargetProperty(frames, new PropertyPath("(Border.Effect).(DropShadowEffect.Color)"));
                storyboard.Children.Add(frames);
                storyboard.Begin();
            }
        }

        private void CornerPasswordBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ShadowBorder != null)
            {
                Storyboard storyboard = new Storyboard();
                ColorAnimationUsingKeyFrames frames = new ColorAnimationUsingKeyFrames
                {
                    KeyFrames = new ColorKeyFrameCollection
                    {
                        new EasingColorKeyFrame(Colors.Transparent, KeyTime.FromTimeSpan(TimeSpan.Zero)),
                        new EasingColorKeyFrame(((SolidColorBrush)this.ShadowColor).Color, KeyTime.Paced)
                    },
                    Duration = TimeSpan.FromSeconds(0.5)
                };
                Storyboard.SetTarget(frames, ShadowBorder);
                Storyboard.SetTargetProperty(frames, new PropertyPath("(Border.Effect).(DropShadowEffect.Color)"));
                storyboard.Children.Add(frames);
                storyboard.Begin();
            }
        }

        private static void OnPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CornerPasswordBox passwordBox)
            {
                passwordBox.UpdatePassword();
            }
        }

        private void UpdatePassword()
        {
            if (!this.isPasswordChanged)
            {
                BasePasswordBox.Password = Password;
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.isPasswordChanged = true;
            Password = BasePasswordBox.Password;
            this.isPasswordChanged = false;
        }

        public override void OnApplyTemplate()
        {
            var passwordbox = this.Template.FindName("passwordBox", this);
            if (passwordbox != null)
            {
                BasePasswordBox = passwordbox as PasswordBox;
            }

            var border = this.Template.FindName("PART_Border", this);
            if (border != null)
            {
                ShadowBorder = border as Border;
            }

            base.OnApplyTemplate();
        }
    }
}
