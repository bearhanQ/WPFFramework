using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfQQDemo
{
    public class VmFactory
    {
        public QQViewModel Main
        {
            get
            {
                return WPFTemplate.ViewModelFactory.Instance.CreateInstance<QQViewModel>();
            }
        }
    }
}
