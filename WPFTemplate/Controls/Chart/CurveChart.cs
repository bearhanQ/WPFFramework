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
        private List<Point> point3Collection = new List<Point>();

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

            point3Collection.Clear();

            if (itemPresenter != null && this.ItemsSource != null && !string.IsNullOrWhiteSpace(ValueMemberPath))
            {
                double pointX = Math.Round(itemPresenter.ActualWidth / this.Items.Count, 2);
                for (int i = 0; i < this.Items.Count; i++)
                {
                    var item = Items[i];
                    if (double.TryParse(item.GetType().GetProperty(ValueMemberPath).GetValue(item).ToString(), out double value))
                    {
                        var pointY = Math.Round(Math.Floor(this.ActualHeight / this.VerticalNumbers.Count) * value / Ratio, 2);
                        point3Collection.Add(new Point(pointX * i, pointY));
                    }
                }
                if (point3Collection.Count > 1)
                {
                    for (int i = 0; i < point3Collection.Count - 1; i++)
                    {
                        if(i == 0)
                        {
                            var bs_point1 = point3Collection[i];
                            var bs_point3 = point3Collection[i + 1];
                            var bs_point2_X = (bs_point3.X - bs_point1.X) / 2;
                            var bs_point2_Y = bs_point3.Y;
                            var bs_point2 = new Point(bs_point2_X, bs_point2_Y);
                            Segments.Add(GenerateSegment(bs_point1, bs_point2, bs_point3));
                        }
                        else
                        {
                            var bs_pre = Segments[i - 1] as BezierSegment;
                            var bs_point3 = point3Collection[i + 1];
                            var bs_point1_X = bs_pre.Point3.X - bs_pre.Point2.X + bs_pre.Point3.X;
                            var bs_point1_Y = bs_pre.Point3.Y;
                            var bs_point1 = new Point(bs_point1_X, bs_point1_Y);
                            var bs_point2_X = (bs_point3.X - bs_point1_X) / 2 + bs_point1_X;
                            var bs_point2_Y = bs_point3.Y;
                            var bs_point2 = new Point(bs_point2_X, bs_point2_Y);
                            Segments.Add(GenerateSegment(bs_point1, bs_point2, bs_point3));
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
