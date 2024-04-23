using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace WPFTemplate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            window.Show();
            Binding bd = new Binding();
            bd.Source = window;
            bd.Path = new PropertyPath("NewMsgCount");
            labelClock.SetBinding(Label.ContentProperty, bd);
        }

        NotifyWindow window = new NotifyWindow();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            window.SendMessage("我有点晕了");
            window.SendMessage(NotifySourceEnum.Warn, "我有点晕了 我有点晕了 我有点晕了 我有点晕了 我有点晕了 我有点晕了");
            window.SendMessage(NotifySourceEnum.Error, "我有点晕了");
        }
    }

    public class People
    {
        public People()
        {
            Children = new List<People>();
        }

        public string Name { get; set; }

        public string Icon { get; set; }

        public string Message { get; set; }

        public List<People> Children { get; set; }
    }
}
