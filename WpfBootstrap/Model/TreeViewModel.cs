using System.Collections.ObjectModel;

namespace WpfBootstrap.Model
{
    public class TreeViewModel
    {
        public string Header { get; set; }
        public string Icon { get; set; }
        public ObservableCollection<TreeViewModel> Children { get; set; }
    }
}
