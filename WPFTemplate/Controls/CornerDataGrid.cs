using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFTemplate.Controls"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根
    /// 元素中:
    ///
    ///     xmlns:MyNamespace="clr-namespace:WPFTemplate.Controls;assembly=WPFTemplate.Controls"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误:
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:CornerDataGrid/>
    ///
    /// </summary>
    public class CornerDataGrid : DataGrid
    {
        static CornerDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerDataGrid), new FrameworkPropertyMetadata(typeof(CornerDataGrid)));
        }
        private ICollectionView _collectionView;

        public CornerDataGrid()
        {
            this.Loaded += CornerDataGrid_Loaded;
        }

        private void CornerDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                _collectionView = CollectionViewSource.GetDefaultView(this.DataContext);
                this.ItemsSource = _collectionView;
            }
        }

        public CommandBase FilterCommand => new CommandBase(ExecuteFilter, CanExecuteFilter);

        private void ExecuteFilter(object parameter)
        {
            var mixparameter = parameter.ToString().Split('&');
            var filterstr = mixparameter[0];
            var content = mixparameter[1];
            Predicate<object> filter = item =>
            {
                var dataItem = item.GetType().GetProperty(content).GetValue(item).ToString();
                return dataItem.Equals(filterstr);
            };
            _collectionView.Filter = filter;
        }

        private bool CanExecuteFilter(object parameter)
        {
            if (parameter == null)
            {
                return false;
            }

            var mixparameter = parameter.ToString().Split('&');
            if (mixparameter.Length < 1)
            {
                return false;
            }

            return true;
        }

        public DataGridType DataGridType
        {
            get { return (DataGridType)GetValue(DataGridTypeProperty); }
            set { SetValue(DataGridTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DataGridType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DataGridTypeProperty =
            DependencyProperty.Register("DataGridType", typeof(DataGridType), typeof(CornerDataGrid), new PropertyMetadata(DataGridType.Original));
    }
}
