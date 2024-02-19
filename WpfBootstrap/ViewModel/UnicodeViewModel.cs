using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBootstrap.Model;

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

        public UnicodeViewModel()
        {
            Unicodes = new List<UnicodeModel>
            {
                new UnicodeModel
                {
                    Icon ='\ue713',
                    Name="DoubleArrow_Right",
                    Code="&#xe713;"
                },
                new UnicodeModel
                {
                    Icon ='\ue714',
                    Name="DoubleArrow_Left",
                    Code="&#xe714;"
                },
                new UnicodeModel
                {
                    Icon ='\ue616',
                    Name="Arrow_Right",
                    Code="&#xe616;"
                },
                new UnicodeModel
                {
                    Icon ='\ue617',
                    Name="Arrow_Left",
                    Code="&#xe617;"
                },
                new UnicodeModel
                {
                    Icon ='\ue619',
                    Name="Arrow_Top",
                    Code="&#xe619;"
                },
                new UnicodeModel
                {
                    Icon ='\ue601',
                    Name="Arrow_Down",
                    Code="&#xe601;"
                },
                new UnicodeModel
                {
                    Icon ='\ue60a',
                    Name="Heart",
                    Code="&#xe60a;"
                },
                new UnicodeModel
                {
                    Icon ='\ue6ea',
                    Name="github",
                    Code="&#xe6ea;"
                },
                new UnicodeModel
                {
                    Icon ='\ue6f3',
                    Name="document",
                    Code="&#xe6f3;"
                },
                new UnicodeModel
                {
                    Icon ='\ue7c6',
                    Name="home",
                    Code="&#xe7c6;"
                },
                new UnicodeModel
                {
                    Icon ='\ue7df',
                    Name="star",
                    Code="&#xe7df;"
                },
                new UnicodeModel
                {
                    Icon ='\ue63b',
                    Name="list",
                    Code="&#xe63b;"
                },
                new UnicodeModel
                {
                    Icon ='\ue600',
                    Name="search",
                    Code="&#xe600;"
                },
                new UnicodeModel
                {
                    Icon ='\ue66a',
                    Name="refresh",
                    Code="&#xe66a;"
                },
                new UnicodeModel
                {
                    Icon ='\ue67e',
                    Name="delete",
                    Code="&#xe67e;"
                },
                new UnicodeModel
                {
                    Icon ='\ue744',
                    Name="copy",
                    Code="&#xe744;"
                },
                new UnicodeModel
                {
                    Icon ='\ue690',
                    Name="set",
                    Code="&#xe690;"
                },
                new UnicodeModel
                {
                    Icon ='\ue60d',
                    Name="cancel",
                    Code="&#xe60d;"
                },
                new UnicodeModel
                {
                    Icon ='\ue66e',
                    Name="update",
                    Code="&#xe66e;"
                },
                new UnicodeModel
                {
                    Icon ='\ue61f',
                    Name="save",
                    Code="&#xe61f;"
                },
                new UnicodeModel
                {
                    Icon ='\ue6e0',
                    Name="addition",
                    Code="&#xe6e0;"
                },
                new UnicodeModel
                {
                    Icon ='\ue6ba',
                    Name="datepicker",
                    Code="&#xe6ba;"
                },
                new UnicodeModel
                {
                    Icon ='\ue66f',
                    Name="export",
                    Code="&#xe66f;"
                },
                new UnicodeModel
                {
                    Icon ='\ue67a',
                    Name="print",
                    Code="&#xe67a;"
                },
                new UnicodeModel
                {
                    Icon ='\ue7db',
                    Name="download",
                    Code="&#xe7db;"
                },
                new UnicodeModel
                {
                    Icon ='\ue741',
                    Name="user",
                    Code="&#xe741;"
                },
            };
        }
    }
}
