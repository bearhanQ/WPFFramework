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
    public class CornerCombobox : ComboBox
    {
        public static readonly DependencyProperty WaterTextProperty;

        public static readonly DependencyProperty SearchableProperty;

        public static readonly DependencyProperty CornerRadiusProperty;

        public static readonly DependencyProperty ShowWatermarkProperty;

        public ComboBoxViewFilterHelper FilterCommand
        {
            get
            {
                if (this.ItemsSource is DataView)
                {
                    return new ComboBoxViewFilterHelper(ItemsSource);
                }
                else
                {
                    return new ComboBoxViewFilterHelper(CollectionViewSource.GetDefaultView(this.Items));
                }
            }
        }
        public string WaterText
        {
            get { return (string)GetValue(WaterTextProperty); }
            set { SetValue(WaterTextProperty, value); }
        }
        public bool Searchable
        {
            get { return (bool)GetValue(SearchableProperty); }
            set { SetValue(SearchableProperty, value); }
        }
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }
        public bool ShowWatermark
        {
            get { return (bool)GetValue(ShowWatermarkProperty); }
            set { SetValue(ShowWatermarkProperty, value); }
        }

        static CornerCombobox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerCombobox), new FrameworkPropertyMetadata(typeof(CornerCombobox)));
            WaterTextProperty = DependencyProperty.Register("WaterText", typeof(string), typeof(CornerCombobox), new PropertyMetadata("Water"));
            SearchableProperty = DependencyProperty.Register("Searchable", typeof(bool), typeof(CornerCombobox), new PropertyMetadata(false));
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerCombobox));
            ShowWatermarkProperty = DependencyProperty.Register("ShowWatermark", typeof(bool), typeof(CornerCombobox), new PropertyMetadata(true));
        }
    }
}
