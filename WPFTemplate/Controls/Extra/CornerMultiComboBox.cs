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
        public static readonly DependencyProperty TextProperty;

        public static readonly DependencyProperty CornerRadiusProperty;

        public static readonly DependencyProperty WaterTextProperty;

        public static readonly DependencyProperty ShowWaterTextProperty;

        public static readonly DependencyProperty MaxDropDownHeightProperty;

        public static readonly DependencyProperty IsDropDownOpenProperty;

        public static readonly DependencyProperty IsSelectedAllProperty;

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            private set { SetValue(TextProperty, value); }
        }

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public string WaterText
        {
            get { return (string)GetValue(WaterTextProperty); }
            set { SetValue(WaterTextProperty, value); }
        }

        public bool ShowWaterText
        {
            get { return (bool)GetValue(ShowWaterTextProperty); }
            set { SetValue(ShowWaterTextProperty, value); }
        }

        public double MaxDropDownHeight
        {
            get { return (double)GetValue(MaxDropDownHeightProperty); }
            set { SetValue(MaxDropDownHeightProperty, value); }
        }

        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }

        public bool IsSelectAll
        {
            get { return (bool)GetValue(IsSelectedAllProperty); }
            set { SetValue(IsSelectedAllProperty, value); }
        }

        private TextBox EditableTextBoxSite;

        private bool updatingText;

        static CornerMultiComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerMultiComboBox), new FrameworkPropertyMetadata(typeof(CornerMultiComboBox)));
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(CornerMultiComboBox), new PropertyMetadata(string.Empty));
            CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(CornerMultiComboBox), new PropertyMetadata(new CornerRadius(0)));
            WaterTextProperty = DependencyProperty.Register("WaterText", typeof(string), typeof(CornerMultiComboBox), new PropertyMetadata("MultiComboBox"));
            ShowWaterTextProperty = DependencyProperty.Register("ShowWaterText", typeof(bool), typeof(CornerMultiComboBox), new PropertyMetadata(true));
            MaxDropDownHeightProperty = DependencyProperty.Register("MaxDropDownHeight", typeof(double), typeof(CornerMultiComboBox),
                new PropertyMetadata(SystemParameters.PrimaryScreenHeight / 3.0));
            IsDropDownOpenProperty = DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(CornerMultiComboBox),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnIsDropDownOpenChanged, null, false, UpdateSourceTrigger.PropertyChanged));
            IsSelectedAllProperty = DependencyProperty.Register("IsSelectAll", typeof(bool), typeof(CornerMultiComboBox),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    new PropertyChangedCallback(OnIsSelectAllChange), null, false, UpdateSourceTrigger.PropertyChanged));

        }

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

        private void ParentWindow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (IsDropDownOpen && !base.IsMouseOver)
            {
                Close();
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
            if (parentWindow != null)
            {
                parentWindow.PreviewMouseDown -= ParentWindow_PreviewMouseDown;
                parentWindow.PreviewMouseDown += ParentWindow_PreviewMouseDown;
            }

            UpdateText();
        }
    }
}
