using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class BarItem : ChartItem
    {
        static BarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BarItem), new FrameworkPropertyMetadata(typeof(BarItem)));
        }
    }
}
