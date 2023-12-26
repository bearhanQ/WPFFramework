using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public class SvgParser : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Path path = new Path();

            // 加载SVG文件
            XmlDocument svgDoc = new XmlDocument();
            svgDoc.Load(@"C:\Qh\Learning\MyWPF\WPFTemplate\Icons\搜索.svg");

            // 提取SVG路径数据
            XmlNodeList pathNodes = svgDoc.GetElementsByTagName("path");
            if (pathNodes.Count > 0)
            {
                string pathData = pathNodes[0].Attributes["d"].Value;
                path.Data = Geometry.Parse(pathData);
            }

            return path.Data;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ButtonTypeToSvg : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value.ToString();
            switch (str)
            {
                case "Add":
                    return Resources.Add;
                case "Update":
                    return Resources.Update;
                case "Delete":
                    return Resources.Delete;
                case "Save":
                    return Resources.Save;
                case "Cancel":
                    return Resources.Cancel;
                case "Import":
                    return Resources.Import;
                case "Export":
                    return Resources.Export;
                case "Print":
                    return Resources.Print;
                case "Set":
                    return Resources.Set;
                case "Query":
                    return Resources.Query;
                case "Copy":
                    return Resources.Copy;
                case "Refresh":
                    return Resources.Refresh;
                case "Default":
                    return Resources.Default;
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TextBoxIconToPathData : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value.ToString();
            switch (str)
            {
                case "User":
                    return Resources.User;
                case "KeyBoard":
                    return Resources.KeyBoard;
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

    public class NoBackgroundToBlack : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "#FFFFFFFF")
            {
                return Brushes.Black;
            }
            return value;
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

            var level = MyVisualTreeHelper.GetTreeViewItemLevel(treeviewitem);

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
            var itemheight = System.Convert.ToDouble(values[0]);
            var bdheight = System.Convert.ToDouble(values[1]);
            var item = values[2] as TreeViewItem;
            var offset = 0d;
            if (item.HasItems)
            {
                offset = bdheight / 2;
            }
            double height = (itemheight - bdheight - offset);

            //if (!item.HasItems)
            //{
            //    return new Thickness(0);
            //}

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

    public class DataGridSourceToCbFilterSource : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var dataSource = values[0] as IList;
            var dt = values[0] as DataTable;
            var header = values[1] as DataGridColumn;

            var itemSource = GetDistinctItems(header, dataSource ?? (IEnumerable)dt);
            return itemSource.Distinct();
        }

        private IEnumerable<string> GetDistinctItems<T>(DataGridColumn header, T dataSource) where T : class, IEnumerable
        {
            if (header == null || dataSource == null)
            {
                yield break;
            }

            var content = header.SortMemberPath;

            foreach (var item in dataSource)
            {
                var itemType = item.GetType();

                if (itemType == typeof(DataRowView))
                {
                    yield return (item as DataRowView)[content].ToString();
                }
                else
                {
                    var property = itemType.GetProperty(content);

                    if (property != null)
                    {
                        var value = property.GetValue(item);

                        if (value != null)
                        {
                            yield return value.ToString();
                        }
                    }
                }
            }
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
            //DataGrid
            if (values[2].ToString() == "CbFilter")
            {
                if (values[0] != null)
                {
                    var header = values[0] as DataGridColumn;
                    var key = header.SortMemberPath;
                    if (values[1] == null)
                    {
                        return key + "&" + "-";
                    }
                    var value = values[1].ToString();
                    return key + "&" + value;
                }
            }

            //ComboBox
            if (values[2].ToString() == "searchTextBox")
            {
                var key = values[0].ToString();
                var value = values[1].ToString();
                //source from datatable or list
                if (!string.IsNullOrWhiteSpace(key))
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        value = "-";
                    }
                    return key + "&" + value;
                }
                //source is string list
                else
                {
                    key = "stringlistmark";
                    value = values[1].ToString();
                    return key + "&" + value;
                }
            }

            return "";
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
                string[] parts = strValue.Split(',');
                if (parts.Length == 3)
                {
                    int hours = 0, minutes = 0, seconds = 0;
                    int.TryParse(parts[0], out hours);
                    int.TryParse(parts[1], out minutes);
                    int.TryParse(parts[2], out seconds);
                    return new TimeSpan(hours, minutes, seconds);
                }
            }
            return base.ConvertFrom(context, culture, value);
        }
    }

    public class StringToGeometryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return Geometry.Parse(value.ToString());
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
