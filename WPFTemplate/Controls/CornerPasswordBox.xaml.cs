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

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value);}
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

        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Watermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(CornerPasswordBox), new PropertyMetadata("Password"));


        public bool ShowWatermark
        {
            get { return (bool)GetValue(ShowWatermarkProperty); }
            set { SetValue(ShowWatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowWatermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowWatermarkProperty =
            DependencyProperty.Register("ShowWatermark", typeof(bool), typeof(CornerPasswordBox), new PropertyMetadata(true));

        public string Icon
        {
            get { return (string)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Icon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(string), typeof(CornerPasswordBox), new PropertyMetadata(null));

        public bool ShowEye
        {
            get { return (bool)GetValue(ShowEyeProperty); }
            set { SetValue(ShowEyeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowEye.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowEyeProperty =
            DependencyProperty.Register("ShowEye", typeof(bool), typeof(CornerPasswordBox), new PropertyMetadata(true));

        public CornerPasswordBox()
        {
            InitializeComponent();
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

            base.OnApplyTemplate();
        }
    }
}
