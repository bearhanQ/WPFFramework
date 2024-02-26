using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfBootstrap.Command;
using WpfBootstrap.Model;
using WpfBootstrap.View;

namespace WpfBootstrap.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand ItemCommand => new CommandBase(ItemCanExecute, ItemExecute);

        private object _content;
        public object Content
        {
            get { return _content; }
            set
            {
                _content = value;
                RaisePropertyChanged();
            }
        }

        private Layout _layout;
        public Layout Layout
        {
            get { return _layout; }
            set 
            { 
                _layout = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeView view = new HomeView();
            Content = view;
        }

        public bool ItemCanExecute(object parameter)
        {
            return true;
        }

        public void ItemExecute(object parameter)
        {
            string viewname = parameter.ToString();
            var treeViewItem = parameter as TreeViewItem;
            if (treeViewItem != null)
            {
                var header = treeViewItem.Header;
                if (header is FrameworkElement)
                {
                    return;
                }
                viewname = header.ToString();
            }
            if (Enum.TryParse<Layout>(viewname, out Layout layout))
            {
                Layout = layout;
                return;
            }
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type windowType = assembly.GetType("WpfBootstrap.View." + viewname + "View");
            object viewInstance = Activator.CreateInstance(windowType);
            var view = viewInstance as UserControl;
            Content = view;
        }
    }

    public enum Layout
    {
        Horizontal,
        Vertical
    }
}