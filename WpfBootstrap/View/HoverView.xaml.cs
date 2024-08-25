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

namespace WpfBootstrap.View
{
    /// <summary>
    /// HoverView.xaml 的交互逻辑
    /// </summary>
    public partial class HoverView : UserControl
    {
        public HoverView()
        {
            InitializeComponent();
            gd1.MouseMove += MainGrid_MouseMove;
            gd1.MouseLeave += Bd1_MouseLeave;
            gd1.MouseEnter += Bd1_MouseEnter;
        }

        private void Bd1_MouseEnter(object sender, MouseEventArgs e)
        {
            bd2.Visibility = Visibility.Visible;
        }

        private void Bd1_MouseLeave(object sender, MouseEventArgs e)
        {
            bd2.Visibility = Visibility.Hidden;
        }

        private void MainGrid_MouseMove(object sender, MouseEventArgs e)
        {
            //这里获取一下鼠标的坐标，然后让clip效果的中心跟着鼠标中心移动
            Point mousePosition = e.GetPosition(gd1);
            eg1.Center = new Point(mousePosition.X, mousePosition.Y);
        }
    }
}
