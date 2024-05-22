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
    public class BarItem : ContentControl
    {
        static BarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BarItem), new FrameworkPropertyMetadata(typeof(BarItem)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            GenerateBar();
        }

        private void GenerateBar()
        {
            var bar = LocalVisualTreeHelper.GetParent(this, typeof(Bar)) as Bar;
            if (bar != null && bar.ItemsSource != null && !string.IsNullOrWhiteSpace(bar.ValueMemberPath))
            {
                double value = 0;
                if (double.TryParse(Content.GetType().GetProperty(bar.ValueMemberPath).GetValue(Content).ToString(), out value))
                {
                    var barItem = bar.ItemContainerGenerator.ContainerFromItem(Content) as BarItem;
                    var rectangle = barItem.Template.FindName("PART_Rectangle", barItem) as Rectangle;
                    if (rectangle != null)
                    {
                        rectangle.Height = value;
                    }
                }
            }
        }
    }
}
