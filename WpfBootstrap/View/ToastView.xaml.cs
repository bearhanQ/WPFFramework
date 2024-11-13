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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFTemplate;

namespace WpfBootstrap.View
{
    /// <summary>
    /// ToastView.xaml 的交互逻辑
    /// </summary>
    public partial class ToastView : UserControl
    {
        public ToastType SelectedToastType
        {
            get { return (ToastType)GetValue(SelectedToastTypeProperty); }
            set { SetValue(SelectedToastTypeProperty, value); }
        }

        public static readonly DependencyProperty SelectedToastTypeProperty =
            DependencyProperty.Register("SelectedToastType", typeof(ToastType), typeof(ToastView), new PropertyMetadata(ToastType.Info));

        public string Msg
        {
            get { return (string)GetValue(MsgProperty); }
            set { SetValue(MsgProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Msg.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MsgProperty =
            DependencyProperty.Register("Msg", typeof(string), typeof(ToastView), new PropertyMetadata("Thank you for viewing my framework..."));

        public ToastView()
        {
            InitializeComponent();
        }

        private void CornerButton_Click(object sender, RoutedEventArgs e)
        {
            Toast.Show(Msg, SelectedToastType);
        }
    }
}
