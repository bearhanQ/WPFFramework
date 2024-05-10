using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfQQDemo
{
    public class QQUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Profile { get; set; }
        public Role Role { get; set; }
    }

    public enum Role
    {
        Leader,
        Manager,
        Customer
    }
}
