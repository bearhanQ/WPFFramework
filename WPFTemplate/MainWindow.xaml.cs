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
using System.Collections.Specialized;
using System.ComponentModel;

namespace WPFTemplate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<People> _list;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<People> list
        {
            get { return _list; }
            set
            {
                _list = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("list"));
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            list = new ObservableCollection<People>();
            list.Add(new People
            {
                Name = "张三"
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<People> peoples = new ObservableCollection<People>
            {
                new People
                {
                    Name = "李四"
                }
            };
            list = peoples;
        }
    }

    public class People
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
