using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class PlaceControl : ContentControl
    {
        public static readonly DependencyProperty PlaceMentProperty;

        public static readonly DependencyProperty PlaceMentTargetProperty;

        public static readonly DependencyProperty IsOpenProperty;

        private Path _mainPath;

        private Popup _popup;

        private FrameworkElement _child;

        private readonly int pathOffset = 5;

        public PlaceMent PlaceMent
        {
            get { return (PlaceMent)GetValue(PlaceMentProperty); }
            set { SetValue(PlaceMentProperty, value); }
        }

        public FrameworkElement PlaceMentTarget
        {
            get { return (FrameworkElement)GetValue(PlaceMentTargetProperty); }
            set { SetValue(PlaceMentTargetProperty, value); }
        }

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        private RoutedEvent _openEvent;
        public RoutedEvent OpenEvent
        {
            get { return _openEvent; }
            set { _openEvent = value; }
        }

        private RoutedEvent _closeEvent;
        public RoutedEvent CloseEvent
        {
            get { return _closeEvent; }
            set { _closeEvent = value; }
        }

        static PlaceControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlaceControl), new FrameworkPropertyMetadata(typeof(PlaceControl)));
            PlaceMentProperty = DependencyProperty.Register("PlaceMent", typeof(PlaceMent), typeof(PlaceControl), new PropertyMetadata(PlaceMent.Top));
            PlaceMentTargetProperty = DependencyProperty.Register("PlaceMentTarget", typeof(FrameworkElement), typeof(PlaceControl));
            IsOpenProperty = DependencyProperty.Register("IsOpen", typeof(bool), typeof(PlaceControl));
        }

        public PlaceControl()
        {
            this.Loaded += PlaceControl_Loaded;
        }

        private void PlaceControl_Loaded(object sender, RoutedEventArgs e)
        {
            CreatePath();
            CorrectPathOffset();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _mainPath = this.Template.FindName("PART_Path", this) as Path;
            _popup = this.Template.FindName("PART_Popup", this) as Popup;
            var childs = LogicalTreeHelper.GetChildren(this).GetEnumerator();
            if (childs != null)
            {
                if (childs.MoveNext())
                {
                    _child = childs.Current as FrameworkElement;
                }
            }

            if (PlaceMentTarget != null)
            {
                if (OpenEvent != null)
                {
                    PlaceMentTarget.AddHandler(OpenEvent, new RoutedEventHandler(OpenEventHandler));
                }
                if (CloseEvent != null)
                {
                    PlaceMentTarget.AddHandler(CloseEvent, new RoutedEventHandler(CloseEventHandle));
                }
                PlaceMentTarget.SizeChanged += (s, e) =>
                {
                    CorrectPathOffset();
                };
            }
        }

        private void OpenEventHandler(object sender,RoutedEventArgs args)
        {
            IsOpen = true;
        }
        
        private void CloseEventHandle(object sender,RoutedEventArgs args)
        {
            IsOpen = false;
        }

        private void CreatePath()
        {
            if (this._mainPath != null && _child != null)
            {
                if (this.PlaceMent == PlaceMent.Bottom)
                {
                    PathGeometry pathGeometry = new PathGeometry();
                    PathFigure pathFigure = new PathFigure();
                    pathFigure.StartPoint = new Point(0, pathOffset);
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width / 2 - pathOffset, pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width / 2, 0), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width / 2 + pathOffset, pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width, pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width, _child.Height + pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width, _child.Height + pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(0, _child.Height + pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(0, pathOffset), true));
                    pathGeometry.Figures.Add(pathFigure);
                    _mainPath.Data = pathGeometry;
                }

                if (this.PlaceMent == PlaceMent.Right)
                {
                    PathGeometry pathGeometry = new PathGeometry();
                    PathFigure pathFigure = new PathFigure();
                    pathFigure.StartPoint = new Point(pathOffset, 0);
                    pathFigure.Segments.Add(new LineSegment(new Point(pathOffset, _child.Height / 2 - pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(0, _child.Height / 2), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(pathOffset, _child.Height / 2 + pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(pathOffset, _child.Height), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width + pathOffset, _child.Height), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width + pathOffset, 0), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(pathOffset, 0), true));
                    pathGeometry.Figures.Add(pathFigure);
                    _mainPath.Data = pathGeometry;
                }

                if (this.PlaceMent == PlaceMent.Left)
                {
                    PathGeometry pathGeometry = new PathGeometry();
                    PathFigure pathFigure = new PathFigure();
                    pathFigure.StartPoint = new Point(_child.Width, 0);
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width, _child.Height / 2 - pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width + pathOffset, _child.Height / 2), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width, _child.Height / 2 + pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width, _child.Height), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(0, _child.Height), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(0, 0), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width, 0), true));
                    pathGeometry.Figures.Add(pathFigure);
                    _mainPath.Data = pathGeometry;
                }

                if (this.PlaceMent == PlaceMent.Top)
                {
                    PathGeometry pathGeometry = new PathGeometry();
                    PathFigure pathFigure = new PathFigure();
                    pathFigure.StartPoint = new Point(0, _child.Height);
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width / 2 - pathOffset, _child.Height), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width / 2, _child.Height + pathOffset), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width / 2 + pathOffset, _child.Height), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width, _child.Height), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(_child.Width, 0), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(0, 0), true));
                    pathFigure.Segments.Add(new LineSegment(new Point(0, _child.Height), true));
                    pathGeometry.Figures.Add(pathFigure);
                    _mainPath.Data = pathGeometry;
                }
            }
        }

        private void CorrectPathOffset()
        {
            if (_child != null && PlaceMentTarget != null && _popup != null)
            {
                if (this.PlaceMent == PlaceMent.Top || this.PlaceMent == PlaceMent.Bottom)
                {
                    _popup.HorizontalOffset = (PlaceMentTarget.ActualWidth - _child.Width) / 2;
                }
                if (this.PlaceMent == PlaceMent.Left || this.PlaceMent == PlaceMent.Right)
                {
                    _popup.VerticalOffset = (PlaceMentTarget.ActualWidth - _child.Width) / 2;
                }
            }
        }
    }
}
