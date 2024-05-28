using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPFTemplate
{
    public class CurveChart : Chart
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
                if (this.Items.Count > 2)
                {
                    double point_X = Math.Round(itemPresenter.ActualWidth / this.Items.Count, 2);
                    int y = 0;
                    for (int i = 0; i < this.Items.Count - 1; i++)
                    {
                        var item1 = Items[i];
                        var item2 = Items[i + 1];
                        var bo1 = double.TryParse(item1.GetType().GetProperty(ValueMemberPath).GetValue(item1).ToString(), out double y1);
                        var bo2 = double.TryParse(item2.GetType().GetProperty(ValueMemberPath).GetValue(item2).ToString(), out double y2);
                        if (bo1 && bo2)
                        {
                            double point1_Y = Math.Round(Math.Floor(this.ActualHeight / this.VerticalNumbers.Count) * y1 / Ratio, 2);
                            double point2_Y = Math.Round(Math.Floor(this.ActualHeight / this.VerticalNumbers.Count) * y2 / Ratio, 2);

                            Point point1 = new Point(point_X * y, point1_Y);
                            Point point2 = new Point(point_X * (++y), point2_Y);
                            Point point3 = new Point(point_X * (++y), point2_Y);
                            Segments.Add(GenerateSegment(point1, point2, point3));
                            ++y;
                        }
                    }
                }
            }
        }

        private PathSegment GenerateSegment(Point point1, Point point2, Point point3)
        {
            return new BezierSegment(point1, point2, point3, true);
        }
    }
}
