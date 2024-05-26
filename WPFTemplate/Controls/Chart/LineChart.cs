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
    public class LineChart : Chart
    {
        static LineChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LineChart), new FrameworkPropertyMetadata(typeof(LineChart)));
        }

        protected override void GeneratePath()
        {
            if (Segments == null)
            {
                Segments = new PathSegmentCollection();
            }
            else
            {
                Segments.Clear();
            }
            if (itemPresenter != null && this.ItemsSource != null && !string.IsNullOrWhiteSpace(ValueMemberPath))
            {
                double pointX = Math.Round(itemPresenter.ActualWidth / this.Items.Count, 2);
                for (int i = 0; i < this.Items.Count; i++)
                {
                    var item = Items[i];
                    double value = 0;
                    if (double.TryParse(item.GetType().GetProperty(ValueMemberPath).GetValue(item).ToString(), out value))
                    {
                        var pointY = Math.Floor(this.ActualHeight / this.VerticalNumbers.Count) * value / Ratio;
                        Segments.Add(GenerateSegment(pointX * i, pointY));
                    }
                }
            }
        }

        private PathSegment GenerateSegment(double x, double y)
        {
            return new LineSegment(new Point(x, y), true);
        }
    }
}
