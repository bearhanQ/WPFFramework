using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBootstrap.Model;

namespace WpfBootstrap.ViewModel
{
    public class TreeViewViewModel : ViewModelBase
    {
        public ObservableCollection<TreeViewModel> TreeViewModels { get; set; }

        public TreeViewViewModel()
        {
            TreeViewModels = new ObservableCollection<TreeViewModel>()
            {
                new TreeViewModel
                {
                    Header ="First",
                    Childs = new ObservableCollection<TreeViewModel>
                    {
                        new TreeViewModel
                        {
                            Header = "First-Child",
                            Childs= new ObservableCollection<TreeViewModel>
                            {
                                new TreeViewModel
                                {
                                    Header= "First-Grandchild"
                                }
                            }
                        }
                    }
                },
                new TreeViewModel
                {
                    Header ="Second",
                    Childs = new ObservableCollection<TreeViewModel>
                    {
                        new TreeViewModel
                        {
                            Header = "Second-Child",
                            Childs = new ObservableCollection<TreeViewModel>
                            {
                                new TreeViewModel
                                {
                                    Header= "Second-Grandchild"
                                }
                            }
                        }
                    }
                },
                new TreeViewModel
                {
                    Header ="Third",
                    Childs = new ObservableCollection<TreeViewModel>
                    {
                        new TreeViewModel
                        {
                            Header = "Third-Child",
                            Childs = new ObservableCollection<TreeViewModel>
                            {
                                new TreeViewModel
                                {
                                    Header= "Third-Grandchild"
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
