using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfQQDemo
{
    public class GroupUser
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string Time { get; set; }
        public string Image { get; set; }
        public List<QQUser> Users { get; set; }
    }
}
