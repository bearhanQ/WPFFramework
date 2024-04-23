using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFTemplate
{
    /// <summary>
    /// NotifyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyWindow : Window,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<NotifyModel> _messages;
        internal ObservableCollection<NotifyModel> Messages
        {
            get { return _messages; }
            set 
            { 
                _messages = value;
                NotifyNewMsgCount();
            }
        }
        public int NewMsgCount
        {
            get
            {
                if (Messages != null)
                {
                    return Messages.Where(x => !x.IsRead).Count();
                }
                return 0;
            }
        }

        public NotifyWindow()
        {
            InitializeComponent();
            Messages = new ObservableCollection<NotifyModel>();
            listbox1.ItemsSource = Messages;
        }
        private void ShowMessage(NotifyModel model)
        {
            if (this.Visibility != Visibility.Visible)
            {
                this.Visibility = Visibility.Visible;
            }
            Messages.Insert(0, model);
            NotifyNewMsgCount();
        }
        public void SendMessage(string message)
        {
            NotifyModel model = new NotifyModel();
            model.Source = NotifySourceEnum.System;
            model.Message = message;
            ShowMessage(model);
        }
        public void SendMessage(NotifySourceEnum source, string message)
        {
            NotifyModel model = new NotifyModel();
            model.Source = source;
            model.Message = message;
            ShowMessage(model);
        }
        public void Clear()
        {
            this.Messages.Clear();
            NotifyNewMsgCount();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var screenWidth = SystemParameters.WorkArea.Width;
            var screenHeight = SystemParameters.WorkArea.Height;
            this.Left = screenWidth - this.Width;
            this.Top = screenHeight - this.Height;
            this.Topmost = true;
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn != null)
            {
                var item = LocalVisualTreeHelper.GetParent(btn, typeof(ListBoxItem));
                if (item != null)
                {
                    var model = listbox1.ItemContainerGenerator.ItemFromContainer(item) as NotifyModel;
                    if (model != null)
                    {
                        Messages.Remove(model);
                        NotifyNewMsgCount();
                    }
                }
            }
        }
        private void ListBoxItem_MouseEnter(object sender, MouseEventArgs e)
        {
            var item = sender as ListBoxItem;
            var model = listbox1.ItemContainerGenerator.ItemFromContainer(item) as NotifyModel;
            if (model != null)
            {
                model.IsRead = true;
            }
            NotifyNewMsgCount();
        }
        private void NotifyNewMsgCount()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("NewMsgCount"));
            }
        }
    }

    public class NotifyModel
    {
        public NotifySourceEnum Source { get; set; }
        public string Time
        {
            get
            {
                return DateTime.Now.ToString("yyyy/MM/dd HH/mm/ss");
            }
        }
        public string ShortTime
        {
            get
            {
                return Time.Substring(0, 10);
            }
        }
        public string Message { get; set; }
        public string ShortMsg
        {
            get
            {
                return Message.Length >= 15 ? Message.Substring(0, 15) + "..." : Message;
            }
        }
        public bool IsRead { get; set; }
    }
}
