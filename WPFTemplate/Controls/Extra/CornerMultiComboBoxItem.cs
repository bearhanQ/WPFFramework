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
    public class CornerMultiComboBoxItem : ListBoxItem
    {
        private CornerMultiComboBox parentComboBox => ItemsControl.ItemsControlFromItemContainer(this) as CornerMultiComboBox;

        static CornerMultiComboBoxItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CornerMultiComboBoxItem), new FrameworkPropertyMetadata(typeof(CornerMultiComboBoxItem)));
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            //e.Handled = true;
            this.IsSelected = !this.IsSelected;
            if (parentComboBox != null)
            {
                parentComboBox.UpdateText();
            }
        }
    }
}
