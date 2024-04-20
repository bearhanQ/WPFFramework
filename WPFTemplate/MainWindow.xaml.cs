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
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            double horizontalChange = e.VerticalChange;
            if (horizontalChange > 0) // 向右拖动，按钮变长
            {
                bd1.Height += horizontalChange;
            }
            else // 向左拖动，按钮变短
            {
                if (bd1.Height + horizontalChange > bd1.MinWidth) // 防止按钮宽度小于最小宽度
                {
                    bd1.Height += horizontalChange;
                }
            }
        }

        private void CornerButton_Click(object sender, RoutedEventArgs e)
        {
            double totalHeight = 0;
            foreach(var item in lb1.Items)
            {
                var a = item as FrameworkElement;
                totalHeight += a.ActualHeight;
            }
            bd1.Height = totalHeight;
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

        public List<People> Children { get; set; }
    }
}
