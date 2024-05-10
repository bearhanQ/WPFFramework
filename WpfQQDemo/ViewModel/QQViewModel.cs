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
        }
    }
}
