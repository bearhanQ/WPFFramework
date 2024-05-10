using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfQQDemo
{
    public class Db
    {
        private static List<QQGroup> _groups;
        public static List<QQGroup> Groups
        {
            get
            {
                if (_groups == null)
                {
                    _groups = new List<QQGroup>()
                    {
                        new QQGroup
                        {
                            GroupId = 1,
                            GroupName=".net技术交流群",
                            Image = @"pack://application:,,,/WpfQQDemo;component/Resources/profile5.jpg"
                        },
                        new QQGroup
                        {
                            GroupId = 2,
                            GroupName="Java技术交流群",
                            Image = @"pack://application:,,,/WpfQQDemo;component/Resources/profile6.jpg"
                        },
                        new QQGroup
                        {
                            GroupId = 3,
                            GroupName="PHP技术交流群",
                            Image = @"pack://application:,,,/WpfQQDemo;component/Resources/profile7.jpg"
                        }
                    };
                }
                return _groups;
            }
        }


        private static List<QQUser> _users;
        public static List<QQUser> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new List<QQUser>
                    {
                        new QQUser
                        {
                            UserId = 1,
                            UserName="曹操",
                            Role=Role.Leader,
                            Profile = @"pack://application:,,,/WpfQQDemo;component/Resources/profile8.jpg"
                        },
                        new QQUser
                        {
                            UserId = 2,
                            UserName="刘备",
                            Role=Role.Manager,
                            Profile = @"pack://application:,,,/WpfQQDemo;component/Resources/profile9.jpg"
                        },
                        new QQUser
                        {
                            UserId = 3,
                            UserName="张飞",
                            Role=Role.Manager,
                            Profile = @"pack://application:,,,/WpfQQDemo;component/Resources/profile5.jpg"
                        },
                        new QQUser
                        {
                            UserId = 4,
                            UserName="董卓",
                            Role=Role.Customer,
                            Profile = @"pack://application:,,,/WpfQQDemo;component/Resources/profile6.jpg"
                        },
                    };
                }
                return _users;
            }
        }


        private static List<QQRelation> _relations;
        public static List<QQRelation> Relations
        {
            get
            {
                if(_relations == null)
                {
                    _relations = new List<QQRelation>
                    {
                        new QQRelation
                        {
                            GroupId = 1,
                            UserId = 1
                        },
                        new QQRelation
                        {
                            GroupId = 1,
                            UserId = 2
                        },

                        new QQRelation
                        {
                            GroupId = 2,
                            UserId = 1
                        },
                        new QQRelation
                        {
                            GroupId = 2,
                            UserId = 2
                        },
                        new QQRelation
                        {
                            GroupId = 2,
                            UserId = 3
                        },

                        new QQRelation
                        {
                            GroupId = 3,
                            UserId = 1
                        },
                        new QQRelation
                        {
                            GroupId = 3,
                            UserId = 2
                        },
                        new QQRelation
                        {
                            GroupId = 3,
                            UserId = 3
                        },
                        new QQRelation
                        {
                            GroupId = 3,
                            UserId = 4
                        },
                    };
                }
                return _relations;
            }
        }
    }
}
