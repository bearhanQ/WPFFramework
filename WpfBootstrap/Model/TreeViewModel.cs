using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfBootstrap.Model
{
    public class TreeViewModel
    {
        public string Name { get; set; }
        public ObservableCollection<TreeViewModel> Childs { get; set; }
    }
}
