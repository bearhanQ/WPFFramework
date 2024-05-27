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

namespace WPFTemplate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<People> list;
        public MainWindow()
        {
            InitializeComponent();
            list = new ObservableCollection<People>();
            Random rd = new Random();
            for(int i = 0; i < 200; i++)
            {
                list.Add(new People
                {
                    Name = "22",
                    Age = rd.Next(1, 200)
                });
            }
            for (int i = 200; i < 800; i++)
            {
                list.Add(new People
                {
                    Name = "22",
                    Age = rd.Next(200, 400)
                });
            }
            for (int i = 800; i < 1000; i++)
            {
                list.Add(new People
                {
                    Name = "22",
                    Age = rd.Next(1, 200)
                });
            }
            lc1.ItemsSource = list;
        }
    }

    public class People
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
