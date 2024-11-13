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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPFTemplate.Controls.Extra
{
    /// <summary>
    /// ToastWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ToastWindow : Window
    {
        public static readonly DependencyProperty ToastTypeProperty =
            DependencyProperty.Register("ToastType", typeof(ToastType), typeof(ToastWindow), new PropertyMetadata(ToastType.Info));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ToastWindow), new PropertyMetadata(string.Empty));

        public ToastType ToastType
        {
            get { return (ToastType)GetValue(ToastTypeProperty); }
            set { SetValue(ToastTypeProperty, value); }
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public ToastWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3.5);
            timer.Tick += (a, b) =>
            {
                this.Close();
            };
            timer.Start();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var animation = FindResource("FadeInOutStoryboard") as Storyboard;
            animation.Begin();
        }
    }
}
