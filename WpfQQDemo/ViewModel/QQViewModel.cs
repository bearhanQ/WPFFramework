using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTemplate;

namespace WpfQQDemo
{
    public class QQViewModel : ViewModelBase
    {
        public ObservableCollection<QQModel> Groups { get; set; }

        public QQViewModel()
        {
            Groups = new ObservableCollection<QQModel>
            {
                new QQModel
                {
                    Name=".net技术交流群",
                    //Image=new System.Windows.Media.Imaging.BitmapImage(new Uri(@"pack://application:,,,/Resources/profile7.jpg")),
                    Time="2024-04-01"
                },
                new QQModel
                {
                    Name="java技术交流群",
                    //Image=new System.Windows.Media.Imaging.BitmapImage(new Uri(@"pack://application:,,,/Resources/profile5.jpg")),
                    Time="2024-04-02"
                },
                new QQModel
                {
                    Name="c++技术交流群",
                    //Image=new System.Windows.Media.Imaging.BitmapImage(new Uri(@"pack://application:,,,/Resources/profile6.jpg")),
                    Time="2024-04-03"
                },
            };
        }
    }
}
