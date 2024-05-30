using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WPFTemplate
{
    public class StepChart : Chart
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
                if (this.Items.Count > 1)
                {
                    for (int i = 0; i < this.Items.Count; i++)
                    {
                        var item = Items[i];
                        var bo1 = double.TryParse(item.GetType().GetProperty(ValueMemberPath).GetValue(item).ToString(), out double value);
                        if (bo1)
                        {
                            var pointY = Math.Round(Math.Floor(this.ActualHeight / this.VerticalNumbers.Count) * value / Ratio, 2);
                            Segments.Add(GenerateSegment(pointX * i, pointY));
                            Segments.Add(GenerateSegment(pointX * (i + 1), pointY));
                        }
                        if (i == this.Items.Count - 1)
                        {
                            Segments.RemoveAt(Segments.Count - 1);
                        }
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
