using GalaSoft.MvvmLight;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using WpfBootstrap.Command;
using WpfBootstrap.View;

namespace WpfBootstrap.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand ItemCommand => new CommandBase(ItemCanExecute, ItemExecute);

        public MainViewModel()
        {

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
            var view = viewInstance as Window;
            view.Show();
        }
    }
}