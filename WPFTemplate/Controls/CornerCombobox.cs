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

        public static readonly DependencyProperty ShowWaterTextProperty;

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
        public bool ShowWaterText
        {
            get { return (bool)GetValue(ShowWaterTextProperty); }
            set { SetValue(ShowWaterTextProperty, value); }
        }

        private CornerTextBox textBox;

        static CornerCombobox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerCombobox), new FrameworkPropertyMetadata(typeof(CornerCombobox)));
            WaterTextProperty = DependencyProperty.Register("WaterText", typeof(string), typeof(CornerCombobox), new PropertyMetadata("Water"));
            SearchableProperty = DependencyProperty.Register("Searchable", typeof(bool), typeof(CornerCombobox), new PropertyMetadata(false));
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerCombobox));
            ShowWaterTextProperty = DependencyProperty.Register("ShowWaterText", typeof(bool), typeof(CornerCombobox), new PropertyMetadata(true));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (textBox != null)
            {
                textBox = null;
            }

            textBox = this.Template.FindName("searchTextBox", this) as CornerTextBox;
            if (textBox != null)
            {
                textBox.TextChanged += SearchTextBox_TextChanged;
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as CornerTextBox;

            if (!string.IsNullOrWhiteSpace(this.DisplayMemberPath))
            {
                FilterCommand.Execute(DisplayMemberPath + "&" + textBox.Text);
            }
            else
            {
                FilterCommand.Execute("Content" + "&" + textBox.Text);
            }
        }
    }
}
