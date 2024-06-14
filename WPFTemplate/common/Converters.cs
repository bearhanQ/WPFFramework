using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml;
using WPFTemplate.Properties;

namespace WPFTemplate
{
    public class TextBoxIconToPathData : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value.ToString();
            switch (str)
            {
                case "User":
                    return "&#xe741;";
                case "KeyBoard":
                    return "&#xea47;";
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TextLengthIsNullOrEmptyToTrue : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return true;
            }
            return string.IsNullOrEmpty(value.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ProgressBarValueToPercentage : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var maximum = System.Convert.ToDouble(values[0]);
            var value = System.Convert.ToDouble(values[1]);

            if (maximum == 0)
            {
                return "0" + "%";
            }

            double progressValue = value / maximum * 100;
            return (Math.Round(progressValue)).ToString() + "%";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NormalTabControlCornerRadiusConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var radius = (CornerRadius)values[0];
            var items = values[1] as ItemCollection;
            var item = values[2] as TabItem;
            var index = items.IndexOf(item);
            if (items.Count == 1)
            {
                return radius;
            }

            if (index == 0)
            {
                return new CornerRadius(radius.TopLeft, 0, 0, radius.BottomLeft);
            }

            if (index == items.Count - 1)
            {
                return new CornerRadius(0, radius.TopRight, radius.BottomRight, 0);
            }

            return new CornerRadius(0, 0, 0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TreeViewHorizontalLineConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var treeviewitem = values[0] as TreeViewItem;
            var path = values[1] as UIElement;

            int x = 19;
            var left = -x / 2;

            var level = LocalVisualTreeHelper.GetTreeViewItemLevel(treeviewitem);

            if (level == 0)
            {
                path.Visibility = Visibility.Collapsed;
            }

            return new Thickness
            {
                Left = left
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TreeViewVerticalLineConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var bdheight = System.Convert.ToDouble(values[1]);
            var item = values[2] as TreeViewItem;

            double height = 0;
            if (item.HasItems)
            {
                for (int i = 0; i < item.Items.Count - 1; i++)
                {
                    var element = item.Items[i] as FrameworkElement;
                    height += element.ActualHeight;
                }
            }

            height += bdheight;
            height -= bdheight / 2;

            return new Thickness
            {
                Bottom = -height,
                Top = 10
            };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ComboBoxItemToCommandParameter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var key = values[0].ToString();
            var value = values[1].ToString();
            if (!string.IsNullOrWhiteSpace(key))
            {
                return key + "&" + value;
            }
            else
            {
                return "Content" + "&" + value;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class CarouselItemsPresenterWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var carousel = value as Carousel;
            if (carousel != null)
            {
                var itemscount = carousel.Items.Count;
                var width = carousel.Width;
                return itemscount * width;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeSpanConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string strValue = value as string;
            if (strValue != null)
            {
                double ms = 0;
                if(double.TryParse(strValue,out ms))
                {
                    return TimeSpan.FromMilliseconds(ms);
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }

    public class ProgressBarSizeToClipConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            RectangleGeometry clip = new RectangleGeometry();
            var border = value as Border;
            if (border != null)
            {
                var borderThickness = border.BorderThickness.Left * 2;
                var cornerRadius = border.CornerRadius.BottomRight;
                clip.Rect = new Rect { X = 0, Y = 0, Width = border.ActualWidth - borderThickness, Height = border.ActualHeight - borderThickness };
                clip.RadiusX = cornerRadius;
                clip.RadiusY = cornerRadius;
            }
            return clip;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MenuItemContenAndArrowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var margin = (Thickness)value;
            var left = margin.Left;
            var top = margin.Top;
            var right = 0;
            var bottom = margin.Bottom;
            return new Thickness(left, top, right, bottom);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MenuItemHeaderTypeIsFrameworkElementConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is FrameworkElement;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PageCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = int.Parse(value.ToString());
            switch (index)
            {
                case 10: return 0;
                case 20: return 1;
                case 50: return 2;
                case 100: return 3;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var index = int.Parse(value.ToString());
            switch (index)
            {
                case 0: return 10;
                case 1: return 20;
                case 2: return 50;
                case 3: return 100;
            }
            return 10;
        }
    }

    public class PaginationPageNumClickConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Dictionary<string, DependencyObject> result = new Dictionary<string, DependencyObject>();
            var button = values[0] as ListBoxItem;
            
            if (button != null)
            {
                var content = button.Content.ToString();
                var pagination = values[1] as CornerPagination;
                result.Add(content, pagination);
            }

            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PaginationNumEqualCurrentIndexConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var pagination = values[0] as CornerPagination;
            var btn = values[1] as ListBoxItem;
            if (btn != null && pagination != null)
            {
                if (int.TryParse(btn.Content.ToString(), out int result))
                {
                    return pagination.CurrentPageIndex == result;
                }
            }
            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class NullableToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? isCheck = value as bool?;
            if (isCheck == null)
            {
                return false;
            }
            else
            {
                return isCheck.Value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class StringToBrushCollectionTypeConverter : TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value != null)
            {
                var result = new ObservableCollection<Brush>();
                var brushs = value.ToString().Split(',');
                foreach (var brush in brushs)
                {
                    result.Add(new SolidColorBrush((Color)ColorConverter.ConvertFromString(brush)));
                }
                return result;
            }
            return null;
        }
    }

    internal class BarRowHeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var actualHeight = (double)values[0];
            var list = values[1] as IList;
            if (list != null)
            {
                var count = list.Count == 0 ? 1 : list.Count;
                var height = Math.Floor(actualHeight / count / 2);
                return new GridLength(height, GridUnitType.Pixel);
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class BarHeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var actualHeight = (double)values[0];
            var list = values[1] as IList;
            if (list != null)
            {
                var height = Math.Floor(actualHeight / list.Count);
                return height;
            }
            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class BarItemWidthConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            var actualWidth = (double)value[0];
            var list = value[1] as IList;
            if (list != null)
            {
                var count = list.Count == 0 ? 1 : list.Count;
                var result = Math.Round((actualWidth) / count / 2, 2);
                return result;
            }
            return 0;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class BarItemMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
{
            Thickness result = new Thickness();
            var actualWidth = (double)value[0];
            var list = value[1] as IList;
            if (list != null)
            {
                var count = list.Count == 0 ? 1 : list.Count;
                var tick = Math.Round((actualWidth) / count / 2, 2);
                result.Right = tick;
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class BarItemScaleYConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var actualHeight = (double)values[0];
            var list = (IList)values[1];
            var ratio = (double)values[2];
            if (list != null)
            {
                var count = list.Count == 0 ? 1 : list.Count;
                var height = actualHeight / count;
                var Y = Math.Round(height / ratio, 10);
                return Y;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class LineChartScaleYConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var actualHeight = (double)values[0];
            var list = (IList)values[1];
            var ratio = (double)values[2];
            if (list != null)
            {
                var count = list.Count == 0 ? 1 : list.Count;
                var height = actualHeight / count;
                var Y = Math.Round(height / ratio, 10);
                return 0 - Y;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class LineChartPathMarginConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Thickness result = new Thickness();
            var actualWidth = (double)values[0];
            var list = values[1] as IList;
            if (list != null)
            {
                var count = list.Count == 0 ? 1 : list.Count;
                var tick = Math.Floor((actualWidth) / count / 4);
                result.Left = tick;
            }
            return result;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class ChartPathStartPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var pc = value as PathSegmentCollection;
            if (pc != null && pc.Count > 1)
            {
                var item = pc[0];
                if (item.GetType() == typeof(LineSegment))
                {
                    var ls = item as LineSegment;
                    return ls.Point;
                }
                if (item.GetType() == typeof(BezierSegment))
                {
                    var bs = item as BezierSegment;
                    return bs.Point1;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
