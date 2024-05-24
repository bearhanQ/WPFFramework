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
        public static readonly DependencyProperty SegmentsProperty;

        private Border BorderMain;

        public PathSegmentCollection Segments
        {
            get { return (PathSegmentCollection)GetValue(SegmentsProperty); }
            set { SetValue(SegmentsProperty, value); }
        }

        static LineChart()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(LineChart), new FrameworkPropertyMetadata(typeof(LineChart)));
            PathSegmentCollection pathSegments = new PathSegmentCollection();
            SegmentsProperty = DependencyProperty.Register("Segments", typeof(PathSegmentCollection), typeof(LineChart));
        }

        public LineChart()
        {
            this.Loaded += LineChart_Loaded;
        }

        private void LineChart_Loaded(object sender, RoutedEventArgs e)
        {
            if (BorderMain != null && this.ItemsSource != null && !string.IsNullOrWhiteSpace(ValueMemberPath))
            {
                double pointX = Math.Floor(BorderMain.ActualWidth / this.Items.Count / 2);
                for (int i = 0; i < this.Items.Count; i++)
                {
                    var item = Items[i];
                    double pointY = 0;
                    if (double.TryParse(item.GetType().GetProperty(ValueMemberPath).GetValue(item).ToString(), out pointY))
                    {
                        if (Segments == null)
                        {
                            Segments = new PathSegmentCollection();
                        }
                        Segments.Add(GenerateSegment(pointX * i, pointY));
                    }
                }
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            BorderMain = GetTemplateChild("BorderMain") as Border;
        }

        private PathSegment GenerateSegment(double x, double y)
        {
            return new LineSegment(new Point(x, y), true);
        }
    }
}
