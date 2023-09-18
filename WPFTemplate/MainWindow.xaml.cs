using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    }

    public class Node:INotifyPropertyChanged
    {
        public Node()
        {
            this.ChildNode = new List<Node>();
        }
        public string Name { get; set; }

        public int Age { get; set; }

        private bool _check;

        public bool Check
        {
            get { return _check; }
            set 
            { 
                _check = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Check"));
                }
            }
        }

        public List<Node> ChildNode { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
