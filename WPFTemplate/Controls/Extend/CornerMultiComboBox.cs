using System;
using System.Collections.Generic;
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
    public class CornerMultiComboBox : MultiSelector
    {
        private TextBox EditableTextBoxSite;

        private bool updatingText;

        static CornerMultiComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerMultiComboBox), new FrameworkPropertyMetadata(typeof(CornerMultiComboBox)));
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            private set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CornerMultiComboBox), new PropertyMetadata(string.Empty));

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerMultiComboBox), new PropertyMetadata(new CornerRadius(0)));


        public string WaterText
        {
            get { return (string)GetValue(WaterTextProperty); }
            set { SetValue(WaterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WaterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WaterTextProperty =
            DependencyProperty.Register("WaterText", typeof(string), typeof(CornerMultiComboBox), new PropertyMetadata("MultiComboBox"));

        public bool ShowWatermark
        {
            get { return (bool)GetValue(ShowWatermarkProperty); }
            set { SetValue(ShowWatermarkProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowWatermark.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowWatermarkProperty =
            DependencyProperty.Register("ShowWatermark", typeof(bool), typeof(CornerMultiComboBox), new PropertyMetadata(true));


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
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(CornerMultiComboBox),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, 
                    OnIsDropDownOpenChanged, null, false, UpdateSourceTrigger.PropertyChanged));

        public bool IsSelectAll
        {
            get { return (bool)GetValue(IsSelectedAllProperty); }
            set { SetValue(IsSelectedAllProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelectAll.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedAllProperty =
            DependencyProperty.Register("IsSelectAll", typeof(bool), typeof(CornerMultiComboBox),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnIsSelectAllChange), null, false, UpdateSourceTrigger.PropertyChanged));

        private static void OnIsSelectAllChange(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var combobox = sender as CornerMultiComboBox;
            if (combobox != null)
            {
                if (combobox.IsSelectAll)
                {
                    combobox.SelectAll();
                }
                else
                {
                    combobox.UnselectAll();
                }
                combobox.UpdateText();
            }
        }

        private static void OnIsDropDownOpenChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var combobox=sender as CornerMultiComboBox;
            if (combobox != null)
            {
                combobox.Focus();
            }
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is CornerMultiComboBoxItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CornerMultiComboBoxItem();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            EditableTextBoxSite = GetTemplateChild("PART_EditableTextBox") as TextBox;

            var parentWindow = Window.GetWindow(this);
            parentWindow.PreviewMouseDown -= ParentWindow_PreviewMouseDown;
            parentWindow.PreviewMouseDown += ParentWindow_PreviewMouseDown;

            UpdateText();
        }

        private void ParentWindow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsDropDownOpen && !base.IsMouseOver)
            {
                Close();
            }
        }

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsKeyboardFocusWithinChanged(e);
            if (IsDropDownOpen && !base.IsKeyboardFocusWithin)
            {
                DependencyObject dependencyObject = Keyboard.FocusedElement as DependencyObject;
                if (dependencyObject == null || (ItemsControl.ItemsControlFromItemContainer(dependencyObject) != this))
                {
                    Close();
                }
            }
        }

        internal void UpdateText()
        {
            if (updatingText)
            {
                return;
            }
            try
            {
                updatingText = true;
                var selectedItem = this.SelectedItem;
                if (selectedItem != null)
                {
                    var property = selectedItem is CornerMultiComboBoxItem ? "Content" : this.DisplayMemberPath;
                    if (!string.IsNullOrWhiteSpace(property))
                    {
                        var Names = new List<string>();
                        foreach (var item in this.SelectedItems)
                        {
                            var name = item.GetType().GetProperty(property).GetValue(item, null).ToString();
                            Names.Add(name);
                        }
                        Text = String.Join(",", Names);
                    }
                }
                else
                {
                    Text = String.Empty;
                }

                if (EditableTextBoxSite != null)
                {
                    EditableTextBoxSite.Text = Text;
                }
            }
            finally
            {
                updatingText = false;
            }
        }

        private void Close()
        {
            if (IsDropDownOpen)
            {
                SetValue(IsDropDownOpenProperty, false);
            }
        }
    }
}
