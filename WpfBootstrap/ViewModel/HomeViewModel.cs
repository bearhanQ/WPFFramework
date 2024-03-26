using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfBootstrap.Model;
using WpfBootstrap.View;

namespace WpfBootstrap.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public ObservableCollection<Revenue> RevenueList { get; set; }

        public ObservableCollection<WorkScream> WorkScreamList { get; set; }

        private DataTable _dataTableSource;

        public DataTable DataTableSource
        {
            get
            {
                if (_dataTableSource == null)
                {
                    _dataTableSource = new DataTable();
                    _dataTableSource.Columns.Add("No", typeof(string));
                    _dataTableSource.Columns.Add("INVOICE", typeof(string));
                    _dataTableSource.Columns.Add("CLIENT", typeof(string));
                    _dataTableSource.Columns.Add("VAT", typeof(string));
                    _dataTableSource.Columns.Add("CREATED", typeof(string));
                    _dataTableSource.Columns.Add("STATUS", typeof(string));
                    _dataTableSource.Columns.Add("PRICE", typeof(string));
                    _dataTableSource.Columns.Add("ACTION", typeof(int));
                }
                return _dataTableSource;
            }
        }

        public RelayCommand<DataRowView> DelCommand { get; set; }

        public RelayCommand EditCommand { get; set; }

        private DataRowView _selectedRowView;
        public DataRowView SelectedRowView
        {
            get { return _selectedRowView; }
            set { _selectedRowView = value; RaisePropertyChanged(); }
        }

        public HomeViewModel()
        {
            RevenueList = new ObservableCollection<Revenue>
            {
                new Revenue{Month = "Jun",Day="First",Profits="Profit",Value=15},
                new Revenue{Month = "Jun",Day="Second",Profits="Profit",Value=16},
                new Revenue{Month = "Jun",Day="Third",Profits="Profit",Value=17},
                new Revenue{Month = "Jun",Day="Fourth",Profits="Profit",Value=18},
                new Revenue{Month = "Jun",Day="Fifth",Profits="Profit",Value=19},
                new Revenue{Month = "Jun",Day="Sixth",Profits="Profit",Value=20},
                new Revenue{Month = "Jun",Day="Seventh",Profits="Profit",Value=19},
                new Revenue{Month = "Jun",Day="Eighth",Profits="Profit",Value=18},
                new Revenue{Month = "Jun",Day="Nineth",Profits="Profit",Value=17},
                new Revenue{Month = "Jun",Day="Tenth",Profits="Profit",Value=16},
                new Revenue{Month = "Jun",Day="Eleventh",Profits="Profit",Value=30},
            };
            WorkScreamList = new ObservableCollection<WorkScream>
            {
                new WorkScream
                {
                    Img ="profile1.jpg",
                    Content ="Jeffie Lewzey commented on your I'm not a witch.post.",
                    Time ="2 days ago"
                },
                new WorkScream
                {
                    Img ="profile2.jpg",
                    Content ="It's Mallory Hulme's birthday. Wish him well!",
                    Time ="yesterday"
                },
                new WorkScream
                {
                    Img ="profile3.jpg",
                    Content ="Dunn Slane posted Well, what do you want?.",
                    Time ="4 days ago"
                },
                new WorkScream
                {
                    Img ="profile4.jpg",
                    Content ="Emmy Levet created a new project Morning alarm clock.",
                    Time ="yesterday"
                },
                new WorkScream
                {
                    Img ="profile5.jpg",
                    Content ="Maryjo Lebarree liked your photo.",
                    Time ="today"
                },
                new WorkScream
                {
                    Img ="profile6.jpg",
                    Content ="Egan Poetz registered new client as Trilia.",
                    Time ="yesterday"
                },
                new WorkScream
                {
                    Img ="profile7.jpg",
                    Content ="Kellie Skingley closed a new deal on project Pen Pineapple Apple Pen.",
                    Time ="3 days ago"
                },
                new WorkScream
                {
                    Img ="profile8.jpg",
                    Content ="Christabel Charlwood created a new project for Wikibox.",
                    Time ="2 days ago"
                },
                new WorkScream
                {
                    Img ="profile9.jpg",
                    Content ="Haskel Shelper change status of Tabler Icons from open to closed.",
                    Time ="yesterday"
                },
                new WorkScream
                {
                    Img ="profile10.jpg",
                    Content ="Lorry Mion liked Tabler UI Kit.",
                    Time ="yesterday"
                },
                new WorkScream
                {
                    Img ="profile11.jpg",
                    Content ="Leesa Beaty posted new video.",
                    Time ="yesterday"
                },
            };
            DataTableSource.Rows.Add("001401", "Design Works", "Carlson Limited", "87956621", "15 Dec 2017", "Paid", "$887", Action.frozen);
            DataTableSource.Rows.Add("001402", "UX Wireframes", "Adobe", "87956421", "12 Apr 2017", "Pending", "$1200", Action.active);
            DataTableSource.Rows.Add("001403", "New Dashboard", "Bluewolf", "87952621", "23 Oct 2017", "Pending", "$534", Action.frozen);
            DataTableSource.Rows.Add("001404", "Landing Page", "Salesforce", "87953421", "2 Sep 2017", "Due in 2 Weeks", "$1500", Action.active);
            DataTableSource.Rows.Add("001405", "Marketing Templates", "Printic", "87956621", "29 Jan 2018", "Due in 2 Weeks", "$648", Action.active);
            DataTableSource.Rows.Add("001406", "Sales Presentation", "Tabdaq", "87956621", "4 Feb 2018", "Paid Today", "$300", Action.frozen);

            DelCommand = new RelayCommand<DataRowView>(DeleteDataRow);
            EditCommand = new RelayCommand(EditDataRow);
        }

        private void DeleteDataRow(DataRowView row)
        {
            if (row != null)
            {
                DataTableSource.Rows.Remove(row.Row);
            }
        }

        private void EditDataRow()
        {
            HomeTableEditView view = new HomeTableEditView();
            view.ShowDialog();
        }
    }

    public enum Action
    {
        active,
        frozen
    }
}
