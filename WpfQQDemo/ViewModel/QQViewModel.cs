using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WPFTemplate;

namespace WpfQQDemo
{
    public class QQViewModel : ViewModelBase
    {
        public ObservableCollection<GroupUser> GroupUsers { get; set; }

        public ObservableCollection<QQMessage> QQMessages { get; set; }

        public CommandBase SendMessage { get; set; }

        public QQViewModel()
        {
            var list = Db.Groups.Select(g =>
            new GroupUser
            {
                GroupId = g.GroupId,
                GroupName = g.GroupName,
                Image = g.Image,
                Time = g.Time,
                Users = Db.Users.Join(Db.Relations, u => u.UserId, gu => gu.UserId, (u, gu) =>
                new { User = u, GroupId = gu.GroupId }).Where(x => x.GroupId == g.GroupId).Select(x => x.User).ToList()
            }).ToList();

            GroupUsers = new ObservableCollection<GroupUser>(list);

            QQMessages = new ObservableCollection<QQMessage>();

            SendMessage = new CommandBase(SendAction, CanSend);
        }

        private void SendAction(object parameter)
        {
            var textBox = parameter as CornerTextBox;
            QQMessage item = new QQMessage();
            item.Profile = "pack://application:,,,/WpfQQDemo;component/Resources/profile5.jpg";
            item.UserName = "曹操";
            item.Time = DateTime.Now.ToString("HH:mm:ss");
            item.Message = textBox.Text;
            QQMessages.Add(item);
            textBox.Clear();
        }

        private bool CanSend(object parameter)
        {
            return true;
        }
    }
}
