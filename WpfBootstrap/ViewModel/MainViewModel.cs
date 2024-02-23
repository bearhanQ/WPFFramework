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
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type windowType = assembly.GetType("WpfBootstrap.View." + parameter.ToString() + "View");
            object viewInstance = Activator.CreateInstance(windowType);
            var view = viewInstance as UserControl;
            Content = view;
        }
    }
}