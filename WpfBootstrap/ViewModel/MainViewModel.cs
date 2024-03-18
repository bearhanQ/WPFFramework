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

        public ObservableCollection<TreeViewModel> VerticalMenuCollection { get; set; }

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

            VerticalMenuCollection = new ObservableCollection<TreeViewModel>()
            {
                new TreeViewModel
                {
                    Header = "Home",
                    Icon = "\ue7c6"
                },
                new TreeViewModel
                {
                    Header = "Original",
                    Icon = "\ue690",
                    Childs = new ObservableCollection<TreeViewModel>
                    {
                        new TreeViewModel { Header = "CornerButton"},
                        new TreeViewModel { Header = "CornerCheckBox"},
                        new TreeViewModel { Header = "CornerComboBox"},
                        new TreeViewModel { Header = "CornerContextMenu"},
                        new TreeViewModel { Header = "CornerPasswordBox"},
                        new TreeViewModel { Header = "CornerProgressBar"},
                        new TreeViewModel { Header = "CornerRadioButton"},
                        new TreeViewModel { Header = "CornerTabControl"},
                        new TreeViewModel { Header = "CornerTreeView"}
                    }
                },
                new TreeViewModel
                {
                    Header = "Extra",
                    Icon = "\ue7df",
                    Childs = new ObservableCollection<TreeViewModel>
                    {
                        new TreeViewModel { Header = "Carousel"},
                        new TreeViewModel { Header = "ColorSlider"},
                        new TreeViewModel { Header = "CornerMultiComboBox"},
                        new TreeViewModel { Header = "CornerPagination"},
                        new TreeViewModel { Header = "ReminderCalendar"},
                        new TreeViewModel { Header = "ReminderDatePicker"},
                        new TreeViewModel { Header = "NotifyWindow"},
                        new TreeViewModel { Header = "PlaceControl"}
                    }
                },
                new TreeViewModel
                {
                    Header = "Layout",
                    Icon = "\ue7c6",
                    Childs = new ObservableCollection<TreeViewModel>
                    {
                        new TreeViewModel { Header = "Vertical"},
                        new TreeViewModel { Header = "Horizontal"},
                    }
                },
                new TreeViewModel
                {
                    Header = "Unicode", 
                    Icon = "\ue7c6" 
                },
                new TreeViewModel
                {
                    Header = "Charts",
                    Icon = "\ue7c6"
                }
            };
        }

        public bool ItemCanExecute(object parameter)
        {
            return true;
        }

        public void ItemExecute(object parameter)
        {
            string viewname = parameter.ToString();
            if (Enum.TryParse<Layout>(viewname, out Layout layout))
            {
                Layout = layout;
                return;
            }
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type windowType = assembly.GetType("WpfBootstrap.View." + viewname + "View");
            if (windowType != null)
            {
                object viewInstance = Activator.CreateInstance(windowType);
                if (viewInstance is UserControl view)
                {
                    Content = view;
                }
                if (viewInstance is Window window)
                {
                    window.Show();
                }
            }
        }
    }

    public enum Layout
    {
        Vertical,
        Horizontal,
    }
}