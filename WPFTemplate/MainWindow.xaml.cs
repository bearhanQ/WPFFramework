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
        ObservableCollection<Man> data;
        public MainWindow()
        {
            InitializeComponent();
            data = new ObservableCollection<Man>
            {
                new Man{Name="123"},
                new Man{Name="321"},
                new Man{Name="456"},
            };
            listbox1.ItemsSource = data;
            listbox1.DisplayMemberPath = "Name";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Add();
                //tb1.Dispatcher.Invoke(() =>
                //{
                //    tb1.Text = "123";
                //});
            });
        }

        private void Add()
        {
            listbox1.Dispatcher.Invoke(() =>
            {
                data.Add(new Man { Name = "new item" });
            });
        }
    }

    class Man
    {
        public string Name { get; set; }
    }
}
