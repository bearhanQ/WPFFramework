using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBootstrap.Model;
using WPFTemplate;

namespace WpfBootstrap.ViewModel
{
    public class UnicodeViewModel : ViewModelBase
    {
        private List<UnicodeModel> _unicodes;

        public List<UnicodeModel> Unicodes
        {
            get { return _unicodes; }
            set
            {
                _unicodes = value;
                RaisePropertyChanged("Unicodes");
            }
        }

        public CommandBase CopyCommand { get; set; }

        public UnicodeViewModel()
        {
            Unicodes = new List<UnicodeModel>
            {
                new UnicodeModel{Name ="edit",Code = "&#xe613;"},
                new UnicodeModel{Name ="computer",Code = "&#xe62f;"},
                new UnicodeModel{Name ="protection",Code = "&#xe663;"},
                new UnicodeModel{Name ="update",Code = "&#xe625;"},
                new UnicodeModel{Name ="cart",Code = "&#xe602;"},
                new UnicodeModel{Name ="twitter",Code = "&#xe736;"},
                new UnicodeModel{Name ="facebook",Code = "&#xe8e7;"},
                new UnicodeModel{Name ="dollar",Code = "&#xe82f;"},
                new UnicodeModel{Name ="fall",Code = "&#xe7f2;"},
                new UnicodeModel{Name ="rise",Code = "&#xe7f3;"},
                new UnicodeModel{Name ="dbright",Code = "&#xe713;"},
                new UnicodeModel{Name ="dbleft",Code = "&#xe714;"},
                new UnicodeModel{Name ="right",Code = "&#xe616;"},
                new UnicodeModel{Name ="left",Code = "&#xe617;"},
                new UnicodeModel{Name ="up",Code = "&#xe619;"},
                new UnicodeModel{Name ="down",Code = "&#xe601;"},
                new UnicodeModel{Name ="heart",Code = "&#xe60a;"},
                new UnicodeModel{Name ="github",Code = "&#xe6ea;"},
                new UnicodeModel{Name ="document",Code = "&#xe6f3;"},
                new UnicodeModel{Name ="home",Code = "&#xe7c6;"},
                new UnicodeModel{Name ="star",Code = "&#xe7df;"},
                new UnicodeModel{Name ="list",Code = "&#xe63b;"},
                new UnicodeModel{Name ="search",Code = "&#xe600;"},
                new UnicodeModel{Name ="refresh",Code = "&#xe66a;"},
                new UnicodeModel{Name ="delete",Code = "&#xe67e;"},
                new UnicodeModel{Name ="copy",Code = "&#xe744;"},
                new UnicodeModel{Name ="eye",Code = "&#xe64f;"},
                new UnicodeModel{Name ="set",Code = "&#xe690;"},
                new UnicodeModel{Name ="cancel",Code = "&#xe60d;"},
                new UnicodeModel{Name ="update",Code = "&#xe66e;"},
                new UnicodeModel{Name ="save",Code = "&#xe61f;"},
                new UnicodeModel{Name ="add",Code = "&#xe6e0;"},
                new UnicodeModel{Name ="calendar",Code = "&#xe6ba;"},
                new UnicodeModel{Name ="eye-close",Code = "&#xe624;"},
                new UnicodeModel{Name ="keyboard",Code = "&#xea47;"},
                new UnicodeModel{Name ="export",Code = "&#xe66f;"},
                new UnicodeModel{Name ="print",Code = "&#xe67a;"},
                new UnicodeModel{Name ="import",Code = "&#xe7db;"},
                new UnicodeModel{Name ="user",Code = "&#xe741;"},
                new UnicodeModel{Name ="phone",Code = "&#xe725;"},
                new UnicodeModel{Name ="face",Code = "&#xe71c;"},
                new UnicodeModel{Name ="silence",Code = "&#xe68b;"},
                new UnicodeModel{Name ="camera",Code = "&#xe698;"},
                new UnicodeModel{Name ="gif",Code = "&#xe90e;"},
                new UnicodeModel{Name ="picture",Code = "&#xe67c;"},
                new UnicodeModel{Name ="package",Code = "&#xecd2;"},
                new UnicodeModel{Name ="scissor",Code = "&#xe6fc;"}
            };
            CopyCommand = new CommandBase(ExecuteCopy);
        }

        private void ExecuteCopy(object code)
        {
            System.Windows.Clipboard.SetText(code.ToString());
        }
    }
}
