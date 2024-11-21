using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class Badge : Adorner
    {
        public static readonly DependencyProperty ContentProperty;

        static Badge()
        {
            ContentProperty = DependencyProperty.RegisterAttached("Content", typeof(string), typeof(Badge),
                new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(ContentChangedCallBack)));
        }

        public Badge(UIElement adornedElement) : base(adornedElement)
        {
            
        }

        private static void ContentChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = d as FrameworkElement;
            if (target != null)
            {
                if (target.IsLoaded)
                {
                    var layer = AdornerLayer.GetAdornerLayer(target);
                    if (layer != null)
                    {
                        var Adorners = layer.GetAdorners(target);
                        if (Adorners != null)
                        {
                            foreach (var adorner in Adorners)
                            {
                                if (adorner is Badge)
                                {
                                    layer.Remove(adorner);
                                }
                            }
                        }
                        layer.Add(new Badge(target));
                    }
                }
                else
                {
                    target.Loaded += (sender, ae) =>
                    {
                        var layer = AdornerLayer.GetAdornerLayer(target);
                        if (layer != null)
                        {
                            var Adorners = layer.GetAdorners(target);
                            if (Adorners != null)
                            {
                                foreach (var adorner in Adorners)
                                {
                                    if (adorner is Badge)
                                    {
                                        layer.Remove(adorner);
                                    }
                                }
                            }
                            layer.Add(new Badge(target));
                        }
                    };
                }
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var element = this.AdornedElement as FrameworkElement;
            Rect adornedElementRect = new Rect(element.DesiredSize);
            var point = adornedElementRect.TopRight;
            point.X = adornedElementRect.Right - element.Margin.Left - element.Margin.Right;

            SolidColorBrush renderBrush = new SolidColorBrush(Colors.Red);
            Pen renderPen = new Pen(new SolidColorBrush(Colors.Red), 0.5);
            double renderRadius = 7;

            var content = this.AdornedElement.GetValue(Badge.ContentProperty).ToString();
            FormattedText formattedText = new FormattedText(content, CultureInfo.GetCultureInfo("zh-cn"), FlowDirection.LeftToRight, new Typeface("Verdana"), 10, Brushes.White);
            var textWidth = formattedText.Width;
            var textHeight = formattedText.Height;
            var rectangleSizeWidth = textWidth < 15 ? 15 : textWidth;
            var rectangleSizeHeight = textHeight < 15 ? 15 : textHeight;
            var size = new Size(rectangleSizeWidth, rectangleSizeHeight);
            Rect rect = new Rect(new Point(point.X - rectangleSizeWidth / 2, point.Y - rectangleSizeHeight / 2), size);

            drawingContext.DrawRoundedRectangle(renderBrush, renderPen, rect, renderRadius, renderRadius);
            drawingContext.DrawText(formattedText, new Point(point.X - textWidth / 2, point.Y - textHeight / 2));
        }

        public static string GetContent(DependencyObject obj)
        {
            return (string)obj.GetValue(ContentProperty);
        }

        public static void SetContent(DependencyObject obj, string value)
        {
            obj.SetValue(ContentProperty, value);
        }
    }
}
