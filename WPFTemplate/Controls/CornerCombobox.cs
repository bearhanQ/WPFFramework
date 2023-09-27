using System;
using System.Collections;
using System.Collections.Generic;
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
    ///     <MyNamespace:CornerCombobox/>
    ///
    /// </summary>
    public class CornerCombobox : ComboBox
    {
        static CornerCombobox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerCombobox), new FrameworkPropertyMetadata(typeof(CornerCombobox)));
        }

        private DefaultViewFilterHelper DefaultViewFilter;

        public CornerCombobox()
        {
            DefaultViewFilter = new DefaultViewFilterHelper();
            DefaultViewFilter.MatchWholeWord = false;
        }


        public CommandBase FilterCommand => new CommandBase(DefaultViewFilter.ExecuteFilter, DefaultViewFilter.CanExecuteFilter);

        public string WaterText
        {
            get { return (string)GetValue(WaterTextProperty); }
            set { SetValue(WaterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WaterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaterTextProperty =
            DependencyProperty.Register("WaterText", typeof(string), typeof(CornerCombobox), new PropertyMetadata("Water"));


        public bool Searchable
        {
            get { return (bool)GetValue(SearchableProperty); }
            set { SetValue(SearchableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Searchable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchableProperty =
            DependencyProperty.Register("Searchable", typeof(bool), typeof(CornerCombobox), new PropertyMetadata(false));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerCombobox));

        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            if (newValue != null)
            {
                DefaultViewFilter.ClearDefaultView();
                if (newValue is DataView view)
                {
                    DefaultViewFilter.SetView(view);
                }
                else
                {
                    var dv = CollectionViewSource.GetDefaultView(newValue);
                    DefaultViewFilter.SetView(dv);
                }
            }
            base.OnItemsSourceChanged(oldValue, newValue);
        }
        //public bool SearchingMatchCase
        //{
        //    get { return (bool)GetValue(SearchingMatchCaseProperty); }
        //    set { SetValue(SearchingMatchCaseProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for SearchingMatchCase.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty SearchingMatchCaseProperty =
        //    DependencyProperty.Register("SearchingMatchCase", typeof(bool), typeof(CornerCombobox), new PropertyMetadata(false));
    }
}
