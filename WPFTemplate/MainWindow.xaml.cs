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
            list = new ObservableCollection<People>();
            Random rd = new Random();
            //for (int i = 0; i < 5; i++)
            //{
            //    list.Add(new People
            //    {
            //        Name = "2" + i.ToString(),
            //        Age = rd.Next(1, 200)
            //    });
            //}
            list.Add(new People
            {
                Name = "21",
                Age = 100
            });
            list.Add(new People
            {
                Name = "22",
                Age = 200
            });
            list.Add(new People
            {
                Name = "23",
                Age = 100
            });
            lc1.ItemsSource = list;
            lc2.ItemsSource = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random rd =new Random();
            list.Add(new People
            {
                Name = "30",
                Age = rd.Next(100,200)
            });
        }
    }

    public class People
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
