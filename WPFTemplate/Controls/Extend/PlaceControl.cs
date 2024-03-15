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
    public class PlaceControl : ContentControl
    {
        public static readonly DependencyProperty PlaceMentProperty;

        private Path _mainPath;

        private readonly int pathOffset = 5;

        public PlaceMent PlaceMent
        {
            get { return (PlaceMent)GetValue(PlaceMentProperty); }
            set { SetValue(PlaceMentProperty, value); }
        }

        static PlaceControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceControl), new FrameworkPropertyMetadata(typeof(PlaceControl)));
            PlaceMentProperty = DependencyProperty.Register("PlaceMent", typeof(PlaceMent), typeof(PlaceControl), new PropertyMetadata(PlaceMent.Top));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _mainPath = this.Template.FindName("PART_PATH", this) as Path;
            CreatePath();
        }

        private void CreatePath()
        {
            if (this._mainPath != null)
            {
                if (this.PlaceMent == PlaceMent.Top)
                {
                    PathGeometry pathGeometry = new PathGeometry();
                    PathFigure pathFigure = new PathFigure();
                    pathFigure.StartPoint = new Point(0, pathOffset);
                    pathFigure.Segments.Add(new LineSegment(new Point(this.Width / 2 - pathOffset, pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(this.Width / 2, 0), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(this.Width / 2 + pathOffset, pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(this.Width, pathOffset), true));
                    pathGeometry.Figures.Add(pathFigure);
                    _mainPath.Data = pathGeometry;
                }

                if (this.PlaceMent == PlaceMent.Left)
                {
                    PathGeometry pathGeometry = new PathGeometry();
                    PathFigure pathFigure = new PathFigure();
                    pathFigure.StartPoint = new Point(pathOffset, 0);
                    pathFigure.Segments.Add(new LineSegment(new Point(pathOffset, this.Height / 2 - pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(0, this.Height / 2), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(pathOffset, this.Height / 2 + pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(pathOffset, this.Height), true));
                    pathGeometry.Figures.Add(pathFigure);
                    _mainPath.Data = pathGeometry;
                }

                if (this.PlaceMent == PlaceMent.Right)
                {
                    PathGeometry pathGeometry = new PathGeometry();
                    PathFigure pathFigure = new PathFigure();
                    pathFigure.StartPoint = new Point(this.Width - pathOffset, 0);
                    pathFigure.Segments.Add(new LineSegment(new Point(this.Width - pathOffset, this.Height / 2 - pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(this.Width, this.Height / 2), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(this.Width - pathOffset, this.Height / 2 + pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(this.Width - pathOffset, this.Height), true));
                    pathGeometry.Figures.Add(pathFigure);
                    _mainPath.Data = pathGeometry;
                }

                if (this.PlaceMent == PlaceMent.Bottom)
                {
                    PathGeometry pathGeometry = new PathGeometry();
                    PathFigure pathFigure = new PathFigure();
                    pathFigure.StartPoint = new Point(0, this.Height - pathOffset);
                    pathFigure.Segments.Add(new LineSegment(new Point(this.Width / 2 - pathOffset, this.Height - pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(this.Width / 2, this.Height), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(this.Width / 2 + pathOffset, this.Height - pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(this.Width, this.Height - pathOffset), true));
                    pathGeometry.Figures.Add(pathFigure);
                    _mainPath.Data = pathGeometry;
                }
            }
        }
    }

    public enum PlaceMent
    {
        Bottom,
        Top,
        Left,
        Right
    }
}
