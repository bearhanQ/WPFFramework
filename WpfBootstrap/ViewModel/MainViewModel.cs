using GalaSoft.MvvmLight;
using System;
using System.Windows;
using System.Windows.Input;
using WpfBootstrap.Command;
using WpfBootstrap.View;

namespace WpfBootstrap.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand ButtonCommand => new CommandBase(ItemButtonCanExecute, ItemButtonExecute);

        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        public bool ItemButtonCanExecute(object parameter)
        {
            return true;
        }

        public void ItemButtonExecute(object parameter)
        {
            CornerTabControlView view = new CornerTabControlView();
            view.Show();
        }
    }
}