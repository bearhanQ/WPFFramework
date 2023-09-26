using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Customer> customers = new List<Customer>
            {
                new Customer
                {
                    FirstName="1",
                    LastName="2",
                    Status=OrderStatus.None,
                    IsMember=true,
                },
                new Customer
                {
                    FirstName="3",
                    LastName="4",
                    Status=OrderStatus.New,
                    IsMember=false,
                },
                new Customer
                {
                    FirstName="5",
                    LastName="6",
                    Status=OrderStatus.Received,
                    IsMember=false,
                }
            };

            for (int i = 0; i < 100; i++)
            {
                if (i % 2 == 0)
                {
                    customers.Add(new Customer
                    {
                        FirstName = i.ToString(),
                        LastName = "2",
                        Status = OrderStatus.Received,
                        IsMember = true,
                    });
                }
                else
                {
                    customers.Add(new Customer
                    {
                        FirstName = i.ToString(),
                        LastName = "2",
                        Status = OrderStatus.New,
                        IsMember = true,
                    });
                }
            }

            var db = ModelConvertHelper.ListToDataTable(customers);
            cb.ItemsSource = db.DefaultView;
            cb.DisplayMemberPath = "FirstName";
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("1");
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

    //Defines the customer object
    public class Customer : NotifyPropertyChangedBase
    {
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged();
            }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; RaisePropertyChanged(); }
        }

        private bool _isMemeber;

        public bool IsMember
        {
            get { return _isMemeber; }
            set { _isMemeber = value; RaisePropertyChanged(); }
        }

        private OrderStatus _status;

        public OrderStatus Status
        {
            get { return _status; }
            set { _status = value; RaisePropertyChanged(); }
        }
    }

    public enum OrderStatus { None, New, Processing, Shipped, Received };
}
