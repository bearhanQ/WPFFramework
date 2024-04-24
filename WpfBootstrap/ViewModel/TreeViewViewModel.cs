using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBootstrap.Model;
using WPFTemplate;

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
                    Children = new ObservableCollection<TreeViewModel>
                    {
                        new TreeViewModel
                        {
                            Header = "First-Child",
                            Children= new ObservableCollection<TreeViewModel>
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
                    Children = new ObservableCollection<TreeViewModel>
                    {
                        new TreeViewModel
                        {
                            Header = "Second-Child",
                            Children = new ObservableCollection<TreeViewModel>
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
                    Children = new ObservableCollection<TreeViewModel>
                    {
                        new TreeViewModel
                        {
                            Header = "Third-Child",
                            Children = new ObservableCollection<TreeViewModel>
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
