using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
                    if (double.TryParse(item.GetType().GetProperty(ValueMemberPath).GetValue(item).ToString(), out double value))
                    {
                        var pointY = Math.Round(Math.Floor(this.ActualHeight / this.VerticalNumbers.Count) * value / Ratio, 2);
                        Segments.Add(GenerateSegment(pointX * i, pointY));
                    }
                }
            }

            if (Segments.Count > 1)
            {
                var startPoint = (Segments[0] as LineSegment).Point;
                var endPoint = (Segments[Segments.Count - 1] as LineSegment).Point;
                Segments.Add(GenerateSegment(endPoint.X, 0, false));
                Segments.Add(GenerateSegment(startPoint.X, 0, false));
                Segments.Add(GenerateSegment(startPoint.X, startPoint.Y, false));
            }
        }

        private PathSegment GenerateSegment(double x, double y, bool isStroked = true)
        {
            return new LineSegment(new Point(x, y), isStroked);
        }
    }
}
