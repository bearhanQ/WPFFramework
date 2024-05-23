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
using Microsoft.Expression.Shapes;

namespace WPFTemplate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Man> Men { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Men = new ObservableCollection<Man>
            {
                new Man {Name="一月",Age=100},
                new Man {Name="二月",Age=200},
                new Man {Name="三月",Age=300},
                new Man {Name="四月",Age=250},
                new Man {Name="五月",Age=150},
                new Man {Name="六月",Age=50}
            };
        }
    }

    public class Man
    {
        public string Name { get; set; }

        public string NickName { get; set; }

        public int Age { get; set; }
    }
}
