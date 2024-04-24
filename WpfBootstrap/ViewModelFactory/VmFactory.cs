using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBootstrap.ViewModel;
using WPFTemplate;

namespace WpfBootstrap
{
    public class VmFactory
    {
        public MainViewModel Main
        {
            get
            {
                return WPFTemplate.ViewModelFactory.Instance.CreateInstance<MainViewModel>();
            }
        }

        public UnicodeViewModel Unicode
        {
            get
            {
                return WPFTemplate.ViewModelFactory.Instance.CreateInstance<UnicodeViewModel>();
            }
        }

        public TreeViewViewModel TreeView
        {
            get
            {
                return WPFTemplate.ViewModelFactory.Instance.CreateInstance<TreeViewViewModel>();
            }
        }

        public CornerPaginationViewModel Pagination
        {
            get
            {
                return WPFTemplate.ViewModelFactory.Instance.CreateInstance<CornerPaginationViewModel>();
            }
        }

        public HomeViewModel Home
        {
            get
            {
                return WPFTemplate.ViewModelFactory.Instance.CreateInstance<HomeViewModel>();
            }
        }
    }
}
