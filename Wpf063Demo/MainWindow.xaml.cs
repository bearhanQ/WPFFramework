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

namespace Wpf063Demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            pc1.IsOpen = true;
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            pc2.IsOpen = true;
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            pc3.IsOpen = true;
        }

        private void btn4_MouseEnter(object sender, MouseEventArgs e)
        {
            pc4.IsOpen = true;
        }
    }
}
