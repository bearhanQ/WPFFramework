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
    /// NotifyWindowView.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyWindowView : UserControl
    {
        readonly NotifyWindow window;

        public NotifyWindowView()
        {
            InitializeComponent();
            window = new NotifyWindow();

            Binding bd = new Binding();
            bd.Source = window;
            bd.Path = new PropertyPath("NewMsgCount");
            tbIsRead.SetBinding(TextBlock.TextProperty, bd);
        }

        private void CornerButton_Click(object sender, RoutedEventArgs e)
        {
            window.SendMessage((NotifySourceEnum)Enum.Parse(typeof(NotifySourceEnum),cbType.Text), tbMessage.Text);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            window.Owner = LocalLogicalTreeHelper.GetParent(this, typeof(Window)) as Window;
        }
    }
}
