using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTemplate
{

    public class TestPassword : INotifyPropertyChanged
    {
        private static TestPassword _testPassword;

        public static TestPassword Instance
        {
            get
            {
                if (_testPassword == null)
                {
                    _testPassword = new TestPassword();
                }
                return _testPassword;
            }
        }

        private TestPassword()
        {

        }

        private string _password;

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Password"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class Node : INotifyPropertyChanged
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
