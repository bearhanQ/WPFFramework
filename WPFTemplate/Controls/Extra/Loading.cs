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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class Loading : Control
    {
        public static readonly DependencyProperty LoadingTypeProperty;

        public LoadingType LoadingType
        {
            get { return (LoadingType)GetValue(LoadingTypeProperty); }
            set { SetValue(LoadingTypeProperty, value); }
        }

        static Loading()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Loading), new FrameworkPropertyMetadata(typeof(Loading)));
            LoadingTypeProperty = DependencyProperty.Register("LoadingType", typeof(LoadingType), typeof(Loading), new PropertyMetadata(LoadingType.Type1));
        }
    }

    public enum LoadingType
    {
        Type1,
        Type2
    }
}
