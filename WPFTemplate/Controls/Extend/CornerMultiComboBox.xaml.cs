using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    /// <summary>
    /// CornerMultiComboBox.xaml 的交互逻辑
    /// </summary>
    public partial class CornerMultiComboBox : UserControl
    {
        public CornerMultiComboBox()
        {
            InitializeComponent();
            this.Loaded += CornerMultiComboBox_Loaded;
        }

        private void CornerMultiComboBox_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(CornerMultiComboBox),new FrameworkPropertyMetadata(null));

        public double MaxDropDownHeight
        {
            get { return (double)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MaxDropDownHeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxDropDownHeightProperty =
            DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(CornerMultiComboBox), new PropertyMetadata(SystemParameters.PrimaryScreenHeight / 3.0));

        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsDropDownOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(CornerMultiComboBox), new PropertyMetadata(false));

        public string DisplayMemberPath
        {
            get { return (string)GetValue(DisplayMemberPathProperty); }
            set { SetValue(DisplayMemberPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DisplayMemberPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register("DisplayMemberPath", typeof(string), typeof(CornerMultiComboBox), new PropertyMetadata(string.Empty));

        public string SelectedValuePath
        {
            get { return (string)GetValue(SelectedValuePathProperty); }
            set { SetValue(SelectedValuePathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedValuePath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedValuePathProperty =
            DependencyProperty.Register("SelectedValuePath", typeof(string), typeof(CornerMultiComboBox), new PropertyMetadata(string.Empty));

        private List<string> _text = new List<string>();
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CornerMultiComboBox), new PropertyMetadata(string.Empty));

        public string WaterText
        {
            get { return (string)GetValue(WaterTextProperty); }
            set { SetValue(WaterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WaterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaterTextProperty =
            DependencyProperty.Register("WaterText", typeof(string), typeof(CornerMultiComboBox), new PropertyMetadata("Water"));

        public bool ShowWatermark
        {
            get { return (bool)GetValue(ShowWatermarkProperty); }
            set { SetValue(ShowWatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowWatermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowWatermarkProperty =
            DependencyProperty.Register("ShowWatermark", typeof(bool), typeof(CornerMultiComboBox), new PropertyMetadata(true));

        public bool Searchable
        {
            get { return (bool)GetValue(SearchableProperty); }
            set { SetValue(SearchableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Searchable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchableProperty =
            DependencyProperty.Register("Searchable", typeof(bool), typeof(CornerMultiComboBox), new PropertyMetadata(false));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerMultiComboBox));

        public List<object> SelectedValues
        {
            get { return (List<object>)GetValue(SelectedValuesProperty); }
            set { SetValue(SelectedValuesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedValues.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedValuesProperty =
            DependencyProperty.Register("SelectedValues", typeof(List<object>), typeof(CornerMultiComboBox), new PropertyMetadata(new List<object>()));

        public List<object> SelectedItems
        {
            get { return (List<object>)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(List<object>), typeof(CornerMultiComboBox),new PropertyMetadata(new List<object>()));

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            var comboBoxItem = MyVisualTreeHelper.GetParent(cb, typeof(ComboBoxItem)) as ComboBoxItem;
            var itemContext = comboBoxItem.DataContext;
            var bo = BaseCornerCombobox.Items.Contains(itemContext);
            if (bo)
            {
                SelectedItems.Add(itemContext);
                if (itemContext.GetType() != typeof(string))
                {
                    if (!string.IsNullOrEmpty(DisplayMemberPath))
                    {
                        var text = itemContext.GetType().GetProperty(DisplayMemberPath).GetValue(itemContext).ToString();
                        _text.Add(text);
                        this.Text = String.Join(",", _text);
                    }

                    if (!string.IsNullOrEmpty(SelectedValuePath))
                    {
                        var value = itemContext.GetType().GetProperty(SelectedValuePath).GetValue(itemContext);
                        SelectedValues.Add(value);
                    }
                    else
                    {
                        SelectedValues.Add(itemContext);
                    }
                }
                else
                {
                    _text.Add(itemContext.ToString());
                    this.Text = String.Join(",", _text);
                    SelectedValues.Add(itemContext);
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;
            var comboBoxItem = MyVisualTreeHelper.GetParent(cb, typeof(ComboBoxItem)) as ComboBoxItem;
            var itemContext = comboBoxItem.DataContext;
            var bo = BaseCornerCombobox.Items.Contains(itemContext);
            if (bo)
            {
                SelectedItems.Remove(itemContext);
                if (itemContext.GetType() != typeof(string))
                {
                    if (!string.IsNullOrEmpty(DisplayMemberPath))
                    {
                        var text = itemContext.GetType().GetProperty(DisplayMemberPath).GetValue(itemContext).ToString();
                        _text.Remove(text);
                        this.Text = String.Join(",", _text);
                    }

                    if (!string.IsNullOrEmpty(SelectedValuePath))
                    {
                        var value = itemContext.GetType().GetProperty(SelectedValuePath).GetValue(itemContext);
                        SelectedValues.Remove(value);
                    }
                    else
                    {
                        SelectedValues.Remove(itemContext);
                    }
                }
                else
                {
                    _text.Add(itemContext.ToString());
                    this.Text = String.Join(",", _text);
                    SelectedValues.Remove(itemContext);
                }
            }
        }

        private CornerCombobox BaseCornerCombobox;
        private CornerTextBox BaseCornerTextBox;

        public override void OnApplyTemplate()
        {
            var combobox = this.Template.FindName("PART_CornerCombobox", this) as CornerCombobox;
            if (combobox != null)
            {
                BaseCornerCombobox = combobox;
            }

            var textbox = this.Template.FindName("PART_CornerTextBox", this) as CornerTextBox;
            if (textbox != null)
            {
                BaseCornerTextBox = textbox;
            }

            base.OnApplyTemplate();
        }
    }

    public class CheckProperty : DependencyObject
    {
        public static bool GetIsChecked(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCheckedProperty);
        }

        public static void SetIsChecked(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCheckedProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.RegisterAttached("IsChecked", typeof(bool), typeof(CheckProperty), new PropertyMetadata(false));
    }
}
