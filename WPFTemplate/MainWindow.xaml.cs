using System.Data;
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
using Microsoft.Expression.Shapes;
using System.Windows.Media;

namespace WPFTemplate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<People> list;

        public PathSegmentCollection psc { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }
    }

    public class People
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
