using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using Wpf063Demo.Model;

namespace Wpf063Demo.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<MainFunction> Functions { get; set; }

        public MainViewModel()
        {
            Functions = new ObservableCollection<MainFunction>
            {
                new MainFunction{Name="木马查杀",Image=@"/Resources/smallpng/mmcs.png"},
                new MainFunction{Name="清理加速",Image=@"/Resources/smallpng/qljs.png"},
                new MainFunction{Name="系统·驱动",Image=@"/Resources/smallpng/xtqd.png"},
                new MainFunction{Name="网络安全",Image=@"/Resources/smallpng/wlaq.png"},
                new MainFunction{Name="数据安全",Image=@"/Resources/smallpng/sjan.png"},
                new MainFunction{Name="软件管家",Image=@"/Resources/smallpng/rjgj.png"}
            };
        }
    }
}