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
        static CornerMultiComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerMultiComboBox), new FrameworkPropertyMetadata(typeof(CornerMultiComboBox)));
        }

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
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(CornerMultiComboBox), new PropertyMetadata(false));

        public DataTemplate SelectionBoxItemTemplate
        {
            get { return (DataTemplate)GetValue(SelectionBoxItemTemplateProperty); }
            set { SetValue(SelectionBoxItemTemplateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionBoxItemTemplate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionBoxItemTemplateProperty =
            DependencyProperty.Register("SelectionBoxItemTemplate", typeof(DataTemplate), typeof(CornerMultiComboBox), new PropertyMetadata(null));


        public object SelectionBoxItem
        {
            get { return (object)GetValue(SelectionBoxItemProperty); }
            set { SetValue(SelectionBoxItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionBoxItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionBoxItemProperty =
            DependencyProperty.Register("SelectionBoxItem", typeof(object), typeof(CornerMultiComboBox), new PropertyMetadata(string.Empty));

        public string SelectionBoxItemStringFormat
        {
            get { return (string)GetValue(SelectionBoxItemStringFormatProperty); }
            set { SetValue(SelectionBoxItemStringFormatProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectionBoxItemStringFormat.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectionBoxItemStringFormatProperty =
            DependencyProperty.Register("SelectionBoxItemStringFormat", typeof(string), typeof(CornerMultiComboBox), new PropertyMetadata(string.Empty));


        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is CornerMultiComboBoxItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CornerMultiComboBoxItem();
        }

        //private void Close()
        //{
        //    if (IsDropDownOpen)
        //    {
        //        SetValue(IsDropDownOpenProperty, false);
        //    }
        //}
    }
}
