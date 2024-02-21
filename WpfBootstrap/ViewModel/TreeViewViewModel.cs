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
                    Name ="First",
                    Childs = new ObservableCollection<TreeViewModel>
                    {
                        new TreeViewModel
                        {
                            Name = "First-Child",
                            Childs= new ObservableCollection<TreeViewModel>
                            {
                                new TreeViewModel
                                {
                                    Name= "First-Grandchild"
                                }
                            }
                        }
                    }
                },
                new TreeViewModel
                {
                    Name ="Second",
                    Childs = new ObservableCollection<TreeViewModel>
                    {
                        new TreeViewModel
                        {
                            Name = "Second-Child",
                            Childs = new ObservableCollection<TreeViewModel>
                            {
                                new TreeViewModel
                                {
                                    Name= "Second-Grandchild"
                                }
                            }
                        }
                    }
                },
                new TreeViewModel
                {
                    Name ="Third",
                    Childs = new ObservableCollection<TreeViewModel>
                    {
                        new TreeViewModel
                        {
                            Name = "Third-Child",
                            Childs = new ObservableCollection<TreeViewModel>
                            {
                                new TreeViewModel
                                {
                                    Name= "Third-Grandchild"
                                }
                            }
                        }
                    }
                }
            };
        }
    }
}
